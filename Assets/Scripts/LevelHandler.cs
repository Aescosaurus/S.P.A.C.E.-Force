using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler
	:
	MonoBehaviour
{
	[System.Serializable]
	class SceneInfo
	{
		[SerializeField] public string sceneName = "";
		[SerializeField] public int kittiesToWin = 0;
	}

	// Move all instance data to static data.
	void Start()
	{
		foreach( var scene in sceneOrder )
		{
			sceneList.Add( scene );
		}

		sceneLoaderPrefab = Resources.Load<GameObject>(
			"Prefabs/NextSceneLoader" );
	}

	public static void SaveKitty()
	{
		++kittiesSaved;

		if( kittiesSaved >= kittiesToWin )
		{
			// LoadNextScene();
			Instantiate( sceneLoaderPrefab );
		}
	}
	public static void DefeatBoss()
	{
		// TODO: Open upgrade menu.
		// When done upgrading go to next level.
	}
	public static void LoadNextScene()
	{
		++curLevel;

		SceneManager.LoadScene( sceneList[curLevel].sceneName );
		kittiesToWin = sceneList[curLevel].kittiesToWin;

		kittiesSaved = 0;

	}
	public static void Reset()
	{
		--curLevel;
		LoadNextScene();
	}
	public static void Restart()
	{
		curLevel = 0;
		LoadNextScene();
	}
    public static void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
    public static void ExitToMainMenu()
    {
        curLevel = -1;
        LoadNextScene();
    }
    public void PlayGame()
    {
        LoadNextScene();
    }
    public void ReturnToMain()
    {
        ExitToMainMenu();
    }
    public void ShowCredits()
    {
        LoadCreditsScene();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

	static GameObject sceneLoaderPrefab;

    [SerializeField] SceneInfo[] sceneOrder = {};

	static List<SceneInfo> sceneList = new List<SceneInfo>();

	static int curLevel = 0;
	static int kittiesSaved = -1;
	static int kittiesToWin = 0;
}
