using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid
	:
	MonoBehaviour
{
	void OnCollisionEnter2D( Collision2D other )
	{
		// print( other.gameObject.tag );
		if( other.gameObject.tag == "Asteroid" )
		{
			Destroy( gameObject );
		}
	}
}
