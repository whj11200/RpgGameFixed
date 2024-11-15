using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : MonoBehaviour
{
    [SerializeField] AiDamage aiDamage;

    void Start()
    {
        // 10�� �Ŀ� ��ü�� �ı�

        Destroy(gameObject, 10);
        // AiDamage�� ã�� �Ҵ�
        StartCoroutine(CallResetSpeedAfterDelay(9.5f));
        // 9�� �Ŀ� ResetSpeed ȣ���ϴ� �ڷ�ƾ ����

    }



    private IEnumerator CallResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        AiDamage[] allAiDamages = FindObjectsOfType<AiDamage>();
        foreach (var damage in allAiDamages)
        {
            if (damage != null)
            {
                print("����");
                damage.ResetSpeed();
            }
        }
    }

}
