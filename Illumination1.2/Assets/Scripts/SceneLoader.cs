using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class SceneLoader : MonoBehaviour
{


    public static SceneLoader instance;
    
    [Tooltip("In seconds")][SerializeField] float loadDelay = 5f;
    public bool fadeComplete = false;


    private void Update()
    {
        if (fadeComplete)
        {
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            fadeComplete = false;
            SceneManager.LoadScene(nextLevel);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1") ||
                OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= Mathf.Epsilon)
            {
                LevelChanger.instance.FadeToLevel(1);
                Invoke("LoadFirstScene", 3f);
            }
        }


    }
    void Awake()
    {
        // check if this is the only instance. If not, destroy this instance.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void LoadFirstScene()
    {
        
        SceneManager.LoadScene(1);
    }
}
