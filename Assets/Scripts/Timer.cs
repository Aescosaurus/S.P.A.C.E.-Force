using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
	public Timer( float time )
	{
		curTime = 0.0f;
		maxTime = time;
	}

	public bool Update( float dt )
	{
		if( curTime <= maxTime ) curTime += dt;

		return( IsDone() );
	}

	public void Reset()
	{
		curTime = 0.0f;
	}

	public bool IsDone()
	{
		return( curTime >= maxTime );
	}

	float curTime;
	float maxTime;
}
