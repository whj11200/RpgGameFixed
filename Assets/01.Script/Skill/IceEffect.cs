using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : MonoBehaviour
{
    [SerializeField] AiDamage aiDamage;

    void Start()
    {
        // 10초 후에 객체를 파괴

        Destroy(gameObject, 10);
        // AiDamage를 찾고 할당
        StartCoroutine(CallResetSpeedAfterDelay(9.5f));
        // 9초 후에 ResetSpeed 호출하는 코루틴 시작

    }



    private IEnumerator CallResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        AiDamage[] allAiDamages = FindObjectsOfType<AiDamage>();
        foreach (var damage in allAiDamages)
        {
            if (damage != null)
            {
                print("전달");
                damage.ResetSpeed();
            }
        }
    }

}
