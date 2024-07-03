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
    private Bullets bullets;
    public static bool isDialogActive = false;
    //private bool clickHealth = false;
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        rewardedAds = FindObjectOfType<RewardedAds>();
        bullets = FindObjectOfType<Bullets>();
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
        isDialogActive = true;
        IconHealth.SetActive(true);
        ButtonAdsHeath.SetActive(true);
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
            HealthManager.health++;
            Dialog.SetActive(false);
            // isDialogActive = false;
            GameOverPanel.SetActive(false);
            Time.timeScale = 1;
            bullets.ResetBullet();
        });
    }

    //============== OPTION SPEED =================

    public void OnLickSpeedOption()
    {
        Dialog.SetActive(true);
        IconHealth.SetActive(false);
        isDialogActive = true;
        ButtonAdsHeath.SetActive(false);
        IconSlow.SetActive(true);
        ButtonAdsSpeed.SetActive(true);
        Time.timeScale = 0;
    }

    public void SlowSpeed()
    {
        
        
        rewardedAds.ShowAd(() =>
        {
            enemySpawner.speedOfRotation = enemySpawner.speedOfRotation / 3;
            Dialog.SetActive(false);
            // isDialogActive = false;
            Time.timeScale = 1;
            bullets.ResetBullet();
        });
    }

    //close dialog
    public void CloseDialog()
    {
        Dialog.SetActive(false);
        isDialogActive = false;
        Time.timeScale = 1;
        bullets.ResetBullet();
    }

    public void PanelGameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        isDialogActive = true;
    }
    
    public void ClickBackToMenu()
    {
        SceneManager.LoadScene("Menu");
        isDialogActive = false;
    }
}
