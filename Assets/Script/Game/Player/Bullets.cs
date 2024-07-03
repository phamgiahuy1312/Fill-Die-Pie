using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bullets : MonoBehaviour
{
    public Transform player; 
    [SerializeField] float radius = 130f; 
    public float speed = 2f;
    private float angle;
    public bool isShooting = false;
    public float ShootForce = 1000f;
    private Vector3 initialPosition; //vị trí ban đầu của viên đạn
    public Vector3 shootDirection; //hướng bắn của viên đạn
    private GameManager gameManager;
    AudioManager audioManager;

    private Vector3 originalPlayerScale;

    private Options options;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        initialPosition = transform.position;
        transform.position = player.position + new Vector3(radius, 0, 0);
        gameManager = FindObjectOfType<GameManager>();
        originalPlayerScale = player.localScale;
        options = FindObjectOfType<Options>();
    }
    void Update()
    {
        if (!isShooting)
        {
            rotateBullet();
        }
        else
        {
            ShootBullet();
        }
    }
    public void rotateBullet()
    {
        angle += speed * Time.deltaTime;
        float x = player.position.x + Mathf.Cos(angle) * radius;
        float y = player.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, 0f);

        //rotate player follow bullet
        Vector3 direction = transform.position - player.position;
        float angleToBullet = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        player.rotation = Quaternion.Euler(new Vector3(0, 0, angleToBullet));
    }
    public void ShootBullet()
    {
        isShooting = true;
        shootDirection = (transform.position - player.position).normalized;
        transform.position += shootDirection * ShootForce * Time.deltaTime;

        //scale player theo trục x
        player.localScale = new Vector3(originalPlayerScale.x + 0.3f, originalPlayerScale.y, originalPlayerScale.z);
        //reset player scale
        StartCoroutine(RestorePlayerScale(0.2f));
        //check if bullet is out of screen
        if (transform.position.x < 0 || 
            transform.position.x > Screen.width || 
            transform.position.y < 0 || 
            transform.position.y > Screen.height)
        {
            BulletOutScreen();
        }
    }

    public void BulletOutScreen()
    {
        ResetBullet();
        HealthManager.health--;
        if (HealthManager.health <= 0)
        {
            if (options.TouchHealthCount < 2)
            {
                options.PanelGameOver();
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            ResetBullet();
        }
    }
    
    //reset player scale
    IEnumerator RestorePlayerScale(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.localScale = originalPlayerScale;
    }

    public void ResetBullet()
    {
        transform.position = player.position + new Vector3(radius, 0, 0);
        isShooting = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Shooted"))
        {
            audioManager.PlaySFX(audioManager.ShootEnemy);
            
            //change color of enemy
            Image bullet = GetComponent<Image>();
            Color bulletcolor = bullet.color;
            Image enemy = collision.GetComponent<Image>();
            enemy.color = bulletcolor;

            //scale enemy
            Vector3 originalScale = enemy.transform.localScale;
            enemy.transform.localScale = originalScale * 1.5f;
            //reset enemy scale
            StartCoroutine(RestoreEnemyScale(enemy, originalScale, 0.1f));


            gameManager.OnBulletHitEmeny(collision.gameObject);
            ResetBullet();

        }
    }

    IEnumerator RestoreEnemyScale(Image enemy, Vector3 originalScale, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.transform.localScale = originalScale;
    }
}
