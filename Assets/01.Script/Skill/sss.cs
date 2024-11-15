using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAi : MonoBehaviour
{
    NavMeshAgent Sk_agent;
    BoxCollider Sword;
    [SerializeField]Transform enemypos;
    Transform Playerpos;
    Animator sk_animator;



    float attackdistance = 1f;
    float finddistance = 8f;
    [SerializeField]float timer;
    float DeadTime= 20f;
    string Enemytag = "Enemy";
    string Bosstag = "Boss";
    string Playertag = "Player";
    void Awake()    
    {
        Sk_agent = GetComponent<NavMeshAgent>();
        Sword = transform.GetChild(0).GetChild(2).GetComponent<BoxCollider>();
        enemypos = GameObject.Find(Enemytag).transform ;
        Playerpos = GameObject.Find(Playertag).transform ;
        sk_animator = GetComponent<Animator>(); 
        timer = Time.time;
    }

    private void OnEnable()
    {
        Sk_agent.speed = 2;
        Sk_agent.isStopped = false;
    }


    void Update()
    {
        StartCoroutine(Sk_MoveAttack());
    }
    IEnumerator Sk_MoveAttack()
    {
        timer += Time.deltaTime;



        yield return new WaitForSeconds(1f);

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
            Vector3 Lookdistance = (enemypos.position - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(Lookdistance);

            if (distanceToEnemy <= attackdistance)
            {
                sk_animator.SetBool("Attack", true);
                sk_animator.SetBool("Find", false);
                Sk_agent.isStopped = true;
            }
            else if (distanceToEnemy <= finddistance)
            {
                sk_animator.SetBool("Attack", false);
                sk_animator.SetBool("Find", true);
                Sk_agent.destination = enemypos.position;
                Sk_agent.isStopped = false;
            }
        }
        // 1초마다 체크
        if (timer >= DeadTime)
        {
            sk_animator.SetBool("Attack", false);
            sk_animator.SetBool("Find", false);
            sk_animator.SetTrigger("Dead");
            Sk_agent.speed = 0;
            Sk_agent.isStopped = true;
            yield break;  // 코루틴 종료
        }
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
