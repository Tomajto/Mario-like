using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;
    private const string BestTimeKey = "BestTime"; // Klíè pro naètení nejlepšího èasu

    private void Start()
    {
        float bestTime = PlayerPrefs.GetFloat(BestTimeKey, float.MaxValue);
        if (bestTime != float.MaxValue)
        {
            bestTimeText.text = FormatTime(bestTime);
        }
        else
        {
            bestTimeText.text = "No best time yet";
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
