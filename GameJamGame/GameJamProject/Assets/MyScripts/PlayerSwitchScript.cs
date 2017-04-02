using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwitchScript : MonoBehaviour
{
    [SerializeField] Vector3 player1Start;
    [SerializeField] Vector3 player2Start;
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] Transform playerMarker;
    [SerializeField] bool player1Turn;
    //[SerializeField] float player1ActiveTime;
    //[SerializeField] float player2ActiveTime;

    //float Timer = 5;

    // Use this for initialization
    void Start()
    {
        player1Start = player1.position;
        player2Start = player2.position;
        EnablePlayer(player1);
        DisablePlayer(player2);
        player1Turn = false;
    }

    public void Switch()
    {
        if (player1Turn == false)
        {
            EnablePlayer(player1);
            DisablePlayer(player2);
            //Time.timeScale = 1;
            player1Turn = true;
        }
        else
        {
            EnablePlayer(player2);
            DisablePlayer(player1);
            //Time.timeScale = 1;
            player1Turn = false;
        }
    }

    void EnablePlayer(Transform player)
    {
        player.GetComponent<Collider>().enabled = true;
        player.GetComponent<PlayerMovementScript>().enabled = true;
        player.GetComponent<PlayerMovementScript>().enabled = true;
        //player.GetComponent<PlayerMovementScript>().StartFootsteps();
        player.GetChild(0).gameObject.SetActive(true);
    }

    void DisablePlayer(Transform player)
    {
        player.GetComponent<Collider>().enabled = false;
        //player.GetComponent<PlayerMovementScript>().StopFootsteps();
        player.GetComponent<PlayerMovementScript>().enabled = false;
        player.GetComponent<PlayerMovementScript>().enabled = false;
        
        player.GetChild(0).gameObject.SetActive(false);
    }

    public void StopAllFootsteps()
    {
        player1.GetComponent<PlayerMovementScript>().StopFootsteps();
        player2.GetComponent<PlayerMovementScript>().StopFootsteps();
    }

    public void StartAllFootsteps()
    {
        player1.GetComponent<PlayerMovementScript>().StartFootsteps();
        player2.GetComponent<PlayerMovementScript>().StartFootsteps();
    }

    public void ResetPlayer1()
    {
        player1.position = player1Start;
    }

    public void ResetPlayer2()
    {
        player2.position = player2Start;
    }
}
