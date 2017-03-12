using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour {

    // breaking platform animation frames sprite renderer
    public Sprite[] frames;
    SpriteRenderer sr;

    public Sprite normal;
    public float lifetime = 2f;
    public float replaceTime = 4f;

    // Destroy's the platform but saves properties so that it can be replaced
    public void Break()
    {
        Vector3 startPos= transform.position;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
        StartCoroutine(PlayAnimation());
        StartCoroutine(Respawn(replaceTime, startPos));
    }

    //Cycles through frames to play animation
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

    // Respawns the platform
    IEnumerator Respawn(float seconds, Vector3 position)
    {
        yield return new WaitForSeconds(replaceTime);
        GetComponent<BoxCollider2D>().isTrigger = false;
        sr.sprite = normal;
    }
}
