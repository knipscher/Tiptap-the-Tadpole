using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public static ScrollingBackground instance;
    public float scrollSpeed = 120;

    [SerializeField]
    private RepeatingBackground[] repeatingBackgrounds = default;

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

    public void AddObjectToBackground(GameObject objectToAdd)
    {
        // add to whichever one is lower
        if (repeatingBackgrounds[0].transform.position.y < repeatingBackgrounds[1].transform.position.y)
        {
            repeatingBackgrounds[0].AddObjectToBackground(objectToAdd);
        }
        else
        {
            repeatingBackgrounds[1].AddObjectToBackground(objectToAdd);
        }
    }
}