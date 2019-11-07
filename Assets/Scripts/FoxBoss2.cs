using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoxBoss2
	:
	MonoBehaviour
{
	enum State
	{
		MissileShotgun,
		Lasers, // Shoots 3 bullets from each in rapid succession
		MissileShotgunBounce,
		Wait,
		MissileBarrage,
		Reposition
	}

	void Start()
	{
		missilePrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Missile" );
		Assert.IsNotNull( missilePrefab );
		player = GameObject.FindGameObjectWithTag( "Player" );
		Assert.IsNotNull( "Player" );

		for( int i = 0; i < 3; ++i )
		{
			shotgunSpawns[i] = transform.Find( "Shotgun" +
				( i + 1 ).ToString() );
		}
	}

	void Update()
	{
		switch( action )
		{
			case State.MissileShotgun:
				if( shotgunRefire.Update( Time.deltaTime ) )
				{
					shotgunRefire.Reset();

					Vector2 diff = player.transform.position -
						transform.position;
					Vector2 start = Deviate( diff,
						-( nShotgunBullets / 2 ) * shotgunSpread );
					Vector2 spawn = shotgunSpawns[Random.Range(
						0,shotgunSpawns.Length )].position;
					for( int i = 0; i < nShotgunBullets; ++i )
					{
						FireBullet( spawn,start,null );
						start = Deviate( start,shotgunSpread );
					}

					if( ++curShotgunBurst >= shotgunSpawns.Length )
					{
						curShotgunBurst = 0;
						action = State.Lasers;
					}
				}
				break;
			case State.Lasers:
				break;
			case State.MissileShotgunBounce:
				break;
			case State.Wait:
				break;
			case State.MissileBarrage:
				break;
			case State.Reposition:
				break;
		}
	}

	void FireBullet( Vector2 loc,Vector2 vel,Transform target )
	{
		// var bullet = Instantiate( bulletPrefab );
		// bullet.transform.position = curShot == 0
		// 	? gun1.position : gun2.position;
		// bullet.GetComponent<Rigidbody2D>()
		// 	.AddForce( diff.normalized *
		// 	bulletSpeed,ForceMode2D.Impulse );
		var missile = Instantiate( missilePrefab );
		missile.transform.position = loc;
		var scr = missile.GetComponent<FoxMissile>();
		scr.SetVel( vel );
		scr.SetTarget( target );
	}

	void OnDestroy()
	{
		LevelHandler.SaveKitty();
	}

	Vector2 Deviate( Vector2 start,float dev )
	{
		dev *= Mathf.Deg2Rad;
		float angle = Mathf.Atan2( start.y,start.x ) + dev;
		Vector2 temp = new Vector2( Mathf.Cos( angle ),
			Mathf.Sin( angle ) );
		return( temp );
	}

	GameObject missilePrefab;
	GameObject player;

	[Header( "Missile Shotgun" )]
	[SerializeField] int nShotgunBullets = 5;
	[SerializeField] float shotgunSpread = 35.0f;
	[SerializeField] Timer shotgunRefire = new Timer( 2.0f );
	Transform[] shotgunSpawns = new Transform[3];
	int curShotgunBurst = 0;

	State action = State.MissileShotgun;
}
