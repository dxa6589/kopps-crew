using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceGlitchObject : MonoBehaviour
{
    public Animator anim;
    public enum AudienceTriggerTag
    {
        GameController,
        Player,
        GameControllerORPlayer,
    }
    public AudienceTriggerTag triggerTag;
    public int glitchNo, threshold = 4;
    public bool glitching, killable, colliding;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (glitching && anim.GetCurrentAnimatorStateInfo(1).IsName("default"))
        { 
                StopGlitch();
        }
        if (colliding)
        {
            Debug.Log("Colliding!! With audience bool");
            if (killable && glitchNo >= threshold)
            {
                Die();
            }
            else
            {
                Glitch();
            }
            colliding = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerTag.ToString().Contains(other.gameObject.tag))
        {
            Debug.Log("Colliding!! With audience");
            if (killable && glitchNo >= threshold)
            {
                Die();
            }
            else
            {
                Glitch();
            }
        }
    }

    void Glitch()
    {
        glitching = true;
        anim.SetLayerWeight(1, 1);
        Debug.Log("Glitch #" + glitchNo + " started");
        anim.Play("Glitch "+Random.Range(0, 6), 1);
        glitchNo++;
    }

    void StopGlitch() 
    {
        glitching = false;
        // stop glitch layer
        anim.SetLayerWeight(1, 0);
    }

    void Die()
    {
        anim.SetLayerWeight(2, 1);
        Debug.Log("Dying started");
        anim.Play("Death " + Random.Range(0, 2), 2);
        Destroy(gameObject, anim.GetCurrentAnimatorClipInfo(2)[0].clip.length + Random.Range(0f, 4f));
    }
}
