using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Bullets bulletScript;

    void Start()
    {
        bulletScript = FindObjectOfType<Bullets>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bulletScript.ShootBullet();
        }
    }
}
