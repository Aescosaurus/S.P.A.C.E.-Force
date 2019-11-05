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
		Assert.IsNotNull( explodeSound );

		explosionPrefab = Resources.Load<GameObject>(
			"Prefabs/Explosion" );
		Assert.IsNotNull( explosionPrefab );
	}

	public void Hurt( int damage )
	{
		health -= damage;
		if( health <= 0 )
		{
			// Play explosion animation.
			var explosion = Instantiate( explosionPrefab );
			var clip = explosion.GetComponent<AudioSource>();
			clip.clip = explodeSound;
			clip.Play();
			Destroy( gameObject );
		}
	}

	GameObject explosionPrefab;

	[SerializeField] int health = 0;
	[SerializeField] AudioClip explodeSound = null;
}
