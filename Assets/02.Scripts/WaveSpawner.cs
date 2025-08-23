using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject rangeObject;
    BoxCollider rangeCollider;
    public float startTime;
    public float endTime;
    public float spawnRate;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddWave(this);
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("EndSpawner", endTime);
    }

    void Spawn()
    {
        Instantiate(prefab, RandomPosition(), Quaternion.identity);
    }

    Vector3 RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPosition = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPosition;
        return respawnPosition;
    }

    void EndSpawner()
    {
        GameManager.instance.RemoveWave(this);
        CancelInvoke();
    }
}
