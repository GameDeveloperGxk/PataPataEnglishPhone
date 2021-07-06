using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonLevel : MonoBehaviour {
    private int level;
    public Text textLevel;

    public Animation aniStar;   
    public Animation aniBlack, aniYellow;
    public GameObject parCicle;

    private string aa ;

    // Use this for initialization
    void Start () {
        GetComponent<Button>().onClick.AddListener(ClickLevel);
	}

    public void InitData(int _level)
    {
        level = _level;
        aa = level + 1 + "";
        textLevel.text = "<size=45>"+aa+"</size>";
        aniStar.gameObject.SetActive(false);
        aniBlack.gameObject.SetActive(false);
        aniYellow.gameObject.SetActive(false);
        parCicle.SetActive(false);

        //当前是全部通关的关卡，并且有新大关卡开启，则本地图内所有关卡为黄色，只判断是否有星星
        if(GameController.GetInstance().hasNewLast&&
            UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().playState==0)
        {
            aniYellow.gameObject.SetActive(true);
            //完美通关星星
            if (LocalData.GetInstance().levelWmState[level] == 0)
            {
                aniStar.gameObject.SetActive(true);
            }
            else
            {
                aniStar.gameObject.SetActive(false);
            }
        }
        else//正常情况，不涉及动画播放的逻辑
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == level + 1)//如果是当前关卡，显示圆圈
            {
                parCicle.SetActive(true);
                aniBlack.gameObject.SetActive(true);
                aniBlack.Play("WJS-WXZ", PlayMode.StopAll);
                textLevel.text = "<size=60>" + aa + "</size>";
            }
            else if (LocalData.GetInstance().GetMaxOpenLevel() > level)//黄色还是 白色的逻辑
            {
                aniYellow.gameObject.SetActive(true);               
            }
            else
            {
                aniBlack.gameObject.SetActive(true);
                aniStar.gameObject.SetActive(true);
            }
            //没有全部通关，并且有新关卡开启的地图
            if (GameController.GetInstance().hasNew && GameController.GetInstance().hasNewLast==false&&
                UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().playState == 0)
            {
                if (LocalData.GetInstance().GetMaxOpenLevel() == level + 1)//要解锁的 新的
                {
                    parCicle.SetActive(false);
                    aniBlack.gameObject.SetActive(true);
                    AnimationState state = aniBlack["WJS-WXZ"];
                    state.time = 0;
                    aniBlack.Stop();
                    aniYellow.gameObject.SetActive(false);
                    aniStar.gameObject.SetActive(false);
                }
            }
            if (LocalData.GetInstance().levelWmState[level] == 0)
            {
                aniStar.gameObject.SetActive(true);
            }
        }        
    }

    public void Play0()
    {
        parCicle.SetActive(false);
        aniStar.gameObject.SetActive(false);
        aniBlack.gameObject.SetActive(false);
        aniYellow.gameObject.SetActive(true);
        aniYellow.Play("YJS-WXZ",PlayMode.StopAll);
    }

    public void Play1()
    {
        parCicle.SetActive(true);
        aniStar.gameObject.SetActive(true);
        aniBlack.gameObject.SetActive(true);
        aniYellow.gameObject.SetActive(false);
        aniBlack.Play("WJS-WXZ", PlayMode.StopAll);
        textLevel.text = "<size=60>" + aa + "</size>";
    }

    public void ClickLevel()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.SelectLevel)
        {
            if(UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isPlayMov == true)
            {
                return;
            }
            if (UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isCanClick == false)
            {
                return;
            }
            if (LocalData.GetInstance().GetMaxOpenLevel() == 1 && LocalData.GetInstance().playGameTime <= 1)
            {
                return;
            }
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            if (LocalData.GetInstance().GetMaxOpenLevel() > level)
            {
                //进行精细划分，看广告，还是买游戏，还是进游戏                
                GameController.GetInstance().currentLevel = level;
                aniYellow.Play("YJS-DJ", PlayMode.StopAll);
                if (level <= 79)
                {
                    if (LocalData.GetInstance().timePlayGame >= 5)
                    {
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.SetActive(true);
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.GetComponent<StartGamePanel>().InitData();
                    }
                    else
                    {
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.SetActive(true);
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.GetComponent<StartGamePanel>().InitData();
                    }
                }
                else
                {
                    if (LocalData.GetInstance().stateBuy == 1)
                    {
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.SetActive(true);
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.GetComponent<StartGamePanel>().InitData();
                    }
                    else
                    {
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelBuy.SetActive(true);
                    }
                }
            }
            else
            {
                aniBlack.Play("WJS-DJ", PlayMode.StopAll);
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "Levels not unlocked yet");
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
