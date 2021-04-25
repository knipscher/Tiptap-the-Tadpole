using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectDestroyer : MonoBehaviour
{
    [SerializeField]
    private Vector3 minPosition = default;
    [SerializeField]
    private Vector3 maxPosition = default;

    [SerializeField]
    private Transform backgroundA = default;
    [SerializeField]
    private Transform backgroundB = default;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent == backgroundA)
        {
            collision.transform.SetParent(backgroundB);
            RespawnAtBottom(collision.transform);
        }
        else if (collision.transform.parent == backgroundB)
        {
            collision.transform.SetParent(backgroundA);
            RespawnAtBottom(collision.transform);
        }
    }

    private void RespawnAtBottom(Transform transformToRespawn)
    {
        var spawnPoint = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
        transformToRespawn.position = spawnPoint;
    }
}