using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button exitButton;

    void OnEnable()
    {
        GameManager.RaceFinish += ShowGameOver;
    }

    void OnDisable()
    {
        GameManager.RaceFinish -= ShowGameOver;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(QuitGame);
    }

    void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void QuitGame()
    {
        Debug.Log("exiting Game");
        Application.Quit();
    }
}

