using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyAi : MonoBehaviour
{
    public AiHp hp;
    public AiMoveattack moveattack;

    public AiDamage Damage;

    void Awake()
    {
        TryGetComponent(out hp);
        TryGetComponent(out moveattack);
        TryGetComponent(out Damage);
        hp.enemyAi = this;
        moveattack.enemyAi = this;
        Damage.enemyAi = this;
    }

    void Start() => GetAllComponents();


}
