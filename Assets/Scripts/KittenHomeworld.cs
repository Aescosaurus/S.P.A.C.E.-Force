using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KittenHomeworld
	:
	MonoBehaviour
{
	void Start()
	{
		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );

		kittenSaveSound = Resources.Load<AudioClip>(
			"Sounds/Kitten Saved" );
		Assert.IsNotNull( kittenSaveSound );
	}

	void Update()
	{
		transform.Rotate( 0.0f,0.0f,rotSpeed * Time.deltaTime );
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.tag == "SpaceKitten" )
		{
			audSrc.PlayOneShot( kittenSaveSound );
			Destroy( coll.gameObject );
			LevelHandler.SaveKitty();
			// TODO: Push back all enemies.
		}
	}

	AudioSource audSrc;

	[SerializeField] float rotSpeed = 0.0f;

	AudioClip kittenSaveSound;
}
