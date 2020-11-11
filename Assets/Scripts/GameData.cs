using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
	private static int lives;
	private static bool newGame = true;
	private static string sceneToReload;
	public static int Lives
	{
		get
		{
			return lives;
		}
		set
		{
			lives = value;
		}
	}

	public static bool NewGame
	{
		get
		{
			return newGame;
		}
		set
		{
			newGame = value;
		}
	}

	public static string SceneToReload
    {
        get
        {
			return sceneToReload;
        }
        set
        {
			sceneToReload = value;
        }
    }
}
