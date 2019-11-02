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
		if( Input.GetAxis( "Attack" ) > 0.0f )
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(
				Input.mousePosition );
			Vector2 diff = mousePos - ( Vector2 )transform.position;
			body.AddForce( -diff.normalized * pushForce,
				ForceMode2D.Impulse );

			var bull = Instantiate( bulletPrefab,
				transform.position,Quaternion.identity );
			var bullBody = bull.GetComponent<Rigidbody2D>();
			bullBody.AddForce( diff * bulletSpeed,
				ForceMode2D.Impulse );
		}
	}

	Rigidbody2D body;
	Camera cam;
	GameObject bulletPrefab;

	[SerializeField] float pushForce = 0.0f;
	[SerializeField] float bulletSpeed = 5.0f;
}
