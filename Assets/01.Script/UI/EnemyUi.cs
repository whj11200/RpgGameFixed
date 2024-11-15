using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUi : MonoBehaviour
{
    Transform playerposCamera;

    void Start()
    {
        playerposCamera = GameObject.Find("Virtual Camera").transform;
    }

    void Update()
    {
        Vector3 look = transform.position - playerposCamera.position;
        transform.rotation = Quaternion.LookRotation(look);
    }
}
