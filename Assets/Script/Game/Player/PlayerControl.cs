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
        if(Options.isDialogActive)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && touchArea.Contains(Input.mousePosition))
        {
            bulletScript.animationPlayer();
            bulletScript.isShooting = true;
        }
    }
}
