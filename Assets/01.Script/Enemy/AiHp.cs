using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AiHp : MonoBehaviour,IAihp
{
    Animator animator;
    NavMeshAgent agent;
    CapsuleCollider Aicap;
    Rigidbody airb;
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
            agent.isStopped = false;
            agent.speed = 5f;
            airb.isKinematic = false;
        }
    }
    void Start()
    {
        
        SetHealth(MaxHp);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Aicap = GetComponent<CapsuleCollider>();
        airb = GetComponent<Rigidbody>();
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
        agent.speed = slow;
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
            Aicap.enabled = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
       
        airb.isKinematic = true;
        animator.SetTrigger("Die");
        isDie = true;
        agent.isStopped = true;
        agent.speed = 0;
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        Enemycanvas.enabled = false;
        
    }
}
