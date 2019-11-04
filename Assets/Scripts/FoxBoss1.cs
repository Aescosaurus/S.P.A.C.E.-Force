using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoxBoss1
	:
	MonoBehaviour
{
	enum State
	{
		Wander,
		GunAttack,
		Chase,
		MissileAttack
	}

	void Start()
	{
		gun1 = transform.Find( "Gun1" );
		Assert.IsNotNull( gun1 );
		gun2 = transform.Find( "Gun2" );
		Assert.IsNotNull( gun2 );
		missile1 = transform.Find( "Missile1" );
		Assert.IsNotNull( missile1 );
		missile2 = transform.Find( "Missile2" );
		Assert.IsNotNull( missile2 );
		body = GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );
		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Bullet" );
		player = GameObject.FindGameObjectWithTag(
			"Player" );
		Assert.IsNotNull( player );

		target = ( Vector2 )transform.position +
			new Vector2( Random.Range( -3,3 ),
			Random.Range( -3,3 ) );
	}

	void Update()
	{
		switch( action )
		{
			case State.Wander:
				if( wanderDuration.Update( Time.deltaTime ) )
				{
					wanderDuration.Reset();
					action = State.GunAttack;
				}
				else
				{
					Vector2 diff = target - ( Vector2 )transform.position;
					body.AddForce( diff * wanderSpeed *
						Time.deltaTime );
				}
				break;
			case State.GunAttack:
				if( gunRefire.Update( Time.deltaTime ) )
				{
					gunRefire.Reset();

					var bullet = Instantiate( bulletPrefab );
					bullet.transform.position = curShot == 0
						? gun1.position : gun2.position;
					bullet.GetComponent<Rigidbody2D>()
						.AddForce( ( player.transform.position -
						bullet.transform.position ) *
						bulletSpeed,ForceMode2D.Impulse );

					if( ++curShot > 1 )
					{
						curShot = 0;
						action = State.Chase;
					}
				}
				break;
			case State.Chase:
				if( chaseDuration.Update( Time.deltaTime ) )
				{
					action = State.MissileAttack;
				}
				else
				{
					Vector2 diff = player.transform.position -
						transform.position;
					body.AddForce( diff * wanderSpeed *
						Time.deltaTime );
				}
				break;
			case State.MissileAttack:
				if( missileRefire.Update( Time.deltaTime ) )
				{
					missileRefire.Reset();

					var missile = Instantiate( bulletPrefab );
					missile.transform.position = curMissile % 2 == 0
						? missile1.position : missile2.position;
					Vector2 diff = player.transform.position -
						missile.transform.position;
					diff = Quaternion.Euler( 0.0f,0.0f,
						Random.Range( -missileDeviation / 2.0f,
						missileDeviation / 2.0f ) ) * diff;
					missile.GetComponent<Rigidbody2D>()
						.AddForce( diff * missileSpeed,
						ForceMode2D.Impulse );

					if( ++curMissile > missileVolleySize )
					{
						curMissile = 0;
						action = State.Wander;
					}
				}
				break;
		}
	}

	Transform gun1;
	Transform gun2;
	Transform missile1;
	Transform missile2;
	Rigidbody2D body;
	GameObject bulletPrefab;
	GameObject player;

	State action = State.Wander;

	Vector2 target = Vector2.zero;
	[SerializeField] float wanderSpeed = 5.0f;
	[SerializeField] Timer wanderDuration = new Timer( 3.0f );

	int curShot = 0;
	Timer gunRefire = new Timer( 0.2f );
	[SerializeField] float bulletSpeed = 10.0f;

	[SerializeField] Timer chaseDuration = new Timer( 1.5f );

	int curMissile = 0;
	[SerializeField] int missileVolleySize = 6;
	[SerializeField] float missileDeviation = 20.0f;
	[SerializeField] Timer missileRefire = new Timer( 0.07f );
	[SerializeField] float missileSpeed = 5.5f;
}
