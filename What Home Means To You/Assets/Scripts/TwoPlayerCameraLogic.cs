using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerCameraLogic : MonoBehaviour
{

    public float zoomDistance = 1.5f;
    public float followSlerpSpeed = .8f;
    public bool runTwoPlayerCameraLogic = true;
    public Transform player1Transform;
    public Transform player2Transform;

    private Camera thisCam;
    private Vector3 originalPosOfCamera; //made to keep Y and Z of camera from inspector.

    void Start()
    {
        originalPosOfCamera = transform.position;
        thisCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (runTwoPlayerCameraLogic)
        {
            SmoothCameraFollow(thisCam, player1Transform, player2Transform);
        }
    }
    // Follow Two Transforms with a Fixed-Orientation Camera
    // Function thanks to TreyH at https://answers.unity.com/questions/1142089/moving-camera-with-2-players.html
    public void SmoothCameraFollow(Camera camera, Transform player1Transform, Transform player2Transform)
    {
        Vector3 middle = (player1Transform.position + player2Transform.position) / 2f;
        float distance = (player1Transform.position - player2Transform.position).magnitude;
        Vector3 targetTransform = middle - camera.transform.forward * distance * zoomDistance;

    //   // Adjust ortho size if we're using one of those
    //   if (camera.orthographic)
    //   {
    //       // The camera's forward vector is irrelevant, only this size will matter
    //       camera.orthographicSize = distance;
    //   }

        camera.transform.position = Vector3.Slerp(camera.transform.position, targetTransform, followSlerpSpeed);
        camera.transform.position = new Vector3(camera.transform.position.x, originalPosOfCamera.y, originalPosOfCamera.z);

        if (camera.transform.position.x < -14.25f)
        {
            camera.transform.position = new Vector3(-14.25f, camera.transform.position.y, camera.transform.position.z);
        }
        else if (camera.transform.position.x > 14.25f)
            {
                camera.transform.position = new Vector3(14.25f, camera.transform.position.y, camera.transform.position.z);
            }

        // Snap when close enough to prevent annoying slerp behavior
        if ((targetTransform - camera.transform.position).magnitude <= 0.05f)
            camera.transform.position = targetTransform;
    }
}
