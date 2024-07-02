using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Bullets bulletScript;
    public Rect touchArea;

    void Start()
    {
        bulletScript = FindObjectOfType<Bullets>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && touchArea.Contains(Input.mousePosition))
        {
            bulletScript.ShootBullet();
        }
    }
}
