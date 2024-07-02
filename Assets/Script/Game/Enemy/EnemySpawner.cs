﻿using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public RectTransform canvasTransform;
    public float radius = 450f;
    private int numberOfEnemies = 3;
    public float speedOfRotation = 3f;
    public bool isRotate = false;
    public GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();
    private List<float> angles = new List<float>();
    private float angleStep;
    private int levelCount = 0;

    void Start()
    {
        SpawnAroundPlayer(0);
    }

    void Update()
    {
        if (isRotate)
        {
            RotateEnemiesAroundPlayer(speedOfRotation);
        }
    }

    public void setLevelOfDifficulty(int levelCount)
    {
        if (levelCount >= 3) isRotate = true;

        //int attributeToIncrement = Random.Range(0, 2);
        //switch (attributeToIncrement)
        //{
        //    case 0:
        //        numberOfEnemies = Mathf.Clamp(numberOfEnemies + 1, 1, 8);
        //        break;
        //    case 1:
        //        if (levelCount >= 3)
        //        {
        //            speedOfRotation = Mathf.Clamp(speedOfRotation * 2f, 1f, 240f);
        //        }
        //        break;
        //}
        //this.levelCount++;

        if(levelCount < 3)
        {
            numberOfEnemies++;
        }
        else
        {
            numberOfEnemies = Random.Range(3, 9);
            speedOfRotation = Mathf.Clamp(speedOfRotation * 1.5f, 5f, 240f);
        }

        this.levelCount++;
    }

    public void SpawnAroundPlayer(int level)
    {
        //ClearEnemies();
        angleStep = 360f / numberOfEnemies;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float angle = i * angleStep;
            angles.Add(angle);
            float posX = player.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float posY = player.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 enemyPosition = new Vector3(posX, posY, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity, canvasTransform);
            newEnemy.tag = "Enemy";
            ScaleEnemy(newEnemy, 1.5f);
            enemies.Add(newEnemy);
        }
        setLevelOfDifficulty(level);
    }
    
    private void RotateEnemiesAroundPlayer(float speed)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                // Update the angle for each enemy
                angles[i] += Time.deltaTime * speed;
                float angle = angles[i];

                // Calculate new position
                float posX = player.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                float posY = player.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                enemies[i].transform.position = new Vector3(posX, posY, 0);
            }
        }
    }

    private void ScaleEnemy(GameObject enemy, float scale)
    {
        enemy.transform.localScale = new Vector3(scale, scale, scale);
    }

    //private void ClearEnemies()
    //{
    //    foreach (GameObject enemy in enemies)
    //    {
    //        if (enemy != null)
    //        {
    //            Destroy(enemy);
    //        }
    //    }
    //    enemies.Clear();
    //    angles.Clear();
    //}
}