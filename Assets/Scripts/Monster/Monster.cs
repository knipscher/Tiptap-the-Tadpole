using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // should be private, but currently public for debugging
    public int attackStrength = 1;
    public int speed = 0;
    public int navigationAbility = 0;
    public List<MonsterTooth> teeth = new List<MonsterTooth>();

    public bool isInBiteCooldown = false;

    [SerializeField]
    private float defaultSpeedMultiplier = default;
    [SerializeField]
    private float defaultRotationMultiplier = .05f;
    
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // from here: https://answers.unity.com/questions/630670/rotate-2d-sprite-towards-moving-direction.html
        Vector2 moveDirection = (Vector2)Player.instance.transform.position - (Vector2)transform.position;
        if (moveDirection != Vector2.zero)
        {
            Rotate(moveDirection);
        }
        rb2d.AddForce(speed * defaultSpeedMultiplier * transform.up);
    }

    private void Rotate(Vector2 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        Quaternion lookingAtTargetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, lookingAtTargetRotation, navigationAbility * defaultRotationMultiplier);
        transform.rotation = newRotation;
    }

    public void Initialize(int speed, int navigationAbility, float size)
    {
        this.speed = speed;
        this.navigationAbility = navigationAbility;

        transform.localScale = transform.localScale * size;

        attackStrength = 1; // originally had this set to teeth amount, but I actually like it better if each tooth does 1 damage
        foreach (MonsterTooth tooth in teeth)
        {
            tooth.attackStrength = this.attackStrength;
        }
    }

    public void Bite()
    {
        StartCoroutine(BiteCooldown());
    }

    private IEnumerator BiteCooldown()
    {
        isInBiteCooldown = true;
        yield return new WaitForSeconds(.15f);
        isInBiteCooldown = false;
    }
}