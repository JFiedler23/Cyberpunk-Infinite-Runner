using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References
    private Rigidbody2D rb;
    private Collider2D feetCollider;
    private Animator animator;
    public SceneLoader sceneLoader;

    //Player stats
    public int lives = 3;

    //movement variables
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    private bool touchingGround = true;
    private bool isRunning;
    private bool isSliding;
    private bool isJumping;

    //collider variables
    public CapsuleCollider2D runCollider;
    public CircleCollider2D slideCollider;
    public CircleCollider2D jumpCollider;

    //bullet variables
    public GameObject bulletPrefab;
    public Transform bulletRunTransform;
    public Transform bulletSlideTransform;
    public Transform bulletJumpTransform;

    //other
    public AnimationClip runShootClip;
    private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        //Setting reload scene to current scene
        GameData.SceneToReload = sceneLoader.GetCurrentScene();

        //If it is a new game set GameData lives
        if (GameData.NewGame)
        {
            //setting lives to persist across scenes
            GameData.Lives = lives;
            GameData.NewGame = false;
        }
        //else reloading scene or continuing a previous game
        else
        {
            lives = GameData.Lives;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        //Player horizontal movement
        rb.velocity = new Vector2(moveSpeed + Time.deltaTime, rb.velocity.y);

        //Checking if player wants to shoot
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            Shoot();
        }

        //Detecting player jump
        if (Input.GetKeyDown("space") && touchingGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed + Time.deltaTime);
            animator.SetTrigger("jump");
            //runCollider.enabled = false;
        }
        //Detecting player slide
        else if (Input.GetKey("down") && touchingGround)
        {
            runCollider.enabled = false;
            slideCollider.enabled = true;
            animator.SetBool("sliding", true);

            //state bools
            isRunning = false;
            isJumping = false;
            isSliding = true;
        }
        //Playing fall animation if playing is falling
        else if (rb.velocity.y < 0 && !touchingGround)
        {
            animator.SetBool("falling", true);
            //runCollider.enabled = false;
            //jumpCollider.enabled = true;

            //state bools
            isRunning = false;
            isJumping = true;
            isSliding = false;
        }
        else
        {
            //collider change depending on state
            runCollider.enabled = true;
            slideCollider.enabled = false;
            jumpCollider.enabled = false;

            //animation change
            animator.SetBool("sliding", false);
            
            //state bools
            isRunning = true;
            isSliding = false;
            isJumping = false;
        }
    }

    private void Shoot()
    {
        if (isRunning)
        {
            StartCoroutine("RunShoot");
        }
        else if (isSliding)
        {
            SpawnBullet(bulletSlideTransform);
        }
        else if (isJumping)
        {
            SpawnBullet(bulletJumpTransform);
        }
    }

    IEnumerator RunShoot()
    {
        //Starting run shoot animation
        animator.SetBool("shooting", true);
        SpawnBullet(bulletRunTransform);

        //waiing until animation is finished
        yield return new WaitForSeconds(runShootClip.length);

        //returning to normal run animation
        animator.SetBool("shooting", false);
    }

    private void SpawnBullet(Transform bulletPos)
    {
        Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Detecting if player has jumped
        if(collision.otherCollider == feetCollider)
        {
            touchingGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Detecting if play is touching a surface they can jump off of
        if (collision.otherCollider == feetCollider)
        {
            touchingGround = true;
            animator.SetBool("falling", false);
        }
        //Detecting if player has been hit
        if(collision.collider.name == "Hazards" || collision.collider.name == "Drone" || collision.collider.name == "Turret")
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "DeathBox")
        {
            //this hit count thing is really dumb but I have yet to figure out why the deathbox triggers 2 collisions
            if(hitCount == 1)
            {
                Die();
            }
            else
            {
                hitCount++;
            }
           
        }
        if(collision.name == "Enemy Bullet(Clone)")
        {
            Die();
        }
    }

    private void Die()
    {
        //stop forward player motion
        //stop camera movement
        //start death animation
        lives--;
        GameData.Lives = lives;

        if(lives > 0)
        {
            sceneLoader.LoadLivesLeftScene();
        }
        else
        {
            sceneLoader.LoadGameOver();
        }
    }
}
