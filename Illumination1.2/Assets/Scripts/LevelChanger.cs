using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class LevelChanger : MonoBehaviour
{
    public static LevelChanger instance;
    public Animator animator;
    private int levelToLoad;


    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneLoader.instance.fadeComplete = true;
       
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
}
