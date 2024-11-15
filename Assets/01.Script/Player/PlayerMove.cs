using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector] public Player player;

    Vector3 PlayerMoveMent;
    private readonly float Player_WalkSpeed = 5f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!player.Attack.isAttacking && !GameManger.instance.gameover && !player.Skills.isSkillings)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float scalar = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));
            PlayerMoveMent = new Vector3(horizontal, 0, vertical).normalized * scalar;

            player.ani.SetFloat("SpeedX", PlayerMoveMent.x);
            player.ani.SetFloat("SpeedY", PlayerMoveMent.z);

            transform.Translate(Player_WalkSpeed * Time.deltaTime * PlayerMoveMent);
        }
    }
}
