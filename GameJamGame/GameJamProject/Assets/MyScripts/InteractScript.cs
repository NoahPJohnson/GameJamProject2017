using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    [SerializeField]
    Transform door;
    [SerializeField]
    DoorScript doorScript;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (doorScript != null)
            {
                doorScript.StartSwing();
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (doorScript != null)
            {
                doorScript.InterruptSwing();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            /*if (other.transform != door)
            {
            }*/
            door = other.transform;
            doorScript = door.parent.GetComponent<DoorScript>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            if (doorScript != null)
            {
                doorScript.InterruptSwing();
            }
            door = null;
            doorScript = null;
        }
    }
}
