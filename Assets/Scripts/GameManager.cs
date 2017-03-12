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

    // Paused state of game
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

    public void Restart()
    {
        Destroy(instance);
        level = 1;
        paused = false;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("Level " + level);
    }


	public void OnClick(){
		SceneManager.LoadScene ("Level 1");
	}

    public void Pause()
    {
        // Show pause menu
        paused = true;
        pauseMenu.SetActive(true);

        // Freeze the game
        Time.timeScale = 0;

        // Hide ingame UI
        stateUI.gameObject.SetActive(false);
        pauseButton.SetActive(false);

    }

    public void Unpause()
    {
        // Hide pause menu
        paused = false;
        pauseMenu.SetActive(false);

        // Unfreeze the game
        Time.timeScale = 1;

        // Show ingame UI
        stateUI.gameObject.SetActive(true);
        pauseButton.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
