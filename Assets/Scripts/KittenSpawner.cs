using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KittenSpawner
	:
	MonoBehaviour
{
	void Start()
	{
		kittenPrefab = Resources.Load<GameObject>(
			"Prefabs/Kitten" );
		Assert.IsNotNull( kittenPrefab );
	}

	void Update()
	{
		if( kittenSpawnTimer.Update( Time.deltaTime ) )
		{
			kittenSpawnTimer.Reset();

			if( Random.Range( 0.0f,100.0f ) < kittenSpawnRate )
			{
				// TODO: Create kitten.
			}
		}
	}

	GameObject kittenPrefab;

	[Tooltip( "Chance of spawning a kitten every second." )]
	[Range( 0.0f,100.0f )]
	[SerializeField] float kittenSpawnRate = 0.0f;

	Timer kittenSpawnTimer = new Timer( 1.0f );
}
