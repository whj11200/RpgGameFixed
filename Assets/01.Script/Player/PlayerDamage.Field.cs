using UnityEngine;

public partial class PlayerDamage : MonoBehaviour
{
    internal ParticleSystem HitparticleSystem;
    public bool playerHit;

    private void GetValue()
    {
        transform.GetChild(4).TryGetComponent(out HitparticleSystem);
        // HitparticleSystem = transform.GetChild(4).GetComponent<ParticleSystem>();
    }

    private void SetValue()
    {
        HitparticleSystem.Stop();
    }
}