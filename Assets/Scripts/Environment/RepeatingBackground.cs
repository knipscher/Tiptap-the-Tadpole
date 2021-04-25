using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    // shamelessly taken from my second-favorite Unity tutorial (and modified), which is also where I learned about singletons several years ago:
    // https://learn.unity.com/tutorial/live-session-making-a-flappy-bird-style-game
    // (my favorite tutorial was Roll-A-Ball, the first Unity tutorial I ever did)

    private BoxCollider2D backgroundCollider; //This stores a reference to the collider attached to the background.
    private float verticalLength; //A float to store the x-axis length of the collider2D attached to the background GameObject.
    private List<Transform> attachedObjects = new List<Transform>();

    private void Awake()
    {
        // Get and store a reference to the collider2D attached to Ground.
        backgroundCollider = GetComponent<BoxCollider2D>();
        // Store the size of the collider along the y axis (its length in units).
        verticalLength = backgroundCollider.size.y * transform.localScale.y;
    }

    private void Update()
    {
        // Check if the difference along the y axis between the main Camera and the position of the object this is attached to is greater than groundHorizontalLength.
        if (transform.position.y > verticalLength)
        {
            // If true, this means this object is no longer visible and we can safely move it down to be re-used.
            RepositionBackground();
        }
    }

    public void AddObjectToBackground(GameObject objectToAdd)
    {
        objectToAdd.transform.SetParent(transform);
    }

    //Moves the object this script is attached to right in order to create our looping background effect.
    private void RepositionBackground()
    {
        foreach (Transform objectTransform in attachedObjects)
        { // move the objects off this background for a minute so they don't jump with it
            objectTransform.SetParent(transform.parent);
        }
        Vector2 offset = new Vector2(0, verticalLength * transform.localScale.y);
        transform.position = (Vector2)transform.position - offset;
        foreach (Transform objectTransform in attachedObjects)
        {
            objectTransform.SetParent(transform);
        }
    }
}