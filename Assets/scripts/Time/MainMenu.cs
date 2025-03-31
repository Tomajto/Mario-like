using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private string levelName; // Název úrovnì pro jedineèný klíè
    private const string BestTimeKeyPrefix = "BestTime_"; // Prefix pro klíè nejlepšího èasu

    private void Start()
    {
        string bestTimeKey = BestTimeKeyPrefix + levelName;
        float bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);
        if (bestTime != float.MaxValue)
        {
            bestTimeText.text = FormatTime(bestTime);
        }
        else
        {
            bestTimeText.text = "DNP";
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 1000F) % 1000F);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}


