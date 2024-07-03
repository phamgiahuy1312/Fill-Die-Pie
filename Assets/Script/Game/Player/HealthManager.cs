using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public static int health = 2;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private Options options;

    void Awake()
    {
        health = 2;
    }
    void Start()
    {
        options = FindObjectOfType<Options>();
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
        if(health < 2)
        {
            options.StateHealthOption(true);
        }
        else
        {
           options.StateHealthOption(false);
        }
    }
}
