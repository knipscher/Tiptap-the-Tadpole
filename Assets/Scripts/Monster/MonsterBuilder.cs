using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterBuilder : MonoBehaviour
{
    public static MonsterBuilder instance;

    [SerializeField]
    private MonsterTemplate currentMonsterTemplate = default; // set in inspector at start
    [SerializeField]
    private GameObject monsterTemplatePrefab = default;
    [SerializeField]
    private Button hatchButton = default;

    public TextMeshProUGUI monsterStatsCaption;
    public Slider monsterTimeSlider = default;

    [SerializeField]
    private float newTemplateDelayTime = 1f;

    public GameObject rightSideFinPrefab; // this is messy but will work in a pinch
    
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

    private void Start()
    {
        hatchButton.interactable = false;
        hatchButton.onClick.AddListener(HatchManually);
        monsterTimeSlider.interactable = false;
    }

    public void HatchManually()
    {
        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Woodblock);
        currentMonsterTemplate.CreateMonsterWithoutDelay(); // also destroys template and creates new in this class -- bit confusing, sorry
    }

    public void SetHatchButtonInteractable(bool isInteractable)
    {
        hatchButton.interactable = isInteractable;
    }

    public void DestroyTemplateAndCreateNew()
    {
        var position = currentMonsterTemplate.transform.position;
        var parent = currentMonsterTemplate.transform.parent;
        Destroy(currentMonsterTemplate.gameObject);
        var newMonsterTemplate = Instantiate(monsterTemplatePrefab, position, Quaternion.identity, parent);
        currentMonsterTemplate = newMonsterTemplate.GetComponent<MonsterTemplate>();
        SetHatchButtonInteractable(false);
    }

    public void DestroyTemplateAndCreateNewWithDelay()
    {
        currentMonsterTemplate.gameObject.SetActive(false);
        StartCoroutine(DelayNewTemplate());
    }

    private IEnumerator DelayNewTemplate()
    {
        yield return new WaitForSeconds(newTemplateDelayTime); // try decreasing this if the player hit Hatch manually
        DestroyTemplateAndCreateNew();
    }

    public void AttemptAddPartToTemplate(MonsterPart part, PartType partType)
    {
        currentMonsterTemplate.AttemptAddPartToTemplate(part, partType);
    }
}