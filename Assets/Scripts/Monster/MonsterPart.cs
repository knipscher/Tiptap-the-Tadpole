using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PartType { Eye, LeftFin, TailFin, Tooth, RightFin }
public class MonsterPart : MonoBehaviour, IPointerDownHandler
{
    // UI element that the user drags to the monster template to build a monster

    public PartType partType;
    public GameObject correspondingPrefab; // this is the part that goes on the actual monster
    private Image image;

    private Vector2 originalPosition;

    private bool isBeingDragged = false;

    private void Start()
    {
        originalPosition = transform.position;
        image = GetComponent<Image>();
        image.raycastTarget = true;
    }

    private void Update()
    {
        if (isBeingDragged)
        {
            transform.position = Input.mousePosition;
            image.enabled = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isBeingDragged)
            {
                PlaceOnMonsterTemplate();
            }
            isBeingDragged = false;
            image.raycastTarget = true;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Woodblock);
        isBeingDragged = true;
        image.raycastTarget = false;
    }

    private void PlaceOnMonsterTemplate()
    {
        MonsterPartBank.instance.SpawnMonsterPart(originalPosition);
        if (MonsterBuilder.instance)
        { // to allow this to be used in the tutorial
            MonsterBuilder.instance.AttemptAddPartToTemplate(this, partType);
        }
    }
}