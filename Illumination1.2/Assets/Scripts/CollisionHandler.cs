using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header ("External Sources")]
    [SerializeField] AudioClip impact;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem successParticle;

    [Header("Local Sources")]
    [SerializeField] ParticleSystem localImpactFX;

    AudioSource audioSource;

    enum State { Alive, Dying, Transcending };
    State currentState = State.Alive;
    bool collisionsEnabled = true; // for debug code

    [Tooltip("In Seconds")][SerializeField] float levelLoadDelay = 5f;
    private int currentScene;
    private int totalScenes;
    private int nextScene;

    void StartLevelAdvanceSequence() { }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene().buildIndex;  // get the active scene's index, stored as int
        totalScenes = SceneManager.sceneCountInBuildSettings - 1; // # scenes in build settings
    }

    private void Update()
    {
        DebugCollision();
    }

    private void DebugCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsEnabled = !collisionsEnabled; // toggle collision enable/disable
        }
    }

    private void LoadNextLevel()
    {
        currentScene = 0;
        SceneManager.LoadScene(0);
    }

    void StartDeathSequence() {
        print("Made contact with bad surface.");
        currentState = State.Dying;
        audioSource.PlayOneShot(death);
        audioSource.PlayOneShot(impact);

        #region deathParticles
        // Two ways to do it, Instantiate a prefab particles system
        //Instantiate(deathParticle, transform.position, transform.rotation);

        // Or enable a child ParticleSystem gameObject
        localImpactFX.gameObject.SetActive(true);
        #endregion

        Invoke("LoadNextLevel", levelLoadDelay); // string reference
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!collisionsEnabled) { return; } // part of debug logic

        if (currentState == State.Alive)
        {
            switch (collision.gameObject.tag)
            {

                case "projectile":
                    // do nothing
                    
                    break;

                case "gate":
                    // do nothing
                    break;

                case "light":
                    // do nothing

                    break;

                case "finish":
                    // add Finished Level
                    print("You have completed the game!");
                    StartLevelAdvanceSequence();
                    break;

                default:
                    // die
                    SendMessage("ControlsEnabled", false); // string reference, disables controls in MAXRPlayerController
                    StartDeathSequence();
                    break;
            }
        }

    }


}
