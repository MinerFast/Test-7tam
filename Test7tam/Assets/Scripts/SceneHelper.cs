using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHelper : MonoBehaviour
{
   public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
