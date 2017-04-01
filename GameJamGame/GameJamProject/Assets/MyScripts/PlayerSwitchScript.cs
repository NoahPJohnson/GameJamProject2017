using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwitchScript : MonoBehaviour
{
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] Transform playerMarker;
    [SerializeField] bool player1Turn;
    //[SerializeField] float player1ActiveTime;
    //[SerializeField] float player2ActiveTime;

    float Timer = 5;

    // Use this for initialization
    void Start()
    {
        EnablePlayer(player1);
        DisablePlayer(player2);
        player1Turn = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Switch()
    {
        if (player1Turn == false)
        {
            EnablePlayer(player1);
            DisablePlayer(player2);
            Time.timeScale = 1;
            Timer = 5;
            player1Turn = true;
        }
        else
        {
            EnablePlayer(player2);
            DisablePlayer(player1);
            Time.timeScale = 1;
            Timer = 5;
            player1Turn = false;
        }
    }

    void EnablePlayer(Transform player)
    {
        player.GetComponent<Collider>().enabled = true;
        player.GetComponent<PlayerMovementScript>().enabled = true;
        player.GetComponent<PlayerMovementScript>().enabled = true;
        player.GetChild(0).gameObject.SetActive(true);
    }

    void DisablePlayer(Transform player)
    {
        player.GetComponent<Collider>().enabled = false;
        player.GetComponent<PlayerMovementScript>().enabled = false;
        player.GetComponent<PlayerMovementScript>().enabled = false;
        player.GetChild(0).gameObject.SetActive(false);
    }
}
