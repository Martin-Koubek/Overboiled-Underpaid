using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject PMenu;
    public GameObject options;
    public bool isOptionOpen;
    public bool isPaused;

    void Start()
    {
        isPaused = false;
        isOptionOpen = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                pauseGame();
            }
            else if (isPaused == true && isOptionOpen == false)
            {
                resume();
            }
            else if (isPaused == true && isOptionOpen == true)
            {
                closeSet();
            }
            else
            {
                resume();
            }
        }


    }

    public void openSet()
    {
        PMenu.SetActive(false);
        options.SetActive(true);
        isOptionOpen = true;
    }
    public void closeSet()
    {
        options.SetActive(false);
        PMenu.SetActive(true);
        isOptionOpen = false;
    }

    public void pauseGame()
    {
        PMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void resume()
    {
        PMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync("menu");
    }

}
