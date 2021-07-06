using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 游戏页面
/// </summary>
public class Game : MonoBehaviour
{
    public enum GameState
    {
        GameTask,
        GamePause,
        GameWin,
        GameLose,
        GameWait,
        GameShoot,
    }

    public GameState currentGameState = GameState.GameWait;

    public Button buttonShoot;
    public Button buttonHelp;
    public Button buttonSet;


    public GameObject unitCiclePanel;
    public GameObject unityCiclePrefab;

    public ShootSign shoot;

    //设计开始后的等待时间，等待时间超过两秒判定失败，有音符被击中则时间重置
    public float shootWaitTime = 0;


    //音符箭头预制件
    public GameObject shootSignPrefab;
    public GameObject shootSignPanel;

    //音符箭头预制件
    public GameObject shootSignJHPrefab;
    public GameObject shootSignJHPanel;

    //起始触发点
    public int startLine, startRow;

    //public Animation aniShootBtn;
    //public GameObject shootAniParBtn;

    //是否击中了核心音符
    public bool isHitHeXinSign = false;

    private int signIndex = 0;

    //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    public GameObject chuanParent;
    public GameObject chuanPrafab;

    //游戏进行时间，用来记录游戏时间的
    public float gameTime = 0;

    public Image imgMapBack;
    public Image imgMapUp;
    public Sprite[] spriteMapBack;
    public Sprite[] spriteMapUp;

    public float moveNum = 0;

    public Image imgRect;

    public GameObject img0, img1, img2;

    public GameObject RewardPanel;
    public GameObject rewardCoinPrefab;

    public GameState stepState;

    public GameObject[] effectBack;

    public GameObject effChuan;

    public GameObject lianjiPanel;
    private int lianjiNum;
    public Image img00, img11, img22;
    public Sprite[] spr00, spr11, spr22;

    public float showLianjiTime;

    public float timeFuzhu;
    public int fuzhuTipState = 0;
    public GameObject fuzhiKuang, fuzhuKai, fuzhuGuan;
    public Text textFuzhu;

    public float zhenTime;
    public int zhenState;
    public int zhenTimes;
    public float offX, offY;

    public GameObject hand;

    float _Movex = 0;

    public Text textLevel;

    public Animation aniZheZhao;

    public GameObject effTie;

    public int mapImdex = 0;
    //新手话语引导文本
    public Text textZhiYin;
    public Text textZhiYinClick;
    public Button buttonZhiYinNext;

    /// <summary>
    /// 用来急速每个小球是第几个被生成的
    /// </summary>
    private int[,] mapDataArrayIndex = new int[7, 5];

    /// <summary>
    /// 第一个被激活的在第几行
    /// </summary>
    private int firstJH_Line = 0;

    public bool isShowGuid = false;
    /// <summary>
    /// 用来计时激活动画播放
    /// </summary>
    public float jhTime;
    /// <summary>
    /// 每次激活的数量 用来判断播放正确音效的0：没有激活新的 1-100：普通激活 1000>：终点被激活
    /// </summary>
    public float jhNum;
    /// <summary>
    /// 终点球是第几个
    /// </summary>
    public int endIndex;
    public GameObject objWanMeiMov;
    /// <summary>
    /// init data finish
    /// </summary>
    public bool isInitDataFinish;
    /// <summary>
    /// 摆动动画
    /// </summary>
    public GameObject objMoveBD;
    void Start()
    {
        buttonHelp.onClick.AddListener(ClickHelp);
        buttonSet.onClick.AddListener(ClickSet);
    }


    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    public void InitGame()
    {
        Debug.Log("Game Class InitGame function");
        isInitDataFinish = false;
        AudioManager.GetInstance().PlayMusic(AudioManager.MusicGame0);
        GetComponent<Animation>().Stop();
        mapImdex = new int[]
        { 0,0,0,0,0,0,0,0,0,0,//密林
        0,0,0,0,0,0,0,0,0,0,//密林
        6,6,6,6,6,6,6,6,6,6,//沙丘
        6,6,6,6,6,6,6,6,6,6,//沙丘
        2,2,2,2,2,2,2,2,2,2,//古城
        4,4,4,4,4,4,4,4,4,4,//传送
        5,5,5,5,5,5,5,5,5,5,//雪地
        5,5,5,5,5,5,5,5,5,5,//雪地
        3,3,3,3,3,3,3,3,3,3,//海底
        3,3,3,3,3,3,3,3,3,3,//海底
        1,1,1,1,1,1,1,1,1,1,//水面
        7,7,7,7,7,7,7,7,7,7,//通用
        7,7,7,7,7,7,7,7,7,7,//通用
        7,7,7,7,7,7,7,7,7,7,//通用
        7,7,7,7,7,7,7,7,7,7,//通用
        }[GameController.GetInstance().currentLevel];
        imgMapBack.sprite = spriteMapBack[mapImdex];
        imgMapUp.sprite = spriteMapUp[mapImdex];

        effectBack[0].SetActive(false);
        effectBack[1].SetActive(false);
        effectBack[2].SetActive(false);
        effectBack[3].SetActive(false);
        effectBack[4].SetActive(false);
        effectBack[5].SetActive(false);
        effectBack[6].SetActive(false);
        effectBack[7].SetActive(false);
        effectBack[mapImdex].SetActive(true);
        effTie.SetActive(false);
        if (UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().currentPage == 4)
        {
            effectBack[mapImdex].SetActive(false);
            effTie.SetActive(true);
        }
        effChuan.gameObject.SetActive(false);
        imgMapUp.gameObject.SetActive(true);
        if (GameController.GetInstance().currentLevel >= 50
            && GameController.GetInstance().currentLevel < 60)
        {
            effChuan.gameObject.SetActive(true);
            imgMapUp.gameObject.SetActive(false);
        }

        signIndex = 0;
        currentGameState = GameState.GameWait;
        //AudioManager.GetInstance().PlayMusic(AudioManager.MusicGame);        
        InitMapData(GameController.GetInstance().currentLevel);

        mapDataArrayIndex = new int[7, 5] {
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1 }};

        //Debug.Log("start打印每个关卡最后一行第一个被触发的行数索引======================================");
        //string ee = "";
        //int a = 0;
        //for (int j = 0; j < 150; j++)
        //{
        //    a = 0;
        //    InitMapData(j);
        //    for (int i = 0; i < 7; i++)
        //    {
        //        int _id = mapDataArray[i, 2];
        //        if (_id != -1)
        //        {
        //            a = i;
        //        }
        //    }
        //    ee += a + ",";
        //}
        //Debug.Log(ee);
        //Debug.Log("end======================================");

