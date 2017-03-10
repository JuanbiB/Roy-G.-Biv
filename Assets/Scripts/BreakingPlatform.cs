using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour {

    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("An Audioclip with the sound that is played when the explosion happens")]
    SpriteRenderer sr;

    public Sprite normal;

    public float lifetime = 2f;

    public float replaceTime = 5f;

    // Use this for initialization
    void Start () {
    }

    public void Break()
    {
        Vector3 startPos= transform.position;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
        StartCoroutine(PlayAnimation());
        StartCoroutine(Respawn(replaceTime, startPos));
    }

    IEnumerator PlayAnimation()
    {
        int currentFrameIndex = 0;
        while (currentFrameIndex < frames.Length)
        {
            sr.sprite = frames[currentFrameIndex];
            yield return new WaitForSeconds(lifetime / frames.Length); // this halts the functions execution for x seconds. Can only be used in coroutines.
            currentFrameIndex++;
        }
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    IEnumerator Respawn(float seconds, Vector3 position)
    {
        yield return new WaitForSeconds(replaceTime);
        GetComponent<BoxCollider2D>().isTrigger = false;
        sr.sprite = normal;
    }
}
