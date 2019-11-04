using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// other.GetComponent<HealthBar>.Hurt( 1 );
public class HealthBar 
	:
	MonoBehaviour
{
	public void Hurt( int damage )
	{
		health -= damage;
		if( health <= 0 )
		{
			// Play explosion animation.
			Destroy( gameObject );
		}
	}

	[SerializeField] int health = 0;
}
