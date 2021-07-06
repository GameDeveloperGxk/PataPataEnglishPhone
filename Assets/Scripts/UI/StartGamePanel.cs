using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGamePanel : MonoBehaviour {

    public Text textLevel;
    public Button btn0, btn1,btn2;
    public Button btnClose;

    public GameObject objRect;

    // Use this for initialization
    void Start () {
        btn0.onClick.AddListener(Click0);
        btn1.onClick.AddListener(Click1);
        //btn2.onClick.AddListener(Click2);
        btnClose.onClick.AddListener(Click2);
    }

    private float _scale;
    public void InitData()
    {
        _scale = 0.2f;
        objRect.transform.localScale = Vector3.one * _scale;
        textLevel.text = "Level" + (GameController.GetInstance().currentLevel+1);
        if (GameController.GetInstance().currentLevel <= 79)
        {
            if (LocalData.GetInstance().timePlayGame >= 5)
            {
                btn0.gameObject.SetActive(true);
                btn1.gameObject.SetActive(false);
            }
            else
            {
                btn1.gameObject.SetActive(true);
                btn0.gameObject.SetActive(false);
            }
        }
        else
        {
            btn1.gameObject.SetActive(true);
            btn0.gameObject.SetActive(false);
        }
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isCanClick = true;
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isPlayMov = false;

        if (LocalData.GetInstance().GetMaxOpenLevel() == 1)
        {
            Invoke("Click0", 5.0f);
        }
        AudioManager.GetInstance().PlaySound(AudioManager.SoundUIShow);
    }

    //看广告
    public void Click0()
    {
        LeanTween.scale(btn0.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
        //AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        Click00();
        AudioManager.GetInstance().PlaySound(AudioManager.SoundBtnStart);
    }

    public void Click1()
    {
        LeanTween.scale(btn1.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
        //AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        Click00();
        AudioManager.GetInstance().PlaySound(AudioManager.SoundBtnStart);
    }

    public void Click2()
    {
        if (LocalData.GetInstance().GetMaxOpenLevel() == 1)
        {
            return;
        }
        LeanTween.scale(btn2.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
        //AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        gameObject.SetActive(false);
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isCanClick = true;
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isPlayMov = false;
    }

    private bool isClick = false;
    public void Click00()
    {
        if(isClick == false)
        {
            isClick = true;
            UIManager.GetInstance().panelCloud.SetActive(true);
            UIManager.GetInstance().panelCloud.GetComponent<PanelCloud>().Play0(1.5F);
            Invoke("Click222", 1.0f);
        }       
    }

    public void Click222()
    {
        isClick = false;
        if(GameController.GetInstance().currentLevel >80 )
        {
            if (LocalData.GetInstance().stateBuy == 0)
            {
                UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelBuy.SetActive(true);
            }
            else
            {
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, false);
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, true);
                GameController.GetInstance().playPage =
                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().currentPage;
                //AudioManager.GetInstance().PlaySound(AudioManager.SoundBtnStart);
            }
        }
        else
        {
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, true);
            GameController.GetInstance().playPage =
                UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().currentPage;
            
        }
            
    }
    

    // Update is called once per frame
    void Update () {
        _scale += 0.1f;
        if (_scale > 1)
        {
            _scale = 1;
        }
        objRect.transform.localScale = Vector3.one * _scale;
    }
}
