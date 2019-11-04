using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// other.GetComponent<HealthBar>.Hurt( 1 );
public class HealthBar 
	:
	MonoBehaviour
{
	void Start()
	{

	}

	public void Hurt( int damage )
	{
		health -= damage;
		if( health <= 0 )
		{
			// Play explosion animation.
			Destroy( gameObject );
		}
	}

	GameObject explosionPrefab;

	[SerializeField] int health = 0;
	[SerializeField] AudioClip explosionSound;
	[SerializeField] Animation explosionAnimation;
}
