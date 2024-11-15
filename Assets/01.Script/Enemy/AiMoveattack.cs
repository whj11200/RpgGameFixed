using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMoveattack : MonoBehaviour
{
    Transform playerpos;
    NavMeshAgent Aiagent;
    Animator Aianimator;
    BoxCollider attackbox;
    AiHp aihp;

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
        Aiagent = gameObject.GetComponent<NavMeshAgent>();
        Aianimator = gameObject.GetComponent<Animator>();
        attackbox = transform.GetChild(5).GetComponent<BoxCollider>();
        aihp = GetComponent<AiHp>();
        attackbox.enabled = false;
    }

    void Update()
    {
        if (!aihp.isDie&&!GameManger.instance.gameover)
        {
            IsMoveAttack();
        }
        if (GameManger.instance.gameover)
        {
            Aianimator.SetBool("Attack", false);
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
            Aianimator.SetBool("Attack", true);
            Aianimator.SetBool("Find", false);
            Aiagent.isStopped = true;  // 공격 중에는 이동을 멈춤
        }
        else if (distance <= 5)
        {
            Aianimator.SetBool("Attack", false);
            Aianimator.SetBool("Find", true);
            Aiagent.destination = playerpos.transform.position;
            Aiagent.isStopped = false;  // 플레이어에게 이동
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5F);
        }
        else
        {
            Vector3 LookPoint = (randomDestination - transform.position).normalized;
            Quaternion AirotLookPoint = Quaternion.LookRotation(LookPoint);
            Aianimator.SetBool("Find", false);
            Aiagent.isStopped = false;  // 이동 활성화

            if (isWaiting)
            {
                Aianimator.SetBool("Walk", false); // 대기 종료 시 애니메이션 끔
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
                    Aiagent.destination = randomDestination;
                    Aianimator.SetBool("Walk", true); // 이동 시작 시 애니메이션 활성화
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
