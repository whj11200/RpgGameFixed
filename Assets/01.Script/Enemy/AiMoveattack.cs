using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMoveattack : MonoBehaviour
{
    public EnemyAi enemyAi;
    Transform playerpos;
  
    BoxCollider attackbox;

    /// <summary>
    /// 변수
    /// </summary>
    public float moveInterval = 1f; // 랜덤 위치로 이동할 간격
    private Vector3 randomDestination;
    private float nextMoveTime;
    private float waitTime = 0.5f; // 대기 시간
    private float waitStartTime; // 대기 시작 시간
    private bool isWaiting = false; // 대기 중인지 여부
    void Start()
    {
        playerpos = GameObject.FindWithTag("Player").transform; 

        attackbox = transform.GetChild(5).GetComponent<BoxCollider>();

        attackbox.enabled = false;
    }

    void Update()
    {
        if (!enemyAi.hp.isDie&&!GameManger.instance.gameover)
        {
            IsMoveAttack();
        }
        if (GameManger.instance.gameover)
        {
            enemyAi.ani.SetBool("Attack", false);
        }

    }

    private void IsMoveAttack()
    {
        var distance = Vector3.Distance(playerpos.position, transform.position);
        Vector3 Lookdistance = (playerpos.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(Lookdistance);
        print(Lookdistance);
        
        if (distance <= 1)
        {
            Attack();
        }
        else if (distance <= 5)
        {
            enemyAi.ani.SetBool("Attack", false);
            enemyAi.ani.SetBool("Find", true);
            enemyAi.nav.destination = playerpos.transform.position;
            enemyAi.nav.isStopped = false;  // 플레이어에게 이동
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5F);
        }
        else
        {
            Vector3 LookPoint = (randomDestination - transform.position).normalized;
            Quaternion AirotLookPoint = Quaternion.LookRotation(LookPoint);
            enemyAi.ani.SetBool("Find", false);
            enemyAi.nav.isStopped = false;  // 이동 활성화

            if (isWaiting)
            {
                enemyAi.ani.SetBool("Walk", false); // 대기 종료 시 애니메이션 끔
                if (Time.time >= waitStartTime + waitTime)
                {
                    isWaiting = false; // 대기 종료
                }
            }
            else
            {
                if (Time.time >= nextMoveTime)
                {
                    
                    randomDestination = GetRandomDestination();
                    enemyAi.nav.destination = randomDestination;
                    enemyAi.ani.SetBool("Walk", true); // 이동 시작 시 애니메이션 활성화
                    nextMoveTime = Time.time + moveInterval;
                    transform.rotation = Quaternion.Slerp(transform.rotation, AirotLookPoint, Time.deltaTime * 5F);
                }

                // 랜덤 위치에 도착했는지 체크
                if (Vector3.Distance(transform.position, randomDestination) < 1f)
                {
                    isWaiting = true; // 대기 시작
                    waitStartTime = Time.time; // 대기 시작 시간 설정
                }
            }
        }

    }

    private void Attack()
    {
        enemyAi.ani.SetBool("Attack", true);
        enemyAi.ani.SetBool("Find", false);
        enemyAi.nav.isStopped = true;  // 공격 중에는 이동을 멈춤
    }

    private Vector3 GetRandomDestination()
    {
        // 랜덤 위치를 생성하는 로직
        float randomX = Random.Range(-3f, 3f); // X축 범위
        float randomZ = Random.Range(-3f, 3f); // Z축 범위
        Vector3 randomPos = new Vector3(randomX, transform.position.y, randomZ); 

        return randomPos;
    }

    void AttackBoxOn()
    {
        attackbox.enabled = true;
    }
    void AttackBoxOff()
    {
        attackbox.enabled = false;  
    }
}
