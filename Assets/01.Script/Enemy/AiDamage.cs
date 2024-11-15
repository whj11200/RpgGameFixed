using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class AiDamage : MonoBehaviour
{
    public EnemyAi enemyAi;
    float backforce = 5f;
    float damageInterval = 0.5f; // 데미지 주는 간격
    public bool iceEffectActive = false;





    ParticleSystem DamageEffect;

    private void Awake()
    {
        

        DamageEffect = transform.GetChild(4).GetComponent<ParticleSystem>();
        DamageEffect.Stop();
    }

    private void Update()
    {
        enemyAi.nav.speed = Mathf.Clamp(enemyAi.nav.speed, 0.5f, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enemyAi.hp.isDie)
        {
            if (other.TryGetComponent(out SwordBoxCol damage))
            {
                enemyAi.ani.SetTrigger("Damage");
                Vector3 distance = (transform.position - other.transform.position).normalized;
                enemyAi.rb.AddForce(distance * backforce, ForceMode.Impulse);
                enemyAi.nav.isStopped = true;
                DamageEffect.Play();
                enemyAi.hp.TakeAttackDamage(15);
            }
            if (other.transform.parent.TryGetComponent(out IceEffect iceEffect))
            {
                iceEffectActive = true;

                StartCoroutine(HitSkill());
            }
            if (other.TryGetComponent(out Sleah sleah))
            {
                enemyAi.ani.SetTrigger("Damage");
                enemyAi.nav.isStopped = true;
                DamageEffect.Play();
                enemyAi.hp.TakeAttackDamage(30);
            }
            if (other.transform.parent.parent.TryGetComponent(out SkAi skAi))
            {
                enemyAi.hp.TakeAttackDamage(3);
                enemyAi.nav.isStopped = true;
                DamageEffect.Play();

            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            enemyAi.nav.isStopped = true;
            DamageEffect.Stop();
        }
        else if (other.gameObject.CompareTag("IceEffect"))
        {
            enemyAi.nav.isStopped = false;
            ResetSpeed();
            iceEffectActive = false; // iceEffectActive를 false로 초기화
        }
    }

    public void ResetSpeed()
    {
        enemyAi.nav.speed = 2; // 속도를 2로 설정
        iceEffectActive = false;
        enemyAi.nav.isStopped = false;
        DamageEffect.Stop();
    }

    IEnumerator HitSkill()
    {
        while (iceEffectActive) // iceEffect가 활성화된 동안 반복
        {
            DamageEffect.Play();
             enemyAi.hp.TakeSkillDamage(2.5f, 0.5f);
            yield return new WaitForSeconds(damageInterval); // 간격 후 반복
        }
    }
}
