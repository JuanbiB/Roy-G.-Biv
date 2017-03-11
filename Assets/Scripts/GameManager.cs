using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // MODIFIED Singleton pattern instance of the GameManager, gotten from:
    // https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
    public static GameManager instance = null;

    // Current level
    public int level;

    public bool paused;

    // UI component representing player state, and array holding all options
    public Image stateUI;
    public Sprite[] stateOptions;

    public GameObject pauseMenu;
    public GameObject pauseButton;

    void Awake ()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }

        // If instance already exists and it's not this:
        else if (instance != this)
        {
            // Then destroy this, enforcing the Singleton pattern.
            Destroy(gameObject);
        }

        // Sets this to not be destroyed when loading a new scene.
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Level begins at the main menu, unpaused
        level = 0;
        paused = false;
        pauseMenu.SetActive(false);
    }


	public void OnClick(){
		SceneManager.LoadScene ("Level 1");
	}

    public void Pause()
    {
        paused = true;
        pauseMenu.SetActive(true);

        //Player.instance.anim.enabled = false;
        Time.timeScale = 0;

        stateUI.gameObject.SetActive(false);
        pauseButton.SetActive(false);

    }

    public void Unpause()
    {
        paused = false;
        pauseMenu.SetActive(false);

        //Player.instance.anim.enabled = true;
        Time.timeScale = 1;

        stateUI.gameObject.SetActive(true);
        pauseButton.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
