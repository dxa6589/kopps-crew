using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSequenceManager : MonoBehaviour
{
    public string opening = "OpeningScene";
    public string main = "MainScene";
    public string credits = "CreditsScene";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Acknowledge(string action)
    {
        Debug.Log("Acknowledging " + action);
    }

    void OnClick()
    {
        //SwitchScene();
        Acknowledge("click");
    }

    public void SwitchScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == opening)
        {
            //SceneManager.LoadScene(main);
            Debug.Log("Interaction happened! Loading " + main);
        }
        else if (currentScene == main)
        {
            //SceneManager.LoadScene(credits);
            Debug.Log("Interaction happened! Loading " + credits);
        }
        else
        {
            Application.Quit();
        }
    }

    void OnMove()
    {
        Debug.Log("Moving!");
    }
}
