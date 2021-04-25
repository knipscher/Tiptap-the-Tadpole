using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTooth : MonoBehaviour
{
    public int attackStrength = 1;
    private Monster monster;

    private void Start()
    {
        monster = GetComponentInParent<Monster>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!monster.isInBiteCooldown)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Player.instance.GetBitten(attackStrength);
                monster.Bite();
            }
        }
    }
}