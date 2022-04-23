using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceController : MonoBehaviour
{
    List<Animator> controllers;
    public Animator singer, drummer, guitar, bass;
    public AudioSource mainAudio;
    public float elapsedTime;
    public GameObject rightMic, leftMic, endingCover, venue;
    public bool micSwitched;
    public string opening = "OpeningScene", main = "MainScene", credits = "CreditsScene";
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
        if (!micSwitched && singer.GetCurrentAnimatorStateInfo(0).IsName("playing"))
        {
            StartPlaying();
        }
        if (!mainAudio.isPlaying)
        {
            EndPerformance(3);
        }
    }

    void StartPlaying()
    {
        SwitchMic();
        drummer.Play("playing");
        guitar.Play("playing");
        bass.Play("playing");
    }

    void SwitchMic()
    {
        rightMic.SetActive(true);
        leftMic.SetActive(false);
        micSwitched = true;
    }

    void EndPerformance(float delay)
    {
        StartCoroutine(ChangeToScene(credits, delay));
    }

    public IEnumerator ChangeToScene(string sceneToChangeTo, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToChangeTo);
    }

}
