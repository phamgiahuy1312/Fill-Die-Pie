using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject PanelOption;
    public Button HealthOption;
    public GameObject IconSlow;
    public GameObject IconHealth;
    public GameObject ButtonAdsHeath;
    public GameObject ButtonAdsSpeed;
    public GameObject Dialog;
    private Bullets bulletScript;
    private HealthManager HealthManager;
    private EnemySpawner enemySpawner;
    private RewardedAds rewardedAds;
    public GameObject GameOverPanel;
    //private bool clickHealth = false;
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        rewardedAds = FindObjectOfType<RewardedAds>();

        if (rewardedAds == null)
        {
            Debug.LogError("rewardedAds is not assigned!");
        }
        else
        {
            Debug.Log("rewardedAds assigned successfully.");
        }
    }

    public void EnableHealthOption(bool action)
    {
        HealthOption.interactable = action;
    }

    //============== OPTION HEALTH =================

    public void OnClickHealthOption()
    {
        Dialog.SetActive(true);
        IconSlow.SetActive(false);
        ButtonAdsSpeed.SetActive(false);
        Time.timeScale = 0;
    }
    public void AddHealth()
    {
        if (rewardedAds == null)
        {
            Debug.LogError("rewardedAds is not assigned in AddHealth!");
            return;
        }

        rewardedAds.ShowAd(() =>
        {
            Time.timeScale = 1;
            HealthManager.health++;
            Dialog.SetActive(false);
            GameOverPanel.SetActive(false);
        });
    }

    //============== OPTION SPEED =================

    public void OnLickSpeedOption()
    {
        Dialog.SetActive(true);
        IconHealth.SetActive(false);
        ButtonAdsHeath.SetActive(false);
        Time.timeScale = 0;
    }

    public void SlowSpeed()
    {
        Time.timeScale = 1;
        enemySpawner.speedOfRotation = enemySpawner.speedOfRotation / 3;
        Dialog.SetActive(false);
    }

    //close dialog
    public void CloseDialog()
    {
        Time.timeScale = 1;
        Dialog.SetActive(false);
    }

    public void PanelGameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }
    public void ClickBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
