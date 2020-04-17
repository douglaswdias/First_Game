using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{
    private Animator anim;
    public float jumpForce;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Trigger", true);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision1)
    {
        if (collision1.gameObject.tag == "Player")
        {
            anim.SetBool("Trigger", false);
        }
    }

}