using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public static GameObjectPool Instance;

    public GameObject shadowPrefab;
    public int shadowCount;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        //初始化对象池
        FullPool();
    }
    public void FullPool() {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            //返回对象池
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject) { gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }
    public GameObject GetFormPool() {
        if (availableObjects.Count==0)
        {
            FullPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
