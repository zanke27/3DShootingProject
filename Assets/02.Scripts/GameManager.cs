using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public void GameSceneLoad()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOverSceneLoadWait()
    {
        Invoke(nameof(GameOverSceneLoad), 2f);
    }

    public void GameOverSceneLoad()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void MainSceneLoad()
    {
        SceneManager.LoadScene("Main");
    }

    public void TutorialSceneLoad()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void GameClearSceneLoad()
    {
        SceneManager.LoadScene("GameClear");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
