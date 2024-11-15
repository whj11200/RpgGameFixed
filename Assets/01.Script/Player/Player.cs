using UnityEngine;

public partial class Player : MonoBehaviour
{
    public PlayerHP HP;
    public PlayerMove Move;
    public PlayerRotation Rotation;
    public PlayerDamage Damage;
    public PlayerAttack Attack;
    public PlayerSkills Skills;
    // 각 클래스나 변수에 Ctrl + 좌클릭을 해보면 참조하는 곳을 바로 확인 가능.

    void Awake()
    {
        TryGetComponent(out HP);
        TryGetComponent(out Move);
        TryGetComponent(out Rotation);
        TryGetComponent(out Damage);
        TryGetComponent(out Attack);
        TryGetComponent(out Skills);

        HP.player = this;
        Move.player = this;
        Rotation.player = this;
        Damage.player = this;
        Attack.player = this;
        Skills.player = this;
    }

    void Start() => GetAllComponents();

    // void OnCollisionEnter(Collision other)
    // {
    //     if (other.transform.TryGetComponent(out Player player))
    //     {

    //     }
    //     if (other.transform.GetComponent<Player>() != null)
    //     {
            
    //     }

    //     // if (other.gameObject.CompareTag("Enemy"))
    //     // {
    //     //     Damage.TakeDamage(10);
    //     // }
    // }
}