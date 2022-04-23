using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGlitchObject : MonoBehaviour
{
    public GlitchAudio gAudio;
    public enum AudioTriggerTag
    {
        GameController,
        Player,
        GameControllerORPlayer,
    }
    public AudioTriggerTag triggerTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding!!");
        if (triggerTag.ToString().Contains(other.gameObject.tag))
        {
            gAudio.Trigger();
        }
    }
}
