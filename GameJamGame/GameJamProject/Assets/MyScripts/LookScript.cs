using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Quaternion turnRotation;
    [SerializeField] Quaternion lookRotation;
    [SerializeField] Quaternion halfRotation;
    [SerializeField] float lookSensitivity;
    

    [SerializeField]
    float xRotation;
    [SerializeField]
    float yRotation;
    [SerializeField]
    int roomType;

    // Use this for initialization
    void Start()
    {
        body = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        xRotation += Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        yRotation += Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        turnRotation = Quaternion.Euler(0, xRotation+180, 0);
        lookRotation = Quaternion.Euler(-yRotation, 0, 0);
        transform.localRotation = lookRotation;
        //transform.localRotation = Quaternion.Euler(0, 180, 0);
        body.localRotation = turnRotation;
    }
}
