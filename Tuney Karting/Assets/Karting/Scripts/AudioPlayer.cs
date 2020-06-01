using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    private Hv_heavy_AudioLib pd;
    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<Hv_heavy_AudioLib>();

        StartCoroutine(DelayStart(0.2f, 0.3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerLayer(int index)
    {
        switch (index)
        {
            case 0:
                pd.SendEvent(Hv_heavy_AudioLib.Event.Bass);
                break;
            case 1:
                pd.SendEvent(Hv_heavy_AudioLib.Event.Drum);
                break;
            case 2:
                pd.SendEvent(Hv_heavy_AudioLib.Event.Fill);
                break;
            case 3:
                pd.SendEvent(Hv_heavy_AudioLib.Event.Startendvol);
                break;
            default:
                break;
        }
    }

    IEnumerator DelayStart(float delayTime1, float delayTime2)
    {
        yield return new WaitForSecondsRealtime(delayTime1);
        pd.SendEvent(Hv_heavy_AudioLib.Event.Startendvol);
        yield return new WaitForSecondsRealtime(delayTime2);
        pd.SendEvent(Hv_heavy_AudioLib.Event.Startstopseq);
    }
}
