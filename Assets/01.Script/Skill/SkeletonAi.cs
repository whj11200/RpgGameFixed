using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class sss : MonoBehaviour
{
    enum State { Idle, Move, Attack, Dead ,NotEnemy}
    State currentState = State.Idle;

    NavMeshAgent Sk_agent;
    BoxCollider Sword;
    [SerializeField] Transform enemypos;
    Transform Playerpos;
    Animator sk_animator;

    float attackdistance = 1f;
    float finddistance = 8f;
    [SerializeField] float timer;
    float DeadTime = 20f;
    string Enemytag = "Enemy";
    string Playertag = "Player";

    void Awake()
    {
        Sk_agent = GetComponent<NavMeshAgent>();
        Sword = transform.GetChild(0).GetChild(2).GetComponent<BoxCollider>();
        Playerpos = GameObject.Find(Playertag).transform;
        sk_animator = GetComponent<Animator>();
        timer = 0f; // 초기화
    }

    private void OnEnable()
    {
        Sk_agent.speed = 2;
        Sk_agent.isStopped = false;
        currentState = State.Idle; // 상태 초기화
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                StartCoroutine(Sk_Idle());
                break;

            case State.Move:
                Move();
                break;

            case State.Attack:
                Sk_Attack();
                break;

            case State.Dead:
                Sk_Dead();
                break;
            case State.NotEnemy:
                NotEnemy();
                break;
        }
    }

    IEnumerator Sk_Idle()
    {
        timer += Time.deltaTime;

     
        if(timer <= DeadTime)
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
            }
        }
        if (timer >= DeadTime)
        {
            currentState = State.Dead;
            yield break;  // 코루틴 종료
        }
    }

    private void Sk_Dead()
    {
        sk_animator.SetTrigger("Dead");
        Sk_agent.speed = 0;
        Sk_agent.isStopped = true;
    }

    private void Move()
    {
        sk_animator.SetBool("Find", true);
        sk_animator.SetBool("Attack", false);
        Sk_agent.destination = enemypos.position;
        Sk_agent.isStopped = false;
    }
    void Sk_Attack()
    {
        sk_animator.SetBool("Find", false);
        sk_animator.SetBool("Attack", true);
        Sk_agent.isStopped = true;
    }

    void NotEnemy()
    {
        Sk_agent.destination = Playerpos.position;
        Sk_agent.isStopped = false;
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
