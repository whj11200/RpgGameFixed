using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAihp
{
    // ü���� �����ϴ� �޼���
    void SetHealth(float health);

    // ���� ü���� �������� �޼���
    float GetHealth();

    // �������� �޴� �޼���
    void TakeAttackDamage(float damage);

    // ���ݷ� ���� �޼���
    void SetAttackDamage(float damage);

    // ���ݷ� �������� �޼���
    float GetAttackDamage();

    float TakeSkillDamage(float damafge, float slow);
}
