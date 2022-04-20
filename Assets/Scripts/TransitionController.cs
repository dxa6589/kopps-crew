using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    List<Animator> controllers;
    public Animator singer, drummer, guitar, bass;
    public float elapsedTime, playTime = 12.0f, totalRunTime = 10;
    public GameObject rightMic, leftMic, endingCover, venue;
    public bool micSwitched;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        controllers = new List<Animator>();
        controllers.Add(singer);
        controllers.Add(drummer);
        controllers.Add(guitar);
        controllers.Add(bass);
        leftMic.SetActive(true);
        rightMic.SetActive(false);
        endingCover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        foreach (Animator anim in controllers)
        {
            anim.SetFloat("elapsedTime", elapsedTime);
            if (elapsedTime >= playTime)
            {
                anim.SetTrigger("clap");
            }
        }
        if (!micSwitched && singer.GetCurrentAnimatorStateInfo(0).IsName("singing"))
        {
            SwitchMic();
        }
        if (elapsedTime >= totalRunTime)
        {
            endingCover.SetActive(true);
            venue.SetActive(false);
        }
    }

    void SwitchMic()
    {
        rightMic.SetActive(true);
        leftMic.SetActive(false);
        micSwitched = true;
    }
}
