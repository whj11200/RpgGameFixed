using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [HideInInspector] public Player player;

    public float sensitivity = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!GameManger.instance.gameover)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
