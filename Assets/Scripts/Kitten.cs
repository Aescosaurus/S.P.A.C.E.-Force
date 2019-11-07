using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Kitten
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "Player" )
		{
			player = coll.gameObject;
		}
		else if( coll.gameObject.tag == "SpaceFox" )
		{
			// Ouch!
		}
	}

	void FixedUpdate()
	{
		if( player != null )
		{
			Vector2 diff = player.transform.position -
				transform.position;

			body.AddForce( diff * moveSpeed * Time.deltaTime );
		}
	}

    private void OnDestroy()
    {
        healthBar = GetComponent<HealthBar>();
        healthBar.DestroyHealthBar();
    }

    GameObject player = null;
	Rigidbody2D body;
    HealthBar healthBar;

	const float moveSpeed = 55.2f;
}
