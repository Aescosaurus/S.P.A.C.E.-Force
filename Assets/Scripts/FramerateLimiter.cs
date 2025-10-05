using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateLimiter
    :
    MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
