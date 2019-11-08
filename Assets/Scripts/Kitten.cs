﻿using System.Collections;
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

		healthBar = GetComponent<HealthBar>();
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "Player" )
		{
			player = coll.gameObject;
		}
		else if( coll.gameObject.tag == "SpaceFox" )
		{
			healthBar.Hurt( 1 );
			var diff = coll.transform.position -
				transform.position;

			body.AddForce( -diff.normalized *
				knockBackForce,ForceMode2D.Impulse );
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
        // healthBar = GetComponent<HealthBar>();
        healthBar.DestroyHealthBar();
    }

    GameObject player = null;
	Rigidbody2D body;
    HealthBar healthBar;

	[SerializeField] float knockBackForce = 10.0f;

	const float moveSpeed = 55.2f;
}
