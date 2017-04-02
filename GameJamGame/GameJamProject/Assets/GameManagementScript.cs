using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagementScript : MonoBehaviour
{
    [SerializeField] bool player1Turn;
    [SerializeField] bool paused;
    [SerializeField] bool firstTurn;
    [SerializeField] int objectiveIndex;
    [SerializeField] int round = 0;
    [SerializeField] int roundMax = 3;
    [SerializeField] float gameTimerInitial = 120f;
    [SerializeField] float gameTimer = 120f;
    [SerializeField] float player1Score = 0f;
    [SerializeField] float player1VictoryValue = 60f;

    [SerializeField] GameObject player1Panel;
    [SerializeField] GameObject player2Panel;
    [SerializeField] GameObject player2Panel2;
    [SerializeField] GameObject endGamePanel;

    [SerializeField] GameObject[] phones;
 
    [SerializeField] Transform[] phoneObjectives;

    [SerializeField] Text timerText;
    [SerializeField] Text endGameText;
    [SerializeField] Image objectiveImage;
    [SerializeField] Image[] roundDisplay;

	// Use this for initialization
	void Start ()
    {
        objectiveIndex = Random.Range(0, phones.Length);
        phones[objectiveIndex].SetActive(true);
        player1Turn = true;
        paused = false;
        firstTurn = true;
        objectiveIndex = Random.Range(0, phoneObjectives.Length);
        round = 0;
        gameTimer = gameTimerInitial;
        player1Score = 0f;
        timerText.text = Mathf.FloorToInt(gameTimer / 60).ToString() + ":" + (gameTimer % 60).ToString();
        for (int i = 0; i < roundDisplay.Length; i ++)
        {
            roundDisplay[i].enabled = false;
        }
        TogglePause();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            TogglePause();
        }
	}

    public void TogglePause()
    {
        if (paused == false)
        {
            paused = true;
            Time.timeScale = 0f;
            GetComponent<PlayerSwitchScript>().StopAllFootsteps();
            GetComponent<PlayerSwitchScript>().Switch();
            if (player1Turn == true)
            {
                if (firstTurn == false)
                {
                    player2Panel2.SetActive(true);
                }
                firstTurn = false;
                player1Panel.SetActive(true);
                player1Turn = false;
                round++;
                UpdateRoundCounter();
                GetComponent<PlayerSwitchScript>().ResetPlayer2();
            }
            else
            {
                player2Panel.SetActive(true);
                player1Turn = true;
                CheckPlayer1Score();
            }
            
        }
        else
        {
            paused = false;
            Time.timeScale = 1f;
            player1Panel.SetActive(false);
            player2Panel.SetActive(false);
            GetComponent<PlayerSwitchScript>().StartAllFootsteps();
            gameTimer = gameTimerInitial;
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        while (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            timerText.text = Mathf.FloorToInt(gameTimer / 60).ToString() + ":" + (gameTimer % 60).ToString("F0");
            yield return null;
        }
        TogglePause();
    }

    IEnumerator IncreasePlayer1Score()
    {
        yield return new WaitForSeconds(1f);
        while (player1Score < player1VictoryValue)
        {
            player1Score += Time.deltaTime;
            UpdateObjectiveImage();
            yield return null;
        }
    }

    void UpdateRoundCounter()
    {
        roundDisplay[round-1].enabled = true;
    }

    void UpdateObjectiveImage()
    {
        objectiveImage.fillAmount = player1Score/player1VictoryValue;
    }

    public void StartIncrementingScore()
    {
        StartCoroutine(IncreasePlayer1Score());
    }

    public void StopIncrementingScore()
    {
        StopCoroutine(IncreasePlayer1Score());
    }

    public void DisablePlayer2Panel2()
    {
        player2Panel2.SetActive(false);
    }

    public void CheckPlayer1Score()
    {
        if (player1Score >= player1VictoryValue)
        {
            //Sneaker Wins
            TogglePause();
            player1Panel.SetActive(false);
            player2Panel.SetActive(false);
            player2Panel2.SetActive(false);
            endGamePanel.SetActive(true);
            endGameText.text = "The whistleblower has successfully leaked the information. Player 1 wins.";
        }
        else
        {
            if (round >= roundMax)
            {
                //Sneaker Loses
                TogglePause();
                player1Panel.SetActive(false);
                player2Panel.SetActive(false);
                player2Panel2.SetActive(false);
                endGamePanel.SetActive(true);
                endGameText.text = "The whistleblower could not leak the information in time. Player 2 wins.";
            }
        }
    }

    public void CheckObjectiveIndex(int indexToCheck)
    {
        if (indexToCheck-1 == objectiveIndex)
        {
            //Finder Wins
            TogglePause();
            player1Panel.SetActive(false);
            player2Panel.SetActive(false);
            player2Panel2.SetActive(false);
            endGamePanel.SetActive(true);
            endGameText.text = "The agent has discovered the source of the leak. Player 2 wins.";
        }
        else
        {
            //Finder Loses
            TogglePause();
            player1Panel.SetActive(false);
            player2Panel.SetActive(false);
            player2Panel2.SetActive(false);
            endGamePanel.SetActive(true);
            endGameText.text = "The agent has made a false accusation. Player 1 wins.";
        }
    }

    public void InitializeGame()
    {
        endGamePanel.SetActive(false);
        GetComponent<PlayerSwitchScript>().ResetPlayer1();
        GetComponent<PlayerSwitchScript>().ResetPlayer2();
        objectiveIndex = Random.Range(0, phones.Length);
        phones[objectiveIndex].SetActive(true);
        player1Turn = true;
        paused = false;
        firstTurn = true;
        objectiveIndex = Random.Range(0, phoneObjectives.Length);
        round = 0;
        gameTimer = gameTimerInitial;
        player1Score = 0f;
        timerText.text = Mathf.FloorToInt(gameTimer / 60).ToString() + ":" + (gameTimer % 60).ToString();
        for (int i = 0; i < roundDisplay.Length; i++)
        {
            roundDisplay[i].enabled = false;
        }
        TogglePause();
    }
}
