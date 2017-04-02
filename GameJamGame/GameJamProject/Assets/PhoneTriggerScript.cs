using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTriggerScript : MonoBehaviour
{
    [SerializeField] Transform GameManagerObject;
    GameManagementScript gameManagerScript;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManagerScript.StartIncrementingScore();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManagerScript.StopIncrementingScore();
        }
    }
}
