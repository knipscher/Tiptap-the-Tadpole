using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField]
    private Color endingColor = default;
    [SerializeField]
    private float lerpSpeed = .001f;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        sprite.color = Color.Lerp(sprite.color, endingColor, lerpSpeed);
    }
}