using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class EscSetting : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject Sound;
    

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
        pausePanel.SetActive(false);
        Sound.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            if (isPaused)
                return;
        }
    }
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // ���� �Ͻ�����
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // ���� �簳
            pausePanel.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
    public void QuitToMain()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
    public void OnClickSound()
    {
        
        if (isPaused)
        Sound.SetActive(true);
    }
    public void OnClickXkey()
    {
        Sound.SetActive(false);

    }
    public void OnClickRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameView");
    }

}