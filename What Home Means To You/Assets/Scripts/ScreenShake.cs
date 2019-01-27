using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Vector3 defaultPos;
    public float shakeRadius;
    float shakeTimer;
    float shakeTime;

    bool shaking;
    private void Start()
    {
        defaultPos = transform.position;
    }

    //private void Update()
    //{
    //    if (shaking)
    //        ShakeScreen();
    //}


}
