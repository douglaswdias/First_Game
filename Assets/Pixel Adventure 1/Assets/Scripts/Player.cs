using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public bool isJumping;
    public bool doubleJump;

    public Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);//Movimento horizontal padrão
        transform.position += movement * Time.deltaTime * speed;
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);//Personagem encarando o lado direto
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);//Rotaciona o eixo z 180, para encarar para a esquerda
        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("Walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))//Padrão de pulo com tecla espaço
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);//Verifica se está no ar para o segundo pulo
                doubleJump = true;
                anim.SetBool("Jump", true);
            }
            else
            {
                if (doubleJump)
                {
                    rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    doubleJump = false;//Valida se o segundo pulo já aconteceu
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }

        if (collision.gameObject.tag == "Spike" | collision.gameObject.tag == "Fire")
        {
            Destroy(gameObject);
            GameController.instance.ShowGameOver();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
}
