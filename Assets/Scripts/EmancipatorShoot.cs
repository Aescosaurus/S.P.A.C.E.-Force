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
	}

	void Update()
	{
		if( Input.GetAxis( "Attack" ) > 0.0f )
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(
				Input.mousePosition );
			Vector2 diff = mousePos - ( Vector2 )transform.position;
			body.AddForce( -diff.normalized * pushForce,
				ForceMode2D.Impulse );

			// TODO: Spawn bullet.
		}
	}

	Rigidbody2D body;
	Camera cam;

	[SerializeField]
	float pushForce = 0.0f;
}
