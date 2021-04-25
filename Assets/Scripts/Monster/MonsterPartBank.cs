using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPartBank : MonoBehaviour
{
    public static MonsterPartBank instance;

    [SerializeField]
    private GameObject[] partPrefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnMonsterPart(Vector2 position)
    {
        var randomPartID = Random.Range(0, partPrefabs.Length);
        Instantiate(partPrefabs[randomPartID], position, Quaternion.identity, transform);
    }
}