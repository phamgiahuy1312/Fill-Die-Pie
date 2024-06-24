using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float timedlayspawn = 2f;
    private EnenySpawner enemySpawner;
    void Start()
    {
        enemySpawner = FindObjectOfType<EnenySpawner>();
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
        enemySpawner.SpawnAroundPlayer();
    }

}
