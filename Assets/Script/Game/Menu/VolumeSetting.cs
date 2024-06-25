// JavaScript source code
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    private int switchStateSFX = -1;
    private int switchStateMusic = -1;
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

        switchStateSFX = PlayerPrefs.GetInt("SFX", -1); 
        switchStateMusic = PlayerPrefs.GetInt("Music", -1);  

        SetSwitchState(switchBtnSFX, BackgroundSFX, switchStateSFX, true);
        SetSwitchState(switchBtnMusic, BackgroundMusic, switchStateMusic, true);

        audioManager.SFXSource.mute = switchStateSFX == -1;
        audioManager.SoundBackground.mute = switchStateMusic == -1;
    }

    public void OnSwitchButtonSFX()
    {
        audioManager.PlaySFX(audioManager.Touch);
        switchStateSFX = -switchStateSFX;
        SetSwitchState(switchBtnSFX, BackgroundSFX, switchStateSFX, false);
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
        float targetX = state == -1 
            ? Math.Abs(switchButton.transform.localPosition.x) 
            : -Math.Abs(switchButton.transform.localPosition.x);

        if (instant)
        {
            switchButton.transform.localPosition = new Vector3(targetX, switchButton.transform.localPosition.y, switchButton.transform.localPosition.z);
        }
        else
        {
            switchButton.transform.DOLocalMoveX(targetX, 0.2f);
        }

        Image backgroundSwitchImage = backgroundSwitch.GetComponent<Image>();
        if (state == -1)
        {
            //set background color #1E1D6D
            backgroundSwitchImage.color = new Color32(30, 29, 109, 255);

        }
        else
        {
            //set background color #CECECE 
            backgroundSwitchImage.color = new Color32(206, 206, 206, 255);
        }
    }
}
