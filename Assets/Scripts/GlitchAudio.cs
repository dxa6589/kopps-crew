using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchAudio : MonoBehaviour
{
    public AudioSource mainAudio, glitchAudio;
    public List<AudioClip> glitches;
    public int gNum = 0, count = 0, current, amount;
    public float initVol, glitchVol;
    public bool glitching, randomize, muting;
    // Random rnd;

    // Start is called before the first frame update
    void Start()
    {
        initVol = mainAudio.volume;
    }

    // Update is called once per frame
    void Update()
    {
        //if glitching, and current exhausted, restore main volume
        if (glitching && !glitchAudio.isPlaying)
        {
            PlayAudio();
        }
    }

    public void Trigger()
    {
        gNum += 1;
        //current = random no
        if (randomize) current = Random.Range(0, glitches.Count);
        glitchAudio.clip = glitches[current];
        switch(current)
        {
            case 0:
            case 5:
                muting = false;
                amount = 1;
                break;
            case 1:
            case 2:
                muting = true;
                //main vol reduce ==> main.volume = glitchVol;
                amount = Random.Range(2, 4);
                break;
            default:
                muting = true;
                amount = 1;
                break;
        }
        PlayAudio();
    }

    void PlayAudio()
    {
        if (count >= amount)
        {
            count = 0;
            glitching = false;
            if (muting) mainAudio.mute = false;
        }
        else if (count == 0)
        {
            count++;
            glitchAudio.Play();
            glitching = true;
            if (muting) mainAudio.mute = true;
        }
        else
        {
            count++;
            glitchAudio.Play();

        }
    }
}
