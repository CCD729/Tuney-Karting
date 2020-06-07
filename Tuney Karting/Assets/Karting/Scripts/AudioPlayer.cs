using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    private Hv_heavy_AudioLib pd;
    public AudioClip _clip;
    public ArcadeKart kartScript;
    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<Hv_heavy_AudioLib>();

        pd.FillTableWithMonoAudioClip("PDTable", _clip);

        pd.SendEvent(Hv_heavy_AudioLib.Event.Engine);
        pd.SetFloatParameter(Hv_heavy_AudioLib.Parameter.Frequency, 1);

        StartCoroutine(DelayStart(0.2f, 0.3f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pd.SetFloatParameter(Hv_heavy_AudioLib.Parameter.Frequency, kartScript.velocityAudio * 60f/13f);
    }

    public void triggerLayer(int index)
    {
        int randomLayer, randomNum;
        bool bassFirst = false;
        randomLayer = Random.Range(1, 3);
        randomNum = Random.Range(1, 3);
        switch (randomLayer)
        {
            case 1:
                bassFirst = true;
                break;
            case 2:
                bassFirst = false;
                break;
            default:
                Debug.LogError("Error in random layer selection");
                break;
        }

        switch (index)
        {
            case 0:
                pd.SendEvent(Hv_heavy_AudioLib.Event.Checkpoint1);
                if (bassFirst)
                {
                    pd.SendEvent(Hv_heavy_AudioLib.Event.Bass);
                }
                else
                {
                    switch (randomNum)
                    {
                        case 1:
                            pd.SendEvent(Hv_heavy_AudioLib.Event.Drum1);
                            break;
                        case 2:
                            pd.SendEvent(Hv_heavy_AudioLib.Event.Drum2);
                            break;
                        default:
                            Debug.LogError("Error in random number selection");
                            break;
                    }
                }

                //Demo logic
                //pd.SendEvent(Hv_heavy_AudioLib.Event.Drum2);
                break;
            case 1:
                pd.SendEvent(Hv_heavy_AudioLib.Event.Checkpoint2);
                if (!bassFirst)
                {
                    pd.SendEvent(Hv_heavy_AudioLib.Event.Bass);
                }
                else
                {
                    switch (randomNum)
                    {
                        case 1:
                            pd.SendEvent(Hv_heavy_AudioLib.Event.Drum1);
                            break;
                        case 2:
                            pd.SendEvent(Hv_heavy_AudioLib.Event.Drum2);
                            break;
                        default:
                            Debug.LogError("Error in random number selection");
                            break;
                    }
                }

                //Demo logic
                //pd.SendEvent(Hv_heavy_AudioLib.Event.Bass);
                break;
            case 2:
                pd.SendEvent(Hv_heavy_AudioLib.Event.Checkpoint3);
                randomNum = Random.Range(1, 3);
                switch (randomNum)
                {
                    case 1:
                        pd.SendEvent(Hv_heavy_AudioLib.Event.Fill1);
                        break;
                    case 2:
                        pd.SendEvent(Hv_heavy_AudioLib.Event.Fill2);
                        break;
                    default:
                        Debug.LogError("Error in random filler selection");
                        break;
                }

                //Demo logic
                //pd.SendEvent(Hv_heavy_AudioLib.Event.Fill2);
                break;
            case 3:
                pd.SendEvent(Hv_heavy_AudioLib.Event.Finishline);
                pd.SendEvent(Hv_heavy_AudioLib.Event.Startendvol);
                break;
            default:
                break;
        }
    }

    public void playBounce()
    {
        pd.SendEvent(Hv_heavy_AudioLib.Event.Wall);
    }

    IEnumerator DelayStart(float delayTime1, float delayTime2)
    {
        yield return new WaitForSecondsRealtime(delayTime1);
        pd.SendEvent(Hv_heavy_AudioLib.Event.Startendvol);
        yield return new WaitForSecondsRealtime(delayTime2);
        pd.SendEvent(Hv_heavy_AudioLib.Event.Startstopseq);
        yield return new WaitForSecondsRealtime(2.4f);
        pd.SendEvent(Hv_heavy_AudioLib.Event.Checkpoint0);
    }
}
