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

		gun1 = transform.Find( "Gun1" );
		Assert.IsNotNull( gun1 );
		gun2 = transform.Find( "Gun2" );
		Assert.IsNotNull( gun2 );
	}

	void Update()
	{
		if( refire.Update( Time.deltaTime ) &&
			Input.GetAxis( "Attack" ) > 0.0f )
		{
			refire.Reset();

			Vector2 mousePos = Camera.main.ScreenToWorldPoint(
				Input.mousePosition );
			Vector2 diff = mousePos - ( Vector2 )transform.position;
			diff.Normalize();

			transform.rotation = Quaternion.Euler( 0.0f,0.0f,
				Mathf.Atan2( diff.y,diff.x ) * Mathf.Rad2Deg - 90.0f );

			body.AddForce( -diff * pushForce,
				ForceMode2D.Impulse );

			if( ++curGun > 1 ) curGun = 0;

			var bull = Instantiate( bulletPrefab,
				curGun == 0 ?
				gun1.transform.position
				: gun2.transform.position,
				Quaternion.identity );
			var bullBody = bull.GetComponent<Rigidbody2D>();
			bullBody.AddForce( diff * bulletSpeed,
				ForceMode2D.Impulse );
		}

		// TODO: Right-click to burst fire (longer cooldown).
	}

	Rigidbody2D body;
	Camera cam;
	GameObject bulletPrefab;
	Transform gun1;
	Transform gun2;

	int curGun = 0;

	[SerializeField] float pushForce = 0.0f;
	[SerializeField] float bulletSpeed = 5.0f;
	[SerializeField] Timer refire = new Timer( 0.2f );
}
