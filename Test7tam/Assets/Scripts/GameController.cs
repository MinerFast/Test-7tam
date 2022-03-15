using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button[] playerControllerButtons;

    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject preGame;

    public static int score;
    #region MonoBehaviour

    private void Awake()
    {
        score = 0;
        Time.timeScale = 0f;
    }
    private void LateUpdate()
    {
        if (score == 2)
        {
            Win();
        }
    }
    #endregion
    public void Lose()
    {
        Time.timeScale = 0f;
        lose.SetActive(true);
        HidingPlayerButtons();
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        preGame.SetActive(false);
        OpenPlayerButtons();
    }
    void Win()
    {
        Time.timeScale = 0f;
        win.SetActive(true);
        HidingPlayerButtons();
    }
    void HidingPlayerButtons()
    {
        for (int i = 0; i < playerControllerButtons.Length; i++)
        {
            playerControllerButtons[i].gameObject.SetActive(false);
        }
    }
    void OpenPlayerButtons()
    {
        for (int i = 0; i < playerControllerButtons.Length; i++)
        {
            playerControllerButtons[i].gameObject.SetActive(true);
        }
    }
}
