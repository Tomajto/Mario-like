using UnityEngine;
using TMPro;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI winTimeText; // Textový prvek pro zobrazení èasu
    [SerializeField] private string levelName; // Název úrovnì pro jedineèný klíè

    private void Awake()
    {
        winScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer.StopTimer();
            float currentTime = timer.GetElapsedTime();
            winTimeText.text = timer.GetFormattedTime(); // Aktualizace textu s èasem
            SaveBestTime(currentTime);
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void SaveBestTime(float currentTime)
    {
        string bestTimeKey = "BestTime_" + levelName;
        float bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);
        if (currentTime < bestTime)
        {
            PlayerPrefs.SetFloat(bestTimeKey, currentTime);
            PlayerPrefs.Save();
        }
    }
}


