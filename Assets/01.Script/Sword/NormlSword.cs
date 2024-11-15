using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormlSword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            AiHp aiHp = other.GetComponent<AiHp>();
            aiHp.TakeAttackDamage(15);
        }
    }
}
