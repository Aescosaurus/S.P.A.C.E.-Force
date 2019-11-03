﻿using System.Collections;
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
		[SerializeField] public string sceneName;
		[SerializeField] public int kittiesToWin;
	}

	// Move all instance data to static data.
	void Start()
	{
		foreach( var scene in sceneOrder )
		{
			sceneList.Add( scene );
		}

		LoadNextScene();

		Destroy( gameObject );
	}

	public static void SaveKitty()
	{
		++kittiesSaved;

		if( kittiesSaved >= kittiesToWin )
		{
			LoadNextScene();
		}
	}
	public static void LoadNextScene()
	{
		SceneManager.LoadScene( sceneList[curLevel].sceneName );
		kittiesToWin = sceneList[curLevel].kittiesToWin;

		kittiesSaved = 0;

		++curLevel;
	}

	[SerializeField] SceneInfo[] sceneOrder = {};

	static List<SceneInfo> sceneList = new List<SceneInfo>();

	static int curLevel = 0;
	static int kittiesSaved = -1;
	static int kittiesToWin = 0;
}