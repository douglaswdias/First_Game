using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int totalScore;
    public Text scoreText;

    public static GameController instance;

    public GameObject gameOver;
    public GameObject nextLevelPreview;
    public GameObject nextLevel;
    public int nextLevelScore;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void ShowNextLevel()
    {
        nextLevelPreview.SetActive(false);
        nextLevel.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }



}
