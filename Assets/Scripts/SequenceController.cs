using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceController : MonoBehaviour
{
    public Animator singerA, drummerA, guitarA, bassA;
    public AudioSource mainAudio;
    public float elapsedTime;
    public GameObject rightMic, leftMic;
    public bool micSwitched;
    public string opening = "OpeningScene", main = "MainScene", credits = "CreditsScene";
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        leftMic.SetActive(true);
        rightMic.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (!micSwitched && singerA.GetCurrentAnimatorStateInfo(0).IsName("playing"))
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
        drummerA.Play("playing");
        guitarA.Play("playing");
        bassA.Play("playing");
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
