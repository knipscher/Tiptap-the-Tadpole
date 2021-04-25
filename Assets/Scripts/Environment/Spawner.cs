using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    [SerializeField]
    private Vector3 minPosition = default;
    [SerializeField]
    private Vector3 maxPosition = default;

    [SerializeField]
    private float minSpawnTime = 2f;
    [SerializeField]
    private float maxSpawnTime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnRepeating());
    }

    private IEnumerator SpawnRepeating()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            var spawnPoint = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
            SpawnObjectAtPosition(spawnPoint);
        }
    }

    public void SpawnObjectAtPosition(Vector2 position)
    {
        var randomPartID = Random.Range(0, prefabs.Length);
        var newSpawn = Instantiate(prefabs[randomPartID], position, Quaternion.identity, transform);
        ScrollingBackground.instance.AddObjectToBackground(newSpawn);
    }
}