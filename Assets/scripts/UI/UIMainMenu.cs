using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [Header("Main Screen")]
    [SerializeField] private GameObject MainScreen;


    private void Awake()
    {
        MainScreen.SetActive(true);
    }

    public void Level1()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    public void Level3()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