        //初始化单位
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int _id = mapDataArray[i, j];
                if (_id != -1)
                {
                    Vector3 _vec = new Vector3(-360 + j * 180, 658 - i * 180, 0);
                    GameObject _unit = GameObject.Instantiate(unityCiclePrefab, _vec, Quaternion.identity) as GameObject;
                    _unit.transform.SetParent(unitCiclePanel.transform);
                    _unit.transform.localScale = new Vector3(1, 1, 1);
                    _unit.transform.localPosition = _vec;
                    //1000060,1100060,1200060,1300060,1400060,1500060,1600060,1700060,1800060,1900060
                    int _numCi = -1;
                    if (_id > 1000000)
                    {
                        switch (_id)
                        {
                            case 1100060: _numCi = 0; _id = 60; break;
                            case 1200060: _numCi = 1; _id = 60; break;
                            case 1300060: _numCi = 2; _id = 60; break;
                            case 1400060: _numCi = 3; _id = 60; break;
                            case 1500060: _numCi = 4; _id = 60; break;
                            case 1600060: _numCi = 5; _id = 60; break;
                            case 1700060: _numCi = 6; _id = 60; break;
                            case 1800060: _numCi = 7; _id = 60; break;
                            case 1900060: _numCi = 8; _id = 60; break;
                            case 1110060: _numCi = 0; _id = 10060; break;
                            case 1210060: _numCi = 1; _id = 10060; break;
                            case 1310060: _numCi = 2; _id = 10060; break;
                            case 1410060: _numCi = 3; _id = 10060; break;
                            case 1510060: _numCi = 4; _id = 10060; break;
                            case 1610060: _numCi = 5; _id = 10060; break;
                            case 1710060: _numCi = 6; _id = 10060; break;
                            case 1810060: _numCi = 7; _id = 10060; break;
                            case 1910060: _numCi = 8; _id = 10060; break;
                        }
                    }

                    bool _isLock = false;
                    if (_id >= 10000 && _id < 1000000)
                    {
                        _isLock = true;
                        _id -= 10000;
                    }

                    if (_id == 1000)
                    {
                        GameController.GetInstance().index1 = signIndex;
                        GameController.GetInstance().gameTotalSignNum--;
                    }
                    else if (_id == 2000)
                    {
                        GameController.GetInstance().index2 = signIndex;
                        GameController.GetInstance().gameTotalSignNum--;
                    }
                    else if (_id == 3000)
                    {
                        GameController.GetInstance().index3 = signIndex;
                        GameController.GetInstance().gameTotalSignNum--;
                    }
                    else if (_id == 4000)
                    {
                        GameController.GetInstance().index4 = signIndex;
                        GameController.GetInstance().gameTotalSignNum--;
                    }

                    int _firstIndex = new int[] {
                        3,4,4,4,4,4,4,5,6,4,4,3,4,3,4,4,4,4,4,6,3,4,4,4,
                        3,4,4,4,5,4,3,5,5,5,4,5,3,4,3,3,4,3,4,4,4,4,4,3,
                        4,4,4,4,4,4,4,3,3,4,4,3,4,4,4,4,4,4,4,4,4,4,4,4,
                        5,4,5,6,5,4,4,4,4,3,5,4,5,5,4,5,5,5,4,3,4,4,5,4,
                        4,4,5,3,4,4,5,5,5,5,4,6,4,5,4,4,5,4,5,5,4,6,5,4,
                        4,3,4,4,4,3,3,3,4,4,5,5,3,5,5,5,5,5,4,5,4,3,5,6,
                        5,5,6,5,6,6
                    }[GameController.GetInstance().currentLevel];

                    firstJH_Line = _firstIndex;

                    signIndex++;
                    _unit.GetComponent<UnitCicle>().InitData(_id, i, j, _isLock, _numCi, signIndex,
                        (i == _firstIndex && j == 2) ? true :
                        false);
                    mapDataArrayIndex[i, j] = signIndex;
                    if (_id != 0)
                    {
                        GameController.GetInstance().gameTotalSignNum++;
                    }
                    
                }
            }
        }
        objWanMeiMov.SetActive(false);
        objWanMeiMov.transform.position = GetPositionOfIndex(endIndex);

        //初始化所有小球之后，激活第一个小球
        ResetJHFirst(0);

        if (GameController.GetInstance().currentLevel == 0 ||
            GameController.GetInstance().currentLevel == 10 ||
            GameController.GetInstance().currentLevel == 20 ||
            GameController.GetInstance().currentLevel == 30 ||
            GameController.GetInstance().currentLevel == 40 ||
            GameController.GetInstance().currentLevel == 50 ||
            GameController.GetInstance().currentLevel == 60 ||
            GameController.GetInstance().currentLevel == 70 ||
            GameController.GetInstance().currentLevel == 80 ||
            GameController.GetInstance().currentLevel == 90 ||
            GameController.GetInstance().currentLevel == 100 )
        {
            InItChuanFuZhu();
        }
        shoot.InitData();

        isHitHeXinSign = false;
        gameTime = 0;
        buttonShoot.GetComponent<ButtonStart>().InitData();
        imgRect.transform.localPosition = new Vector3(-500, -7, 0);
        moveNum = 600 / GameController.GetInstance().scoreTotal;
        if (moveNum < 1)
        {
            moveNum = 1;
        }
        img0.transform.localScale = new Vector3(1, 1, 1);
        img1.transform.localScale = new Vector3(1, 1, 1);
        img2.transform.localScale = new Vector3(1, 1, 1);

        _Movex = imgRect.transform.localPosition.x;

        effectBack[mapImdex].SetActive(true);
        if (UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().currentPage == 4)
        {
            effectBack[mapImdex].SetActive(false);

        }

        lianjiNum = 0;
        img00.gameObject.SetActive(false);
        img11.gameObject.SetActive(false);
        img22.gameObject.SetActive(false);


        timeFuzhu = 0;
        fuzhuTipState = 0;
        fuzhiKuang.SetActive(false);
        textFuzhu.gameObject.SetActive(false);
        fuzhuKai.SetActive(true);
        fuzhuGuan.SetActive(false);

        zhenState = 0;
        zhenTime = 0;
        buttonSet.gameObject.SetActive(true);

        textLevel.text = GameController.GetInstance().currentLevel + 1 + "";

        buttonZhiYinNext.onClick.AddListener(ClickZhiYinNext);
        //默认隐藏引导
        HideGuid();
        //显示新手引导内容
        if (GameController.GetInstance().currentLevel == 0)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 1)
            {
                LocalData.GetInstance().guidCurrentStep = -1;
                SetGuidCurrentStep(0);
            }
        }
        else if (GameController.GetInstance().currentLevel == 1)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 2)
            {
                LocalData.GetInstance().guidCurrentStep = 2;
                Invoke("ShowGuid3", 5.0f);
                isShowGuid = true;
            }
        }
        else if (GameController.GetInstance().currentLevel == 10)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 11)
            {
                SetGuidCurrentStep(5);
            }
        }
        else if (GameController.GetInstance().currentLevel == 20)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 21)
            {
                SetGuidCurrentStep(6);
            }
        }
        else if (GameController.GetInstance().currentLevel == 30)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 31)
            {
                SetGuidCurrentStep(7);
            }
        }
        else if (GameController.GetInstance().currentLevel == 40)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 41)
            {
                SetGuidCurrentStep(8);
            }
        }
        else if (GameController.GetInstance().currentLevel == 50)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 51)
            {
                SetGuidCurrentStep(9);
            }
        }
        else if (GameController.GetInstance().currentLevel == 60)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 61)
            {
                SetGuidCurrentStep(10);
            }
        }
        else if (GameController.GetInstance().currentLevel == 70)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 71)
            {
                SetGuidCurrentStep(11);
            }
        }
        else if (GameController.GetInstance().currentLevel == 80)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 81)
            {
                SetGuidCurrentStep(12);
            }
        }
        else if (GameController.GetInstance().currentLevel == 90)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 91)
            {
                SetGuidCurrentStep(13);
            }
        }
        else if (GameController.GetInstance().currentLevel == 100)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 101)
            {
                SetGuidCurrentStep(14);
            }
        }
        else if (GameController.GetInstance().currentLevel == 105)
        {
            if (LocalData.GetInstance().GetMaxOpenLevel() == 106)
            {
                SetGuidCurrentStep(15);
            }
        }
        isInitDataFinish = true;
    }

    private float alphaWM = 0;
    public int stateWM = 0;
    public GameObject wm0, wm1, wm2, wm3, wm4, wm5, wm6, wm7;

    public void SetWanMeiMov(bool _show)
    {
        if(objWanMeiMov.activeSelf != _show)
        {
            objWanMeiMov.SetActive(true);
            if (_show)
            {
                stateWM = 1;
                alphaWM = 0;
                if (currentGameState == GameState.GameWait)
                {
                    AudioManager.GetInstance().PlayMusic(AudioManager.MusicGameWM);
                    Tools.PlayAnimation(objMoveBD.GetComponent<Animation>(), "Dance-Ball");
                }
            }
            else
            {
                stateWM = 2;
                alphaWM = 1;
                if (currentGameState == GameState.GameWait)
                {
                    AudioManager.GetInstance().PlayMusic(AudioManager.MusicGame0);
                    Tools.StopAnimation(objMoveBD.GetComponent<Animation>(), "Dance-Ball");
                }               
            }
            wm0.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm3.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm4.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm5.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm6.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm7.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
        }
    }

    public void UpdateWMMov()
    {
        if (stateWM == 1)//显示
        {
            alphaWM += 0.03f;
            if (alphaWM >= 1)
            {
                alphaWM = 1;
                stateWM = 0;
            }
            wm0.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm3.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm4.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm5.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm6.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm7.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
        }
        else if (stateWM == 2)//消失
        {
            alphaWM -= 0.1f;
            if (alphaWM <= 0)
            {
                alphaWM = 0;
                stateWM = 0;
                objWanMeiMov.SetActive(false);
            }
            wm0.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm3.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm4.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm5.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm6.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
            wm7.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaWM);
           
        }
    }


    /// <summary>
    /// 显示引导3
    /// </summary>
    private void ShowGuid3()
    {
        SetGuidCurrentStep(3);
    }
    //点击使精灵旋转
    //击中悦动精灵即可获胜
    //点击任意位置跳过
    //点击触发攻击
    //已解锁新关卡，点击开始游戏
    //哦？发现了新的精灵，点击尝试控制它
    //这个精灵看来不会被控制，试试其他方法击中悦动精灵吧 ~ 试试用其他方法击中悦动精灵吧~
    // 星星是完美通关后获得的奖励
    //是用来解锁精灵的重要资源
    //"Click to rotate the elves",
    //            "Hit The elantra elves to win",
    //            "Click anywhere to skip",
    //            "Click trigger attack",
    //            "A new level has been unlocked. Click to start the game",
    //            "Oh? Found a new elve, click to try to control it",
    //            "The elve seems to be uncontrollable.Try another method to hit the elantra elves",
    //            "Try hitting the elantra elves in a different way",
    //            "A star is a reward for completing a perfect game",
    //            "An important resource for unlocking elves",


    /// <summary>
    /// 设置新手引导步数，刷新显示
    /// </summary>
    /// <param name="_step"></param>
    public void SetGuidCurrentStep(int _step)
    {
        LocalData.GetInstance().guidCurrentStep = _step;
        hand.SetActive(true);
        aniZheZhao.gameObject.SetActive(true);
        textZhiYin.gameObject.SetActive(true);
        switch (_step)
        {
            case 1://点击任意位置开始游戏
            case 4:
                textZhiYinClick.gameObject.SetActive(true);
                buttonZhiYinNext.gameObject.SetActive(true);
                break;
            case 3://只显示 点击任意位置开始游戏
                textZhiYinClick.gameObject.SetActive(true);
                buttonZhiYinNext.gameObject.SetActive(false);
                break;
            default:
                textZhiYinClick.gameObject.SetActive(false);
                buttonZhiYinNext.gameObject.SetActive(false);
                break;
        }
        //小手位置
        Vector3 _posHand = new Vector3[20] {
                    new Vector3(GetSignPos(1).x + 150,GetSignPos(1).y - 30, 0),
                    new Vector3(GetSignPos(0).x + 150,GetSignPos(0).y - 30, 0),
                    new Vector3(shoot.transform.localPosition.x+250,shoot.transform.localPosition.y-250,0),
                    new Vector3(GetSignPos(5).x + 150,GetSignPos(5).y - 30, 0),
                    new Vector3(GetSignPos(3).x + 150,GetSignPos(3).y - 30, 0),
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0),
                    new Vector3(GetSignPos(1).x + 150,GetSignPos(1).y - 100, 0),//21
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0),//31
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0),//41
                    new Vector3(GetSignPos(7).x + 150,GetSignPos(7).y - 100, 0),//51
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0),//61
                    new Vector3(GetSignPos(4).x + 150,GetSignPos(4).y - 100, 0),//71
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0),//81
                    new Vector3(GetSignPos(4).x + 150,GetSignPos(4).y - 100, 0),//91
                    new Vector3(GetSignPos(4).x + 150,GetSignPos(4).y - 100, 0),//101
                    new Vector3(GetSignPos(6).x + 150,GetSignPos(6).y - 100, 0),//106
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0),//121
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0),//131
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0),//141
                    new Vector3(GetSignPos(2).x + 150,GetSignPos(2).y - 100, 0)//151
                }[_step];
        //暗层位置
        Vector3 _posAn = new Vector3[20] {
                    new Vector3(-1.3f,0.6f,0),
                    new Vector3(-1.4f,0.6f,0),
                    new Vector3(-2.46f,-0.12f,0),
                    new Vector3(-1.5f,0.65f,0),
                    new Vector3(-1.3f,1.6f,0),
                    new Vector3(-1.3f,1.6f,0),
                    new Vector3(-1.6f,1.06f,0),
                    new Vector3(-1.4f,0.9f,0),
                    new Vector3(-1.45f,1.2f,0),
                    new Vector3(-1.7f,0.8f,0),
                    new Vector3(-1.4f,1.1f,0),
                    new Vector3(-1.4f,1.2f,0),
                    new Vector3(-1.47f,1.1f,0),//91
                     new Vector3(-1.47f,1.1f,0),//101
                      new Vector3(-1.47f,1.1f,0),//106
                       new Vector3(-1.47f,1.1f,0),
                        new Vector3(-1.47f,1.1f,0),
                         new Vector3(-1.47f,1.1f,0),
                          new Vector3(-1.47f,1.1f,0),
                           new Vector3(-1.47f,1.1f,0)
            }[_step];
        //介绍文本位置
        Vector3 _posTextInfo = new Vector3[20] {
                    new Vector3(0,-300,0),
                    new Vector3(0,-300,0),
                    new Vector3(0,-500,0),
                    new Vector3(0,-600,0),
                    new Vector3(0,-600,0),
                    new Vector3(0,-600,0),
                    new Vector3(0,-400,0),//21
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
                    new Vector3(0,-400,0),
            }[_step];//介绍文本位置
        float _scaleAn = new int[] {
              30, 10, 10, 10, 30, 10, 10
            , 10, 10, 10, 10, 10, 10
            , 10, 10, 10, 10, 10, 10
            , 10, 10, 10, 10, 10, 10
            , 10, 10, 10, 10, 10, 10}[_step];
        string _info = new string[] {
                "Click to rotate the elves",
                "Hit The elantra elves to win",
                "Click trigger attack",
                "Can not attack, can only be destroyed by the elantra elves",
                "Try another method to hit the elantra elves",
                "Jump to attack an elves",
                "Oh? Found a new elve, click to try to control it",
                 "Oh? Found a new elve, click to try to control it",
                  "Oh? Found a new elve, click to try to control it",
                   "Oh? Found a new elve, click to try to control it",
                    "Oh? Found a new elve, click to try to control it",
                 "Oh? Found a new elve, click to try to control it",
                  "Oh? Found a new elve, click to try to control it",
                   "Oh? Found a new elve, click to try to control it",
                    "Oh? Found a new elve, click to try to control it",
                 "Oh? Found a new elve, click to try to control it",
                  "Oh? Found a new elve, click to try to control it",
                   "Oh? Found a new elve, click to try to control it",
                    "Oh? Found a new elve, click to try to control it",
                 "Oh? Found a new elve, click to try to control it",
                  "Oh? Found a new elve, click to try to control it",
                   "Oh? Found a new elve, click to try to control it",
                    "Oh? Found a new elve, click to try to control it",
                 "Oh? Found a new elve, click to try to control it",
                  "Oh? Found a new elve, click to try to control it",
                   "Oh? Found a new elve, click to try to control it",
            }[_step];

        //刷新位置
        aniZheZhao.gameObject.transform.localScale = Vector3.one * _scaleAn;
        hand.transform.localPosition = _posHand;
        aniZheZhao.transform.localPosition = _posAn;
        textZhiYin.transform.position = _posTextInfo / 100;

        textZhiYin.text = _info;
        isShowGuid = true;
    }

    private Vector3 GetSignPos(int _i)
    {
        if (unitCiclePanel.transform.childCount > _i)
        {
            return unitCiclePanel.transform.GetChild(_i).transform.localPosition;
        }
        return Vector3.zero;
    }


    /// <summary>
    /// 隐藏新手引导显示
    /// </summary>
    public void HideGuid()
    {
        hand.SetActive(false);
        aniZheZhao.gameObject.SetActive(false);
        textZhiYin.gameObject.SetActive(false);
        textZhiYinClick.gameObject.SetActive(false);
        buttonZhiYinNext.gameObject.SetActive(false);
        isShowGuid = false;
    }

    /// <summary>
    /// 点击引导进入下一步
    /// </summary>
    private void ClickZhiYinNext()
    {
        //Debug.Log("指引点击");
        if (buttonZhiYinNext.gameObject.activeSelf)
        {
            switch (LocalData.GetInstance().guidCurrentStep)
            {
                case 0:

                    break;
                case 1:
                    SetGuidCurrentStep(2);
                    break;
                case 4:
                    HideGuid();
                    break;
                default:
                    break;
            }
        }
    }



    /// <summary>
    /// 设置连击
    /// </summary>
    public void SetLianji(int _id)
    {
        if (_id == 18)
        {
            img00.gameObject.SetActive(false);
            img11.gameObject.SetActive(false);
            img22.gameObject.SetActive(false);
        }
        else
        {
            lianjiNum++;
            if (lianjiNum >= 3 && lianjiNum < 10)
            {
                img00.gameObject.SetActive(true);
                img11.gameObject.SetActive(true);
                img22.gameObject.SetActive(false);
                img00.sprite = spr00[0];
                img11.sprite = spr11[lianjiNum - 3];
            }
            else if (lianjiNum >= 10)
            {
                img00.gameObject.SetActive(true);
                img11.gameObject.SetActive(true);
                img22.gameObject.SetActive(true);
                img00.sprite = spr00[1];
                img11.sprite = spr22[lianjiNum / 10];
                img22.sprite = spr22[lianjiNum % 10];
            }
        }
        lianjiPanel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        LeanTween.scale(lianjiPanel, new Vector3(1.7f, 1.7f, 1.7f), 0.1f).setLoopPingPong(1);
        showLianjiTime = 0;

    }

    public void updateLianji()
    {
        if (lianjiNum >= 3)
        {
            showLianjiTime += Time.deltaTime;
            if (showLianjiTime > 0.8f)
            {
                img00.gameObject.SetActive(false);
                img11.gameObject.SetActive(false);
                img22.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 进度条更新
    /// </summary>
    public void MoveRect(int _type)
    {
        img0.transform.localScale = new Vector3(1, 1, 1);
        img1.transform.localScale = new Vector3(1, 1, 1);
        img2.transform.localScale = new Vector3(1, 1, 1);


        if (_type == 1)
        {
            LeanTween.scale(img0.gameObject, new Vector3(2f, 2f, 1f), 0.05f).setLoopPingPong(1);
            LeanTween.scale(img1.gameObject, new Vector3(1f, 2f, 1f), 0.05f).setLoopPingPong(1);
            LeanTween.scale(img2.gameObject, new Vector3(2f, 2f, 1f), 0.05f).setLoopPingPong(1);
            Invoke("ResetRect", 0.15f);
        }
        else
        {
            LeanTween.scale(img0.gameObject, new Vector3(1.5f, 1.5f, 1f), 0.05f).setLoopPingPong(1);
            Invoke("ResetRect", 0.15f);
        }

        _Movex += moveNum;

        if (_Movex > 0)
        {
            _Movex = 0;
        }
        LeanTween.moveLocal(imgRect.gameObject, new Vector3(_Movex, -3, 0), 0.2f);
        //LeanTween.scale(imgRect.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.05f).setLoopPingPong(1);
    }

    public void ResetRect()
    {
        img0.transform.localScale = new Vector3(1, 1, 1);
        img1.transform.localScale = new Vector3(1, 1, 1);
        img2.transform.localScale = new Vector3(1, 1, 1);
    }

    public int randomIndex = 0;

    public GameObject panelShootRandom;
    public GameObject prefabRandom;

    //随机球攻击任意
    public void ShootRandom(Vector3 _ranP)
    {
        int[] a = new int[35];
        int num = 0;
        int _length = unitCiclePanel.transform.childCount;
        for (int i = 0; i < _length; i++)
        {
            UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
            if (_cicle.createID > 0 && _cicle.createID < 1000 && _cicle.imageID != 16)
            {
                if (_cicle.currentUnitState == UnitCicle.unitCicleState.wait)
                {
                    a[num] = i;
                    num++;
                }
            }
        }
        randomIndex = a[Random.Range(0, num)];
        GameObject rand = GameObject.Instantiate(prefabRandom, _ranP, Quaternion.identity) as GameObject;
        rand.transform.SetParent(panelShootRandom.transform);
        rand.transform.localScale = new Vector3(1, 1, 1);
        rand.transform.localPosition = _ranP;

        Vector3 _vec = unitCiclePanel.transform.GetChild(randomIndex).
            GetComponent<UnitCicle>().transform.position;
        rand.GetComponent<RandomStar>().InitData(_vec);

    }

    public void ShootRandom2()
    {
        UnitCicle _cicle = unitCiclePanel.transform.GetChild(randomIndex).GetComponent<UnitCicle>();
        _cicle.SetUnitState(UnitCicle.unitCicleState.shoot);
    }


    /// <summary>
    ///  重置所有球为初始坐标
    /// </summary>
    public void ResetAllBallPositionBase()
    {
        if (objWanMeiMov.activeSelf)
        {
            int _length = unitCiclePanel.transform.childCount;
            for (int i = 0; i < _length; i++)
            {
                UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
                if (_cicle.imageID != 0 && _cicle.imageID != 17 && _cicle.imageID != 18)
                {
                    _cicle.ResetBasePosition();
                }
            }
        }
    }



    /// <summary>
    ///  随机球连接后 切换其他球状态
    /// </summary>
    public void RandomStartLight()
    {
        int _length = unitCiclePanel.transform.childCount;
        for (int i = 0; i < _length; i++)
        {
            UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
            if (_cicle.createID > 0 && _cicle.createID < 1000 && _cicle.imageID != 16)
            {
                if (_cicle.isJH == false)
                {
                    _cicle.SetLignt(true);
                }
            }
        }
    }

    /// <summary>
    /// 刷新某一个音符的状态---设置为激活
    /// </summary>
    /// <param name="line"></param>
    /// <param name="row"></param>
    /// <param name="_jh"></param>
    public void RefushJH(int line, int row, bool _jh)
    {
        if (line >= 0 && row >= 0 && line < 7 && row < 5)
        {
            int _index = mapDataArrayIndex[line, row] - 1;
            if (_index >= 0)
            {
                UnitCicle _cicle = unitCiclePanel.transform.GetChild(_index).GetComponent<UnitCicle>();
                if (_cicle.imageID > 0)
                {
                    _cicle.SetJH(_jh, 0);
                }               
            }
        }
    }

    /// <summary>
    /// 重置所有音符球状态未false
    /// </summary>
    public void ResetJH()
    {
        Debug.Log("======= 重置所有音符球状态start");
        int _length = unitCiclePanel.transform.childCount;
        for (int i = 0; i < _length; i++)
        {
            UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
            if (_cicle.createID > 0 && _cicle.imageID > 0 && _cicle.isFirst == false)
            {
                _cicle.SetJH(false, 999);
                _cicle.SetLignt(false);
                _cicle.jhTimes = 0;
            }
        }
        Debug.Log("======== 重置所有音符球状态finish");
    }

    private bool isJHTime = false;
    /// <summary>
    /// 重置激活状态第一个重置后用来刷新所有的状态
    /// </summary>
    public void ResetJHFirst(int _type)
    {
        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>重新激活第一个小球");
        GameController.GetInstance().gameCurrentJHNum = 1;
        int _index = mapDataArrayIndex[firstJH_Line, 2] - 1;
        unitCiclePanel.transform.GetChild(_index).
            GetComponent<UnitCicle>().RefushAroundJH(true);
        if (_type == 1)
        {
            isJHTime = true;
            jhTime = 0;
            jhNum = 0;
        }
        Debug.Log(">>>>>>>>>>重新激活第一个小球"+ GameController.GetInstance().gameCurrentJHNum);
    }

    public Vector3 GetPositionOfIndex(int _index)
    {
        return unitCiclePanel.transform.GetChild(_index).transform.position;
    }

    private void UpdateJHMov()
    {
        if (isJHTime)
        {
            jhTime += Time.deltaTime;
            if (jhTime > 0.1f)
            {
                int _length = unitCiclePanel.transform.childCount;
                for (int i = 0; i < _length; i++)
                {
                    UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
                    if (_cicle.createID > 0 && _cicle.isFirst == false)
                    {
                        _cicle.ResetOldAndPlayMov();
                    }
                }
                if (jhNum >= 100000)
                {
                    AudioManager.GetInstance().PlaySound(AudioManager.SoundRightEnd);
                }
                else if (jhNum >= 1000 && jhNum < 100000)
                {
                    AudioManager.GetInstance().PlaySound(AudioManager.SoundRightScendEnd);
                }
                else if (jhNum > 0 && jhNum < 1000)
                {
                    AudioManager.GetInstance().PlaySound(AudioManager.SoundRight);
                }
                isJHTime = false;
                jhTime = 0;
                jhNum = 0;
                if (GameController.GetInstance().gameCurrentJHNum == GameController.GetInstance().gameTotalSignNum)
                {
                    SetWanMeiMov(true);
                }
                else
                {
                    SetWanMeiMov(false);
                }
            }
        }
    }

    /// <summary>
    /// 重设置闪烁状态《随机球激活后导致》
    /// </summary>
    public void SetLight()
    {
        int _length = unitCiclePanel.transform.childCount;
        for (int i = 0; i < _length; i++)
        {
            UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
            if (_cicle.createID > 0)
            {
                if (_cicle.isJH == false)
                {
                    _cicle.SetLignt(true);
                }
            }
        }
    }


    /// <summary>
    /// 刷新某一个音符的状态
    /// </summary>
    /// <param name="line"></param>
    /// <param name="row"></param>
    /// <param name="_jh"></param>
    public void RefushBallState(int line, int row)
    {
        if (line >= 0 && row >= 0 && line < 7 && row < 5)
        {
            int _index = mapDataArrayIndex[line, row] - 1;
            if (_index >= 0)
            {
                UnitCicle _cicle = unitCiclePanel.transform.GetChild(_index).GetComponent<UnitCicle>();

                _cicle.SetUnitState(UnitCicle.unitCicleState.shoot);
            }
        }
    }

    /// <summary>
    /// 石头消失
    /// </summary>
    public void DestroyStone()
    {
        //    Invoke("DesStone", 1f);
        //}

        ///// <summary>
        ///// 石头消失
        ///// </summary>
        //public void DesStone()
        //{
        AudioManager.GetInstance().PlaySound(AudioManager.SoundStoneBoom);
        for (int i = 0; i < unitCiclePanel.transform.childCount; i++)
        {
            UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
            if (_cicle.createID == 0)
            {
                if (_cicle.currentUnitState == UnitCicle.unitCicleState.wait)
                {
                    _cicle.Stone();
                }
            }
        }
    }

    //生成替代传送门
    public void CreateChuan(int _id, Vector3 _position)
    {
        GameObject _chuan = GameObject.Instantiate(chuanPrafab, _position, Quaternion.identity) as GameObject;
        _chuan.transform.SetParent(chuanParent.transform);
        _chuan.GetComponent<UnitChuan>().InitData(_id);
        _chuan.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// 播放传送门特效---目标点
    /// </summary>
    private int createID = 0;
    public void PlayChuanJin(int _createID)
    {
        createID = _createID;
        if (createID >= 100 && createID <= 199)
        {
            UnitCicle _cicle = unitCiclePanel.transform.
                GetChild(GameController.GetInstance().index1).GetComponent<UnitCicle>();
            _cicle.parJin11.GetComponent<ParticleSystem>().Stop();
            _cicle.parJin11.GetComponent<ParticleSystem>().Play();
            _cicle.gameObjectChuan1.SetActive(false);
            _cicle.gameObjectChuan11.SetActive(false);
            _cicle.HideFuzhu();
        }
        else if (createID >= 200 && createID <= 299)
        {
            UnitCicle _cicle = unitCiclePanel.transform.
                GetChild(GameController.GetInstance().index2).GetComponent<UnitCicle>();
            _cicle.parJin22.GetComponent<ParticleSystem>().Stop();
            _cicle.parJin22.GetComponent<ParticleSystem>().Play();
            _cicle.gameObjectChuan2.SetActive(false);
            _cicle.gameObjectChuan22.SetActive(false);
            _cicle.HideFuzhu();
        }
        else if (createID >= 300 && createID <= 399)
        {
            UnitCicle _cicle = unitCiclePanel.transform.
                GetChild(GameController.GetInstance().index3).GetComponent<UnitCicle>();
            _cicle.parJin33.GetComponent<ParticleSystem>().Stop();
            _cicle.parJin33.GetComponent<ParticleSystem>().Play();
            _cicle.gameObjectChuan3.SetActive(false);
            _cicle.gameObjectChuan33.SetActive(false);
            _cicle.HideFuzhu();
        }
        else if (createID >= 400 && createID <= 499)
        {
            UnitCicle _cicle = unitCiclePanel.transform.
                GetChild(GameController.GetInstance().index4).GetComponent<UnitCicle>();
            _cicle.parJin44.GetComponent<ParticleSystem>().Stop();
            _cicle.parJin44.GetComponent<ParticleSystem>().Play();
            _cicle.gameObjectChuan4.SetActive(false);
            _cicle.gameObjectChuan44.SetActive(false);
            _cicle.HideFuzhu();
        }
    }

    /// <summary>
    /// 初始化传送门辅助
    /// </summary>
    public void InItChuanFuZhu()
    {
        if (GameController.GetInstance().index1 != -1)
        {
            ShowChuanFuZhu(GameController.GetInstance().index1, GameController.GetInstance().chuanFuZhuID0);
        }
        else if (GameController.GetInstance().index2 != -1)
        {
            ShowChuanFuZhu(GameController.GetInstance().index2, GameController.GetInstance().chuanFuZhuID1);
        }
        else if (GameController.GetInstance().index3 != -1)
        {
            ShowChuanFuZhu(GameController.GetInstance().index3, GameController.GetInstance().chuanFuZhuID2);
        }
        else if (GameController.GetInstance().index4 != -1)
        {
            ShowChuanFuZhu(GameController.GetInstance().index4, GameController.GetInstance().chuanFuZhuID3);
        }
    }

    private void ShowChuanFuZhu(int _index, int _imgID)
    {
        UnitCicle _cicle = unitCiclePanel.transform.GetChild(_index).GetComponent<UnitCicle>();
        _cicle.ShowChuanFuZhu(_imgID);
    }

    /// <summary>
    /// 旋转传送门辅助线
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_imgID"></param>
    public void RotateChuanFuZhu(int _index, float _rotate)
    {
        if (GameController.GetInstance().currentLevel == 0 ||
            GameController.GetInstance().currentLevel == 10 ||
            GameController.GetInstance().currentLevel == 20 ||
            GameController.GetInstance().currentLevel == 30 ||
            GameController.GetInstance().currentLevel == 40 ||
            GameController.GetInstance().currentLevel == 50 ||
            GameController.GetInstance().currentLevel == 60 ||
            GameController.GetInstance().currentLevel == 70 ||
            GameController.GetInstance().currentLevel == 80 ||
            GameController.GetInstance().currentLevel == 90 ||
            GameController.GetInstance().currentLevel == 100 ||
            GameController.GetInstance().currentLevel == 110 ||
            GameController.GetInstance().currentLevel == 120 ||
            GameController.GetInstance().currentLevel == 130 ||
            GameController.GetInstance().currentLevel == 140)
        {
            UnitCicle _cicle = unitCiclePanel.transform.GetChild(_index).GetComponent<UnitCicle>();
            LeanTween.rotate(_cicle.gameObject, new Vector3(0, 0, _rotate), 0.1f);
        }
    }


    /// <summary>
    /// 切换游戏状态
    /// </summary>
    /// <param name="_state"></param>
    public void SetGameState(GameState _state)
    {
        if (currentGameState != _state)
        {
            currentGameState = _state;
            switch (currentGameState)
            {
                case GameState.GameWait:

                    break;
                case GameState.GameShoot:
                    shootWaitTime = 0;
                    break;
                case GameState.GameTask:
                    break;
                case GameState.GamePause:
                    PauseGame();
                    break;
                case GameState.GameWin:
                    WinGame();
                    break;
                case GameState.GameLose:
                    LoseGame();
                    break;
            }
        }
    }

    //点击
    public void ClickShoot()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            if (currentGameState == GameState.GameWait)
            {
                SetGameState(GameState.GameShoot);
                AudioManager.GetInstance().PlaySound(AudioManager.SoundShoot);
                shoot.StartMove();
            }
        }
    }

    public void ShootBall()
    {
        if (currentGameState == GameState.GameWait)
        {
            SetGameState(GameState.GameShoot);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundShoot);
            shoot.StartMove();
        }
    }

    public void ClickHelp()
    {
        if (GameController.GetInstance().currentLevel == 0)
        {
            return;
        }
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            if (currentGameState == GameState.GameWait)
            {
                LeanTween.scale(buttonHelp.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
                AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
                PauseGame();
            }
        }
    }

    public void ClickSet()
    {
        //if (GameController.GetInstance().currentLevel == 0 && LocalData.GetInstance().newState == 0)
        //{
        //    return;
        //}
        //if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        //{
        //    AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);

        //    if (GameController.GetInstance().fuzhuState == 1)
        //    {
        //        GameController.GetInstance().fuzhuState = 0;
        //        fuzhuKai.SetActive(false);
        //        fuzhuGuan.SetActive(true);
        //        fuzhuTipState = 0;
        //        fuzhiKuang.SetActive(false);
        //        textFuzhu.gameObject.SetActive(false);
        //        for (int i = 0; i < unitCiclePanel.transform.childCount; i++)
        //        {
        //            UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
        //            if (_cicle.imageID > 0)
        //            {
        //                if (_cicle.currentUnitState == UnitCicle.unitCicleState.wait)
        //                {
        //                    _cicle.HideFuzhu();
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        GameController.GetInstance().fuzhuState = 1;
        //        fuzhuKai.SetActive(true);
        //        fuzhuGuan.SetActive(false);
        //        for (int i = 0; i < unitCiclePanel.transform.childCount; i++)
        //        {
        //            UnitCicle _cicle = unitCiclePanel.transform.GetChild(i).GetComponent<UnitCicle>();
        //            if (_cicle.imageID > 0)
        //            {
        //                if (_cicle.currentUnitState == UnitCicle.unitCicleState.wait)
        //                {
        //                    _cicle.InitFuZhu(0);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    private void PauseGame()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            //处理游戏逻辑 
            UIManager.GetInstance().pausePanel.GetComponent<PausePanel>().upState = currentGameState;
            currentGameState = GameState.GamePause;
            //UI
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Pause, true);
        }
    }
    /// <summary>
    /// 游戏胜利
    /// </summary>
    private void WinGame()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            //处理游戏逻辑
            Clear();
            currentGameState = GameState.GameWin;

            //UI
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Win, true);
        }
    }

    /// <summary>
    /// 游戏失败
    /// </summary>
    private void LoseGame()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Game)
        {
            //处理游戏逻辑
            Clear();
            currentGameState = GameState.GameLose;

            //UI跳转.
            //AudioManager.GetInstance().StopMusic(AudioManager.MusicGame);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundGameLose);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Lose, true);
        }
    }

    public void ResetShootWaitTime()
    {
        shootWaitTime = 0;
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        switch (currentGameState)
        {
            case GameState.GameTask:
                break;
            case GameState.GameWait:
                UpdateJHMov();
                UpdateWMMov();
                break;
            case GameState.GameShoot:
                shootWaitTime += Time.deltaTime;
                if (shootWaitTime > 3)
                {
                    shootWaitTime = 0;
                    if (isHitHeXinSign == false)
                    {
                        LoseGame();
                    }
                    else
                    {
                        WinGame();
                    }                    
                }
               
                updateLianji();
                if (GameController.GetInstance().fuzhuState == 1)
                {
                    if (fuzhuTipState == 1)
                    {
                        timeFuzhu += Time.deltaTime;
                        if (timeFuzhu > 5.0f)
                        {
                            fuzhuTipState = 0;
                            timeFuzhu = 0;
                            fuzhiKuang.SetActive(false);
                            textFuzhu.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        timeFuzhu += Time.deltaTime;
                        if (timeFuzhu > 1.0f)
                        {
                            fuzhuTipState = 1;
                            timeFuzhu = 0;
                            fuzhiKuang.SetActive(true);
                            textFuzhu.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                }
                UpdateZhen();
                UpdateWMMov();
                break;
            case GameState.GamePause:
                break;
            case GameState.GameWin:
                break;
            case GameState.GameLose:
                break;
        }
    }

    /// <summary>
    /// 游戏结束处理游戏逻辑
    /// </summary>
    public void Clear()
    {
        buttonSet.gameObject.SetActive(false);
        Tools.RemoveAllChildren(shootSignPanel);
        Tools.RemoveAllChildren(shootSignJHPanel);
        Tools.RemoveAllChildren(unitCiclePanel);
        Tools.RemoveAllChildren(RewardPanel);
        Tools.RemoveAllChildren(panelShootRandom);
        Tools.RemoveAllChildren(chuanParent);
        effectBack[0].SetActive(false);
        effectBack[1].SetActive(false);
        effectBack[2].SetActive(false);
        effectBack[3].SetActive(false);
        effectBack[4].SetActive(false);
        effectBack[5].SetActive(false);
        effectBack[6].SetActive(false);
        effectBack[7].SetActive(false);
        effTie.SetActive(false);
    }

    /// <summary>
    /// 发射普通子弹
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_position"></param>
    /// <param name="_rotate"></param>
    /// <param name="_bID"></param>
    public void CreateShootSign(int _id, Vector3 _position, int _rotate, int _bID)
    {
        GameObject _shootSign = GameObject.Instantiate(shootSignPrefab, _position, Quaternion.identity) as GameObject;
        _shootSign.transform.SetParent(shootSignPanel.transform);
        _shootSign.GetComponent<ShootSigns>().InitData(_id, _rotate, _bID * 1000 + Random.Range(1, 50));
        _shootSign.GetComponent<ShootSigns>().transform.localPosition = _position;
    }

    /// <summary>
    /// 发射激活子弹
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_position"></param>
    /// <param name="_rotate"></param>
    /// <param name="_bID"></param>
    public void CreateShootSignJH(int _id, Vector3 _position, int _rotate, int _bID)
    {
        GameObject _shootSign = GameObject.Instantiate(shootSignJHPrefab, _position, Quaternion.identity) as GameObject;
        _shootSign.transform.SetParent(shootSignJHPanel.transform);
        _shootSign.GetComponent<ShootJH>().InitData(_id, _rotate, _bID * 1000 + Random.Range(1, 50));
        _shootSign.GetComponent<ShootJH>().transform.localPosition = _position;
    }

    /// <summary>
    /// 生成音符奖励 {0,1,1,1,1,2,2,3,3,3,3,4,4,4,4,5,5,8,20};
    /// </summary>
    /// <param name="_vec"></param>
    public void CreateYinfu(Vector3 _vec, int _id)
    {
        int[,] type = new int[19, 20] {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,11,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,10,11,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,2,10,11,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,1,2,3,4,5,10,11,12,13,14,15,0,0,0,0,0,0,0,0}
        };
        Debug.Log(_id + "id");

        int _length = GameController.GetInstance().scoreSign[_id];
        for (int i = 0; i < _length; i++)
        {
            GameObject _unit = GameObject.Instantiate(rewardCoinPrefab, _vec, Quaternion.identity) as GameObject;
            _unit.transform.SetParent(RewardPanel.transform);

            _unit.transform.position = _vec;
            _unit.transform.localScale = new Vector3(1, 1, 1);
            _unit.GetComponent<CoinReward>().InitData(_vec, img0, /**type[_id,i]**/Random.Range(0, 10) < 5 ? 0 : 10, _length, i);
        }
    }


    public void startZhen()
    {
        if (zhenState == 0)
        {
            Handheld.Vibrate();
            zhenState = 1;
            zhenTime = 0;
        }

    }
    float[] zhenFu = new float[] { 0.8f,-0.8f,0.6f,-0.6f,0.4f,-0.4f,
                                    0.2f,-0.2f,0.1f,-0.1f,0.05f,-0.05f,0.02f,-0.02f,0,0,0};
    public void UpdateZhen()
    {
        if (zhenState == 1)
        {
            if (GetComponent<Animation>().IsPlaying("Game") == false && zhenTime == 0)
            {
                GetComponent<Animation>().Play("Game", PlayMode.StopAll);
            }
            zhenTime += Time.deltaTime;
            if (zhenTime >= 0.45f)
            {
                GetComponent<Animation>().Stop();
                transform.localPosition = new Vector3(0, 0, 0);
                zhenState = 0;
            }
        }
    }



    //=======================================地图数据=====================================
    private int[,] mapDataArray = new int[7, 5];

    private void InitMapData(int _level)
    {
        switch (_level)
        {
            case 0:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,61,-1,-1},
                    { -1,-1,4,-1,-1 },
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 1:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,1,61,0,-1},
                    { -1,1,0,0,-1 },
                    { -1,1,1,0,-1 },
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 2:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,0,61,0,-1},
                    { -1,0,0,8,-1 },
                    { -1,0,8,-1,-1 },
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 3:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,61,-1,-1},
                    { -1,6,1,4,-1 },
                    { -1,-1,8,-1,-1 },
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 4:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,61,-1,-1},
                    { -1,0,4,4,-1 },
                    { -1,3,4,3,-1 },
                    { -1,1,1,-1,-1 },
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 5:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1 },
                    { -1,61,6,0,-1 },
                    { -1,6,7,1,-1 },
                    { -1,-1,7,-1,-1 },
                    { -1,-1,-1,-1,-1 },
                    { -1,-1,-1,-1,-1}};
                break;
            case 6:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,0,6,3,-1},
                    { -1,61,7,7,-1},
                    { -1,-1,4,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 7:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,5,-1,-1},
                    { -1,2,6,6,-1},
                    { -1,5,6,0,-1},
                    { -1,-1,61,8,-1},
                    { -1,-1,8,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 8:
                mapDataArray = new int[7, 5] {
                    { -1,0,7,0,-1},
                    { 0,5,0,6,0},
                    { 6,0,0,0,7},
                    { 0,8,61,3,0},
                    { -1,0,5,0,-1},
                    { -1,-1,0,8,-1},
                    { -1,-1,8,-1,-1}};
                break;
            case 9:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,0,6,7,-1},
                    { 4,6,61,4,5},
                    { -1,5,3,5,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 10:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,61,-1,-1},
                    { -1,-1,0,-1,-1},
                    { -1,-1,12,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 11:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,3,61,9,-1},
                    { -1,0,8,0,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}};
                break;
            case 12:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,13,0,12},
                    { -1,0,-1,0,-1},
                    { 61,0,15,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 13:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,16,-1,-1},
                    { -1,0,-1,0,-1},
                    { 10,-1,16,-1,15},
                    { -1,0,-1,0,-1},
                    { 61,-1,-1,-1,9},
                    { -1,-1,-1,-1,-1}};
                break;
            case 14:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,13,0,11,-1},
                    { -1,1,61,0,-1},
                    { -1,13,8,8,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 15:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,6,-1},
                    { -1,-1,16,0,7},
                    { -1,16,0,7,-1},
                    { -1,-1,7,0,8},
                    { -1,-1,-1,61,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 16:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { 11,0,9,0,12},
                    { -1,5,0,9,-1},
                    { 61,0,11,0,8},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 17:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,2,-1,0,-1},
                    { 0,61,3,0,12},
                    { -1,11,1,7,-1},
                    { -1,-1,7,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 18:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,-1,2,-1,12},
                    { -1,0,61,0,-1},
                    { 14,0,15,0,12},
                    { -1,8,4,9,-1},
                    { 9,-1,-1,-1,8},
                    { -1,-1,-1,-1,-1}}; break;
            case 19:
                mapDataArray = new int[7, 5] {
                    { 10,0,12,0,12},
                    { 0,-1,-1,-1,0},
                    { 11,0,1,4,12},
                    { 0,-1,61,3,0},
                    { 3,-1,0,2,9},
                    { 1,2,1,4,0},
                    { -1,0,9,0,12}}; break;
            case 20:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,1100060,18,61,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 21:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,1100060,-1,-1,-1},
                    { -1,-1,19,0,-1},
                    { -1,-1,61,4,-1},
                    { -1,-1,12,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 22:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,3,-1,0,-1},
                    { 0,61,17,17,1100060},
                    { -1,0,7,1200060,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 23:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,61,-1,-1},
                    { -1,0,0,1100060,-1},
                    { -1,-1,12,-1,-1},
                    { -1,17,20,9,-1},
                    { 1200060,-1,-1,-1,0},
                    { -1,-1,-1,-1,-1}}; break;
            case 24:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,14,17,1200060,-1},
                    { -1,1100060,61,7,-1},
                    { -1,0,20,8,-1},
                    { -1,10,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 25:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 1200060,20,12,0,0},
                    { -1,9,0,3,-1},
                    { -1,20,61,16,-1},
                    { 0,1100060,7,0,0},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 26:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,0,3,-1},
                    { -1,10,2,1,-1},
                    { -1,13,17,9,-1},
                    { -1,61,7,1100060,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 27:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 5,1200060,-1,2,1100060},
                    { -1,19,0,61,-1},
                    { 0,13,13,20,12},
                    { -1,-1,8,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 28:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,1200060,0,-1},
                    { -1,1100060,18,1,-1},
                    { -1,19,5,12,-1},
                    { -1,13,7,61,-1},
                    { -1,0,1,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 29:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 61,1,19,1300060,1200060},
                    { -1,7,-1,8,-1},
                    { 1100060,19,1,19,12},
                    { -1,0,5,16,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 30:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { 61,0,22,0,1100060},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 31:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,1100060},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,23,-1,-1},
                    { -1,0,61,-1,-1},
                    { 2,8,9,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 32:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 61,-1,22,4,1200060},
                    { -1,-1,-1,-1,-1},
                    { -1,0,-1,23,-1},
                    { -1,0,6,0,-1},
                    { -1,0,4,1100060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 33:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 1200060,-1,-1,-1,1100060},
                    { 0,-1,-1,-1,0},
                    { 22,0,10,0,23},
                    { 0,-1,-1,-1,0},
                    { 61,-1,16,-1,16},
                    { -1,-1,-1,-1,-1}}; break;
            case 34:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,1100060,-1,-1},
                    { -1,61,3,2,-1},
                    { 13,-1,22,-1,7},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 35:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,-1,15,-1},
                    { -1,-1,0,-1,-1},
                    { -1,1,-1,23,-1},
                    { -1,61,7,0,-1},
                    { -1,1100060,4,12,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 36:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 2,-1,-1,-1,0},
                    { 3,-1,-1,-1,1100060},
                    { 10,-1,21,-1,3},
                    { 0,-1,-1,-1,0},
                    { 61,-1,-1,-1,12},
                    { -1,-1,-1,-1,-1}}; break;
            case 37:
                mapDataArray = new int[7, 5] {
                    { 1300060,-1,-1,-1,0},
                    { 0,0,-1,0,0},
                    { -1,-1,21,-1,-1},
                    { 0,17,61,0,9},
                    { 1200060,-1,19,-1,19},
                    { 0,0,-1,1100060,1400060},
                    { -1,-1,-1,-1,-1}}; break;
            case 38:
                mapDataArray = new int[7, 5] {
                    { -1,0,0,11,-1},
                    { -1,1200060,17,3,-1},
                    { -1,1400060,0,12,-1},
                    { -1,23,17,24,-1},
                    { -1,1300060,-1,61,-1},
                    { -1,1,-1,1100060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 39:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,1100060},
                    { -1,1200060,-1,-1,-1},
                    { -1,-1,23,1300060,-1},
                    { -1,23,4,17,-1},
                    { 6,0,-1,1,61},
                    { -1,9,-1,9,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 40:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,10061,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { 13,-1,24,-1,16},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 41:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,10061,17,11},
                    { -1,0,5,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 42:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,6,-1,0,-1},
                    { -1,-1,10061,-1,-1},
                    { -1,6,18,14,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 43:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,10061,-1,12,-1},
                    { -1,18,0,0,-1},
                    { -1,14,5,16,-1},
                    { -1,-1,1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 44:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { 9,10,17,10007,0},
                    { -1,-1,61,-1,-1},
                    { -1,-1,13,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 45:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,10061,-1,-1},
                    { -1,-1,8,-1,-1},
                    { 14,18,11,12,0},
                    { -1,-1,9,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 46:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,4,3,16,-1},
                    { 0,17,1,17,1100060},
                    { -1,10061,8,0,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 47:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,10061,-1,-1,-1},
                    { -1,3,1100060,-1,-1},
                    { -1,10,18,16,-1},
                    { -1,0,-1,17,-1},
                    { -1,10,-1,9,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 48:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,61,0,-1},
                    { -1,1110060,20,4,-1},
                    { -1,6,10,4,-1},
                    { -1,2,18,16,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 49:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 1100060,-1,-1,-1,0},
                    { 18,0,15,16,0},
                    { 9,0,19,0,0},
                    { 10,10061,11,-1,0},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 50:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,0,1000,0,-1},
                    { -1,0,61,0,-1},
                    { -1,0,101,0,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 51:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,61,-1,-1},
                    { -1,1000,0,108,-1},
                    { -1,-1,7,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 52:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,119,0,61,-1},
                    { -1,0,1000,0,-1},
                    { -1,1100060,1,14,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 53:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,4000,0,107,-1},
                    { -1,0,61,0,-1},
                    { -1,1000,0,409,-1},
                    { -1,-1,8,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 54:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 114,4000,-1,-1,1000},
                    { -1,0,0,0,-1},
                    { -1,0,61,0,-1},
                    { -1,-1,4,403,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 55:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,61,-1,1000,-1},
                    { 2000,110,20,207,-1},
                    { -1,1100060,-1,0,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 56:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,1000,10061,-1,-1},
                    { -1,0,0,103,-1},
                    { -1,-1,1,18,2000},
                    { -1,-1,-1,213,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 57:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,10201,1000,12,-1},
                    { -1,117,61,4,-1},
                    { -1,0,7,2000,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 58:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,1100060,214,-1},
                    { -1,4000,61,1000,-1},
                    { 2000,6,118,401,0},
                    { -1,5,2,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 59:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,3000,4000,-1},
                    { -1,313,1,211,-1},
                    { -1,404,20,113,61},
                    { 1000,2000,-1,1100060,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 60:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,61,-1,1100060,-1},
                    { -1,-1,31,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 61:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,1100060,-1,0,-1},
                    { -1,27,1,61,-1},
                    { -1,-1,7,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 62:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,10061,0,12,-1},
                    { -1,0,32,0,-1},
                    { -1,8,1,0,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 63:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,1,10061,-1},
                    { -1,0,27,2,-1},
                    { -1,5,0,0,-1},
                    { -1,-1,7,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 64:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,6,-1,6,-1},
                    { 8,0,10061,0,8},
                    { -1,7,0,8,-1},
                    { -1,-1,30,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 65:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,10061,0,10012,-1},
                    { -1,-1,31,-1,-1},
                    { -1,13,25,1100060,-1},
                    { -1,0,27,1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 66:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,8,0,11,-1},
                    { 2,28,1100060,0,0},
                    { -1,10061,20,12,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 67:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,7,3,0,-1},
                    { 5,1,10061,7,2},
                    { -1,1,27,8,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 68:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { 1200060,4,61,16,0},
                    { 10017,10019,1100060,18,0},
                    { 14,5,32,16,0},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 69:
                mapDataArray = new int[7, 5] {
                    { -1,-1,61,-1,-1},
                    { -1,1400060,26,0,-1},
                    { 0,1200060,6,19,1100060},
                    { 0,25,5,32,1300060},
                    { -1,15,8,0,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},}; break;
            case 70:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { 61,-1,-1,-1,1100060},
                    { -1,0,-1,0,-1},
                    { -1,-1,39,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 71:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 1100060,-1,-1,-1,0},
                    { -1,-1,-1,-1,-1},
                    { 1000,0,9,134,61},
                    { -1,-1,8,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 72:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,-1,11,-1},
                    { 0,0,61,139,0},
                    { -1,1000,-1,17,-1},
                    { 0,16,-1,1100060,1300060},
                    { -1,0,32,1200060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 73:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { 0,1200060,10003,-1,0},
                    { 0,19,17,9,0},
                    { 1100060,-1,35,61,7},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 74:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,3,10061,2,0},
                    { 31,0,0,0,32},
                    { 2,1,1110060,2,3},
                    { 0,0,0,0,0},
                    { -1,-1,37,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 75:
                mapDataArray = new int[7, 5] {
                    { 33,0,6,0,0},
                    { 0,0,5,4,0},
                    { 1,6,13,10061,0},
                    { 0,0,0,0,0},
                    { -1,-1,-1,-1,16},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,16,-1,-1}}; break;
            case 76:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,0,-1,61,27},
                    { 10,-1,-1,-1,1200060},
                    { 19,-1,37,-1,0},
                    { 10010,-1,3,-1,0},
                    { 0,-1,14,-1,1100060},
                    { -1,-1,-1,-1,-1}}; break;
            case 77:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 9,0,10002,10061,11},
                    { -1,0,0,0,-1},
                    { -1,4,38,35,-1},
                    { -1,-1,6,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 78:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,0,3,0,1100060},
                    { -1,0,37,19,-1},
                    { -1,0,11,1200060,-1},
                    { 2,10061,17,11,8},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 79:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { 5,0,-1,1110060,0},
                    { 10025,10061,24,18,1},
                    { -1,7,27,10036,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            //调整关卡
            case 110://原来80
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 419,-1,-1,-1,0},
                    { 0,0,-1,0,316},
                    { 0,3000,22,117,4000},
                    { 0,1000,20,0,1400060},
                    { 1200060,1100060,-1,61,1300060},
                    { -1,-1,-1,-1,-1}}; break;
            case 111:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,15,-1,61,-1},
                    { -1,19,0,10001,-1},
                    { -1,1,10005,3,-1},
                    { -1,13,18,16,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 112:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,4000,-1,-1},
                    { 1000,10,61,10411,0},
                    { -1,-1,1,-1,-1},
                    { -1,15,17,16,-1},
                    { -1,-1,115,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 113:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,-1,401,-1},
                    { 0,4000,10061,2000,1000},
                    { 0,14,18,16,12},
                    { -1,-1,101,-1,-1},
                    { -1,201,-1,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 114:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,-1,1100060,-1},
                    { 0,1200060,10019,61,0},
                    { -1,0,-1,1000,-1},
                    { 14,0,22,0,13},
                    { -1,0,124,7,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 115:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,61,7,4},
                    { -1,6,29,0,-1},
                    { -1,2,24,5,-1},
                    { -1,1100060,1200060,7,-1},
                    { 8,0,6,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 116:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,6,-1,11,-1},
                    { -1,12,3,1200060,-1},
                    { -1,9,6,24,-1},
                    { 3,3,6,18,0},
                    { 61,0,-1,3,1100060},
                    { -1,-1,-1,-1,-1}}; break;
            case 117:
                mapDataArray = new int[7, 5] {
                    { 4000,1500060,19,3,61},
                    { 1400060,-1,-1,-1,-1},
                    { 3,-1,-1,-1,-1},
                    { 1000,11,1200060,18,1300060},
                    { 21,-1,-1,-1,-1},
                    { 1100060,-1,-1,-1,-1},
                    { 2,128,17,4,412}}; break;
            case 118:
                mapDataArray = new int[7, 5] {
                    { 10,-1,3,-1,-1},
                    { -1,0,10,0,-1},
                    { 13,0,23,0,15},
                    { -1,6,10061,0,-1},
                    { 0,2,4,0,9},
                    { -1,-1,8,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 119:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,0,11},
                    { -1,-1,7,0,11},
                    { -1,5,14,11,18},
                    { 61,19,2,0,8},
                    { 1,1100060,-1,1200060,0},
                    { -1,-1,-1,-1,-1}}; break;
            case 80://90
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,61,-1,-1},
                    { -1,1100060,42,1200060,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 81:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,1100060,-1,0,-1},
                    { 10,44,1210060,10061,0},
                    { -1,0,47,0,-1},
                    { -1,16,-1,9,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 82:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,1000,-1,-1},
                    { 0,-1,0,-1,10015},
                    { 1100060,0,-1,10018,42},
                    { 0,-1,10061,-1,5},
                    { -1,-1,140,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 83:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,-1,15,-1,7},
                    { -1,32,-1,61,-1},
                    { 0,28,1310060,1200060,11},
                    { -1,-1,47,-1,-1},
                    { -1,0,-1,1100060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 84:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,1200060,-1,0,0},
                    { 0,-1,-1,-1,0},
                    { 0,1100060,8,37,12},
                    { -1,44,7,0,-1},
                    { -1,10061,13,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 85:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,0,-1,-1},
                    { -1,1200060,1400060,0,1500060},
                    { 1100060,44,1300060,46,0},
                    { 0,1,30,1,1},
                    { -1,0,6,61,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 86:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,-1,10006,-1,11},
                    { -1,10,-1,10061,-1},
                    { 14,28,10012,18,12},
                    { -1,-1,48,-1,-1},
                    { -1,0,-1,1100060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 87:
                mapDataArray = new int[7, 5] {
                    { -1,6,10061,0,-1},
                    { -1,20,41,10020,-1},
                    { 1200060,0,19,37,1300060},
                    { 0,1110060,8,0,0},
                    { -1,1,-1,0,-1},
                    { -1,0,9,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 88:
                mapDataArray = new int[7, 5] {
                    { -1,1700060,-1,0,-1},
                    { 0,-1,-1,-1,1600060},
                    { 1400060,0,-1,39,41},
                    { 1800060,0,61,1300060,1500060},
                    { 0,10029,39,43,1200060},
                    { -1,0,45,0,-1},
                    { -1,0,-1,1100060,-1}}; break;
            case 89:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,0,-1,1200060,1400060},
                    { 1300060,0,-1,1500060,18},
                    { 0,37,61,41,4},
                    { -1,17,39,1600060,-1},
                    { -1,1100060,5,8,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 90://100
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,3,-1,-1},
                    { -1,-1,61,-1,-1},
                    { 1100060,0,51,0,1200060},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 91:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 9,-1,10061,-1,12},
                    { -1,0,-1,0,-1},
                    { -1,-1,56,-1,10016},
                    { -1,0,-1,0,-1},
                    { 0,-1,-1,-1,12},
                    { -1,-1,-1,-1,-1}}; break;
            case 92:
                mapDataArray = new int[7, 5] {
                    { 0,-1,-1,-1,1300060},
                    { -1,0,-1,0,-1},
                    { 0,0,8,1500060,50},
                    { -1,0,61,41,-1},
                    { 0,0,40,1400060,1200060},
                    { -1,0,-1,0,-1},
                    { 0,-1,-1,-1,1100060}}; break;
            case 93:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,-1,1100060,-1},
                    { -1,10,0,10061,-1},
                    { -1,10042,1200060,52,-1},
                    { -1,13,5,0,-1},
                    { -1,0,-1,16,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 94:
                mapDataArray = new int[7, 5] {
                    { 1400060,-1,49,-1,1500060},
                    { 0,1200060,-1,0,437},
                    { 3000,-1,61,-1,0},
                    { 0,0,1000,4000,137},
                    { 0,-1,1300060,-1,0},
                    { 0,340,15,0,1100060},
                    { -1,-1,-1,-1,-1}}; break;
            case 95:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 1100060,0,1000,12,15},
                    { -1,-1,0,-1,-1},
                    { -1,3000,1210060,16,-1},
                    { -1,61,1,337,-1},
                    { -1,2,-1,149,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 96:
                mapDataArray = new int[7, 5] {
                    { -1,0,-1,1300060,-1},
                    { 0,5,-1,0,1200060},
                    { 1100060,4000,10061,0,20},
                    { 0,1000,-1,7,439},
                    { -1,1400060,40,152,-1},
                    { -1, 1,-1,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 97:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 3,-1,-1,-1,0},
                    { 11,0,61,0,0},
                    { 1000,1,10015,9,0},
                    { -1,0,19,0,-1},
                    { 1110060,9,-1,149,0},
                    { -1,-1,-1,-1,-1}}; break;
            case 98:
                mapDataArray = new int[7, 5] {
                    { 0,0,-1,0,1400060},
                    { -1,0,6,0,-1},
                    { 1200060,10061,44,1500060,49},
                    { -1,1100060,104,0,-1},
                    { 1000,44,10037,8,1300060},
                    { -1,8,8,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 99:
                mapDataArray = new int[7, 5] {
                    { 1110060,1200060,-1,0,211},
                    { -1,5,2,2000,-1},
                    { -1,0,53,1000,-1},
                    { 10140,0,51,0,10061},
                    { 2,9,-1,6,0},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 100://110
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,1300060,0,61,-1},
                    { -1,0,57,0,-1},
                    { -1,1100060,0,1200060,-1},
                    { -1,-1,9,-1,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 101:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,103,-1,10006,-1},
                    { 0,0,6,58,10007},
                    { -1,1000,61,10016,-1},
                    { -1,-1,4,12,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 102:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,1300060,3,0,1200060},
                    { -1,-1,0,57,0},
                    { -1,0,1100060,0,8},
                    { -1,16,2,61,0},
                    { 0,5,1,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 103:
                mapDataArray = new int[7, 5] {
                    { -1,0,-1,1000,10015},
                    { 0,3,0,1,0},
                    { 0,10010,10048,10061,24},
                    { 0,10003,18,57,0},
                    { 0,10,1100060,5,10125},
                    { -1,-1,7,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 104:
                mapDataArray = new int[7, 5] {
                    { -1,10207,-1,1000,-1},
                    { 4000,1100060,0,10011,0},
                    { 11,10050,10058,1200060,9},
                    { 1,27,101,10430,2000},
                    { 338,1300060,3000,61,0},
                    { -1,0,13,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 105:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,0,0},
                    { -1,-1,-1,0,61},
                    { -1,-1,-1,0,0},
                    { -1,59,-1,-1,-1},
                    { -1,-1,5,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 106:
                mapDataArray = new int[7, 5] {
                    { -1,-1,10061,-1,-1},
                    { -1,-1,-1,8,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,40,0,59,-1},
                    { -1,0,7,0,-1},
                    { -1,0,-1,12,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 107:
                mapDataArray = new int[7, 5] {
                    { 61,0,-1,-1,-1},
                    { 0,0,-1,-1,-1},
                    { -1,-1,5,3,4},
                    { -1,-1,-1,59,1},
                    { -1,-1,2,1,4},
                    { -1,8,0,0,-1},
                    { -1,-1,8,-1,-1}}; break;
            case 108:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 10061,-1,-1,-1,59},
                    { 0,2000,0,1000,11},
                    { 0,1,3,18,3},
                    { 0,206,7,108,1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 109:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,7,-1,-1},
                    { -1,1100060,-1,10061,-1},
                    { 5,-1,34,-1,36},
                    { 0,0,-1,2,1},
                    { 0,0,14,59,3},
                    { -1,-1,-1,-1,-1}}; break;
            case 120://120
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,38,28,-1},
                    { -1,1400060,61,1200060,-1},
                    { 29,19,-1,1000,1300060},
                    { 1510060,25,123,0,0},
                    { -1,0,-1,1100060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 121:
                mapDataArray = new int[7, 5] {
                    { -1,0,10061,1300060,-1},
                    { 1200060,0,0,339,3},
                    { 14,3000,24,4000,10016},
                    { -1,-1,56,-1,-1},
                    { -1,0,-1,417,-1},
                    { 0,-1,-1,-1,1100060},
                    { -1,-1,-1,-1,-1}}; break;
            case 122:
                mapDataArray = new int[7, 5] {
                    { -1,1300060,1100060,0,-1},
                    { 0,-1,19,-1,11},
                    { 0,23,18,1200060,4},
                    { 1400060,-1,61,-1,0},
                    { -1,30,15,0,-1},
                    { -1,-1,-1,-1,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 123:
                mapDataArray = new int[7, 5] {
                    { 0,0,-1,1400060,1100060},
                    { -1,0,-1,20,-1},
                    { -1,223,1000,10061,-1},
                    { 1200060,0,31,2000,0},
                    { 26,10005,18,124,0},
                    { -1,0,-1,1300060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 124:
                mapDataArray = new int[7, 5] {
                    { -1,-1,0,1100060,-1},
                    { -1,10061,17,122,-1},
                    { 0,1300060,26,24,0},
                    { -1,1000,10001,0,-1},
                    { 0,14,25,18,-1},
                    { -1,1400060,-1,0,1200060},
                    { -1,-1,-1,-1,-1}}; break;
            case 125:
                mapDataArray = new int[7, 5] {
                    { 0,-1,-1,-1,1100060},
                    { -1,0,10015,10019,9},
                    { -1,0,24,4000,0},
                    { 1,61,26,10021,-1},
                    { 419,-1,-1,-1,0},
                    { -1,0,-1,1200060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 126:
                mapDataArray = new int[7, 5] {
                    { -1,1200060,-1,0,-1},
                    { 5,0,-1,10061,1},
                    { 0,49,0,10003,1100060},
                    { -1,40,38,51,-1},
                    { -1,13,-1,-1,-1},
                    { -1,-1,-1,1110061,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 127:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 11,-1,10006,-1,121},
                    { 19,0,1000,10011,1},
                    { 14,-1,23,-1,0},
                    { -1,0,-1,61,-1},
                    { 1100060,-1,-1,-1,0},
                    { -1,-1,-1,-1,-1}}; break;
            case 128:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,0,-1,0,-1},
                    { 3,61,1510060,1600060,1},
                    { 0,45,32,47,0},
                    { 1300060,10017,44,10017,1400060},
                    { 0,1100060,-1,1200060,0},
                    { -1,-1,-1,-1,-1}}; break;
            case 129:
                mapDataArray = new int[7, 5] {
                    { -1,0,-1,6,-1},
                    { 1000,0,10061,43,1200060},
                    { 0,3,411,10338,0},
                    { 0,3000,17,0,0},
                    { -1,10,116,4000,-1},
                    { -1,0,-1,1100060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 130:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,25,1500060,1200060,-1},
                    { -1,10061,1000,0,-1},
                    { -1,18,40,38,-1},
                    { -1,1,47,0,-1},
                    { 1100060,1410060,36,1300060,10118},
                    { -1,-1,-1,-1,-1}}; break;
            case 131:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,0,0,0,0},
                    { 1200060,1600060,4000,112,344},
                    { 0,419,28,1410060,1000},
                    { 2000,20,39,3000,232},
                    { 1100060,1500060,7,1300060,61},
                    { -1,-1,-1,-1,-1}}; break;
            case 132:
                mapDataArray = new int[7, 5] {
                    { 0,10061,-1,0,1200060},
                    { -1,10001,4000,13,-1},
                    { -1,26,37,3000,-1},
                    { 339,3,17,0,418},
                    { 0,1400060,-1,1100060,19},
                    { 0,-1,-1,-1,1300060},
                    { -1,-1,-1,-1,-1}}; break;
            case 133:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,-1,1300060,41,5},
                    { -1,0,38,10019,1400060},
                    { 0,37,0,-1,0},
                    { 1500060,18,-1,61,4},
                    { 0,1100060,6,1200060,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 134:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { -1,1200060,-1,0,-1},
                    { 1300060,8,-1,61,26},
                    { 0,6,44,37,1400060},
                    { -1,10,10037,0,-1},
                    { -1,1100060,8,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 135:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,-1,-1,-1,1300060},
                    { 0,0,7,418,27},
                    { 1400060,10003,19,1110060,1000},
                    { 4000,141,32,6,1200060},
                    { 61,0,6,0,0},
                    { -1,-1,-1,-1,-1}}; break;
            case 136:
                mapDataArray = new int[7, 5] {
                    { -1,0,10039,1300060,-1},
                    { 0,1400060,44,7,0},
                    { 1500060,37,17,10008,10061},
                    { 1100060,0,-1,5,42},
                    { 0,0,-1,12,1200060},
                    { -1,0,37,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 137:
                mapDataArray = new int[7, 5] {
                    { 61,0,0,0,1500060},
                    { 10017,-1,1300060,-1,10017},
                    { 1400060,19,42,17,1100060},
                    { -1,20,10037,19,0},
                    { 1200060,-1,6,-1,0},
                    { -1,1000,116,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 138:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 3000,0,10061,0,-1},
                    { -1,0,19,10040,335},
                    { 1200060,9,31,10005,0},
                    { 0,18,36,34,1100060},
                    { -1,14,-1,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 139:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 0,1400060,0,0,0},
                    { 0,18,20,10011,1200060},
                    { 1100060,28,20,19,27},
                    { 0,10010,12,10061,-1},
                    { -1,0,40,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 140:
                mapDataArray = new int[7, 5] {
                    { -1,-1,-1,-1,-1},
                    { 1200060,-1,0,-1,1400060},
                    { 0,17,10014,61,17},
                    { 0,19,10019,17,7},
                    { 1300060,36,25,18,10009},
                    { 0,-1,-1,-1,1100060},
                    { -1,-1,-1,-1,-1}}; break;
            case 141:
                mapDataArray = new int[7, 5] {
                    { -1,0,1600060,32,-1},
                    { 0,1200060,25,17,1400060},
                    { 0,40,10061,0,1710060},
                    { 30,19,24,37,27},
                    { 1510060,10028,-1,1100060,0},
                    { 1300060,25,-1,0,0},
                    { -1,-1,-1,-1,-1}}; break;
            case 142:
                mapDataArray = new int[7, 5] {
                    { -1,1400060,39,0,-1},
                    { 1300060,43,41,1600060,1100060},
                    { 1700060,43,1510060,17,127},
                    { -1,10061,19,17,-1},
                    { 1800060,1000,8,1210060,0},
                    { -1,0,4,5,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 143:
                mapDataArray = new int[7, 5] {
                    { -1,0,1200060,1300060,-1},
                    { -1,0,10025,28,10061},
                    { 0,20,1100060,19,0},
                    { 14,0,20,0,-1},
                    { 0,10,1,10009,0},
                    { -1,0,7,0,-1},
                    { -1,0,28,9,-1}}; break;
            case 144:
                mapDataArray = new int[7, 5] {
                    { -1,0,-1,0,-1},
                    { 0,0,10,0,0},
                    { 0,10006,44,11,0},
                    { 0,11,10039,10020,0},
                    { 0,10017,0,9,1100060},
                    { 1200060,16,20,0,61},
                    { -1,9,-1,0,-1}}; break;
            case 145:
                mapDataArray = new int[7, 5] {
                    { -1,0,3,11,-1},
                    { -1,2,61,0,-1},
                    { 14,0,10015,9,0},
                    { 0,10,10019,1200060,12},
                    { 10,0,35,30,15},
                    { -1,1100060,28,0,-1},
                    { -1,-1,-1,-1,-1}}; break;
            case 146:
                mapDataArray = new int[7, 5] {
                    { 1600060,0,10012,28,0},
                    { 1400060,3000,0,422,318},
                    { 10011,39,1000,36,17},
                    { 4000,1100060,1200060,27,1500060},
                    { 9,0,30,1310060,0},
                    { 121,0,0,8,0},
                    { 4,61,7,0,0}}; break;
            case 147:
                mapDataArray = new int[7, 5] {
                    { 1800060,0,4000,0,61},
                    { 1410060,18,1510060,19,1610060},
                    { 18,3,32,1,17},
                    { 1210060,29,48,29,1310060},
                    { 17,10001,2,1700060,18},
                    { 1100060,4,26,1000,119},
                    { -1,0,-1,424,-1}}; break;
            case 148:
                mapDataArray = new int[7, 5] {
                    { 1610060,1710060,1500060,61,7},
                    { 2,41,27,10001,3},
                    { 1410060,36,38,10005,12},
                    { 14,10008,1110060,9,7},
                    { 9,30,31,30,12},
                    { 17,27,44,28,19},
                    { 3,1210060,41,1310060,1}}; break;
            case 149:
                mapDataArray = new int[7, 5] {
                    { 0,3,7,1510060,28},
                    { 26,5,6,19,1400060},
                    { 1610060,28,22,10012,6},
                    { 22,61,52,9,7},
                    { 10011,10018,29,16,52},
                    { 10042,1210060,19,10028,0},
                    { 1310060,10009,15,9,1100060}}; break;
            default:
                mapDataArray = new int[7, 5] {
                    { 0,3,7,1510060,28},
                    { 26,5,6,19,1400060},
                    { 1610060,28,23,9,6},
                    { 22,61,52,9,7},
                    { 10011,10018,29,16,52},
                    { 10042,1210060,19,10028,0},
                    { 1310060,10009,15,9,1100060}}; break;
        }
    }

}
