using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class MAXRPlayerController : MonoBehaviour
{
    [Header("General")] // provides a GUI section header
    [Tooltip("In ms^-1")] [SerializeField] float xControlSpeed = 6f;
    [Tooltip("In ms^-1")] [SerializeField] float yControlSpeed = 6f;
    [Tooltip("In M")] [SerializeField] float xControlRange = 3f;
    [Tooltip("In M")] [SerializeField] float yControlRange = 3f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionRollFactor = -5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlYawFactor = 5f;

    private float xThrow, yThrow; // two variables declared on the same line

    private int currentScene;

    public bool controlsEnabled = true;
   
    public void ControlsEnabled(bool b) // called by string reference
    {
        controlsEnabled = b;
        print("controlsEnabled set to: " + controlsEnabled);
    }

    // Update is called once per frame
    void Update()
    {

        if(!controlsEnabled) { return; }
        else
        {
            ProcessTranslation();
            ProcessRotation();
        }
        
        if (Debug.isDebugBuild) { DebugGame(); } // debug controls for build settings

        //stuff for later
        //Vector2 ovrDirectController = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        //print("Oculus Vector2 PrimaryThumb = " + ovrDirectController);
    }


    void LoadNextLevel()
    {
        currentScene = 0;
        SceneManager.LoadScene(0);
    }

    private void ProcessRotation()
    {

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        //float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float pitch = pitchDueToControlThrow;

        float rollDueToPosition = transform.localPosition.x * positionRollFactor;
        float rollDueToControlThrow = xThrow * controlRollFactor;
        //float roll = rollDueToPosition + rollDueToControlThrow;
        float roll = rollDueToControlThrow;

        float yaw = transform.localPosition.x * xThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

        // todo make return rotation smooth and slow. Current return is too fast.
        // the if statement doesn't work, but has components that might jog my memory.
        // The axis Sensitivity and Gravity register numbers all the time, which is why
        // the below if doesn't work.
        if (CrossPlatformInputManager.GetAxis("Horizontal") >= Mathf.Epsilon |
            CrossPlatformInputManager.GetAxis("Horizontal") <= Mathf.Epsilon) //if input
        {
            //move
        }
        else
        {
            //run function to return to a smooth Quaternion.Euler(0,0,0)
        }

    }

    private void ProcessTranslation()
    {
        // movement input
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        // how much to move in this frame
        float xOffset = xThrow * xControlSpeed * Time.deltaTime;
        float yOffset = yThrow * yControlSpeed * Time.deltaTime;
        // current xPosition plus offset
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;
        // restrict movement range 
        float clampedXPos = Mathf.Clamp(rawXPos, -xControlRange, xControlRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yControlRange, yControlRange);
        // Moves the player object using input above
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void DebugGame()
    {
        if (Input.GetButton("Fire2"))
        {
            // reload first level

            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            // restart current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
