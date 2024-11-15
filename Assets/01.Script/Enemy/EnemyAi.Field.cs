using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class EnemyAi : MonoBehaviour
{
    public CapsuleCollider capCol;
    public Rigidbody rb;

    public Animator ani;
    public NavMeshAgent nav;


    public void GetAllComponents()
    {
        TryGetComponent(out capCol);
        TryGetComponent(out rb);
        TryGetComponent(out ani);
        TryGetComponent(out nav);       
    }
}
