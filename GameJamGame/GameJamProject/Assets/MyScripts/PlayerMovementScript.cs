using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] bool sneakPlayer;
    [SerializeField] bool sneaking;
    [SerializeField] float speed;
    [SerializeField] float jogSpeed;
    [SerializeField] float sneakSpeed;
    [SerializeField] float floorTypeValue;
    [SerializeField] Vector3 moveVector;
    [SerializeField] Camera headCamera;
    [SerializeField] CharacterController characterController;

    [FMODUnity.EventRef]
    [SerializeField] string footstepName = "event:/Seeker Sounds/Footsteps";
    FMOD.Studio.EventInstance footstepEvent;
    FMOD.Studio.ParameterInstance floorType;

	// Use this for initialization
	void Start ()
    {
        footstepEvent = FMODUnity.RuntimeManager.CreateInstance(footstepName);
        footstepEvent.getParameter("FootStep Type", out floorType);
        floorType.setValue(0);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstepEvent, transform, GetComponent<Rigidbody>());
        footstepEvent.start();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        moveVector.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = transform.TransformDirection(moveVector);
        characterController.Move(moveVector*speed*Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sneaking = true;
            speed = sneakSpeed;
            floorType.setValue(0);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sneaking = false;
            speed = jogSpeed;
        }


        if (moveVector.magnitude > 0 && sneaking == false)
        {
            floorType.setValue(2);
        }
        else
        {
            floorType.setValue(0);
        }
	}
}
