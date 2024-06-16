using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bullets : MonoBehaviour
{
    public Transform player; 
    public float radius = 110f; 
    public float speed = 2f;
    private float angle;
    private bool isShooting = false;
    public float ShootForce = 1000f;
    private Vector3 initialPosition; //vị trí ban đầu của viên đạn
    private Vector3 shootDirection; //hướng bắn của viên đạn



    void Start()
    {
        initialPosition = transform.position;
        transform.position = player.position + new Vector3(radius, 0, 0);
    
    }
    void Update()
    {
        if(!isShooting)
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
    }

    public void ShootBullet()
    {
        isShooting = true;
        shootDirection = (transform.position - player.position).normalized;
        transform.position += shootDirection * ShootForce * Time.deltaTime;
        if (transform.position.x < 0 || 
            transform.position.x > Screen.width || 
            transform.position.y < 0 || 
            transform.position.y > Screen.height)
        {
            //ResetBullet();
            SceneManager.LoadScene("Menu");
        }
    }

    public void ResetBullet()
    {
        transform.position = player.position + new Vector3(radius, 0, 0);
        isShooting = false;
    }
    //get color bullet

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Image bullet = GetComponent<Image>();
            Color bulletcolor = bullet.color;
            Image enemy = collision.GetComponent<Image>();
            enemy.color = bulletcolor;
            ResetBullet();
        }
    }
}
