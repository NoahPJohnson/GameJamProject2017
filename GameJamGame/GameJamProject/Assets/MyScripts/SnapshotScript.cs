using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotScript : MonoBehaviour
{
    [SerializeField] float intensityValue;
    [SerializeField] float changeSpeed;

    [FMODUnity.EventRef]
    [SerializeField] string snapshotName = "snapshot:/Large Room";
    FMOD.Studio.EventInstance snapshotEvent;
    FMOD.Studio.ParameterInstance intensity;

	// Use this for initialization
	void Start ()
    {
        snapshotEvent = FMODUnity.RuntimeManager.CreateInstance(snapshotName);
        snapshotEvent.getParameter("Intensity", out intensity);
        //snapshotEvent.start();
        intensity.setValue(0f);
    }

    IEnumerator IncreaseIntensity()
    {
        snapshotEvent.start();
        intensityValue = 0;
        intensity.setValue(intensityValue);
        while(intensityValue < 100f)
        {
            intensityValue += changeSpeed * Time.deltaTime;
            intensity.setValue(intensityValue);
            yield return null;
        }   
    }

    IEnumerator DecreaseIntensity()
    {
        intensityValue = 100;
        intensity.setValue(intensityValue);
        while (intensityValue > 0f)
        {
            intensityValue -= changeSpeed * Time.deltaTime;
            intensity.setValue(intensityValue);
            yield return null;
        }
        snapshotEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.tag == "Player")
        {
            StopCoroutine(DecreaseIntensity());
            StartCoroutine(IncreaseIntensity());
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopCoroutine(IncreaseIntensity());
            StartCoroutine(DecreaseIntensity());
        }
    }
}
