using UnityEngine;

public class SwordBoxCol : MonoBehaviour
{
    public BoxCollider SwordBox;

    void Awake() => TryGetComponent(out SwordBox);
}
