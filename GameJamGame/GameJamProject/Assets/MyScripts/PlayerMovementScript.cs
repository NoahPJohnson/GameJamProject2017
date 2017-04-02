using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] bool sneakPlayer;
    [SerializeField] bool sneaking;
    [SerializeField] float speed;
    [SerializeField] float jogSpeed;
    [SerializeField] float sneakSpeed;
    [SerializeField] float floorTypeValue;
    [SerializeField] Vector3 moveVector;
    [SerializeField] Vector3 downVector;
    [SerializeField] Material[] floorTypes;
    ArrayList floorList;
    //RaycastHit hit;
    [SerializeField] Camera headCamera;
    [SerializeField] CharacterController characterController;

    [SerializeField] Image stealthIndicator;
    [SerializeField] Color noiseColor;

    [FMODUnity.EventRef]
    [SerializeField]
    string footstepName = "event:/Seeker Sounds/Footsteps";
    FMOD.Studio.EventInstance footstepEvent;
    FMOD.Studio.ParameterInstance floorType;

    // Use this for initialization
    void Start()
    {
        downVector = new Vector3(0, -1, 0);
        footstepEvent = FMODUnity.RuntimeManager.CreateInstance(footstepName);
        footstepEvent.getParameter("FootStep Type", out floorType);
        floorType.setValue(0);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstepEvent, transform, GetComponent<Rigidbody>());
        footstepEvent.start();
        floorList = new ArrayList();
        for (int i = 0; i < floorTypes.Length; i++)
        {
            //Debug.Log(i);
            //Debug.Log(floorTypes[i]);
            floorList.Add(floorTypes[i]);
            //Debug.Log(floorList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstepEvent, transform, GetComponent<Rigidbody>());
        RaycastHit hit;
        Ray ray = new Ray(transform.position, downVector);
        if (Physics.Raycast(ray, out hit))
        {
            //floorTypeValue = floorList.BinarySearch(hit.transform.GetComponent<Renderer>().material);
            //Debug.Log(floorList.IndexOf(hit.transform.GetComponent<Renderer>().sharedMaterial));
            floorTypeValue = (floorList.IndexOf(hit.transform.GetComponent<Renderer>().sharedMaterial)+1);
            floorType.setValue(floorTypeValue);
        }

        moveVector.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = transform.TransformDirection(moveVector);
        characterController.Move(moveVector * speed * Time.deltaTime);
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
            //stealthIndicator.color = noiseColor;
        }
        else
        {
            //stealthIndicator.color = noiseColor;
            floorType.setValue(0);
        }
    }

    public void StopFootsteps()
    {
        floorType.setValue(0f);
        footstepEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StartFootsteps()
    {
        floorType.setValue(0f);
        footstepEvent.start();
    }
}
