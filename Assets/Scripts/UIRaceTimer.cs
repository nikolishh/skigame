using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRaceTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float raceStartTime;
    private float penaltyTime = 0f;
    private int penaltyCount = 0;
    private bool raceOngoing = false;

    void OnEnable()
    {
        GameManager.RaceStart += OnRaceStart;
        GameManager.RacePenalty += OnRacePenalty;
        GameManager.RaceFinish += OnRaceFinish;
    }

    void OnDisable()
    {
        GameManager.RaceStart -= OnRaceStart;
        GameManager.RacePenalty -= OnRacePenalty;
        GameManager.RaceFinish -= OnRaceFinish;
    }

    void Update()
    {
        if (raceOngoing)
        {
            float currentTime = Time.time - raceStartTime + penaltyTime;
            timerText.text = FormatTime(currentTime);
        }
    }

    void OnRaceStart()
    {
        raceStartTime = Time.time;
        penaltyTime = 0f;
        penaltyCount = 0;
        raceOngoing = true;

        Debug.Log("race started");
    }

    void OnRacePenalty()
    {
        penaltyTime += 3f;
        penaltyCount++;

        Debug.Log("penalty, total penalties: " + penaltyCount);
    }

    void OnRaceFinish()
    {
        raceOngoing = false;

        float rawTime = Time.time - raceStartTime;
        float finalTime = rawTime + penaltyTime;

        Debug.Log($"race finished\n" +
                  $"base time: {FormatTime(rawTime)}\n" +
                  $"penalties: {penaltyCount} (+" + penaltyTime.ToString("0.00") + "s)\n" +
                  $"final Time: {FormatTime(finalTime)}");

        timerText.text = "Final Time: " + FormatTime(finalTime);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        float seconds = time % 60;
        return string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    public float GetRawTime()
    {
        return Time.time - raceStartTime;
    }

    public float GetPenaltyTime()
    {
        return penaltyTime;
    }

}

