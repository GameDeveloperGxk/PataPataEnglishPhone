using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SettingPanel : MonoBehaviour {

    public Button btn0,btn1,btn2,btn3,btnClose;

	// Use this for initialization
	void Start () {
        btn0.onClick.AddListener(Click0);
        btn1.onClick.AddListener(Click1);
        btn2.onClick.AddListener(Click2);
        btn3.onClick.AddListener(Click3);
        btnClose.onClick.AddListener(Click4);
    }

    public void InitData()
    {
        LocalData.GetInstance().SaveLocalData();
        if (GameController.GetInstance().stateZhen == 1)
        {
            btn0.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            btn0.transform.localScale = new Vector3(1, 1, 1);
        }

        if (AudioManager.GetInstance().soundState == 1)
        {
            btn1.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            btn1.transform.localScale = new Vector3(1, 1, 1);
        }
    }
	
    public void Click0()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        if (btn0.transform.localScale.x == -1)
        {
            btn0.transform.localScale = new Vector3( 1, 1, 1);
            GameController.GetInstance().stateZhen = 0;
        }
        else
        {
            btn0.transform.localScale = new Vector3(-1, 1, 1);
            GameController.GetInstance().stateZhen = 1;
        }
        
    }

    public void Click1()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        if (btn1.transform.localScale.x == -1)
        {
            btn1.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            btn1.transform.localScale = new Vector3(-1, 1, 1);
        }
        AudioManager.GetInstance().SetSoundState(btn1.transform.localScale.x == -1 ? 1 : 0);
    }

    public void Click2()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        Application.Quit();
    }

    public void Click3()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        gameObject.SetActive(false);
    }
    public void Click4()
    {
        LeanTween.scale(btnClose.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.1f).setLoopPingPong(1);
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
