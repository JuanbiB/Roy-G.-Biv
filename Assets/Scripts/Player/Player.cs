using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Player : MonoBehaviour {
	ColorManager CM;
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
        //originalGravity = controller.gravity;
        rb2d = gameObject.GetComponent<Rigidbody2D>();

		// first checkpoint is always at beginning of the level
		checkPoint = transform.position;

        //get color manager 
        CM = GetComponent<ColorManager>();

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
			other.GetComponent<Player> ().Die ();
		} else if (other.CompareTag ("Checkpoint")) {
			SetCheckpoint ();
			SpriteRenderer flagsr = other.gameObject.GetComponent<SpriteRenderer> ();
			flagsr.color = Color.black;
		} else if (other.CompareTag ("SpikeFallTrigger")) {
			other.GetComponent<FallingSpikesTrigger> ().Fall ();
		} else if (other.CompareTag ("NextLevelPortal")) {
			NextLevel ();
		} else if (other.CompareTag ("raindrop")) {
			CM.BlackMode ();

		}
    }



    public void Die()
    {
        // Prevent player from moving 
        inDisplay = true;

        // storing the original gravity value
        //originalGravity = controller.gravity;
        //controller.gravity = 0;
        //rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;

        // Slow velocity as they enter the cloud
        rb2d.velocity = new Vector3(0, -1, 0);

        // Begin death animation
        gameObject.GetComponent<Animator>().SetBool("dead", true);

        // Start coroutine that'll reset the player's location to last checkpoint
        StartCoroutine(BeginRespawn(2f));

    }

    IEnumerator BeginRespawn(float seconds){
		yield return new WaitForSeconds (seconds);

		// return control to the player
		inDisplay = false;

		// reset position to latest checkpoint
		gameObject.transform.position = checkPoint;

		// reset velocity to zero
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;

		// reset dead boolean in the animator
		gameObject.GetComponent<Animator> ().SetBool ("dead", false);

        // reset gravity 
        //controller.gravity = originalGravity;
        //rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            hitSideRay = Physics2D.Linecast(transform.position, other.transform.position, playerMask);
            Vector3 hitSideNormal = hitSideRay.normal;
            hitSideNormal = hitSideRay.transform.TransformDirection(hitSideNormal);

            if (hitSideNormal == hitSideRay.transform.up)
            {
                transform.SetParent(other.transform);
            }
     
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
            gameObject.GetComponent<Animator>().SetBool("grounded", false);
        }
    }

    public void NextLevel()
    {
		gameObject.transform.position = Vector2.zero;
        level++;
        SceneManager.LoadScene("Level " + level);
    }

}
