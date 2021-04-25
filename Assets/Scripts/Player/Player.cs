using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int health = 15;
    private int maxHealth;

    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float turboLength = 1f;
    [SerializeField]
    private float turboCooldown = 3f;
    private bool isInTurboCoroutine = false;

    [SerializeField]
    private ParticleSystem turboParticles = default;
    [SerializeField]
    private GameObject trail = default;

    [SerializeField]
    private GameObject teethCollider = default;

    private Rigidbody2D rb2d;
    private Vector2 previousPosition;

    [SerializeField]
    private float maxVelocity = 20f;

    [SerializeField]
    private Slider playerHealthBar = default;

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
        maxHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
        teethCollider.SetActive(false);

        if (playerHealthBar) // so we can still use the player in other scenes without getting an error
        {
            playerHealthBar.interactable = false;
            playerHealthBar.maxValue = health;
            playerHealthBar.value = health;
        }

        trail.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Turbo());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            EatFood(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Great Food"))
        {
            EatFood(3);
            Destroy(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        var movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        rb2d.AddForce(movement);

        RotateToFaceForward();

        previousPosition = transform.position;

        if (rb2d.velocity.magnitude > maxVelocity)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxVelocity;
        }
    }

    private void RotateToFaceForward()
    {
        // from here: https://answers.unity.com/questions/630670/rotate-2d-sprite-towards-moving-direction.html
        Vector2 moveDirection = (Vector2)transform.position - previousPosition;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }

    private IEnumerator Turbo()
    {
        // do turbo here
        if (!isInTurboCoroutine)
        {
            isInTurboCoroutine = true;
            turboParticles.Play();
            trail.SetActive(true);
            Debug.Log(trail);
            speed *= 5;
            yield return new WaitForSeconds(turboLength);
            speed /= 5;
            yield return new WaitForSeconds(turboCooldown);
            trail.SetActive(false);
            isInTurboCoroutine = false;
        }
    }

    public void GetBitten(int attackDamage)
    {
        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.Attack);
        health -= attackDamage;
        playerHealthBar.value = health;
        if (health <= 0)
        {
            GameControl.instance.Player1LoseGame();
        }
    }

    public void EatFood(int foodValue)
    {
        health += foodValue;
        SoundEffectsManager.instance.PlaySoundEffect(SoundEffect.EatFood);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        playerHealthBar.value = health;
    }
}