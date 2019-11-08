using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoxBoss3
	:
	FoxBossBase
{
	enum State
	{
		LaserFrenzy,
		MissileBurst,
		Move,
		ShotgunFrenzy,
		BulletStorm
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag( "Player" );
		Assert.IsNotNull( player );
		missilePrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Missile" );
		Assert.IsNotNull( missilePrefab );
		bulletPrefab = Resources.Load<GameObject>(
			"Prefabs/Fox Bullet" );
		Assert.IsNotNull( bulletPrefab );
		audSrc = GetComponent<AudioSource>();
		Assert.IsNotNull( audSrc );

		for( int i = 0; i < guns.Length; ++i )
		{
			guns[i] = transform.Find( "Gun" + ( i + 1 ) );
			Assert.IsNotNull( guns[i] );
		}
		for( int i = 0; i < arms.Length; ++i )
		{
			arms[i] = transform.Find( "Arm" + ( i + 1 ) );
			Assert.IsNotNull( arms[i] );
		}
		for( int i = 0; i < 4; ++i )
		{
			shootSounds.Add( Resources.Load<AudioClip>(
				"Sounds/Boss Shoot 0" + ( i + 1 ) ) );
			Assert.IsNotNull( shootSounds[i] );
		}
	}

	void Update()
	{
		switch( action )
		{
			case State.LaserFrenzy:
				if( laserReload.Update( Time.deltaTime ) )
				{
					laserTarget = player.transform.position;
					if( randLaserArm == null )
					{
						randLaserArm = arms[Random.Range( 0,
							arms.Length )];
					}

					if( laserRefire.Update( Time.deltaTime ) )
					{
						laserRefire.Reset();

						FireBullet( randLaserArm.position,
							( laserTarget - ( Vector2 )randLaserArm.position )
							.normalized * laserMoveSpeed );

						if( ++curLaser >= laserVolleySize )
						{
							curLaser = 0;
							++curLaserVolley;
							laserReload.Reset();
							randLaserArm = null;
						}
					}

					if( curLaserVolley >= nLaserVolleys )
					{
						curLaserVolley = 0;
						action = State.MissileBurst;
					}
				}
				break;
			case State.MissileBurst:
				break;
			case State.Move:
				break;
			case State.ShotgunFrenzy:
				break;
			case State.BulletStorm:
				break;
		}
	}

	void FireMissile( Vector2 loc,Vector2 vel,Transform target )
	{
		var missile = Instantiate( missilePrefab );
		missile.transform.position = loc;
		var scr = missile.GetComponent<FoxMissile>();
		scr.SetVel( vel );
		scr.SetTarget( target );

		audSrc.PlayOneShot( shootSounds[Random
			.Range( 0,shootSounds.Count )] );
	}

	void FireBullet( Vector2 loc,Vector2 vel )
	{
		var bullet = Instantiate( bulletPrefab );
		bullet.transform.position = loc;
		bullet.GetComponent<Rigidbody2D>()
			.AddForce( vel,ForceMode2D.Impulse );

		audSrc.PlayOneShot( shootSounds[Random
			.Range( 0,shootSounds.Count )] );
	}

	GameObject player;
	GameObject missilePrefab;
	GameObject bulletPrefab;
	AudioSource audSrc;
	Transform[] guns = new Transform[4];
	Transform[] arms = new Transform[4];
	List<AudioClip> shootSounds = new List<AudioClip>();

	State action = State.LaserFrenzy;

	[SerializeField] Timer laserReload = new Timer( 1.0f );
	[SerializeField] Timer laserRefire = new Timer( 0.1f );
	[SerializeField] float laserMoveSpeed = 1.2f;
	[SerializeField] int laserVolleySize = 8;
	[SerializeField] int nLaserVolleys = 10;
	Vector2 laserTarget;
	Transform randLaserArm = null;
	int curLaserVolley = 0;
	int curLaser = 0;
}
