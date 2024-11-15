using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AiHp : MonoBehaviour,IAihp
{
    public EnemyAi enemyAi;




    [SerializeField]Image Hpimage;
    Canvas Enemycanvas;

    float Hp;
    float MaxHp = 100;
    float attackDamage;
    public bool isDie;

    private void OnEnable()
    {
        if(isDie)
        {
            isDie = false;
            Enemycanvas.enabled = true;
            enemyAi.nav.isStopped = false;
            enemyAi.nav.speed = 5f;
            enemyAi.rb.isKinematic = false;
        }
    }
    void Start()
    {
        
        SetHealth(MaxHp);
        Hpimage = transform.GetChild(6).GetChild(2).GetComponent<Image>();
        Enemycanvas = transform.GetChild(6).GetComponent<Canvas>();
    }
    void Update()
    {
        Hpimage.fillAmount = (float)Hp / (float)MaxHp;
    }
    public float GetAttackDamage()
    {
        throw new System.NotImplementedException();
    }

    public float GetHealth()
    {
        throw new System.NotImplementedException();
    }

    public void SetAttackDamage(float damage)
    {
        // ������ �κ�
    }

    public void SetHealth(float health)
    {
        Hp = health;
    }
    public float TakeSkillDamage(float damafge, float slow)
    {
        Hp -= damafge;
        Hp = Mathf.Clamp(Hp, 0, MaxHp);
        enemyAi.nav.speed = slow;
        if (Hp <= 0)
        {
            StartCoroutine(Die());
        }
        return Hp;
    }

    public void TakeAttackDamage(float damage)
    {
        Hp -= damage;
        Hp = Mathf.Clamp(Hp, 0, MaxHp);
        if (Hp <= 0)
        {
            enemyAi.capCol.enabled = false;
            isDie = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        enemyAi.rb.isKinematic = true;
        enemyAi.ani.SetTrigger("Die");
        enemyAi.nav.isStopped = true;
        enemyAi.nav.speed = 0;
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        Enemycanvas.enabled = false;
        
    }
}
