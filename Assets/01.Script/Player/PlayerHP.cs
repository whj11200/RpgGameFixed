using UnityEngine;
using UnityEngine.UI;

public partial class PlayerHP : MonoBehaviour, IPlayerHealth
{
    [HideInInspector] public Player player;

    void Start()
    {
        SetValue();
    }
    void Update()
    {
        RefreshUI();
        if (hp <= 0 && !GameManger.instance.gameover)
            Die();
    }

    private void RefreshUI()
    {
        hpImage.fillAmount = hp / maxhp;
        hpText.text = $"{hp}";
    }

    private void Die()
    {
        GameManger.instance.gameover = true;
        player.ani.SetTrigger("Die");
        player.capCol.enabled = false;
        player.rb.isKinematic = true;
    }

    public void SetHealth(float health) => hp = health;
    public void TakeDamage(float damage) => hp -= damage;
}
