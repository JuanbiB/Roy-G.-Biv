using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour {

    public float minSpawnTime = 0.3f;
    public float maxSpawnTime = 1.0f;

    private float timer = 0.0f;
    private float nextTime;

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

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > nextTime)
        {
            Vector3 pos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);

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

