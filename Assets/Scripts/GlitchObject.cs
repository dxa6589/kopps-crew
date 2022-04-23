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
        none
    }
    public int gNum = 0;
    public GlitchType gType = GlitchType.none;
    public float gTime = 0, gDur = 0;
    public bool glitching;
    public List<GlitchType> progression;
    public List<int> glitchDurations;
    public GlitchType final = GlitchType.none;

    // Specific glitch data
    public Material ogMat, glitchMat;
    public float colorTime = 0, colorDur = 0, twitchTime = 0, twitchDur = 0;
    public bool colorBool = false, twitchBool = false;
    public Vector3 ogPos, twitchPos;

    // Start is called before the first frame update
    void Start()
    {
        ogMat = gameObject.GetComponent<Renderer>().material;
        ogPos = transform.position;
        if (final == GlitchType.none  && progression.Count > 0)
        {
            final = progression[progression.Count - 1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (glitching)
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
                gDur = 2;
                ColorChange();
                break;
            case GlitchType.colorFlash:
                gDur = 2;
                colorDur = 0.5f;
                ColorFlash();
                break;
            case GlitchType.twitch:
                gDur = 1;
                twitchDur = 0.5f;
                Twitch();
                break;
            case GlitchType.twitchFlash:
                gDur = 1;
                twitchDur = 0.5f;
                Twitch();
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
            //gTime += 1;
            gTime += Time.deltaTime;
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
                gameObject.GetComponent<Renderer>().material = ogMat;
                break;
            //end colorFlash
            case GlitchType.colorFlash:
                gameObject.GetComponent<Renderer>().material = ogMat;
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
                gameObject.GetComponent<Renderer>().material = ogMat;
                break;
        break;
        }
        gType = GlitchType.none;
    }

    void ColorChange()
    {
        gameObject.GetComponent<Renderer>().material = glitchMat;

        //Material newMaterial = new Material(ogMat.shader);
        //newMaterial.CopyPropertiesFromMaterial(ogMat);
        //newMaterial.color = color;
        //gameobject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void ColorFlash()
    {
        colorTime += Time.deltaTime;
        if (colorTime >= colorDur)
        {
            colorBool = !colorBool;
            if (colorBool)
            {
                gameObject.GetComponent<Renderer>().material = ogMat;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = glitchMat;
            }
        }
    }

    void Twitch()
    {
        twitchTime += Time.deltaTime;
        twitchPos = new Vector3(ogPos.x+Random.Range(-0.5f, 0.5f), ogPos.y+Random.Range(-0.5f, 0.5f), ogPos.z+Random.Range(-0.5f, 0.5f));
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
        gameObject.GetComponent<Renderer>().material = glitchMat;
        twitchTime += Time.deltaTime;
        twitchPos = new Vector3(ogPos.x + Random.Range(-0.5f, 0.5f), ogPos.y + Random.Range(-0.5f, 0.5f), ogPos.z + Random.Range(-0.5f, 0.5f));
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
