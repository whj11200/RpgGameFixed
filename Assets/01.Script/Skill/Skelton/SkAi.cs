using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkAi : MonoBehaviour
{
    enum State { Idle, Move, Attack, Dead, NotEnemy }
    State currentState = State.Idle;

    NavMeshAgent Sk_agent;
    BoxCollider Sword;
    [SerializeField] Transform enemypos;
    Transform Playerpos;
    Animator sk_animator;

    float attackdistance = 1f;
    float finddistance = 8f;
    float timer = 0f;
    float DeadTime = 20f;
    string Enemytag = "Enemy";
    string Playertag = "Player";

    void Awake()
    {
        Sk_agent = GetComponent<NavMeshAgent>();
        Sword = transform.GetChild(0).GetChild(2).GetComponent<BoxCollider>();
        Playerpos = GameObject.Find(Playertag).transform;
        sk_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Sk_agent.speed = 2;
        Sk_agent.isStopped = false;
        currentState = State.Idle; // ���� �ʱ�ȭ
        timer = 0f; // Ÿ�̸� �ʱ�ȭ
        StartCoroutine(StateMachine());
    }

    IEnumerator StateMachine()
    {
        while (true)
        {
            // Ÿ�̸� üũ
            timer += Time.deltaTime;
            if (timer >= DeadTime && currentState != State.Dead)
            {
                currentState = State.Dead;
                yield return StartCoroutine(Sk_Dead());
                break; // �ڷ�ƾ ����
            }

            EnemyFind();

            switch (currentState)
            {
                case State.Move:
                    Move();
                    break;

                case State.Attack:
                    Sk_Attack();
                    break;

                case State.NotEnemy:
                    NotEnemy();
                    break;
            }

            yield return null; // ���� �����ӱ��� ���
        }
    }

    private void EnemyFind()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Enemytag);
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            enemypos = closestEnemy.transform;
            var distanceToEnemy = Vector3.Distance(transform.position, enemypos.position);

            if (distanceToEnemy <= attackdistance)
            {
                currentState = State.Attack;
            }
            else if (distanceToEnemy <= finddistance)
            {
                currentState = State.Move;
            }
            else if(distanceToEnemy >= finddistance)
            {
                 currentState = State.NotEnemy;
            }
        }
        else
        {
            currentState = State.NotEnemy; 
        }
    }

    IEnumerator Sk_Dead()
    {
        sk_animator.SetTrigger("Dead");
        Sk_agent.speed = 0;
        Sk_agent.isStopped = true;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void Move()
    {
        sk_animator.SetBool("Idle", false);
        sk_animator.SetBool("Find", true);
        sk_animator.SetBool("Attack", false);
        Sk_agent.destination = enemypos.position;
        Sk_agent.isStopped = false;

        // Move ���¿��� ������ �Ÿ� üũ
        if (Vector3.Distance(transform.position, enemypos.position) <= attackdistance)
        {
            currentState = State.Attack; // ���� ���·� ��ȯ
        }
    }

    void Sk_Attack()
    {
        sk_animator.SetBool("Idle", false);
        sk_animator.SetBool("Find", false);
        sk_animator.SetBool("Attack", true);
        Sk_agent.isStopped = true;

        // ���� ���¿��� ������ �Ÿ� üũ
        if (Vector3.Distance(transform.position, enemypos.position) > attackdistance)
        {
            currentState = State.Move; // �ٽ� �̵� ���·� ��ȯ
        }
    }

    void NotEnemy()
    {
        var Playerdistance = Vector3.Distance(transform.position, Playerpos.position);
        if(Playerdistance <= 1.5)
        {
            sk_animator.SetBool("Attack", false);
            sk_animator.SetBool("Idle", true);
            sk_animator.SetBool("Find", false);
            Sk_agent.isStopped=true;
        }
        else if(Playerdistance >= 1)
        {
             sk_animator.SetBool("Attack", false);
            sk_animator.SetBool("Idle", false);
            sk_animator.SetBool("Find", true);
            Sk_agent.destination = Playerpos.position;
            Sk_agent.isStopped = false;
        }
       
    }

    void Attackbox()
    {
        Sword.enabled = true;
    }

    void AttackOffbox()
    {
        Sword.enabled = false;
    }
}
