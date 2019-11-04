using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmancipatorBullet
	:
	MonoBehaviour
{
	void Start()
	{
		rotationSpeed = Random.Range( rotationSpeedMin,
			rotationSpeedMax );
	}

	void Update()
	{
		transform.Rotate( 0.0f,0.0f,
			rotationSpeed * Time.deltaTime );

		// if( despawn.Update( Time.deltaTime ) )
		// {
		// 	Destroy( gameObject );
		// }
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		// TODO: Deal damage to evil foxes if you hit one.
		if( coll.gameObject.tag == "SpaceFox" )
		{
			coll.gameObject.GetComponent<HealthBar>()
				.Hurt( 1 );
		}
		Destroy( gameObject );
	}

	// [SerializeField] Timer despawn = new Timer( 3.5f );
	[SerializeField] float rotationSpeedMin = 0.0f;
	[SerializeField] float rotationSpeedMax = 0.0f;

	float rotationSpeed;
}
