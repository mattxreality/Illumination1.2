using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class MAXRPlayerController : MonoBehaviour
{
    [Header("General")] // provides a GUI section header
    public bool isControlEnabled = true;
    [Tooltip("In ms^-1")] [SerializeField] float xControlSpeed = 12f;
    [Tooltip("In ms^-1")] [SerializeField] float yControlSpeed = 12f;
    [Tooltip("In M")] [SerializeField] float xControlRange = 26f;
    [Tooltip("In M")] [SerializeField] float yControlRange = 16f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -1.43f;
    [SerializeField] float positionRollFactor = -5f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlYawFactor = 10f;
    
    [Header("Rotation Return")]
    [SerializeField] float rotateSpeed = 0.1f;
    bool restoreRotation = false;
    Quaternion originalRotation;

    private float xThrow, yThrow; // two variables declared on the same line
    private int currentScene;
    private bool m_isAxisInUse = false;

    public void ControlsEnabled(bool b) // called by string reference
    {
        isControlEnabled = b;
        print("controlsEnabled set to: " + isControlEnabled);
    }

    private void Start()
    {
        // set starting rotation
        originalRotation = transform.rotation;
        print("original rotation " + originalRotation);
    }
    void Update()
    {
        CheckAxisInUse();

        if (!isControlEnabled) { return; }
        else
        {
            ProcessTranslation();
            ProcessRotation();
        }

        if (Debug.isDebugBuild) { DebugGame(); } // debug controls for build settings

        //stuff for later
        //Vector2 ovrDirectController = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        //print("Oculus Vector2 PrimaryThumb = " + ovrDirectController);

        // RestoreRotation(); // not used yet
    }

    private void CheckAxisInUse()
    {
        // checks if position controls are in use
        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") != 0)
        {
            if (m_isAxisInUse == false)
            {
                // Call your event function here.
                m_isAxisInUse = true;
            }
        }
        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") == 0)
        {
            m_isAxisInUse = false;
        }
        //print("m_isAxisInUse = " + m_isAxisInUse);
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
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
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

    private void RestoreRotation()
    {
        // todo make return rotation smooth and slow. !!! Possibly I should use throw 'sensitivity' and 'gravity'
        // for gradual return...? Current return is too fast.

        // Note: The axis Sensitivity and Gravity register numbers all the time.
        if (!m_isAxisInUse && !restoreRotation)
        {
            restoreRotation = true;
        }

        if (restoreRotation)
        {
            // this does strange things to the current playerObject, probably due to waypoint follower
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.time * rotateSpeed);
            if (transform.rotation == originalRotation)
            {
                restoreRotation = false;
            }
        }
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
