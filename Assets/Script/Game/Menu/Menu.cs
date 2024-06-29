using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] Text bestScoreText;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        UpdateBestScore();
    }   
    // Hàm xử lý sự kiện click
    public void ButtonPlayGameClick()
    {
        audioManager.PlaySFX(audioManager.Touch);
        SceneManager.LoadScene("Game");
    }

    public void ButtonSetting()
    {
        settingPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.Touch);
    }

    public void ButtonCloseSetting()
    {
        settingPanel.SetActive(false);
        audioManager.PlaySFX(audioManager.Touch);
    }

    private void UpdateBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("", 0);
        bestScoreText.text = bestScore.ToString();
    }
}
