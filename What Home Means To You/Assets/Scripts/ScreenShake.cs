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

    private void Update()
    {
        if (shaking)
            ShakeScreen();
    }

    public void beginShaking(float time)
    {
        shakeTime = time;
        shakeTimer = 0;
        shaking = true;
    }

    void ShakeScreen()
    {
        shakeTimer += Time.deltaTime;

        if (shakeTimer >= shakeTime)
        {
            shaking = false;

            transform.position = defaultPos;

            return;
        }

        float randX = defaultPos.x + Random.Range(0, shakeRadius);
        float randY = defaultPos.y + Random.Range(0, shakeRadius);

        transform.position = new Vector3(randX, randY, defaultPos.z);
    }
}
