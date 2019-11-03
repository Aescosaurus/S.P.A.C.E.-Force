using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenHomeworld
	:
	MonoBehaviour
{
	void Update()
	{
		transform.Rotate( 0.0f,0.0f,rotSpeed * Time.deltaTime );
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.tag == "SpaceKitten" )
		{
			Destroy( coll.gameObject );
			LevelHandler.SaveKitty();
			// TODO: Push back all enemies.
		}
	}

	[SerializeField] float rotSpeed = 0.0f;
}
