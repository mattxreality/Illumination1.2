using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class MAXRPlayer : MonoBehaviour
{
    [Tooltip("In ms^-1")]
    [SerializeField]
    float xSpeed = 6f;
    [Tooltip("In ms^-1")]
    [SerializeField]
    float ySpeed = 6f;

    [Tooltip("In M")] [SerializeField] float xRange = 3f;
    [Tooltip("In M")] [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionRollFactor = -5f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlYawFactor = 5f;

    float xThrow, yThrow; // two variables declared on the same line

    int currentScene;

    #region Projectile
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 10f;
    private float nextFire;
    #endregion

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "accelerator")
        {
            print("collided with accelerator gate");
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        DebugGame();

        float ovrPrimaryThumbstickHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        //print("Horizontal PrimaryThumb = " + ovrPrimaryThumbstickHorizontal);

        //stuff for later
        //Vector2 ovrDirectController = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        //print("Oculus Vector2 PrimaryThumb = " + ovrDirectController);

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //GetComponent<AudioSource>().Play();
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
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        // current xPosition plus offset
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;
        // restrict movement range 
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        // Moves the player object using input above
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
