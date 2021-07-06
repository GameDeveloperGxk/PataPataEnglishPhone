using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public Button buttonStart;
    public Button buttonSound;
    public Button buttonExit;
    public Image ImageSound;
    public Sprite[] spriteSound;
    

    private void Awake()
    {
    }

    // Use this for initialization
    void Start ()
    {
        buttonStart.onClick.AddListener(StartClick);
        buttonSound.onClick.AddListener(SoundClick);
        buttonExit.onClick.AddListener(ExitClick);
        AudioManager.GetInstance().soundState = 1;
        ImageSound.sprite = spriteSound[AudioManager.GetInstance().soundState];
        
    }

    void StartClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.StartMenu)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.StartMenu, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, true);
        }
    }
    void SoundClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.StartMenu)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            AudioManager.GetInstance().soundState = AudioManager.GetInstance().soundState == 1 ? 0 : 1;
            ImageSound.sprite = spriteSound[AudioManager.GetInstance().soundState];
            Tools.writTxt("nizainane","0001");
        }
    }
    void ExitClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.StartMenu)
        {
            LocalData.GetInstance().SaveLocalData();
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.ExitPanel, true);
        }
    }

}
