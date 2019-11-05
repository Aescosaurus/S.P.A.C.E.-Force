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

		audSrc = GetComponent<AudioSource>();
	}

	public void Hurt( int damage )
	{
		health -= damage;

		if( audSrc != null && hurtSound != null )
		{
			audSrc.PlayOneShot( hurtSound );
		}

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
	AudioSource audSrc;

	[SerializeField] int health = 0;
	[SerializeField] AudioClip hurtSound = null;
	[SerializeField] AudioClip explodeSound = null;
}
