using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;

    private const int maxEntries = 5;
    private const string keyPrefix = "BestTime_";

    public void AddNewTime(float finalTime)
    {
        List<float> times = LoadTimes();

        times.Add(finalTime);
        times.Sort();

        if (times.Count > maxEntries)
            times.RemoveAt(times.Count - 1);

        SaveTimes(times);
    }

    public void ShowLeaderboard()
    {
        List<float> times = LoadTimes();
        leaderboardText.text = "Leaderboard:\n";

        for (int i = 0; i < times.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {FormatTime(times[i])}\n";
        }
    }

    private List<float> LoadTimes()
    {
        List<float> times = new List<float>();

        for (int i = 0; i < maxEntries; i++)
        {
            string key = keyPrefix + i;
            if (PlayerPrefs.HasKey(key))
            {
                times.Add(PlayerPrefs.GetFloat(key));
            }
        }

        return times;
    }

    private void SaveTimes(List<float> times)
    {
        for (int i = 0; i < times.Count; i++)
        {
            PlayerPrefs.SetFloat(keyPrefix + i, times[i]);
        }

        PlayerPrefs.Save();
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        float seconds = time % 60;
        return $"{minutes:00}:{seconds:00.00}";
    }

    public void ResetLeaderboard()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey("BestTime_" + i);
        }
        PlayerPrefs.Save();
    }
}

