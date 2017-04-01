using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    [SerializeField] Transform door;
    DoorScript doorScript;

	// Use this for initialization
	void Start ()
    {
        doorScript = door.GetComponent<DoorScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            doorScript.StartSwing();
        }
	}
}
