using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Player : MonoBehaviour {
	
    // Instance of the player
    public static Player instance;

    Rigidbody2D rb2d;

    public bool inDisplay;

    RaycastHit2D hitSideRay;
    public LayerMask playerMask;

    // Keeps track of player's latest checkpoint
    Vector2 checkPoint;

	// Stores the original value of gravity used in respawn
	float originalGravity;

    // Current level being played;
    int level;

    // Breaking Platform
    public GameObject ConfettiPrefab;

    public Sprite[] backgrounds;

    public AudioClip DeathSound;
    public AudioClip CheckpointSound;
    public AudioClip crumbleSound;

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
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        inDisplay = false;

		// first checkpoint is always at beginning of the level
		checkPoint = transform.position;

        // Get the level number
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            level = 0;
        }
        else
        {
            string levelName =Regex.Replace(SceneManager.GetActiveScene().name, "[A-Za-z ]", "");
            level = int.Parse(levelName);
        }

    }

    public void SetCheckpoint()
    {
        checkPoint = transform.position;
    }
    
    public void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("obstacle")) {
			ObstacleDie ();
		} else if (other.CompareTag ("Checkpoint")) {
			SetCheckpoint ();
            SpriteRenderer flagsr = other.gameObject.GetComponent<SpriteRenderer>();
            if(flagsr.color != Color.black) {
                AudioSource.PlayClipAtPoint(CheckpointSound, transform.position);
                flagsr.color = Color.black;
            }
		} else if (other.CompareTag ("SpikeFallTrigger")) {
			other.GetComponent<FallingSpikesTrigger> ().Fall ();
		} else if (other.CompareTag ("NextLevelPortal")) {
			NextLevel ();
		}
    }

    public void ObstacleDie()
    {
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);

        // Prevent player from moving 
        inDisplay = true;

        rb2d.velocity = Vector3.zero;

        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        Instantiate(ConfettiPrefab, transform.position, Quaternion.identity);

        // Start coroutine that'll reset the player's location to last checkpoint
        StartCoroutine(BeginRespawn(2f));
    }

    public void CloudDie()
    {
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);

        // Prevent player from moving 
        inDisplay = true;

        // Slow velocity as they enter the cloud
        rb2d.velocity = new Vector3(0, -1, 0);

        // Begin death animation
        gameObject.GetComponent<Animator>().SetBool("dead", true);

        // Start coroutine that'll reset the player's location to last checkpoint
        StartCoroutine(BeginRespawn(2f));

    }

    IEnumerator BeginRespawn(float seconds){
		yield return new WaitForSeconds (seconds);
        
        if (gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

		// return control to the player
		inDisplay = false;

		// reset position to latest checkpoint
		gameObject.transform.position = checkPoint;

		// reset velocity to zero
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;

		// reset dead boolean in the animator
		gameObject.GetComponent<Animator> ().SetBool ("dead", false);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform"|| other.gameObject.tag == "BreakingPlatform")
        {
            hitSideRay = Physics2D.Linecast(transform.position, other.transform.position, playerMask);
            Vector3 hitSideNormal = hitSideRay.normal;
            hitSideNormal = hitSideRay.transform.TransformDirection(hitSideNormal);

            if (hitSideNormal == hitSideRay.transform.up)
            {
                if (other.gameObject.tag == "MovingPlatform")
                {
                    transform.SetParent(other.transform);
                }
                else if(other.gameObject.tag == "BreakingPlatform")
                {
                    AudioSource.PlayClipAtPoint(crumbleSound, transform.position);
                    other.gameObject.GetComponent<BreakingPlatform>().Break();
                }
            }     
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
            gameObject.GetComponent<Animator>().SetBool("grounded", false);
            DontDestroyOnLoad(gameObject);
        }
    }

    public void NextLevel()
    {
        AudioSource.PlayClipAtPoint(CheckpointSound, transform.position);
        gameObject.transform.position = Vector2.zero;
        level++;

        if (level == 5)
        {
            SceneManager.LoadScene("Win");
        }
        else
        {
            SceneManager.LoadScene("Level " + level);

            // Changes the background when the level changes
            Transform background = transform.FindChild("Main Camera").FindChild("background");
            background.GetComponent<SpriteRenderer>().sprite = backgrounds[level - 1];
            SetCheckpoint();
        }
    }
}
