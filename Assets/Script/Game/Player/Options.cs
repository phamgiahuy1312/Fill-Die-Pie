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
    public GameObject GameOverPanel;

    private Bullets bullets;
    //private bool clickHealth = false;
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        bullets = FindObjectOfType<Bullets>();
    }

    public void EnableHealthOption(bool action)
    {
        HealthOption.interactable = action;
    }

    //============== OPTION HEALTH =================

    public void OnClickHealthOption()
    {

        Dialog.SetActive(true);
        IconHealth.SetActive(true);
        ButtonAdsHeath.SetActive(true);
        IconSlow.SetActive(false);
        ButtonAdsSpeed.SetActive(false);
        Time.timeScale = 0;

    }
    public void AddHealth()
    {
        HealthManager.health++;
        Dialog.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;
        bullets.ResetBullet();


    }

    //============== OPTION SPEED =================

    public void OnLickSpeedOption()
    {
        Dialog.SetActive(true);
        IconHealth.SetActive(false);
        ButtonAdsHeath.SetActive(false);
        IconSlow.SetActive(true);
        ButtonAdsSpeed.SetActive(true);
        Time.timeScale = 0;
    }

    public void SlowSpeed()
    {
        enemySpawner.speedOfRotation = enemySpawner.speedOfRotation / 3;
        Dialog.SetActive(false);
        Time.timeScale = 1;
        bullets.ResetBullet();

    }

    //close dialog
    public void CloseDialog()
    {
        Dialog.SetActive(false);
        Time.timeScale = 1;
        bullets.ResetBullet();

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
