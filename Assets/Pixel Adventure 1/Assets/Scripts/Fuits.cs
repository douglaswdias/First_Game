using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuits : MonoBehaviour
{
    private SpriteRenderer sr;//Chama o componente Sprite Render do objeto
    private CircleCollider2D circle;//Chama o Componete Circle Collider2D do Objeto
    public GameObject collected;
    public int score = 10;
    // Start is called before the first frame update
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            sr.enabled = false;//Deixa o componente Sprite Render desmarcado
            circle.enabled = false;//Deixa o componente Circle Collider2D desmarcado
            collected.SetActive(true);//Ativa o componente pricipal para ficar visível
            GameController.instance.totalScore += score;
            GameController.instance.UpdateScoreText();
            Destroy(gameObject, 0.5f);//Destroi o objeto
        }
    }
}
