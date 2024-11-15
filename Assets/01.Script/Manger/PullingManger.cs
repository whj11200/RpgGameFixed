using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingManger : MonoBehaviour
{
    public static PullingManger Instance;
    private GameObject Enemy; // 스폰할 적 프리팹
    [SerializeField] int poolSize = 5; // 풀 크기
    private List<GameObject> pool;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        var enemypoolGroup = new GameObject("EnemyGroup");
        Enemy = Resources.Load<GameObject>("Enemy");
        pool = new List<GameObject>();
          for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(Enemy);
            obj.SetActive(false); // 비활성화 상태로 초기화
            obj.transform.parent = enemypoolGroup.transform; // 부모 설정
            pool.Add(obj);
        }
         
    }

    public GameObject GetObject()
    {
       
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy) // 비활성화된 오브젝트 찾기
            {
                obj.SetActive(true); // 활성화
                return obj;
            }
        }
        return null; // 사용할 수 있는 오브젝트가 없으면 null 반환
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 비활성화하여 풀에 반환
    }
}
