using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this script is applied to Roy in the Win scene. It allows for confetti and color changes 
 * on the screen. Confetti spawn adapted from a unity response by robertbu:
 * http://answers.unity3d.com/questions/774085/random-spawn-objects-within-the-screen-width-and-h.html*/

public class WinScene : MonoBehaviour {

    // timer for confetti spawn
    public float minSpawnTime = 0.3f;
    public float maxSpawnTime = 1.0f;
    private float timer = 0.0f;
    private float nextTime;

    // variables for changing Roy's colors
    private SpriteRenderer Roy;
    Color[] colors;
    int counter;

    public GameObject ConfettiPrefab;

    void Start()
    {
        nextTime = Random.Range(minSpawnTime, maxSpawnTime);
        Roy = GetComponent<SpriteRenderer>();

        colors = new Color[4] { Color.white, Color.red, Color.yellow, Color.blue };
        counter = 0;
    }

    /*If the timer is later than the threshold for instantiating confetti, then instantiate
     * and change Roy's color*/
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > nextTime)
        {
            Vector3 pos = new Vector3(Random.Range(-8, 8), Random.Range(-5, 5), 0);

            Instantiate(ConfettiPrefab, pos, Quaternion.identity);

            timer = 0.0f;
            nextTime = Random.Range(minSpawnTime, maxSpawnTime);

            changeRoyColor();
        }
    }

    void changeRoyColor()
    {
        counter++;
        Color newColor = colors[counter % 4];
        Roy.color = newColor;
    }
}

