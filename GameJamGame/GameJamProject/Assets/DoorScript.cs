using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] bool interactible = true;
    [SerializeField] bool open = false;
    [SerializeField] float swingSpeed;
    [SerializeField] float swingValue;
    [SerializeField] Quaternion closeRotation;
    [SerializeField] Quaternion openRotation;
    //[SerializeField] GameObject player;



    // Use this for initialization
    void Start ()
    {
        //Debug.Log("FUCK");
        closeRotation = transform.localRotation;
        openRotation = Quaternion.Euler(transform.localRotation.eulerAngles + new Vector3(0, 90, 0));
        Debug.Log(transform.localRotation.y + 90);
        Debug.Log(openRotation);
    }
	
	IEnumerator SwingDoor()
    {
        //swingValue = Mathf.Clamp01(swingValue);
        yield return new WaitForSeconds(1);
        if (open == true)
        {
            while (Mathf.Abs(transform.localRotation.eulerAngles.y - openRotation.eulerAngles.y) > 0)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, openRotation, swingSpeed * Time.deltaTime);
                yield return null;
            }
            //PlaySound
            Debug.Log("PLAY SOUND");
            interactible = true;
            yield return null;
        }
        else
        {
            while (Mathf.Abs(transform.localRotation.eulerAngles.y - closeRotation.eulerAngles.y) > 0)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, closeRotation, swingSpeed * Time.deltaTime);
                yield return null;
            }
            //PlaySound
            Debug.Log("PLAY OTHER SOUND");
            interactible = true;
            yield return null;
        }
    }

    public void StartSwing()
    {
        if (interactible == true)
        {
            interactible = false;
            open = !open;
            StartCoroutine(SwingDoor());
        }
    }
}
