using UnityEngine;

public partial class Player : MonoBehaviour
{
    internal Animator ani;
    internal Rigidbody rb;
    internal CapsuleCollider capCol;

    private void GetAllComponents()
    {
        TryGetComponent(out ani);
        TryGetComponent(out rb);
        TryGetComponent(out capCol);
    }
}