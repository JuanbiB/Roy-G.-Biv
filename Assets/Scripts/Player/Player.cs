using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Instance of the player
    public static Player instance;

    // Different modes for the player
    public enum Mode { White, Red, Yellow, Blue }

    // Current mode of the player
    public Mode playerMode;

    // Controller of the player
    public PlatformerController controller;

    // Booleans representing if the player has unlocked colors
    public bool redUnlocked;
    public bool yellowUnlocked;
    public bool blueUnlocked;

    void Awake()
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

    // Use this for initialization
    void Start ()
    {
        // Register the controller
        controller = gameObject.GetComponent<PlatformerController>();

        // Start white, without having any powers unlocked
        instance.playerMode = Mode.White;
        redUnlocked = false;
        yellowUnlocked = false;
        blueUnlocked = false;
    }

    
    void Respawn()
    {

    }
    
}
