using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiExplosion : MonoBehaviour {

    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("How fast does the animation play")]
    public float framesPerSecond;
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// Start is called by Unity. This will play our explosion sound and start the sprite animation
    /// </summary>
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimation());
    }

    /// <summary>
    /// This is a coroutine that cycles through the sprites of our explosion animation. It needs to be started using StartCoroutine().
    /// </summary>
    IEnumerator PlayAnimation()
    {
        int currentFrameIndex = 0;
        while (currentFrameIndex < frames.Length)
        {
            spriteRenderer.sprite = frames[currentFrameIndex];
            yield return new WaitForSeconds(1f / framesPerSecond); // this halts the functions execution for x seconds. Can only be used in coroutines.
            currentFrameIndex++;
        }
        Destroy(gameObject);
    }
}
