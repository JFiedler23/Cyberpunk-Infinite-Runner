using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI livesLeftText;
    public SceneLoader sceneLoader;

    private void Start()
    {
        //check if current scene is a level
        //call coroutine to countdown from 3
    }

    private void Update()
    {
        //If in a level scene or lives left scene
        if (sceneLoader.GetCurrentScene() != "Game Over")
        {
            livesLeftText.text = "x" + GameData.Lives.ToString();

            if (sceneLoader.GetCurrentScene() == "Lives Left")
            {
                if (Input.GetKeyDown("space"))
                {
                    sceneLoader.ReloadLevel();
                }
            }
        }
        //game over scene
        else
        {
            if (Input.GetKeyDown("space"))
            {
                sceneLoader.LoadMainMenu();
            }
        }
    }

    //create coroutine to countdown from 3
}
