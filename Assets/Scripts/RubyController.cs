using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    public NewBehaviourScript AC;

    public Text timer;

    public float timeRemaining;

    public static int level0tru;

    public static int timeattack;

    public LevelController z_LevelController;

    public MusicController musicController;

    GameObject someGameObject;

    public Text winText;

    public float speed = 3.0f;

    public int maxHealth = 5;

    public GameObject projectilePrefab;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public ParticleSystem Health;
    public ParticleSystem Damage;

    public int chickendinner;

    public int Cogs;

    public Text CogText;

    public AudioClip collectedCogsClip;

    public AudioClip DamagedClip;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 120;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

        chickendinner = 2;

        someGameObject = GameObject.Find("Ruby");

        Cogs = 8;

        CogText.text = "Cogs: " + Cogs.ToString();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    Debug.Log("Hit");
                    character.DisplayDialog();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (chickendinner == 3)
            {
                level0tru = 4;
                Debug.Log("Reset");
                SceneManager.LoadScene("Main");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                level0tru = 3;
                
                AC.go1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
                AC.go2();
        }
        if (timeattack == 3)
        {

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                timer.text = "Time: " + timeRemaining.ToString();
            }
            else
            {
                chickendinner = 3;
                z_LevelController.loser();
                someGameObject.SetActive(false);
                winText.text = "Time Up! Game created by Noah McArthur. Press R to reset";


                musicController.Lose();
            }
        }
    }

    public void go11()
    {
        timeattack = 2;
    }
    public void go22()
    {
        timeattack = 3;
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            PlaySound(DamagedClip);

            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if (amount < 0)
        {
            Instantiate(Damage, transform.position, transform.rotation);
        }
        if (amount > 0)
        {
            Instantiate(Health, transform.position, transform.rotation);
        }

        if (currentHealth <= 0)
        {
            z_LevelController.loser();
            Debug.Log("Die");
            someGameObject.SetActive (false);
            winText.text = "You Lose! Game created by Noah McArthur. Press R to reset";

            chickendinner = 3;

            musicController.Lose();
        }
    }

    void Launch()
    {
        if (Cogs >= 1)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");

            Cogs = (Cogs - 1);

            CogText.text = "Cogs: " + Cogs.ToString();
        }
    }

    public void tele()
    {
        Debug.Log("teleScc");
        transform.position = new Vector3(0, 500, 0);
    }

    public void winner()
    {
        chickendinner = 3;
        timeattack = 2;
    }

    void OnCollisionEnter2D(Collision2D other)   
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            Debug.Log("Detected");
            other.gameObject.SetActive(false);
            Cogs = (Cogs + 4);
            CogText.text = "Cogs: " + Cogs.ToString();

            PlaySound(collectedCogsClip);
        }
    }
}