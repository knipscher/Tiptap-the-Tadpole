using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GameControl.instance && !GameControl.instance.gameOver == true && Time.timeScale != 0)
        {
            transform.Translate(new Vector2(0, ScrollingBackground.instance.scrollSpeed * Time.deltaTime), Space.World);
        }
    }
}