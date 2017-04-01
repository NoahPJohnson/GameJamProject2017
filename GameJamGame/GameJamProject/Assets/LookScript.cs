using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Quaternion turnRotation;
    [SerializeField] Quaternion lookRotation;

    [SerializeField] float xRotation;
    [SerializeField] float yRotation;

	// Use this for initialization
	void Start ()
    {
        body = transform.parent;
	}
	
	// Update is called once per frame
	void Update ()
    {
        xRotation += Input.GetAxis("Mouse X");
        yRotation += Input.GetAxis("Mouse Y");
        turnRotation = Quaternion.Euler(0, xRotation, 0);
        lookRotation = Quaternion.Euler(-yRotation, 0, 0);
        transform.localRotation = lookRotation;
        body.localRotation = turnRotation;
	}
}
