using System.Collections;
using UnityEngine;

public partial class PlayerDamage : MonoBehaviour
{
    [HideInInspector] public Player player;

    void Start()
    {
        GetValue();
        SetValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WeapenDamage damage))
        {
            playerHit = true;
            StartCoroutine(ReturnHit());
            player.ani.SetTrigger("Hit");
            HitparticleSystem.Play();
            player.HP.TakeDamage(5);
        }
    }
    
    private IEnumerator ReturnHit()
    {
        yield return new WaitForSeconds(2f);
        playerHit = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WeapenDamage damage))
        {
            playerHit = false;
            HitparticleSystem.Stop();
        }
    }
}