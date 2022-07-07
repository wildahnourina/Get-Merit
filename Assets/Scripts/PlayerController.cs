using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    //Start() variables
    // public playerFall respawn;
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;

    //FSM
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    private int healthDefault;
    public Image[] lives;
    [SerializeField] public int livesRemaining;
    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpforce = 7f;
    [SerializeField] private int coins = 0;
    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private float hurtforce = 1f;

    //AudioSource
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource coin;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource powerup;
    [SerializeField] private AudioSource healthlife;
    [SerializeField] private AudioSource hurts;
    [SerializeField] private AudioSource hurryup;
    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private float respawnDelay;
    [SerializeField] private bool poweringUp;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthDefault = health;
        //healthText.text = health.ToString();
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        animator.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            coin.Play();
            Destroy(collision.gameObject);
            coins += 1;
            coinText.text = coins.ToString();
        }
        if (collision.tag == "Health")
        {
            healthlife.Play();
            Destroy(collision.gameObject);
            health += 1;
            healthText.text = health.ToString();
        }
        if (collision.tag == "PowerUp")
        {
            poweringUp = true;
            powerup.Play();
            Destroy(collision.gameObject);
            jumpforce = 7f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
        if (collision.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }
        // if (collision.tag == "FallDetector")
        // {
        //     loseLife();
        //     transform.position = respawnPoint;
        // }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "movingPlatform")
        {
            this.transform.parent = other.transform;
        }

        // if (other.gameObject.tag == "PowerUp"){
        //     powerup.Play();
        //     Destroy(other.gameObject);
        //     jumpforce = 7f;
        //     GetComponent<SpriteRenderer>().color = Color.yellow;
        //     StartCoroutine(ResetPower());
        // }

        if (other.gameObject.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
                coins += 10;
                coinText.text = coins.ToString();

            }
            else
            {
                if (poweringUp == true)
                {
                    enemy.JumpedOn();
                    coins += 10;
                    coinText.text = coins.ToString();
                }
                else
                {
                    state = State.hurt;
                    hurts.Play();
                    Health();
                    if (other.gameObject.transform.position.x < transform.position.x)
                    {
                        rb.velocity = new Vector2(hurtforce + 1, 5);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-hurtforce - 2, 5);
                    }
                }

            }
        }
        if (other.gameObject.tag == "obstacle")
        {
            state = State.hurt;
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(hurtforce + 1, 5);
            }
            else
            {
                rb.velocity = new Vector2(-hurtforce - 1, 5);
            }
            hurts.Play();
            Health();

        }
        if (other.gameObject.tag == "newFall")
        {
            // Debug.Log("new");
            falling();
        }
        // if (other.gameObject.tag == "FallDetector")
        // {
        //     Debug.Log("masuk");
        //     //    falling();
        // }


    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "movingPlatform")
        {
            this.transform.parent = null;
        }
    }
    public void falling()
    {
        loseLife();
        transform.position = respawnPoint;
    }

    private void Health()
    {
        health -= 1;
        healthText.text = health.ToString();
        if (health == 0)
        {
            respawn();
            loseLife();
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(hDirection * speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            //animator.SetBool("running", true);
        }
        //moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(hDirection * speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            //animator.SetBool("running", true);
        }
        else
        {
            //animator.SetBool("running", false);
        }
        //jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            jump.Play();
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        state = State.jumping;
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void footsteps()
    {
        footstep.Play();
    }


    private IEnumerator ResetPower()
    {
        hurryup.Play();
        yield return new WaitForSeconds(10);
        poweringUp = false;
        jumpforce = 7f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }
    public IEnumerator RespawnCoroutine()
    {
        healthText.text = health.ToString();
        yield return new WaitForSeconds(respawnDelay);
        health = healthDefault;
        healthText.text = health.ToString();
        transform.position = respawnPoint;
    }
    public void loseLife()
    {
        livesRemaining -= 1;
        Debug.Log(livesRemaining);
        lives[livesRemaining].enabled = false;

        if (livesRemaining == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            livesRemaining = 3;
        }
    }
}
