using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // MODIFIED Singleton pattern instance of the GameManager, gotten from:
    // https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
    public static GameManager instance = null;

    // Current level
    public int level;

    // Boolean representing if the game is paused or not
    public bool paused;

    // UI component representing player state, and array holding all options
    public Image stateUI;
    public Sprite[] stateOptions;

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
    }


}
