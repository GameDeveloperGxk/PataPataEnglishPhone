using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 胜利页面
/// </summary>
public class WinPanel : MonoBehaviour
{
    public Button btnOK;

    public Animation ani0, ani1;
    public Text textWin, textWan, textStar, textLevel;
    public Image imgStar;

    public float showTime;
    private int curLian;
    private int addNum;

    public Animation ani00,ani11;

    private int playStarTime = 0;
    private int playStarTime2 = 0;
    private float jianScale;

    public Animation aniYellow;

    public GameObject parStar;



    // Use this for initialization
    void Start()
    {
        btnOK.onClick.AddListener(ClickOK);
    }

    /// <summary>
    /// 初始化结束界面数据
    /// </summary>
    public void InitData()
    {
        ani00.gameObject.SetActive(false);
        ani11.gameObject.SetActive(false);
        showTime = 0;
        curLian = 0;
        addNum = 1;
        textWan.gameObject.SetActive(false);
        textStar.gameObject.SetActive(false);
        imgStar.gameObject.SetActive(false);
        textWin.gameObject.SetActive(false);
        btnOK.gameObject.SetActive(false);
        ani0.gameObject.SetActive(false);
        ani1.gameObject.SetActive(false);
        Tools.PlayAnimation(aniYellow, "SLXG-TanChu");
        Invoke("YellowChiXv", 0.15f);
        textLevel.gameObject.SetActive(false);
        textLevel.text = "" + (GameController.GetInstance().currentLevel+1);
        //完美通关 行星+1
        if (GameController.GetInstance().gameTotalSignNum == GameController.GetInstance().gameDestroySignNum)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundGameWinBest);
            ani0.gameObject.SetActive(true);
            ani0.Play("JieSuan-WanMei-TanChu", PlayMode.StopAll);
            //修改数据
            if (LocalData.GetInstance().levelWmState[GameController.GetInstance().currentLevel] == 0)
            {
                LocalData.GetInstance().levelWmState[GameController.GetInstance().currentLevel] = 1;
                GameController.GetInstance().hasWan = true;//有完美通关
            }
        }
        else
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundGameWin);
            ani00.gameObject.SetActive(false);
            ani1.gameObject.SetActive(true);
            ani1.Play("JieSuan-TongGuan-TanChu", PlayMode.StopAll);
        }
        addNum = GameController.GetInstance().gameDestroySignNum / 7;
        if (addNum < 1)
        {
            addNum = 1;
        }
        textWan.gameObject.SetActive(false);
        textWin.gameObject.SetActive(false);
        btnOK.gameObject.SetActive(false);

        playStarTime = 0;
        imgStar.transform.localScale = new Vector3(4,4,4);
        playStarTime2 = 0;
        jianScale = 0;

        parStar.SetActive(false);
        parStar.GetComponent<ParticleSystem>().Stop();

        //第一次玩当前关卡
        if (GameController.GetInstance().currentLevel + 1 == LocalData.GetInstance().GetMaxOpenLevel())
        {
            if(GameController.GetInstance().currentLevel==9||
                GameController.GetInstance().currentLevel == 19 ||
                GameController.GetInstance().currentLevel == 29 ||
                GameController.GetInstance().currentLevel == 39 ||
                GameController.GetInstance().currentLevel == 49 ||
                GameController.GetInstance().currentLevel == 59 ||
                GameController.GetInstance().currentLevel == 69 ||
                GameController.GetInstance().currentLevel == 79 ||
                GameController.GetInstance().currentLevel == 89 ||
                GameController.GetInstance().currentLevel == 99 ||
                GameController.GetInstance().currentLevel == 109 ||
                GameController.GetInstance().currentLevel == 119 ||
                GameController.GetInstance().currentLevel == 129 ||
                GameController.GetInstance().currentLevel == 139 ||
                GameController.GetInstance().currentLevel == 149 )
            {//第一次玩最后一个大关
                if(LocalData.GetInstance().starNum >=
                    GameController.GetInstance().starNum
                    [(int)GameController.GetInstance().currentLevel / 10])//星星数量达到要求才会开启新关卡
                {
                    LocalData.GetInstance().SetMaxOpenLevel(GameController.GetInstance().currentLevel + 2);
                    GameController.GetInstance().hasNew = true;//新关卡开启
                    GameController.GetInstance().hasNewLast = true;//新的大关卡开启
                }
                else
                {
                    GameController.GetInstance().hasNotOpenNext = true;
                }           
            }
            else
            {//如果是平常关卡
               
                if (GameController.GetInstance().currentLevel < 80)
                {
                    LocalData.GetInstance().SetMaxOpenLevel(GameController.GetInstance().currentLevel + 2);
                    GameController.GetInstance().hasNew = true;
                }
                else
                {
                    if (LocalData.GetInstance().stateBuy == 1 )//已经购买的话才会开启
                    {
                        LocalData.GetInstance().SetMaxOpenLevel(GameController.GetInstance().currentLevel + 2);
                        GameController.GetInstance().hasNew = true;
                    }
                }           
            }            
        }
        //保存本地数据
        LocalData.GetInstance().SaveLocalData();
        AudioManager.GetInstance().StopMusic();

        
        if (LocalData.GetInstance().GetMaxOpenLevel() == 2
            ||LocalData.GetInstance().GetMaxOpenLevel() == 3
            ||LocalData.GetInstance().GetMaxOpenLevel() == 12
            || LocalData.GetInstance().GetMaxOpenLevel() == 22
            || LocalData.GetInstance().GetMaxOpenLevel() == 32
            || LocalData.GetInstance().GetMaxOpenLevel() == 42
            || LocalData.GetInstance().GetMaxOpenLevel() == 52
            || LocalData.GetInstance().GetMaxOpenLevel() == 62
            || LocalData.GetInstance().GetMaxOpenLevel() == 72
            || LocalData.GetInstance().GetMaxOpenLevel() == 82
            || LocalData.GetInstance().GetMaxOpenLevel() == 92
            || LocalData.GetInstance().GetMaxOpenLevel() == 102
            || LocalData.GetInstance().GetMaxOpenLevel() == 107
            )
        {
            UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().showBook = true;
        }
    }

    /// <summary>
    /// 大背景动画进入持续状态
    /// </summary>
    void YellowChiXv()
    {
        Tools.PlayAnimation(aniYellow, "SLXG-ChiXu");
    }

    /// <summary>
    /// 点击OK按钮
    /// </summary>
    private void ClickOK()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Win)
        {
            LeanTween.scale(btnOK.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            
            UIManager.GetInstance().panelCloud.SetActive(true);
            UIManager.GetInstance().panelCloud.GetComponent<PanelCloud>().Play0(1);
            Invoke("ChangeView", 1f);
        }
    }

    /// <summary>
    /// 切换显示,延迟执行
    /// </summary>
    void ChangeView()
    {
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Win, false);
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, false);
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, true);
        AudioManager.GetInstance().PlayMusic(AudioManager.MusicUI);
    }

    // Update is called once per frame
    void Update()
    {
        showTime += Time.deltaTime;
        if(showTime > 1.0f&& showTime<1.5f)
        {
            if (GameController.GetInstance().gameTotalSignNum == GameController.GetInstance().gameDestroySignNum)
            {
                ani0.Play("JieSuan-WanMei-ChiXu", PlayMode.StopAll);
                parStar.SetActive(true);
                parStar.GetComponent<ParticleSystem>().Stop();
                parStar.GetComponent<ParticleSystem>().Play();
            }
            else
            {                
                ani1.Play("JieSuan-TongGuan-ChiXu", PlayMode.StopAll);
            }
        }else if(showTime >= 1.5f&& showTime < 2.0f)
        {
            if (playStarTime2 == 0)
            {
                playStarTime2++;                
                
            }
            curLian += addNum;
            if (curLian > GameController.GetInstance().gameDestroySignNum)
            {
                curLian = GameController.GetInstance().gameDestroySignNum;
            }
            if (GameController.GetInstance().gameTotalSignNum == GameController.GetInstance().gameDestroySignNum)
            {
                textWan.text = "Combo ： " + curLian;
                textWan.gameObject.SetActive(true);
                textLevel.gameObject.SetActive(true);
            }
            else
            {
                textWin.text = "Combo ： " + curLian;
                textWin.gameObject.SetActive(true);
                textLevel.gameObject.SetActive(true);
            }            
        }
        else if (showTime >= 2.0f&& showTime < 2.5f)
        {
            if (GameController.GetInstance().gameTotalSignNum == GameController.GetInstance().gameDestroySignNum)
            {
                //if (playStarTime==0)
                //{
                //    playStarTime++;
                //LeanTween.scale(imgStar.gameObject, new Vector3(1, 1, 1), 0.5f).setLoopPingPong(1);
                //ani11.gameObject.SetActive(true);
                //ani11.Play("JieSuanXingXing", PlayMode.StopAll);
                //}                
                imgStar.gameObject.SetActive(true);
                jianScale += 0.3f;
                if (jianScale > 3)
                {
                    jianScale = 3;
                    textStar.gameObject.SetActive(true);
                }
                imgStar.transform.localScale = new Vector3(4-jianScale, 4 - jianScale, 4 - jianScale);
            }
            else
            {
                btnOK.gameObject.SetActive(true);
            }
        }
        else if (showTime > 2.5f)
        {
            imgStar.transform.localScale = new Vector3(1, 1, 1);
            btnOK.gameObject.SetActive(true);
        }
    }
}
