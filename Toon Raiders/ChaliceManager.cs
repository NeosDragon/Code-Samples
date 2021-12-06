using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ChaliceManager : MiniGameManager
{

    public static ChaliceManager instance;

    [Header("Scores")]
    public float scoreRed;
    public float scoreBlue;
    public float scoreYellow;
    public float scoreGreen;

    [Header("Score Text")]
    public TextMeshProUGUI scoreRedText;
    public TextMeshProUGUI scoreBlueText;
    public TextMeshProUGUI scoreYellowText;
    public TextMeshProUGUI scoreGreenText;

    [Header("Victory Text")]
    public TextMeshProUGUI redVictory;
    public TextMeshProUGUI blueVictory;
    public TextMeshProUGUI yellowVictory;
    public TextMeshProUGUI greenVictory;

    public float timeValue;
    public TextMeshProUGUI timerText;

    public List<float> scores;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Resets scores to 0
        scoreRed = 0f;
        scoreBlue = 0f;
        scoreYellow = 0f;
        scoreGreen = 0f;

        //Resets timer to 120 seconds
        timeValue = 120f;

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Decreases timer, when it goes below 0 it sets the timer to 0
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);


    }

    public void AddRedPoint()
    {
        //Adds point and updates UI
        scoreRed += 1f;
        scoreRedText.text = scoreRed.ToString();

        //Victory Condition when P1 gets 4 points
        if (scoreRed == 4)
        {
            redVictory.gameObject.SetActive(true);
            DetermineWinner();
            GameEnd();
        }
    }

    public void AddBluePoint()
    {
        //Adds point and updates UI
        scoreBlue += 1f;
        scoreBlueText.text = scoreBlue.ToString();

        //Victory Condition when P2 gets 4 points
        if (scoreBlue == 4)
        {
            blueVictory.gameObject.SetActive(true);
            DetermineWinner();
            GameEnd();
        }
    }

    public void AddYellowPoint()
    {
        //Adds point and updates UI
        scoreYellow += 1f;
        scoreYellowText.text = scoreYellow.ToString();

        //Victory Condition when P3 gets 4 points
        if (scoreYellow == 4)
        {
            yellowVictory.gameObject.SetActive(true);
            DetermineWinner();
            GameEnd();
        }
    }

    public void AddGreenPoint()
    {
        //Adds point and updates UI
        scoreGreen += 1f;
        scoreGreenText.text = scoreGreen.ToString();

        //Victory Condition when P4 gets 4 points
        if (scoreGreen == 4)
        {
            greenVictory.gameObject.SetActive(true);
            DetermineWinner();
            GameEnd();
        }
    }

    void DisplayTime(float timeToDisplay)
    {

        //If loop that compares scores if timer hits 0
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
            if ((timeValue <= 0) && (scoreRed > scoreBlue) && (scoreRed > scoreYellow) && (scoreRed > scoreGreen))
            {
                redVictory.gameObject.SetActive(true);
                DetermineWinner();
                GameEnd();
            }

            if ((timeValue <= 0) && (scoreBlue > scoreRed) && (scoreBlue > scoreYellow) && (scoreBlue > scoreGreen))
            {
                blueVictory.gameObject.SetActive(true);
                DetermineWinner();
                GameEnd();
            }

            if ((timeValue <= 0) && (scoreYellow > scoreBlue) && (scoreYellow > scoreRed) && (scoreYellow > scoreGreen))
            {
                yellowVictory.gameObject.SetActive(true);
                DetermineWinner();
                GameEnd();
            }

            if ((timeValue <= 0) && (scoreGreen > scoreBlue) && (scoreGreen > scoreYellow) && (scoreGreen > scoreRed))
            {
                greenVictory.gameObject.SetActive(true);
                DetermineWinner();
                GameEnd();
            }
        }

        //Converts time to display it in 00:00 format
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Ends game and switches scenes to Leaderboard
    public void GameEnd()
    {
        isGameEnded = true;
        SceneSwitchManager.Instance.SwitchSceneAfterTime("Leaderboard", 2f);

    }

    public void DetermineWinner()
    {
        //Creates list of all the player scores
        List<float> playerScores = new List<float>() { scoreRed, scoreBlue, scoreYellow, scoreGreen };
        //Sorts scores in descending order
        List<float> sortedScores = playerScores.OrderByDescending(i => i).ToList();
        int maxScore = (int)playerScores.Max();
        //Assigns index to players
        if (sortedScores[1] != maxScore)
        {
            int index = playerScores.IndexOf(maxScore);
            List<float> finalScores = new List<float>();
            foreach (int score in playerScores)
            {
                if (!finalScores.Contains(score))
                {
                    finalScores.Add(score);
                }
            }
            //Organizes indexes in placement order
            List<float> sortedFinalScores = finalScores.OrderByDescending(i => i).ToList();
            List<float> overallScores = new List<float>();
            for (int i = 0; i < PlayerControllers.Count; i++)
            {
                Standings.Add(PlayerControllers[i]);
                int scoreIndex = sortedFinalScores.IndexOf((int)playerScores[i]);
                overallScores.Add(scoreIndex);
            }
            CalculateChaliceScores(overallScores);
        }
    }
}

