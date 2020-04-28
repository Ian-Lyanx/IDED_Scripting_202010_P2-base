using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(GameObjectPool))]
public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private float firstSpawnDelay = 0f;

    private Vector3 spawnPoint;

    private GameObjectPool pool;

    void Awake()
    {
        pool = GetComponent<GameObjectPool>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("SpawnObject", firstSpawnDelay, spawnRate);
    }

    private void SpawnObject()
    {
        spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(
            UnityEngine.Random.Range(0F, 1F), 1F, transform.position.z));
        
        Random r = new Random((int)Time.realtimeSinceStartup);
        GameObject spawnGO = pool.AllocateObject(spawnPoint,Quaternion.identity,r.Next(0,3));
        spawnGO.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void StopSpawning()
    {
        CancelInvoke();
    }
}