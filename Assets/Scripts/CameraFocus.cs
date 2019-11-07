using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFocus
	:
	MonoBehaviour
{
	void Start()
	{
		player = GameObject.FindGameObjectWithTag(
			"Player" );
		Assert.IsNotNull( player );
		body = player.GetComponent<Rigidbody2D>();
		Assert.IsNotNull( body );
		cam = GetComponent<Camera>();
		Assert.IsNotNull( cam );

		startSize = cam.orthographicSize;
	}

	void Update()
	{
		var size = cam.orthographicSize;
		size = startSize + body.velocity.magnitude /
			scaleFactor;
		cam.orthographicSize = size;
	}

	GameObject player;
	Rigidbody2D body;
	Camera cam;

	float startSize;
	[SerializeField] float scaleFactor = 4.0f;
}
