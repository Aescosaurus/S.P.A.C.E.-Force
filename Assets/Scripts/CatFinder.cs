using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFinder
	:
	MonoBehaviour
{
	void Start()
	{
		sprRend = GetComponentInChildren<SpriteRenderer>();
		ToggleVis( false );
	}

	void LateUpdate()
	{
		if( targetCat != null )
		{
			var diff = targetCat.transform.position - transform.position;
			var ang = Mathf.Atan2( diff.y,diff.x );
			transform.rotation = Quaternion.Euler( 0.0f,0.0f,ang * Mathf.Rad2Deg - 90.0f );
		}
	}
	public void SetTargetCat( GameObject cat )
	{
		targetCat = cat;
	}

	public void ToggleVis( bool visible )
	{
		sprRend.enabled = visible;
	}

	GameObject targetCat = null;
	SpriteRenderer sprRend;
}
