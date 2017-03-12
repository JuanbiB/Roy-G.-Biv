using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Confetti explosion script modeled after Benno's explosion script from 
// ShootEmUp lab example
public class ConfettiExplosion : MonoBehaviour {

    // The confetti animation frames and properties
    public Sprite[] frames;
    public float framesPerSecond;
    SpriteRenderer spriteRenderer;

    //Play confetti animation
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimation());
    }

    // Cycle through frames of confetti animation
    IEnumerator PlayAnimation()
    {
        int currentFrameIndex = 0;
        while (currentFrameIndex < frames.Length)
        {
            spriteRenderer.sprite = frames[currentFrameIndex];
            yield return new WaitForSeconds(1f / framesPerSecond);
            currentFrameIndex++;
        }
        Destroy(gameObject);
    }
}
