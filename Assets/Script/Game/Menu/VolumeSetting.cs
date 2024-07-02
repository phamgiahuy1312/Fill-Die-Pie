// JavaScript source code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    private int switchStateSFX = 1;
    private int switchStateMusic = 1;
    public GameObject switchBtnSFX;
    public GameObject switchBtnMusic;
    public GameObject BackgroundSFX;
    public GameObject BackgroundMusic;

    private AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        audioManager = AudioManager.instance;
        // Load saved states from PlayerPrefs: 1(enabled)
        switchStateSFX = PlayerPrefs.GetInt("SFX", 1); 
        switchStateMusic = PlayerPrefs.GetInt("Music", 1);  
        // Set state of switch buttons
        SetSwitchState(switchBtnSFX, BackgroundSFX, switchStateSFX, true);
        SetSwitchState(switchBtnMusic, BackgroundMusic, switchStateMusic, true);
        // Mute audio sources
        audioManager.SFXSource.mute = switchStateSFX == -1;
        audioManager.SoundBackground.mute = switchStateMusic == -1;


    }

    public void OnSwitchButtonSFX()
    {

        if(switchStateSFX == -1)
        {
            audioManager.PlaySFX(audioManager.Touch);
        }
      
        // Play touch sound
        audioManager.PlaySFX(audioManager.Touch);
        // Switch state
        switchStateSFX = -switchStateSFX;
        // Set switch state
        SetSwitchState(switchBtnSFX, BackgroundSFX, switchStateSFX, false);
        // Mute audio source
        audioManager.SFXSource.mute = switchStateSFX == -1;
        

        // Save state
        PlayerPrefs.SetInt("SFX", switchStateSFX);
    }

    public void OnSwitchButtonMusic()
    {
        audioManager.PlaySFX(audioManager.Touch);
        switchStateMusic = -switchStateMusic;
        SetSwitchState(switchBtnMusic, BackgroundMusic, switchStateMusic, false);
        audioManager.SoundBackground.mute = switchStateMusic == -1;

        // Save state
        PlayerPrefs.SetInt("Music", switchStateMusic);
    }

    private void SetSwitchState(GameObject switchButton, GameObject backgroundSwitch ,int state, bool instant)
    {
        // Set position of switch button
        float targetX = state == 1 
            // if state is 1, set position to positive value of x
            ? Math.Abs(switchButton.transform.localPosition.x) 
            // if state is -1, set position to negative value of x
            : -Math.Abs(switchButton.transform.localPosition.x);

       // Move switch button to target position
        if (instant)
        {
            // if instant is true, move switch button animation
            switchButton.transform.DOLocalMoveX(targetX, 0);
        }
        else
        {
            // if instant is false, move switch button with animation
            switchButton.transform.DOLocalMoveX(targetX, 0.2f);
        }

        Image backgroundSwitchImage = backgroundSwitch.GetComponent<Image>();

        //on
        if (state == 1)
        {
            //set background color #5AC6FE
            backgroundSwitchImage.color = new Color32(90, 198, 254, 255);
        }
        //off
        else
        {
            //set background color #D9D9D9
            backgroundSwitchImage.color = new Color32(217, 217, 217, 255);
        }
    }
}
