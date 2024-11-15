using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class AiDamage : MonoBehaviour
{
    float backforce = 5f;
    float damageInterval = 0.5f; // 데미지 주는 간격
    public bool iceEffectActive = false;

    Rigidbody rb;
    Animator Enemy_ani;
    [SerializeField] NavMeshAgent agent;
    ParticleSystem DamageEffect;
    AiHp aiHp;

    private void Awake()
    {
        aiHp = GetComponent<AiHp>();
        rb = GetComponent<Rigidbody>();
        Enemy_ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        DamageEffect = transform.GetChild(4).GetComponent<ParticleSystem>();
        DamageEffect.Stop();
    }

    private void Update()
    {
        agent.speed = Mathf.Clamp(agent.speed, 0.5f, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!aiHp.isDie)
        {
            if (other.gameObject.CompareTag("Sword"))
            {
                Enemy_ani.SetTrigger("Damage");
                Vector3 distance = (transform.position - other.transform.position).normalized;
                rb.AddForce(distance * backforce, ForceMode.Impulse);
                agent.isStopped = true;
                DamageEffect.Play();
                aiHp.TakeAttackDamage(15);
            }
            if (other.gameObject.CompareTag("IceEffect"))
            {
                iceEffectActive = true;

                StartCoroutine(HitSkill());
            }
            if (other.gameObject.CompareTag("IceSleash"))
            {
                Enemy_ani.SetTrigger("Damage");
                agent.isStopped = true;
                DamageEffect.Play();
                aiHp.TakeAttackDamage(30);
            }
            if (other.gameObject.CompareTag("SkeletonAttack"))
            {
                aiHp.TakeAttackDamage(3);
                agent.isStopped = true;
                DamageEffect.Play();

            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            agent.isStopped = true;
            DamageEffect.Stop();
        }
        else if (other.gameObject.CompareTag("IceEffect"))
        {
            agent.isStopped = false;
            ResetSpeed();
            iceEffectActive = false; // iceEffectActive를 false로 초기화
        }
    }

    public void ResetSpeed()
    {
        agent.speed = 2; // 속도를 2로 설정
        iceEffectActive = false;
        agent.isStopped = false;
        DamageEffect.Stop();
    }

    IEnumerator HitSkill()
    {
        while (iceEffectActive) // iceEffect가 활성화된 동안 반복
        {
            DamageEffect.Play();
            aiHp.TakeSkillDamage(2.5f, 0.5f);
            yield return new WaitForSeconds(damageInterval); // 간격 후 반복
        }
    }
}
