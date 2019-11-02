using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EmancipatorShoot
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );

		cam = Camera.main;
		Assert.IsNotNull( cam );

		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/Emancipator Bullet" );
		Assert.IsNotNull( bulletPrefab );
	}

	void Update()
	{
		if( Input.GetAxis( "Attack" ) > 0.0f &&
			refire.Update( Time.deltaTime ) )
		{
			refire.Reset();

			Vector2 mousePos = Camera.main.ScreenToWorldPoint(
				Input.mousePosition );
			Vector2 diff = mousePos - ( Vector2 )transform.position;
			diff.Normalize();

			body.AddForce( -diff * pushForce,
				ForceMode2D.Impulse );

			var bull = Instantiate( bulletPrefab,
				transform.position,Quaternion.identity );
			var bullBody = bull.GetComponent<Rigidbody2D>();
			bullBody.AddForce( diff * bulletSpeed,
				ForceMode2D.Impulse );

			// TODO: Rotate sprite in direction of shot.
		}


		// TODO: Right-click to burst fire (longer cooldown).
	}

	Rigidbody2D body;
	Camera cam;
	GameObject bulletPrefab;

	[SerializeField] float pushForce = 0.0f;
	[SerializeField] float bulletSpeed = 5.0f;
	[SerializeField] Timer refire = new Timer( 0.2f );
}
