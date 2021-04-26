using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterTemplate : MonoBehaviour
{
    [SerializeField]
    private GameObject monsterPrefab = default;

    private int speed = 0;
    private int navigationAbility = 0;

    [SerializeField]
    private float minXSpawnPosition = -1.5f;
    [SerializeField]
    private float maxXSpawnPosition = 1.5f;

    [SerializeField]
    private MonsterPartLocator eyeLocator = default;
    [SerializeField]
    private MonsterPartLocator toothLocator = default;
    [SerializeField]
    private MonsterPartLocator leftFinLocator = default;
    [SerializeField]
    private MonsterPartLocator rightFinLocator = default;
    [SerializeField]
    private MonsterPartLocator tailFinLocator = default;

    [SerializeField]
    private TextMeshProUGUI monsterStatsCaption = default;
    private Animator monsterStatsCaptionAnimator = default;

    [SerializeField]
    private Transform partsContainer = default;

    private List<MonsterPart> monsterParts = new List<MonsterPart>();

    public float[] monsterCreationTimes = new float[] { 5, 10, 10, 10, 10, 10, 15, 15, 20 };
    public float monsterCreationTime;
    [SerializeField]
    private Slider monsterTimeSlider = default;

    [SerializeField]
    private float distanceThreshold = 1f; // the width of the circle background
    private bool hasParts = false;
    private float rotationSpeed;

    private void Start()
    {
        rotationSpeed = Random.Range(-1.5f, 1.5f);

        var randomID = Random.Range(0, monsterCreationTimes.Length);
        monsterCreationTime = monsterCreationTimes[randomID];
        monsterTimeSlider = MonsterBuilder.instance.monsterTimeSlider;
        StartTimer();
        StartCoroutine(CreateMonstersAtTimedIntervals());

        monsterStatsCaption = MonsterBuilder.instance.monsterStatsCaption;
        monsterStatsCaptionAnimator = monsterStatsCaption.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
        monsterTimeSlider.value -= Time.deltaTime;
    }

    private void StartTimer()
    {
        monsterTimeSlider.maxValue = monsterCreationTime;
        monsterTimeSlider.value = monsterCreationTime;
    }

    private IEnumerator CreateMonstersAtTimedIntervals()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(monsterCreationTime);
            if (hasParts)
            {
                CreateMonsterWithDelay();
            }
            else
            {
                Fail();
            }
        }
    }

    public void CreateMonsterWithDelay()
    {
        CreateMonster();
        MonsterBuilder.instance.DestroyTemplateAndCreateNewWithDelay();
        hasParts = false;
    }

    public void CreateMonsterWithoutDelay()
    {
        var monster = CreateMonster();
        CreateDuplicateMonstersIfFastEnough(monster);
        MonsterBuilder.instance.DestroyTemplateAndCreateNew();
    }

    private void SetMonsterStatsCaption(string caption) {
        monsterStatsCaption.text = caption;
        monsterStatsCaptionAnimator.Play("MonsterStatsCaption");
    }

    private void CreateDuplicateMonstersIfFastEnough(Monster monster)
    {
        if (monsterTimeSlider.value > monsterCreationTime * .3f)
        {
            SetMonsterStatsCaption("Great!");
            Instantiate(monster.gameObject, monster.transform.position, monster.transform.rotation, monster.transform.parent);
        }
        if (monsterTimeSlider.value > monsterCreationTime * .6f)
        {
            SetMonsterStatsCaption("AMAZING!");
            Instantiate(monster.gameObject, monster.transform.position, monster.transform.rotation, monster.transform.parent);
        }
    }

    private Monster CreateMonster()
    {
        SetMonsterStatsCaption("Nice!");

        var position = new Vector2(Random.Range(minXSpawnPosition, maxXSpawnPosition), -4.5f);
        var monsterGameObject = Instantiate(monsterPrefab, position, Quaternion.identity, ScrollingBackground.instance.transform);
        var monster = monsterGameObject.GetComponent<Monster>();

        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Finished);

        AddPartsToMonster(monster);
        monster.Initialize(speed, navigationAbility, transform.localScale.x);
        return monster;
    }

    private void AddPartsToMonster(Monster monster)
    {
        foreach (MonsterPart part in monsterParts)
        {
            var spritePosition = new Vector2(part.transform.localPosition.x / 80, part.transform.localPosition.y / 80);
            GameObject spritePart;
            if (part.partType == PartType.RightFin)
            {
                spritePart = Instantiate(MonsterBuilder.instance.rightSideFinPrefab, Vector2.zero, part.transform.rotation, monster.transform);
            }
            else
            {
                spritePart = Instantiate(part.correspondingPrefab, Vector2.zero, part.transform.rotation, monster.transform);
            }
            spritePart.transform.localPosition = spritePosition;

            if (part.partType == PartType.Tooth)
            {
                monster.teeth.Add(spritePart.GetComponent<MonsterTooth>());
                monster.attackStrength++;
            }
            else if (part.partType == PartType.LeftFin || part.partType == PartType.TailFin)
            {
                spritePart.GetComponent<SpriteRenderer>().color += new Color(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            }
        }
        monsterParts = new List<MonsterPart>();
    }

    private void Fail()
    {
        MonsterBuilder.instance.DestroyTemplateAndCreateNewWithDelay();
    }

    public void AttemptAddPartToTemplate(MonsterPart part, PartType partType)
    {
        if (Vector2.Distance(Input.mousePosition, transform.position) < distanceThreshold)
        {
            hasParts = true;
            part.transform.SetParent(partsContainer);
            monsterParts.Add(part);
            MonsterBuilder.instance.SetHatchButtonInteractable(true);

            // check the location based on the part type to see if the stat increase is successful
            switch (partType)
            {
                case PartType.Eye:
                    if (eyeLocator.isMouseOver)
                    {
                        SetMonsterStatsCaption("+1 vision!");
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Correct);
                        navigationAbility++;
                    }
                    else
                    {
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Incorrect);
                    }
                    break;
                case PartType.LeftFin:
                    if (leftFinLocator.isMouseOver)
                    {
                        SetMonsterStatsCaption("+1 speed!");
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Correct);
                        speed++;
                    }
                    else if (rightFinLocator.isMouseOver)
                    {
                        SetMonsterStatsCaption("+1 speed!");
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Correct);
                        speed++;
                        part.partType = PartType.RightFin;
                    }
                    else
                    {
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Incorrect);
                    }
                    break;
                case PartType.TailFin:
                    if (tailFinLocator.isMouseOver)
                    {
                        SetMonsterStatsCaption("+1 speed!");
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Correct);
                        speed++;
                    }
                    else
                    {
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Incorrect);
                    }
                    break;
                case PartType.Tooth:
                    if (toothLocator.isMouseOver)
                    {
                        SetMonsterStatsCaption("+1 tooth!");
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Correct);
                    }
                    else
                    {
                        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Incorrect);
                    }
                    break;
                default:
                    // default case includes teeth, because they will work no matter where they are placed & don't need a location check
                    break;
            }
        }
        else
        {
            SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Incorrect);
            Destroy(part.gameObject);
        }
    }
}