using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchObject : MonoBehaviour
{
    public enum GlitchType
    {
        colorChange,
        colorFlash,
        twitch,
        twitchFlash,
        destroy,
        smrTest,
        none
    }
    public int gNum = 0;
    public GlitchType gType = GlitchType.none;
    public int gTime = 0;
    public int gDur = 0;
    public bool glitching;
    public List<GlitchType> progression;
    public List<int> glitchDurations;
    public GlitchType final = GlitchType.none;

    // Specific glitch data
    public Material ogMat, glitchMat;
    public int colorTime = 0, colorDur = 0, twitchTime = 0, twitchDur = 0;
    public bool colorBool = false, twitchBool = false;
    public Vector3 ogPos, twitchPos;

    // Start is called before the first frame update
    void Start()
    {
        ogMat = gameObject.GetComponent<MeshRenderer>().material;
        ogPos = transform.position;
        if (final == GlitchType.none  && progression.Count > 0)
        {
            final = progression[progression.Count - 1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        ContinueGlitch();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding!!");
        if (other.gameObject.tag == "GameController")
        {
            StartGlitch();
        }
    }

    void StartGlitch()
    {
        glitching = true;
        gDur = 10;
        
        if (gNum >= progression.Count)
        {
            gType = final;
        }
        else
        {
            gType = progression[gNum];
            gNum += 1;
        }
        switch (gType)
        {
            case GlitchType.colorChange:
                ColorChange();
                break;
            case GlitchType.colorFlash:
                colorDur = 3;
                ColorFlash();
                break;
            case GlitchType.twitch:
                twitchDur = 3;
                Twitch();
                break;
            case GlitchType.twitchFlash:
                twitchDur = 3;
                Twitch();
                break;
            case GlitchType.smrTest:
                ColorChange2();
                break;
            case GlitchType.destroy:
                Destroy();
                break;
        }
    }

    void ContinueGlitch()
    {
        if (gTime >= gDur)
        {
            EndGlitch();
        }
        else if (glitching)
        {
            gTime += 1;
            // call method from gtype
            switch (gType)
            {
                case GlitchType.colorFlash:
                    ColorFlash();
                    break;
                case GlitchType.twitch:
                    Twitch();
                    break;
                case GlitchType.twitchFlash:
                    TwitchFlash();
                    break;
            }
        }
    }

    void EndGlitch()
    {
        glitching = false;
        gTime = 0;
        switch (gType)
        {
            //end colorChange
            case GlitchType.colorChange:
                gameObject.GetComponent<MeshRenderer>().material = ogMat;
                break;
            //end colorFlash
            case GlitchType.colorFlash:
                gameObject.GetComponent<MeshRenderer>().material = ogMat;
                break;
            //end twitch
            case GlitchType.twitch:
                twitchTime = 0;
                transform.position = ogPos;
                break;
            //end twitchFlash
            case GlitchType.twitchFlash:
                twitchTime = 0;
                transform.position = ogPos;
                gameObject.GetComponent<MeshRenderer>().material = ogMat;
                break;
        }
        gType = GlitchType.none;
    }

    void ColorChange()
    {
        gameObject.GetComponent<MeshRenderer>().material = glitchMat;

        //Material newMaterial = new Material(ogMat.shader);
        //newMaterial.CopyPropertiesFromMaterial(ogMat);
        //newMaterial.color = color;
        //gameobject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void ColorChange2()
    {
        gameObject.GetComponent<MeshRenderer>().material = glitchMat;

        //Material newMaterial = new Material(ogMat.shader);
        //newMaterial.CopyPropertiesFromMaterial(ogMat);
        //newMaterial.color = color;
        //gameobject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void ColorFlash()
    {
        gTime += 1;
        colorTime += 1;
        if (colorTime >= colorDur)
        {
            colorBool = !colorBool;
            if (colorBool)
            {
                gameObject.GetComponent<MeshRenderer>().material = ogMat;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = glitchMat;
            }
        }
    }

    void Twitch()
    {
        gTime += 1;
        twitchTime += 1;
        if (twitchTime >= twitchDur)
        {
            twitchBool = !twitchBool;
            if (twitchBool)
            {
                transform.position = ogPos;
            }
            else
            {
                transform.position = twitchPos;
            }
        }
    }

    void TwitchFlash()
    {
        gameObject.GetComponent<MeshRenderer>().material = glitchMat;
        gTime += 1;
        twitchTime += 1;
        if (twitchTime >= twitchDur)
        {
            twitchBool = !twitchBool;
            if (twitchBool)
            {
                transform.position = ogPos;
            }
            else
            {
                transform.position = twitchPos;
            }
        }
    }

    void Destroy()
    {
        //TODO: add destroy time
        Destroy(gameObject);
    }
    // add other position twitch, destroy combos
}
