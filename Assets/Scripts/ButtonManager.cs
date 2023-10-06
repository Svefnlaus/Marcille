using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject ConfirmationWindow;
    private bool isGamePaused;

    private void Awake()
    {
        isGamePaused = false;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return; 
        if (isGamePaused && ConfirmationWindow.activeSelf == true) ResumeGame();
        if (!isGamePaused && ConfirmationWindow.activeSelf == false) PauseGame();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

        #endif

        Application.Quit();
    }

    public void ReturnToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        OpenConfirmationWindow();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        CloseConfirmationWindow();
    }

    private void OpenConfirmationWindow()
    {
        ConfirmationWindow.SetActive(true);
    }

    private void CloseConfirmationWindow()
    {
        ConfirmationWindow.SetActive(false);
    }
}
