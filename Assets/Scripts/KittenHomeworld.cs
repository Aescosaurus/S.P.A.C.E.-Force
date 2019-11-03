using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenHomeworld
	:
	MonoBehaviour
{
	void Update()
	{
		transform.Rotate( 0.0f,0.0f,rotSpeed * Time.deltaTime );
	}

	[SerializeField] float rotSpeed = 0.0f;
}
