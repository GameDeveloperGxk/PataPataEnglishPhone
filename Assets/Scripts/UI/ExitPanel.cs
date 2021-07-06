﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    public Button ButtonYes;
    public Button ButtonNo;

    void Start()
    {
        ButtonYes.onClick.AddListener(ButtonYesClick);
        ButtonNo.onClick.AddListener(ButtonNoClick);
    }

    void ButtonYesClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.ExitPanel)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            Application.Quit();
        }
    }
    void ButtonNoClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.ExitPanel)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().currentUIStep = UIManager.UIStep.StartMenu;
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.ExitPanel, false);            
        }
    }
}
