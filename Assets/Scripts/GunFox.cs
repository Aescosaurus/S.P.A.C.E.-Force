using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GunFox
	:
	MonoBehaviour
{
	void Start()
	{
		player = GameObject.FindGameObjectWithTag( "Player" );
		Assert.IsNotNull( player );
		gun1 = transform.Find( "Gun1" );
		Assert.IsNotNull( gun1 );
		gun2 = transform.Find( "Gun2" );
		Assert.IsNotNull( gun2 );
		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/FoxBullet" );
		Assert.IsNotNull( bulletPrefab );
	}

	void Update()
	{

	}

	GameObject player;
	Transform gun1;
	Transform gun2;
	GameObject bulletPrefab;
}
