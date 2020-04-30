using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private Transform enemy_position;
    private int bossSpeed  = 2;
    private int bossCurrentSpeed = 2;
    public Health_Bar bossHealthBar;
    public Health_Bar playerHealthBar;
    private int bossMaxHealth = 3;
    private int bossCurrentHealth= 3;
    private int boss_State = 1;
    private int playerMaxHealth = 5;
    private int playerCurrentHealth = 5;
    public Transform headPoint;
    public Transform leftCol;
    public Transform rightCol;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    bool player_Destroyed;
    private Transform target;
    private float knockback = 10;
    bool bossHit = false;
    bool playerHit = false;
    float height;
    float bossTimer;
    float playerTimer;
    bool vulnerable = true;
    float invulnerable = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemy_position = GetComponent<Transform>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossCurrentHealth = bossMaxHealth;
        bossHealthBar.SetMaxHealth(bossMaxHealth);
        playerCurrentHealth = playerMaxHealth;
        playerHealthBar.SetMaxHealth(playerMaxHealth);

    }

    // Update is called once per frame
    void Update()
    {

        if(bossHit == true)
        {
            bossTimer -= Time.deltaTime;
            if(bossTimer <= 0)
            {
                bossTimer = 1;
                bossHit = false;
            }
        }

        if (vulnerable == false)
        {
            invulnerable -= Time.deltaTime;
            if (invulnerable <= 0)
            {
                invulnerable = 2;
                vulnerable = true;
                bossCurrentSpeed += 1;
                if(vulnerable == true)
                {
                    boss_State += 1;
                    bossSpeed = bossCurrentSpeed;
                }
            }
        }

        if (playerHit == true)
        {
            playerTimer -= Time.deltaTime;
            if (playerTimer <= 0)
            {
                playerTimer = 1;
                playerHit = false;
            }
        }

        if (boss_State == 1)
        {
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, bossSpeed * Time.deltaTime);
                if(enemy_position.position.x > target.position.x)
                {
                    enemy_position.eulerAngles = new Vector3(0f, 180f, 0f);
                }
                if(enemy_position.position.x < target.position.x)
                {
                    enemy_position.eulerAngles = new Vector3(0f, 0f, 0f);
                }
            }
        }

        if (boss_State == 2)
        {

            if (target != null)
            {
                enemy_position.transform.localScale = new Vector3(3.2499f, 3.2499f, 3.2499f);
                transform.position = Vector2.MoveTowards(transform.position, target.position, bossSpeed * Time.deltaTime);
                if (enemy_position.position.x > target.position.x)
                {
                    enemy_position.eulerAngles = new Vector3(0f, 180f, 0f);
                }
                if (enemy_position.position.x < target.position.x)
                {
                    enemy_position.eulerAngles = new Vector3(0f, 0f, 0f);
                }
            }
        }

        if (boss_State == 3)
        {
            if (target != null)
            {
                enemy_position.transform.localScale = new Vector3(4.87485f, 4.87485f, 4.87485f);
                transform.position = Vector2.MoveTowards(transform.position, target.position, bossSpeed * Time.deltaTime);
                if (enemy_position.position.x > target.position.x)
                {
                    enemy_position.eulerAngles = new Vector3(0f, 180f, 0f);
                }
                if (enemy_position.position.x < target.position.x)
                {
                    enemy_position.eulerAngles = new Vector3(0f, 0f, 0f);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            height = collision.contacts[0].point.y - headPoint.position.y;
            if (boss_State == 1 && playerCurrentHealth >= 1 && bossCurrentHealth > 0)
            {
                if(height > 0 && !player_Destroyed && vulnerable)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                    if(bossHit == false)
                    {
                        BossTakeDamage(1);
                        bossHit = true;
                    }
                    
                    if (bossCurrentHealth <= 0)
                    {
                        knockback -= 2;
                        bossSpeed = 0;
                        vulnerable = false;
                        bossMaxHealth = 5;
                        bossCurrentHealth = bossMaxHealth + 1;
                        bossHealthBar.SetMaxHealth(bossMaxHealth);

                    }
                }
                else
                {
                    if (playerHit == false)
                    {
                        PlayerTakeDamage(1);
                        playerHit = true;
                    }

                    if (enemy_position.position.x > target.position.x)
                    {
                        rig.velocity = new Vector2(knockback, knockback);
                    }

                    if (enemy_position.position.x < target.position.x)
                    {
                        rig.velocity = new Vector2(-knockback, knockback);
                    }
                }
            }

            if (boss_State == 2 && playerCurrentHealth >= 1 && bossCurrentHealth > 0)
            {
                if (height > 0 && !player_Destroyed && vulnerable)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    if (bossHit == false)
                    {
                        BossTakeDamage(1);
                        bossHit = true;
                    }
                    if (bossCurrentHealth <= 0)
                    {
                        knockback -= 2;
                        bossSpeed = 0;
                        vulnerable = false;
                        bossMaxHealth = 7;
                        bossCurrentHealth = bossMaxHealth + 1;
                        bossHealthBar.SetMaxHealth(bossMaxHealth);
                    }
                }
                else
                {
                    if (playerHit == false)
                    {
                        PlayerTakeDamage(1);
                        playerHit = true;
                    }

                    if (enemy_position.position.x > target.position.x)
                    {
                        rig.velocity = new Vector2(knockback, knockback);
                    }

                    if (enemy_position.position.x < target.position.x)
                    {
                        rig.velocity = new Vector2(-knockback, knockback);
                    }
                }
            }

            if (boss_State == 3 && playerCurrentHealth >= 1 && bossCurrentHealth > 0)
            {
                bossSpeed = 4;
                if (height > 0 && !player_Destroyed && vulnerable)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 08, ForceMode2D.Impulse);
                    if (bossHit == false)
                    {
                        BossTakeDamage(1);
                        bossHit = true;
                    }
                    if (bossCurrentHealth <= 0)
                    {
                        bossSpeed = 0;
                        boxCollider2D.enabled = false;
                        circleCollider2D.enabled = false;
                        rig.bodyType = RigidbodyType2D.Kinematic;
                        Destroy(gameObject, 0.25f);
                    }
                }
                else
                {
                    if (playerHit == false)
                    {
                        PlayerTakeDamage(1);
                        playerHit = true;
                    }

                    if (enemy_position.position.x > target.position.x)
                    {
                        rig.velocity = new Vector2(knockback, knockback);
                    }

                    if (enemy_position.position.x < target.position.x)
                    {
                        rig.velocity = new Vector2(-knockback, knockback);
                    }
                }
            }

            if (playerCurrentHealth <= 0)
            {
                player_Destroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(collision.gameObject);
            }
        }
    }

    public void BossTakeDamage(int damage)
    {
        bossCurrentHealth -= damage;
        bossHealthBar.SetHealth(bossCurrentHealth);
    }
    public void PlayerTakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        playerHealthBar.SetHealth(playerCurrentHealth);
    }
}