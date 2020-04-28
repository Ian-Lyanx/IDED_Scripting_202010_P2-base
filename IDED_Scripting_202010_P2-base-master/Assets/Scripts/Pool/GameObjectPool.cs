using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Vector3 spawnPoint = new Vector3(100, 100, 100);
    [SerializeField] private int objectCount;

    private List<List<GameObject>> stock;

    private void OnValidate()
    {
        if (objectCount < 0)
        {
            objectCount *= -1;
        }
    }

    void Awake()
    {
        FillStock();
    }

    public GameObject AllocateObject(Vector3 pos, Quaternion rot, int index)
    {
        GameObject g = stock[index].FirstOrDefault(x => !x.activeSelf);

        if (g == null)
        {
            g = CreateObjectCopy(index);
            stock[index].Add(g);
        }

        g.SetActive(true);
        g.transform.position = pos;
        g.transform.rotation = rot;

        return g;
    }

    void FillStock()
    {
        stock = new List<List<GameObject>>();
        for (int i = 0; i < prefabs.Length; i++)
        {
            stock.Add(new List<GameObject>());
            for (int j = 0; j < objectCount; j++)
            {
                stock[i].Add(CreateObjectCopy(i));
            }
        }
    }

    GameObject CreateObjectCopy(int index)
    {
        return Instantiate<GameObject>(prefabs[index], spawnPoint, Quaternion.identity);
    } 

}
