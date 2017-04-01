using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] bool sneakPlayer;
    [SerializeField] bool sneaking;
    [SerializeField] float speed;
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
        floorType.setValue(2);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstepEvent, transform, GetComponent<Rigidbody>());
        footstepEvent.start();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        moveVector.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = headCamera.transform.TransformDirection(moveVector);
        characterController.Move(moveVector*speed*Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sneaking = true;
            speed = speed / 2;
        }
        else
        {
            sneaking = false;
        }


        if (moveVector.magnitude > 0)
        {
            Debug.Log("PlayFootsteps");
            floorType.setValue(2);
        }
        else
        {
            floorType.setValue(0);
        }
	}
}
