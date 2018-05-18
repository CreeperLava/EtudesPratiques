using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate: MonoBehaviour
{
    public bool slideChangeWithGestures = true;
    public bool slideChangeWithKeys = true;
    public float spinSpeed = 5;

    public bool autoChangeAfterDelay = false;
    public float slideChangeAfterDelay = 10;

    // if the presentation cube is behind the user (true) or in front of the user (false)
    public bool isBehindUser = false;

    private int maxSides = 4;
    private int side = 0;
    private bool isSpinning = false;
    private float slideWaitUntil;
    private Quaternion targetRotation;

    private GestureListener gestureListener;

    void Start()
    {
        // hide mouse cursor
        Cursor.visible = false;

        // delay the first slide
        slideWaitUntil = Time.realtimeSinceStartup + slideChangeAfterDelay;

        targetRotation = transform.rotation;
        isSpinning = false;

        side = 0;
        // get the gestures listener
        gestureListener = Camera.main.GetComponent<GestureListener>();
    }

    void Update()
    {
        // dont run Update() if there is no user
        KinectManager kinectManager = KinectManager.Instance;
        if (autoChangeAfterDelay && (!kinectManager || !kinectManager.IsInitialized() || !kinectManager.IsUserDetected()))
            return;

        if (!isSpinning)
        {
            if (slideChangeWithKeys)
            {
                if (Input.GetKeyDown(KeyCode.PageDown))
                    RotateToNext();
                else if (Input.GetKeyDown(KeyCode.PageUp))
                    RotateToPrevious();
            }

            if (slideChangeWithGestures && gestureListener)
            {
                if (gestureListener.IsSwipeLeft())
                    RotateToNext();
                else if (gestureListener.IsSwipeRight())
                    RotateToPrevious();
            }

            // check for automatic slide-change after a given delay time
            if (autoChangeAfterDelay && Time.realtimeSinceStartup >= slideWaitUntil)
                RotateToNext();
        }
        else
        {
            // spin the presentation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, spinSpeed * Time.deltaTime);

            // check if transform reaches the target rotation. If yes - stop spinning
            float deltaTargetX = Mathf.Abs(targetRotation.eulerAngles.x - transform.rotation.eulerAngles.x);
            float deltaTargetY = Mathf.Abs(targetRotation.eulerAngles.y - transform.rotation.eulerAngles.y);

            if (deltaTargetX < 1f && deltaTargetY < 1f)
            {
                // delay the slide
                slideWaitUntil = Time.realtimeSinceStartup + slideChangeAfterDelay;
                isSpinning = false;
            }
        }
    }


    private void RotateToNext()
    {
        if (!isBehindUser)
            side = (side + 1) % maxSides;
        else
        {
            if (side <= 0)
                side = maxSides - 1;
            else
                side -= 1;
        }
        
        // rotate the presentation
        print(KinectGestures.pos);
        float yawRotation = !isBehindUser ? 360f / (maxSides * 4) : -360f / (maxSides * 4);
        Vector3 rotateDegrees = new Vector3(0f, yawRotation, 0f);
        targetRotation *= Quaternion.Euler(rotateDegrees);
        isSpinning = true;
    }


    private void RotateToPrevious()
    {
        // set the previous texture slide

        if (!isBehindUser)
        {
            if (side <= 0)
                side = maxSides - 1;
            else
                side -= 1;
        }
        else
            side = (side + 1) % maxSides;
       
        // rotate the presentation
        float yawRotation = !isBehindUser ? -360f / maxSides : 360f / maxSides;
        Vector3 rotateDegrees = new Vector3(0f, yawRotation, 0f);
        targetRotation *= Quaternion.Euler(rotateDegrees);
        isSpinning = true;
    }
}