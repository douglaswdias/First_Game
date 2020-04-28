using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private Transform enemy_position;
    public int bossSpeed = 1;
    public Health_Bar bossHealthBar;
    public Health_Bar playerHealthBar;
    public int bossMaxHealth = 3;
    public int bossCurrentHealth= 3;
    public int boss_State = 1;
    public int playerMaxHealth = 5;
    public int playerCurrentHealth = 5;
    public Transform headPoint;
    public Transform leftCol;
    public Transform rightCol;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    bool player_Destroyed;
    private Transform target;
    public float knockback;

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
            float height = collision.contacts[0].point.y - headPoint.position.y;
            if (boss_State == 1 && playerCurrentHealth >= 1 && bossCurrentHealth > 0)
            {
                if(height > 0 && !player_Destroyed)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                    BossTakeDamage(1);
                    if (bossCurrentHealth <= 0)
                    {
                        knockback -= 2;
                        bossSpeed += 1;
                        boss_State += 1;
                        bossMaxHealth = 5;
                        bossCurrentHealth = bossMaxHealth + 1;
                        bossHealthBar.SetMaxHealth(bossMaxHealth);

                    }
                }
                else
                {
                    PlayerTakeDamage(1);
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
                if (height > 0 && !player_Destroyed )
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    BossTakeDamage(1);
                    if (bossCurrentHealth <= 0)
                    {
                        boss_State += 1;
                        knockback -= 2;
                        bossSpeed += 1;
                        bossMaxHealth = 7;
                        bossCurrentHealth = bossMaxHealth + 1;
                        bossHealthBar.SetMaxHealth(bossMaxHealth);
                    }
                }
                else
                {
                    PlayerTakeDamage(1);
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
                if (height > 0 && !player_Destroyed)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 08, ForceMode2D.Impulse);
                    BossTakeDamage(1);
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
                    PlayerTakeDamage(1);
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