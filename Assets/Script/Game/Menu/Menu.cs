using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Hàm xử lý sự kiện click
    public void ButtonPlayGameClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void ButtonSetting()
    {
        settingPanel.SetActive(true);
    }

    public void ButtonCloseSetting()
    {
        settingPanel.SetActive(false);
    }
}
