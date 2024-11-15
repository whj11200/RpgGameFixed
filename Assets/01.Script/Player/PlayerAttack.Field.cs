using UnityEngine;

public partial class PlayerAttack : MonoBehaviour
{
    internal ParticleSystem TwoAttackPar;
    internal ParticleSystem ThreeAttackPar;
    internal BoxCollider SwordBox;

    public bool isAttacking = false;
    private float comboResetTime = 0.3f;
    private float lastAttackTime = 0f;

    private void GetValue()
    {
        SwordBox = transform.GetComponentInChildren<SwordBoxCol>().SwordBox; // 이 클래스는 원래 Collider을 잡으려고 했던 게임 오브젝트 위치에 있습니다.
        TwoAttackPar = transform.GetChild(2).GetComponent<ParticleSystem>();
        ThreeAttackPar = transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    private void SetValue()
    {
        SwordBox.enabled = false;
        TwoAttackPar.Stop();
        ThreeAttackPar.Stop();
    }
}