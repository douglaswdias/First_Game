using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision1)
    {
        if (collision1.gameObject.tag == "Player")
        {
            anim.SetBool("Trigger", true);
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