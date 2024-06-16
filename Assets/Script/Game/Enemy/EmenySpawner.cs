using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.UIElements;

public class EmenySpawner : MonoBehaviour
{
    public Transform player;
    public RectTransform canvasTransform;
    public float radius = 450f;
    private int numberOfEnemies;
    public GameObject enemyPrefab;
    void Start()
    {
        numberOfEnemies = Random.Range(3,9);
        SpawnAroundPlayer();
    }

    // Update is called once per frame
    void SpawnAroundPlayer()
    {
        float angleStep = 360f / numberOfEnemies;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float angle = i * angleStep;
            float posX = player.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float posY = player.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 enemyPosition = new Vector3(posX, posY, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity, canvasTransform);
            newEnemy.tag = "Enemy";

        }
    }

   
}
