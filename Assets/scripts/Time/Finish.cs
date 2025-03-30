using UnityEngine;
using TMPro;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI winTimeText; // Textový prvek pro zobrazení èasu
    private const string BestTimeKey = "BestTime"; // Klíè pro uložení nejlepšího èasu

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
        float bestTime = PlayerPrefs.GetFloat(BestTimeKey, float.MaxValue);
        if (currentTime < bestTime)
        {
            PlayerPrefs.SetFloat(BestTimeKey, currentTime);
            PlayerPrefs.Save();
        }
    }
}
