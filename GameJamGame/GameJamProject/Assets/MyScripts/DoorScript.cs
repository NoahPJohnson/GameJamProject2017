using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] bool interactible = true;
    [SerializeField] bool open = false;
    [SerializeField] bool quietDoor;
    [SerializeField] float swingOpenDelay;
    [SerializeField] float swingOpenSpeed;
    [SerializeField] float swingCloseSpeed;
    [SerializeField] float swingValue;
    [SerializeField] float timer;
    [SerializeField] Quaternion closeRotation;
    [SerializeField] Quaternion openRotation;
    //[SerializeField] GameObject player;

    [FMODUnity.EventRef]
    [SerializeField] string doorEventName = "event:/Seeker Sounds/Door";
    FMOD.Studio.EventInstance doorEvent;
    FMOD.Studio.ParameterInstance doorSoundType;

    // Use this for initialization
    void Start ()
    {
        doorEvent = FMODUnity.RuntimeManager.CreateInstance(doorEventName);
        doorEvent.getParameter("Door Select", out doorSoundType);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(doorEvent, transform, GetComponent<Rigidbody>());
        closeRotation = transform.localRotation;
        openRotation = Quaternion.Euler(transform.localRotation.eulerAngles + new Vector3(0, 90, 0));
        Debug.Log(transform.localRotation.y + 90);
        Debug.Log(openRotation);
    }
	
	IEnumerator SwingDoor()
    {
        if (open == true)
        {
            while (timer < swingOpenDelay)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            //PlaySound
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(doorEvent, transform, GetComponent<Rigidbody>());
            doorEvent.start();
            Debug.Log("PLAY SOUND");
            while (Mathf.Abs(transform.localRotation.eulerAngles.y - openRotation.eulerAngles.y) > 0)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, openRotation, swingOpenSpeed * Time.deltaTime);
                yield return null;
            }
            
            interactible = true;
            yield return null;
        }
        else
        {
            while (Mathf.Abs(transform.localRotation.eulerAngles.y - closeRotation.eulerAngles.y) > 0)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, closeRotation, swingCloseSpeed * Time.deltaTime);
                yield return null;
            }
            //PlaySound
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(doorEvent, transform, GetComponent<Rigidbody>());
            doorEvent.start();
            Debug.Log("PLAY OTHER SOUND");
            interactible = true;
            yield return null;
        }
    }

    public void StartSwing()
    {
        if (interactible == true)
        {
            doorSoundType.setValue(0f);
            timer = 0;
            swingOpenDelay = 3;
            swingCloseSpeed = 25;
            interactible = false;
            open = !open;
            StartCoroutine(SwingDoor());
        }
    }

    public void InterruptSwing()
    {
        swingOpenDelay = 0f;
        swingCloseSpeed = 130;
        if (open == true)
        {
            doorSoundType.setValue(2f);
        }
        else
        {
            doorSoundType.setValue(3f);
        }
    }
}
