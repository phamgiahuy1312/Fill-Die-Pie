using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float timedlayspawn = 2f;
    private EnemySpawner enemySpawner;
    public Text scoreText;
    private int score;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        score = 0;
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
        CheckBestScore();
    }

    private void CheckBestScore()
    {
        int bestSCore = PlayerPrefs.GetInt("", 0);
        if (score > bestSCore)
        {
            PlayerPrefs.SetInt("", score);
        }
    }

    public void OnBulletHitEmeny(GameObject enemy)
    {
        enemy.tag = "Shooted";
        CheckAllEnemiesShooted();
    }

    public void CheckAllEnemiesShooted()
    {

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            print("All enemies shooted");
            DestroyAllShootedEnemies();
            audioManager.PlaySFX(audioManager.Win);
            IncrementScore();
            StartCoroutine(DelaySpawnEnemies());
        }
    }

    private void DestroyAllShootedEnemies()
    {
        //destroy all enemies with tag "Shooted"
        GameObject[] shootedEnemies = GameObject.FindGameObjectsWithTag("Shooted");
        foreach (GameObject enemy in shootedEnemies)
        {
            Destroy(enemy);
        }
    }

    private IEnumerator DelaySpawnEnemies()
    {
        yield return new WaitForSeconds(timedlayspawn);
        enemySpawner.SpawnAroundPlayer(score);
    }

}
