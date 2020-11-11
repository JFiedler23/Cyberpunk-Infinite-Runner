using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadScene("Level 1");
        GameData.NewGame = true;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(GameData.SceneToReload);
    }

    public void LoadLivesLeftScene()
    {
        SceneManager.LoadScene("Lives Left");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
