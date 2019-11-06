using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBoss2
	:
	MonoBehaviour
{
	void OnDestroy()
	{
		LevelHandler.SaveKitty();
	}
}
