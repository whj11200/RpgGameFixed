using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    public bool gameover = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }
}
