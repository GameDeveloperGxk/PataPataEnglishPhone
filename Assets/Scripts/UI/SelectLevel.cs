using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
/// <summary>
/// 关卡选择页面
/// </summary>
public class SelectLevel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isExit = true;

    public Button ButtonBack;

    public int currentPage = 0;
    public Text textStarNum;

    public GameObject panelSet, panelBuy, panelStartGame;
    public Button btnBuy, btnSet,btnBook;

    public Sprite[] stepSprite;
    public GameObject
        panelStep0,
        panelStep1,
        panelStep2,
        panelStep3,
        panelStep4,
        panelStep5,
        panelStep6,
        panelStep7,
        panelStep8,
        panelStep9,
        panelStep10,
        panelStep11,
        panelStep12,
        panelStep13,
        panelStep14;

    public Image imgBack;
    public Sprite[] spriteImgBack;
    public Image imgBackUp;
    public Sprite[] spriteImgBackUp;

    private Vector3[] btnPosition;

    public Text text0, text1;
    public Image imgStar;

    public Button btnBigLevel;
    public Sprite[] sprBtnBig;

    public Button btnBig0, btnBig1, btnBig2, btnBig3, btnBig4, btnBig5, btnBig6, btnBig7, btnBig8, btnBig9, btnBig10
                 , btnBig11, btnBig12, btnBig13, btnBig14;
    public Sprite[] sprBig0, sprBig1, sprBig2, sprBig3, sprBig4, sprBig5, sprBig6, sprBig7, sprBig8, sprBig9,
        sprBig10, sprBig11, sprBig12, sprBig13, sprBig14;

    public GameObject objPanelBtnBig;
    public GameObject[] aniBtnBig;

    public GameObject panelBtn;
    public GameObject btnSelect;

    public Button btnDrag;


    public Image imgChoose0, imgChoose1, imgChoose2, imgChoose3, imgChoose4, imgChoose5, imgChoose6,
        imgChoose7, imgChoose8, imgChoose9, imgChoose10, imgChoose11, imgChoose12, imgChoose13, imgChoose14;

    public Animation aniClear0,
        aniClear1,
        aniClear2,
        aniClear3,
        aniClear4,
        aniClear5,
        aniClear6,
        aniClear7,
        aniClear8,
        aniClear9,
        aniClear10,
        aniClear11,
        aniClear12,
        aniClear13,
        aniClear14;
    public Animation aniSuo8, aniSuo9, aniSuo10, aniSuo11, aniSuo12, aniSuo13, aniSuo14;

    public GameObject[] effectBack;

    public GameObject effChuan;

    public GameObject bigBtnPanel;

    public Animation aniShop0, aniShop1;

    public GameObject panelStar;
    public GameObject prefabStar;

    public Button buttonPage;
    public GameObject imgR0, imgR1;

    private int _length = 0;
    private GameObject curPageStep;
    private int[] stepNum;

    public bool isPlayMov = false;

    private float timePlay = 0;
    public int playState = 0;
    private int needStwp;
    private int upStwp;
    private int currStwp;
    private bool isS1, isS2, isS3, isS4, isS5, isS6;

    public bool isCanClick;

    public GameObject big120, big121;

    public GameObject testLeft, testRight;
    private float testNum;

    public GameObject mask0, mask1, mask2;

    public Image imgStarUp;
    //
    public float btnBigPanelY;

    public WinLastPanel winLast;

    /// <summary>
    /// 关卡页面各个动画时间显示间隔
    /// 1 默认延迟显示时间（通关按钮播放已通关动画时间）
    /// 2 步数切换显示时间（单个）
    /// 3 新关卡动画播放已解锁动画时间
    /// 4 显示开始游戏页面时间间隔(有飞星时间长，没飞星时间短)
    /// 5 有石像解锁---石像解锁显示时间
    /// 6 有石像解锁---切换到新的大关卡等待的时间
    /// </summary>
    private float[] stepTime = new float[] { 0.2f, 0.2f, 0.2f, 3.0f, 0.7f, 1.0f, 1.0f };
    //初始化的方式
    private int typeInit = 0;

    public GameObject imgBlack;

    public Button buttonStone;
    public Button buttonStone1;
    public Button buttonStone2;

    public Animation aniMap0, aniMap1, aniMap2, aniMap3, aniMap4, aniMap5, aniMap6;
    public GameObject aniMap7;

    public Text textSound;

    public GameObject imgStarLeft;

    public bool showBook = false;

    // Use this for initialization
    void Start()
    {
        //设置大关卡按钮底框默认Y坐标，用来纠正上下移动坐标
        btnBigPanelY = buttonPage.transform.position.y;
        //大关卡按钮添加点击事件
        btnBig0.onClick.AddListener(ClickBig0);
        btnBig1.onClick.AddListener(ClickBig1);
        btnBig2.onClick.AddListener(ClickBig2);
        btnBig3.onClick.AddListener(ClickBig3);
        btnBig4.onClick.AddListener(ClickBig4);
        btnBig5.onClick.AddListener(ClickBig5);
        btnBig6.onClick.AddListener(ClickBig6);
        btnBig7.onClick.AddListener(ClickBig7);
        btnBig8.onClick.AddListener(ClickBig8);
        btnBig9.onClick.AddListener(ClickBig9);
        btnBig10.onClick.AddListener(ClickBig10);
        btnBig11.onClick.AddListener(ClickBig11);
        btnBig12.onClick.AddListener(ClickBig12);
        btnBig13.onClick.AddListener(ClickBig13);
        btnBig14.onClick.AddListener(ClickBig14);
        //商城 设置按钮添加点击事件
        btnBuy.onClick.AddListener(ShowBuy);
        btnSet.onClick.AddListener(ShowSet);
        btnBook.onClick.AddListener(ClickBook);
        buttonPage.onClick.AddListener(ShowPage);
        buttonStone.onClick.AddListener(StoneClick);
        buttonStone1.onClick.AddListener(StoneClick1);
        buttonStone2.onClick.AddListener(StoneClick2);
        //添加拖动事件监听
        EventTriggerListener.Get(gameObject).onDown += OnClickDown;
        EventTriggerListener.Get(gameObject).onUp += OnClickUp;
        EventTriggerListener.Get(gameObject).onEnter += OnClickEnter;
        EventTriggerListener.Get(gameObject).onExit += OnClickExit;
        //根据屏幕隐藏点，计算自适应状态下屏幕宽度---用来计算拖动边缘X坐标点
        testNum = (6.3f - Mathf.Abs(testLeft.transform.position.x)) * 120;
        if (testNum < 0)
        {
            testNum = 0;
        }
        //默认初始化数据
        InitData(0);
        imgBlack.SetActive(true);
        Invoke("HideBlack", 2.0f);
    }

    private void HideBlack()
    {
        imgBlack.SetActive(false);
    }

    /// <summary>
    /// 初始化关卡数据,每次进入关卡会调用
    /// </summary>
    public void InitData(int _type)
    {
        //测试start
        //LocalData.GetInstance().playGameTime = 2;
        //GameController.GetInstance().playPage = 1;
        //GameController.GetInstance().currentLevel = 19;
        //LocalData.GetInstance().SetMaxOpenLevel(21);
        //GameController.GetInstance().hasNew = true;
        //GameController.GetInstance().hasNewLast = true;
        //LocalData.GetInstance().stateBuy = 1;
        //LocalData.GetInstance().starNum = 150;
        //测试end

        typeInit = _type;
        //隐藏遮罩动画
        mask0.SetActive(false);
        mask1.SetActive(false);
        mask2.SetActive(false);
        //初始化动画状态机
        playState = 0;
        //默认不可以点击
        isCanClick = true;
        //默认结束拖拽
        isExit = true;

        isPlayMov = false;

        if (LocalData.GetInstance().playGameTime == 0)//第一次进游戏
        {
            imgR0.SetActive(true);
            imgR1.SetActive(false);
            buttonPage.transform.position = new Vector3(buttonPage.transform.position.x, btnBigPanelY - 2.8f, 0);
            InitCicleImage(0);
            LocalData.GetInstance().playGameTime++;
        }
        else
        {
            currentPage = GameController.GetInstance().playPage;
            InitCicleImage(currentPage);
        }
        //刷新拖动条位置
        btnDrag.GetComponent<SelectDrag>().ChangePositon(currentPage * 0.5f);
        //如果有新关卡或者完美通关的则打开下方大关卡显示面板
        if ((GameController.GetInstance().hasNew || GameController.GetInstance().hasWan) && playState == 0)
        {
            if (imgR0.gameObject.activeSelf == false)
            {
                imgR0.SetActive(true);
                imgR1.SetActive(false);
                buttonPage.transform.position = new Vector3(buttonPage.transform.position.x, btnBigPanelY - 2.8f, 0);
            }
        }
        //大按钮
        InitBigBtnSprite();
        //
        Refrush(0);
        //初始化商城按钮状态
        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniShop0.gameObject.SetActive(false);
            aniShop1.gameObject.SetActive(true);
            Tools.PlayAnimation(aniShop1, "SC-01-XianZhi");
        }
        else
        {
            btnBuy.gameObject.SetActive(false);
        }
        //初始化 星星数量文本，初始化二次弹框显示状态
        textStarNum.text = LocalData.GetInstance().starNum + "/150";
        panelSet.SetActive(false);
        panelBuy.SetActive(false);
        panelStartGame.SetActive(false);
        winLast.gameObject.SetActive(false);
        if (GameController.GetInstance().hasNotOpenNext)
        {
            Invoke("ShotStarNotEnough", 1.5f);
        }
       // ShowClear();

        RefureshPosition();
    }

    private void ShotStarNotEnough()
    {
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "You don't have enough stars to unlock the level");
    }



    /// <summary>
    /// 刷新大关卡按钮图片状态
    /// </summary>
    public void InitBigBtnSprite()
    {
        btnBig0.image.sprite = sprBig0[1];
        btnBig1.image.sprite = sprBig1[LocalData.GetInstance().GetMaxOpenLevel() > 10 && LocalData.GetInstance().starNum >= 5 ? 1 : 0];
        btnBig2.image.sprite = sprBig2[LocalData.GetInstance().GetMaxOpenLevel() > 20 && LocalData.GetInstance().starNum >= 11 ? 1 : 0];
        btnBig3.image.sprite = sprBig3[LocalData.GetInstance().GetMaxOpenLevel() > 30 && LocalData.GetInstance().starNum >= 17 ? 1 : 0];
        btnBig4.image.sprite = sprBig4[LocalData.GetInstance().GetMaxOpenLevel() > 40 && LocalData.GetInstance().starNum >= 24 ? 1 : 0];
        btnBig5.image.sprite = sprBig5[LocalData.GetInstance().GetMaxOpenLevel() > 50 && LocalData.GetInstance().starNum >= 30 ? 1 : 0];
        btnBig6.image.sprite = sprBig6[LocalData.GetInstance().GetMaxOpenLevel() > 60 && LocalData.GetInstance().starNum >= 36 ? 1 : 0];
        btnBig7.image.sprite = sprBig7[LocalData.GetInstance().GetMaxOpenLevel() > 70 && LocalData.GetInstance().starNum >= 42 ? 1 : 0];
        btnBig8.image.sprite = sprBig8[LocalData.GetInstance().GetMaxOpenLevel() > 80 && LocalData.GetInstance().starNum >= 48 ? 1 : 0];
        btnBig9.image.sprite = sprBig9[LocalData.GetInstance().GetMaxOpenLevel() > 90 && LocalData.GetInstance().starNum >= 56 ? 1 : 0];
        btnBig10.image.sprite = sprBig10[LocalData.GetInstance().GetMaxOpenLevel() > 100 && LocalData.GetInstance().starNum >= 63 ? 1 : 0];
        btnBig11.image.sprite = sprBig11[LocalData.GetInstance().GetMaxOpenLevel() > 110 && LocalData.GetInstance().starNum >= 75 ? 1 : 0];
        btnBig12.image.sprite = sprBig12[LocalData.GetInstance().GetMaxOpenLevel() > 120 && LocalData.GetInstance().starNum >= 88 ? 1 : 0];
        btnBig13.image.sprite = sprBig13[LocalData.GetInstance().GetMaxOpenLevel() > 130 && LocalData.GetInstance().starNum >= 100 ? 1 : 0];
        btnBig14.image.sprite = sprBig14[LocalData.GetInstance().GetMaxOpenLevel() > 140 && LocalData.GetInstance().starNum >= 120 ? 1 : 0];
        //如果有新大关卡解锁
        if (GameController.GetInstance().hasNewLast && playState == 0)
        {
            switch (currentPage)
            {
                case 0: btnBig1.image.sprite = sprBig1[0]; break;
                case 1: btnBig2.image.sprite = sprBig2[0]; break;
                case 2: btnBig3.image.sprite = sprBig3[0]; break;
                case 3: btnBig4.image.sprite = sprBig4[0]; break;
                case 4: btnBig5.image.sprite = sprBig5[0]; break;
                case 5: btnBig6.image.sprite = sprBig6[0]; break;
                case 6: btnBig7.image.sprite = sprBig7[0]; break;
                case 7: btnBig8.image.sprite = sprBig8[0]; break;
                case 8: btnBig9.image.sprite = sprBig9[0]; break;
                case 9: btnBig10.image.sprite = sprBig10[0]; break;
                case 10: btnBig11.image.sprite = sprBig11[0]; break;
                case 11: btnBig12.image.sprite = sprBig12[0]; break;
                case 12: btnBig13.image.sprite = sprBig13[0]; break;
                case 13: btnBig14.image.sprite = sprBig14[0]; break;
            }
        }
    }

    /// <summary>
    /// 初始化地图场景动画
    /// 0森林，1海底，2城墙（锁），3暗夜，4传送，5雪地 ，6沙漠 ，7通用 ； 
    /// </summary>
    /// <param name="_mapID"></param>
    private void InitMapAni(int _mapID)
    {
        aniMap0.gameObject.SetActive(false);
        aniMap1.gameObject.SetActive(false);
        aniMap2.gameObject.SetActive(false);
        aniMap3.gameObject.SetActive(false);
        aniMap4.gameObject.SetActive(false);
        aniMap5.gameObject.SetActive(false);
        aniMap6.gameObject.SetActive(false);
        aniMap7.gameObject.SetActive(false);
        switch (_mapID)
        {
            case 0:
                aniMap0.gameObject.SetActive(true);
                break;
            case 1:
                aniMap1.gameObject.SetActive(true);
                break;
            case 2:
                aniMap2.gameObject.SetActive(true);
                break;
            case 3:
                // aniMap3.gameObject.SetActive(true);
                break;
            case 4:
                //aniMap4.gameObject.SetActive(true);
                break;
            case 5:
                aniMap5.gameObject.SetActive(true);
                break;
            case 6:
                aniMap6.gameObject.SetActive(true);
                break;
            case 7:
                aniMap7.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    /// <param name="_type"></param>
    public void Refrush(int _type)
    {
        isCanClick = false;
        //初始化页数，初次进入就用initdata内的计算好的，二次进入就用上次的
        panelStep0.SetActive(false);
        panelStep1.SetActive(false);
        panelStep2.SetActive(false);
        panelStep3.SetActive(false);
        panelStep4.SetActive(false);
        panelStep5.SetActive(false);
        panelStep6.SetActive(false);
        panelStep7.SetActive(false);
        panelStep8.SetActive(false);
        panelStep9.SetActive(false);
        panelStep10.SetActive(false);
        panelStep11.SetActive(false);
        panelStep12.SetActive(false);
        panelStep13.SetActive(false);
        panelStep14.SetActive(false);
        //初始化选择框
        imgChoose0.gameObject.SetActive(false);
        imgChoose1.gameObject.SetActive(false);
        imgChoose2.gameObject.SetActive(false);
        imgChoose3.gameObject.SetActive(false);
        imgChoose4.gameObject.SetActive(false);
        imgChoose5.gameObject.SetActive(false);
        imgChoose6.gameObject.SetActive(false);
        imgChoose7.gameObject.SetActive(false);
        imgChoose8.gameObject.SetActive(false);
        imgChoose9.gameObject.SetActive(false);
        imgChoose10.gameObject.SetActive(false);
        imgChoose11.gameObject.SetActive(false);
        imgChoose12.gameObject.SetActive(false);
        imgChoose13.gameObject.SetActive(false);
        imgChoose14.gameObject.SetActive(false);
        switch (currentPage)
        {
            case 0: panelStep0.SetActive(true); imgChoose0.gameObject.SetActive(true); break;
            case 1: panelStep1.SetActive(true); imgChoose1.gameObject.SetActive(true); break;
            case 2: panelStep2.SetActive(true); imgChoose2.gameObject.SetActive(true); break;
            case 3: panelStep3.SetActive(true); imgChoose3.gameObject.SetActive(true); break;
            case 4: panelStep4.SetActive(true); imgChoose4.gameObject.SetActive(true); break;
            case 5: panelStep5.SetActive(true); imgChoose5.gameObject.SetActive(true); break;
            case 6: panelStep6.SetActive(true); imgChoose6.gameObject.SetActive(true); break;
            case 7: panelStep7.SetActive(true); imgChoose7.gameObject.SetActive(true); break;
            case 8: panelStep8.SetActive(true); imgChoose8.gameObject.SetActive(true); break;
            case 9: panelStep9.SetActive(true); imgChoose9.gameObject.SetActive(true); break;
            case 10: panelStep10.SetActive(true); imgChoose10.gameObject.SetActive(true); break;
            case 11: panelStep11.SetActive(true); imgChoose11.gameObject.SetActive(true); break;
            case 12: panelStep12.SetActive(true); imgChoose12.gameObject.SetActive(true); break;
            case 13: panelStep13.SetActive(true); imgChoose13.gameObject.SetActive(true); break;
            case 14: panelStep14.SetActive(true); imgChoose14.gameObject.SetActive(true); break;
        }
        SetpInitView();
        //背景图:0森林，1海底，2城墙（锁），3暗夜，4传送，5雪地 ，6沙漠 ，7通用 ； 
        int _id = new int[] { 0, 0, 6, 6, 2, 4, 5, 5, 3, 3, 1, 7, 7, 7, 7 }[currentPage];
        imgBack.sprite = spriteImgBack[_id];
        imgBackUp.sprite = spriteImgBackUp[_id];
        effectBack[0].SetActive(false);
        effectBack[1].SetActive(false);
        effectBack[2].SetActive(false);
        effectBack[3].SetActive(false);
        effectBack[4].SetActive(false);
        effectBack[5].SetActive(false);
        effectBack[6].SetActive(false);
        effectBack[7].SetActive(false);
        effChuan.gameObject.SetActive(false);
        effectBack[_id].SetActive(true);
        if (currentPage == 4)
        {
            effectBack[_id].SetActive(false);
        }
        if (currentPage == 5)
        {
            effChuan.gameObject.SetActive(true);
        }

        InitMapAni(_id);

        //星星文本
        text1.text = LocalData.GetInstance().starNum + "";
        text0.text = "/" + (GameController.GetInstance().starNum[currentPage]);
        if (LocalData.GetInstance().starNum >= GameController.GetInstance().starNum[currentPage])
        {
            text1.color = Color.white;
        }
        else
        {
            text1.color = Color.red;
        }
        //小按钮坐标初始化
        switch (currentPage)
        {
            case 0:
                btnPosition = new Vector3[] {new Vector3(-1.181539f,-4.553846f,0f),
new Vector3(1.124103f,-3.306667f,0f),
new Vector3(-2.428718f,-2.404103f,0f),
new Vector3(0.4266667f,-1.673846f,0f),
new Vector3(2.52718f,-0.4512825f,0f),
new Vector3(0.2215385f,0.2461539f,0f),
new Vector3(-2.20718f,0.7220513f,0f),
new Vector3(-2.756923f,2.904616f,0f),
new Vector3(-1.074872f,4.627693f,0f),
new Vector3(1.271795f,5.10359f,0f),
new Vector3(0.5f,2.548718f,0f),
new Vector3(327+45,264,0),
new Vector3(145+45,264,0),
new Vector3(375,264,0),};
                break;
            case 1:
                btnPosition = new Vector3[] {
                new Vector3(0.763077f,-4.578462f,0f),
new Vector3(-2.034872f,-3.347692f,0f),
new Vector3(0.04923077f,-1.878975f,0f),
new Vector3(2.535385f,-1.017436f,0f),
new Vector3(1.148718f,0.7958975f,0f),
new Vector3(-1.050256f,0.7712821f,0f),
new Vector3(-2.436923f,3.257436f,0f),
new Vector3(-1.017436f,5.505641f,0f),
new Vector3(1.091282f,5.481026f,0f),
new Vector3(2.486154f,3.282052f,0f),
new Vector3(0.04923077f,3.2f,0f),
new Vector3(327-252,254,0),
new Vector3(145-252,254,0),
new Vector3(375-252,254,0),};
                break;
            case 2:
                btnPosition = new Vector3[] {
                new Vector3(1.641026f,-4.578462f,0f),
new Vector3(-0.5005128f,-4.004103f,0f),
new Vector3(-2.026667f,-2.592821f,0f),
new Vector3(-0.5005128f,-1.099487f,0f),
new Vector3(1.033846f,-2.494359f,0f),
new Vector3(2.354872f,-0.763077f,0f),
new Vector3(1.083077f,1.214359f,0f),
new Vector3(-1.558975f,0.6482052f,0f),
new Vector3(-2.116923f,2.56f,0f),
new Vector3(0.3856411f,3.282052f,0f),
new Vector3(-1.115898f,5.046154f,0f),
new Vector3(182+20,629,0),
new Vector3(-7+20,629,270),
new Vector3(263,629,270),};
                break;
            case 3:
                btnPosition = new Vector3[] {
                new Vector3(-0.01641026f,-4.46359f,0f),
new Vector3(-1.870769f,-3.150769f,0f),
new Vector3(-2.510769f,-0.8779488f,0f),
new Vector3(-0.2707692f,-1.673846f,0f),
new Vector3(1.862564f,-2.978462f,0f),
new Vector3(2.724103f,-0.8779488f,0f),
new Vector3(0.6235898f,0.6153846f,0f),
new Vector3(2.379487f,2.330257f,0f),
new Vector3(1.362051f,4.586667f,0f),
new Vector3(-1.181539f,5.136411f,0f),
new Vector3(0.02461539f,2.732308f,0f),
new Vector3(-243,345,0),
new Vector3(-426,345,270),
new Vector3(-190,345,270),};
                break;
            case 4:
                btnPosition = new Vector3[] {
                new Vector3(0.574359f,-4.512821f,0f),
new Vector3(-1.706667f,-3.364103f,0f),
new Vector3(1.042051f,-2.699487f,0f),
new Vector3(-1.025641f,-1.673846f,0f),
new Vector3(1.88718f,-0.8697436f,0f),
new Vector3(-0.3282052f,0.1641026f,0f),
new Vector3(2.609231f,1.132308f,0f),
new Vector3(2.707693f,3.298462f,0f),
new Vector3(1.058462f,5.202052f,0f),
new Vector3(-1.534359f,4.422565f,0f),
new Vector3(0.4758975f,2.732308f,0f),
new Vector3(-172,293,0),
new Vector3(-355,293,270),
new Vector3(-103,293,270),};
                break;
            case 5:
                btnPosition = new Vector3[] {
                    new Vector3(0.6153846f,-4.488205f,0f),
new Vector3(-1.493333f,-3.602051f,0f),
new Vector3(-2.305641f,-1.558975f,0f),
new Vector3(-1.025641f,0.574359f,0f),
new Vector3(-2.272821f,2.387692f,0f),
new Vector3(-1.29641f,4.389744f,0f),
new Vector3(0.8943591f,5.013334f,0f),
new Vector3(2.535385f,3.495385f,0f),
new Vector3(0.5005128f,2.436923f,0f),
new Vector3(2.026667f,1.115898f,0f),
new Vector3(1.222564f,-1.214359f,0f),
new Vector3(218,-300,0),
new Vector3(40,-300,270),
new Vector3(282,-300,270),};
                break;
            case 6:
                btnPosition = new Vector3[] {
                new Vector3(0.03282052f,-4.46359f,0f),
new Vector3(-2.043077f,-3.224616f,0f),
new Vector3(-2.52718f,-0.9600001f,0f),
new Vector3(-2.551795f,1.485128f,0f),
new Vector3(-2.051282f,3.692308f,0f),
new Vector3(-0.03282052f,5.144616f,0f),
new Vector3(2.026667f,3.659487f,0f),
new Vector3(2.715898f,1.476923f,0f),
new Vector3(2.715898f,-0.9764103f,0f),
new Vector3(0.008205129f,-2.141539f,0f),
new Vector3(0.008205129f,0.04923077f,0f),
new Vector3(34,177,0),
new Vector3(-144,177,270),
new Vector3(98,177,270),};
                break;
            case 7:
                btnPosition = new Vector3[] {
                new Vector3(0.03282052f,-4.488205f,0f),
new Vector3(2.609231f,-3.265641f,0f),
new Vector3(-0.02461539f,-2.642051f,0f),
new Vector3(-2.551795f,-3.257436f,0f),
new Vector3(-2.56f,-1.050256f,0f),
new Vector3(-2.56f,1.140513f,0f),
new Vector3(-0.03282052f,-0.1476923f,0f),
new Vector3(2.740513f,1.148718f,0f),
new Vector3(2.20718f,3.076923f,0f),
new Vector3(-0.01641026f,2.108718f,0f),
new Vector3(-0.01641026f,4.324103f,0f),
new Vector3(47,687,0),
new Vector3(-140,687,270),
new Vector3(98,687,270), };
                break;
            case 8:
                btnPosition = new Vector3[] {
                new Vector3(2.330257f,-0.8697436f,0f),
new Vector3(0.5497437f,0.2297436f,0f),
new Vector3(-0.02461539f,-1.846154f,0f),
new Vector3(2.067693f,-3.290257f,0f),
new Vector3(-0.2953846f,-4.283077f,0f),
new Vector3(-2.322052f,-2.740513f,0f),
new Vector3(-1.534359f,-0.7056411f,0f),
new Vector3(-2.313846f,1.739487f,0f),
new Vector3(-0.763077f,3.134359f,0f),
new Vector3(1.452308f,2.289231f,0f),
new Vector3(1.747692f,4.356924f,0f),
new Vector3(9,563,0),
new Vector3(-174,563,270),
new Vector3(64,563,270),
};
                break;
            case 9:
                btnPosition = new Vector3[] {
                new Vector3(-2.363077f,-3.2f,0f),
new Vector3(-0.5661539f,-4.46359f,0f),
new Vector3(1.304615f,-3.963077f,0f),
new Vector3(2.912821f,-2.56f,0f),
new Vector3(1.88718f,-0.8615385f,0f),
new Vector3(-0.1394872f,-2.166154f,0f),
new Vector3(-2.075898f,-1.099487f,0f),
new Vector3(-0.01641026f,-0.008205129f,0f),
new Vector3(-1.854359f,1.107692f,0f),
new Vector3(-0.03282052f,2.379487f,0f),
new Vector3(-1.28f,4.14359f,0f),
new Vector3(59,677,0),
new Vector3(-120,677,270),
new Vector3(143,677,270),};
                break;
            case 10:
                btnPosition = new Vector3[] {
                new Vector3(1.083077f,-4.258462f,0f),
new Vector3(-0.7794873f,-3.224616f,0f),
new Vector3(-2.330257f,-1.6f,0f),
new Vector3(0.06564103f,-1.140513f,0f),
new Vector3(2.100513f,-2.010257f,0f),
new Vector3(2.157949f,0.01641026f,0f),
new Vector3(0.1066667f,1.140513f,0f),
new Vector3(-1.952821f,2.264616f,0f),
new Vector3(-1.312821f,4.553846f,0f),
new Vector3(1.083077f,4.857436f,0f),
new Vector3(2.354872f,3.085129f,0f),
new Vector3(289,216,0),
new Vector3(109,216,270),
new Vector3(369,216,270),};
                break;
            case 11:
                btnPosition = new Vector3[] {
                new Vector3(0.008205129f,-0.008205129f,0f),
new Vector3(2.116923f,-1.206154f,0f),
new Vector3(0.01641026f,-2.100513f,0f),
new Vector3(2.190769f,-3.372308f,0f),
new Vector3(-0.02461539f,-4.46359f,0f),
new Vector3(-2.174359f,-3.347692f,0f),
new Vector3(-2.018462f,-1.206154f,0f),
new Vector3(-2.034872f,1.271795f,0f),
new Vector3(0.04102565f,2.346667f,0f),
new Vector3(2.100513f,3.561026f,0f),
new Vector3(0.008205129f,4.545641f,0f),
new Vector3(-259,553,0),
new Vector3(-444,553,270),
new Vector3(-175,553,270),};
                break;
            case 12:
                btnPosition = new Vector3[] {
                new Vector3(0.008205129f,-0.828718f,0f),
new Vector3(2.043077f,-2.026667f,0f),
new Vector3(1.665641f,0.4923077f,0f),
new Vector3(-0.008205129f,2.22359f,0f),
new Vector3(2.322052f,3.2f,0f),
new Vector3(1.042051f,4.76718f,0f),
new Vector3(-1.321026f,3.963077f,0f),
new Vector3(-2.756923f,2.092308f,0f),
new Vector3(-1.780513f,0.3035898f,0f),
new Vector3(-2.092308f,-1.854359f,0f),
new Vector3(0.008205129f,-2.945641f,0f),
new Vector3(43,-500,0),
new Vector3(-142,-500,270),
new Vector3(123,-500,270),};
                break;
            case 13:
                btnPosition = new Vector3[] {
                new Vector3(-2.436923f,-1.698462f,0f),
new Vector3(-1.427692f,-3.922052f,0f),
new Vector3(-0.1805128f,-2.075898f,0f),
new Vector3(1.517949f,-3.79077f,0f),
new Vector3(2.371282f,-1.165128f,0f),
new Vector3(3.117949f,1.124103f,0f),
new Vector3(2.551795f,3.290257f,0f),
new Vector3(0.5169231f,4.217436f,0f),
new Vector3(0.01641026f,2.174359f,0f),
new Vector3(-2.289231f,1.362051f,0f),
new Vector3(-1.93641f,3.848205f,0f),
new Vector3(-203,619,0),
new Vector3(-388,619,270),
new Vector3(-124,619,270),};
                break;
            case 14:
                btnPosition = new Vector3[] {
                new Vector3(0.03282052f,-4.471795f,0f),
new Vector3(1.542564f,-3.249231f,0f),
new Vector3(-1.050256f,-2.978462f,0f),
new Vector3(-2.822564f,-2.018462f,0f),
new Vector3(-1.001026f,-0.8943591f,0f),
new Vector3(1.370257f,-0.5251282f,0f),
new Vector3(3.454359f,0.6153846f,0f),
new Vector3(1.56718f,1.673846f,0f),
new Vector3(-0.6564103f,1.952821f,0f),
new Vector3(-2.510769f,3.035898f,0f),
new Vector3(-0.01641026f,4.225641f,0f),
new Vector3(20,670,0),
new Vector3(-163,670,270),
new Vector3(98,670,270),};
                break;
            default:
                btnPosition = new Vector3[] {
                new Vector3(0.01f, -5.47f, 8f),
                new Vector3(-2.33f, -3.9f, 8f),
                new Vector3(-3.09f, -1.14f, 8f),
                new Vector3(-0.3f, -2f, 8f),
                new Vector3(2.21f, -3.69f, 8f),
                new Vector3(3.35f, -1.1f, 8f),
                new Vector3(0.8000001f, 0.6700001f, 8f),
                new Vector3(2.85f, 2.8f, 8f),
                new Vector3(1.62f, 5.440001f, 8f),
                new Vector3(-1.42f, 6.170001f, 8f),
                new Vector3(-0.18f, 3.45f, 8f)};
                break;
        }
        Tools.RemoveAllChildren(panelBtn);
        for (int i = 0; i < 10; i++)
        {
            int _level = currentPage * 10 + i;
            GameObject _unit = GameObject.Instantiate(btnSelect, btnPosition[i], Quaternion.identity) as GameObject;
            _unit.transform.SetParent(panelBtn.transform);
            _unit.GetComponent<ButtonLevel>().InitData(_level);
            _unit.transform.localScale = new Vector3(1, 1, 1);
        }

        text0.transform.localPosition = btnPosition[11];
        text1.transform.localPosition = btnPosition[12];
        imgStar.transform.localPosition = btnPosition[13];

        buttonStone.transform.position = btnPosition[10];
        //隐藏 两个石像的美术
        big120.SetActive(false);
        big121.SetActive(false);
        if (GameController.GetInstance().hasNewLast && playState == 0)//如果有新大关卡开启，默认隐藏石像，因为需要手动点亮
        {
            //石像
            btnBigLevel.gameObject.SetActive(true);
            btnBigLevel.image.sprite = sprBtnBig[currentPage];
            btnBigLevel.transform.position = btnPosition[10];
            objPanelBtnBig.SetActive(false);
            if (currentPage == 9)
            {
                big120.SetActive(true);
            }
        }
        else//正常初始化石像
        {
            big120.SetActive(false);
            big121.SetActive(false);
            //石像
            if ((currentPage + 1) * 10 >= LocalData.GetInstance().GetMaxOpenLevel())
            {
                btnBigLevel.gameObject.SetActive(true);
                btnBigLevel.image.sprite = sprBtnBig[currentPage];
                btnBigLevel.transform.position = btnPosition[10];
                objPanelBtnBig.SetActive(false);
                if (currentPage == 9)
                {
                    big120.SetActive(true);
                }
            }
            else
            {
                btnBigLevel.gameObject.SetActive(false);
                objPanelBtnBig.SetActive(true);
                aniBtnBig[0].gameObject.SetActive(false);
                aniBtnBig[1].gameObject.SetActive(false);
                aniBtnBig[2].gameObject.SetActive(false);
                aniBtnBig[3].gameObject.SetActive(false);
                aniBtnBig[4].gameObject.SetActive(false);
                aniBtnBig[5].gameObject.SetActive(false);
                aniBtnBig[6].gameObject.SetActive(false);
                aniBtnBig[7].gameObject.SetActive(false);
                aniBtnBig[8].gameObject.SetActive(false);
                aniBtnBig[9].gameObject.SetActive(false);
                aniBtnBig[10].gameObject.SetActive(false);
                aniBtnBig[11].gameObject.SetActive(false);
                aniBtnBig[12].gameObject.SetActive(false);
                aniBtnBig[13].gameObject.SetActive(false);
                aniBtnBig[14].gameObject.SetActive(false);
                aniBtnBig[currentPage].gameObject.SetActive(true);
                objPanelBtnBig.transform.position = btnPosition[10];
                if (currentPage == 9)
                {
                    big121.SetActive(true);
                }
            }
        }

        //if (LocalData.GetInstance().stateBuy == 1)
        //{
        //    btnBuy.gameObject.SetActive(false);
        //}

        //锁动画初始化
        bool _isLock = LocalData.GetInstance().stateBuy == 1 ? false : true;
        aniSuo8.gameObject.SetActive(_isLock);
        aniSuo9.gameObject.SetActive(_isLock);
        aniSuo10.gameObject.SetActive(_isLock);
        aniSuo11.gameObject.SetActive(_isLock);
        aniSuo12.gameObject.SetActive(_isLock);
        aniSuo13.gameObject.SetActive(_isLock);
        aniSuo14.gameObject.SetActive(_isLock);

        float _t = 1f;//clear动画延迟播放时间
        int _num = 0;
        if (currentPage == 0 && playState == 0)
        {
            Tools.StopAnimation(aniClear0, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear0.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear0.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 1 && playState == 0)
        {
            Tools.StopAnimation(aniClear1, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear1.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear1.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 2 && playState == 0)
        {
            Tools.StopAnimation(aniClear2, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear2.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear2.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 3 && playState == 0)
        {
            Tools.StopAnimation(aniClear3, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear3.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear3.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 4 && playState == 0)
        {
            Tools.StopAnimation(aniClear4, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear4.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear4.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 5 && playState == 0)
        {
            Tools.StopAnimation(aniClear5, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear5.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear5.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 6 && playState == 0)
        {
            Tools.StopAnimation(aniClear6, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear6.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear6.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 7 && playState == 0)
        {
            Tools.StopAnimation(aniClear7, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear7.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear7.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 8 && playState == 0)
        {
            Tools.StopAnimation(aniClear8, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear8.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear8.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 9 && playState == 0)
        {
            Tools.StopAnimation(aniClear9, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear9.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear9.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 10 && playState == 0)
        {
            Tools.StopAnimation(aniClear10, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear10.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear10.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 11 && playState == 0)
        {
            Tools.StopAnimation(aniClear11, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear11.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear11.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 12 && playState == 0)
        {
            Tools.StopAnimation(aniClear12, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear12.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear12.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 13 && playState == 0)
        {
            Tools.StopAnimation(aniClear13, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear13.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear13.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        _num = 0;
        if (currentPage == 14 && playState == 0)
        {
            Tools.StopAnimation(aniClear14, "Clear");
            _num = GetStarNum(currentPage * 10, currentPage * 10 + 10);
            aniClear14.gameObject.SetActive(_num == 10 ? true : false);
            if (GameController.GetInstance().hasWan && _num == 10)
            {
                aniClear14.gameObject.SetActive(false);
                Invoke("PlayClear" + currentPage, _t);
            }
        }
        //第一次直接进入游戏
        if(LocalData.GetInstance().GetMaxOpenLevel() == 1)
        {
            GameController.GetInstance().currentLevel = 0;
            UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.SetActive(true);
            UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.GetComponent<StartGamePanel>().InitData();
        }

        if (showBook)
        {
            Invoke("ShowHelp",2f);
            return;
        }

        //动画部分，初始化各个动画时间间隔
        stepTime = new float[]
        { 0.2f, 0.2f, 0.2f, GameController.GetInstance().hasWan ? 0.5f : 0.1f, 1.5f, 1.5f, 1.2f };

        //飞星生成
        if (GameController.GetInstance().hasWan)
        {
            GameController.GetInstance().hasWan = false;
            int _i = GameController.GetInstance().currentLevel % 10;
            Vector3 _vec = btnPosition[_i];
            GameObject _star = GameObject.Instantiate(prefabStar, _vec, Quaternion.identity) as GameObject;
            _star.transform.SetParent(panelStar.transform);
            _star.transform.position = _vec;
            _star.GetComponent<StarLevel>().InitData(imgStarUp.transform.position);
        }



        //新关卡开启动画部分
        if (GameController.GetInstance().hasNew && playState == 0)
        {
            //if ( typeInit == 100)
            //{
            //    GameController.GetInstance().currentLevel = currentPage * 10 + 9;
            //    LocalData.GetInstance().SetMaxOpenLevel(GameController.GetInstance().currentLevel + 2);
            //}

            isPlayMov = true;
            if ((LocalData.GetInstance().GetMaxOpenLevel() - 1) % 10 == 0)
            {
                if (GameController.GetInstance().hasNewLast == false)
                {
                    isPlayMov = false;
                    return;
                }
                else
                {
                    btnBigLevel.gameObject.SetActive(true);
                    btnBigLevel.image.sprite = sprBtnBig[currentPage];
                    btnBigLevel.transform.position = btnPosition[10];
                    objPanelBtnBig.SetActive(false);
                    if (currentPage == 9)
                    {
                        big120.SetActive(true);
                    }
                }
            }
            timePlay = 0;
            playState = 0;

            ButtonLevel _cicle = panelBtn.transform.
                GetChild((LocalData.GetInstance().GetMaxOpenLevel() - 2) % 10).GetComponent<ButtonLevel>();
            _cicle.Play0();
            needStwp = GetSetp();
            currStwp = 0;
            isS1 = isS2 = isS3 = isS4 = isS5 = isS6 = false;
            return;
        }
        if (isPlayMov == false)
        {
            isCanClick = true;
        }

    }

    /// <summary>
    /// 获得指定关卡星星数量
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <returns></returns>
    private int GetStarNum(int startIndex, int endIndex)
    {
        int _num = 0;
        for (int i = startIndex; i < endIndex; i++)
        {
            if (LocalData.GetInstance().levelWmState[i] == 0)
            {
                _num = 1;
                break;
            }
            _num++;
        }
        return _num;
    }

    public void PlayClear0() { aniClear0.gameObject.SetActive(true); Tools.PlayAnimation(aniClear0, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear1() { aniClear1.gameObject.SetActive(true); Tools.PlayAnimation(aniClear1, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear2() { aniClear2.gameObject.SetActive(true); Tools.PlayAnimation(aniClear2, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear3() { aniClear3.gameObject.SetActive(true); Tools.PlayAnimation(aniClear3, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear4() { aniClear4.gameObject.SetActive(true); Tools.PlayAnimation(aniClear4, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear5() { aniClear5.gameObject.SetActive(true); Tools.PlayAnimation(aniClear5, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear6() { aniClear6.gameObject.SetActive(true); Tools.PlayAnimation(aniClear6, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear7() { aniClear7.gameObject.SetActive(true); Tools.PlayAnimation(aniClear7, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear8() { aniClear8.gameObject.SetActive(true); Tools.PlayAnimation(aniClear8, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear9() { aniClear9.gameObject.SetActive(true); Tools.PlayAnimation(aniClear9, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear10() { aniClear10.gameObject.SetActive(true); Tools.PlayAnimation(aniClear10, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear11() { aniClear11.gameObject.SetActive(true); Tools.PlayAnimation(aniClear11, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear12() { aniClear12.gameObject.SetActive(true); Tools.PlayAnimation(aniClear12, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear13() { aniClear13.gameObject.SetActive(true); Tools.PlayAnimation(aniClear13, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }
    public void PlayClear14() { aniClear14.gameObject.SetActive(true); Tools.PlayAnimation(aniClear14, "Clear"); AudioManager.GetInstance().PlaySound(AudioManager.SoundClear); }

    /// <summary>
    /// 点击大关卡图标，刷新坐标
    /// </summary>
    public void InitCicleImage(int _currentPage)
    {
        if (_currentPage > 14)
        {
            return;
        }
        if (LocalData.GetInstance().playGameTime == 0 || currentPage != _currentPage)
        {
            currentPage = _currentPage;
            Refrush(0);
            int _add = 390;
            float _xx = new float[] {testNum,testNum,-224,
        -224-_add,-224-_add*2,-224-_add*3,-224-_add*4,-224-_add*5,
            -224-_add*6,-224-_add*7,-224-_add*8,-224-_add*9,-224-_add*10,
            -4320-testNum,-4320-testNum}[currentPage];
            LeanTween.moveLocal(bigBtnPanel, new Vector3(_xx, 0, 0), 0.1f);
            btnDrag.GetComponent<SelectDrag>().ChangePositon2(currentPage * 0.5f);
        }
    }

    /// <summary>
    /// 拖动刷新
    /// </summary>
    /// <param name="_x"></param>
    public void Refrush(float _x, int _type)
    {
        float _moveX = (_x + 260) / 52 * 432;
        bigBtnPanel.transform.localPosition = new Vector3(-_moveX, bigBtnPanel.transform.localPosition.y, 0);
    }

    public void PlayLockOpen()
    {
        btnBuy.gameObject.SetActive(false);
        InitCicleImage(8);
        aniSuo8.gameObject.SetActive(true);
        aniSuo9.gameObject.SetActive(true);
        aniSuo10.gameObject.SetActive(true);
        aniSuo11.gameObject.SetActive(true);
        aniSuo12.gameObject.SetActive(true);
        aniSuo13.gameObject.SetActive(true);
        aniSuo14.gameObject.SetActive(true);
        Invoke("DelayLock", 0.5f);
    }

    void DelayLock()
    {

        Tools.PlayAnimation(aniSuo8, "DGQsd-BaoZha");
        Tools.PlayAnimation(aniSuo9, "DGQsd-BaoZha");
        Tools.PlayAnimation(aniSuo10, "DGQsd-BaoZha");
        Tools.PlayAnimation(aniSuo11, "DGQsd-BaoZha");
        Tools.PlayAnimation(aniSuo12, "DGQsd-BaoZha");
        Tools.PlayAnimation(aniSuo13, "DGQsd-BaoZha");
        Tools.PlayAnimation(aniSuo14, "DGQsd-BaoZha");
    }

    public Button GetLevelButton(int _index)
    {

        return null;
    }

    public void ShowBuy()
    {
        //LeanTween.scale(btnBuy.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setLoopPingPong(1);

        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniShop0.gameObject.SetActive(false);
            aniShop1.gameObject.SetActive(true);
            aniShop1.Play("SC-01-DianJi", PlayMode.StopAll);
            panelBuy.SetActive(true);
            panelBuy.GetComponent<BuyADPanel>().InitData();
            Invoke("Reset0", 1.0f);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundUIShow);
        }
        else
        {
            aniShop0.gameObject.SetActive(true);
            aniShop1.gameObject.SetActive(false);
            aniShop0.Play("SC-02-DianJ", PlayMode.StopAll);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, " Has buy success");
            Invoke("Reset1", 1.0f);
        }
    }

    private void Reset0()
    {
        aniShop0.Play("SC-02-XianZhi", PlayMode.StopAll);
    }

    private void Reset1()
    {
        aniShop1.Play("SC-01-XianZhi", PlayMode.StopAll);
    }

    public void ShowSet()
    {
        LeanTween.scale(btnSet.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
        panelSet.SetActive(true);
        panelSet.GetComponent<SettingPanel>().InitData();
        AudioManager.GetInstance().PlaySound(AudioManager.SoundUIShow);
        //测试音效用
        //GameController.GetInstance().SoundType++;
        //if (GameController.GetInstance().SoundType > 3)
        //{
        //    GameController.GetInstance().SoundType = 0;
        //}
        //textSound.text = GameController.GetInstance().SoundType + 1 + "";
    }
    public void ClickBook()
    {
        LeanTween.scale(btnBook.gameObject, new Vector3(0.85f,0.85f,0.85f), 0.1f).setLoopPingPong(1);
        Invoke("ShowHelp",0.1f);
    }
    

    public void ShowHelp()
    {
        LeanTween.scale(btnSet.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
        gameObject.SetActive(false);
        UIManager.GetInstance().viewBook.SetActive(true);
        UIManager.GetInstance().viewBook.GetComponent<ViewBook>().InitData();
    }

    public void ShowPage()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        if (imgR0.gameObject.activeSelf == false)
        {
            imgR0.SetActive(true);
            imgR1.SetActive(false);
            LeanTween.move(buttonPage.gameObject, new Vector3(buttonPage.transform.position.x, btnBigPanelY - 2.8f, 0), 0.1f);
        }
        else
        {
            imgR0.SetActive(false);
            imgR1.SetActive(true);
            LeanTween.move(buttonPage.gameObject, new Vector3(buttonPage.transform.position.x, btnBigPanelY, 0), 0.1f);
        }
    }

    public void StoneClick()
    {
        if (isCanClick)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            if ((currentPage + 1) * 10 >= LocalData.GetInstance().GetMaxOpenLevel())
            {
                LeanTween.scale(btnBigLevel.gameObject, new Vector3(0.7f, 0.7f, 0.7f), 0.1f).setLoopCount(1);
                Invoke("Reset00", 0.1f);
            }
            else
            {
                LeanTween.scale(objPanelBtnBig.gameObject, new Vector3(0.7f, 0.7f, 0.7f), 0.1f).setLoopOnce();
                Invoke("Reset00", 0.1f);
            }
        }
    }

    public void StoneClick1()
    {
        if (isCanClick)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            LeanTween.scale(big120.gameObject, new Vector3(0.7f, 0.7f, 0.7f), 0.1f).setLoopOnce();
            Invoke("Reset11", 0.1f);
        }
    }

    public void StoneClick2()
    {
        if (isCanClick)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            LeanTween.scale(big121.gameObject, new Vector3(90,90,90), 0.1f).setLoopOnce();
            Invoke("Reset22", 0.1f);
        }
    }

    private void Reset00()
    {
        if ((currentPage + 1) * 10 >= LocalData.GetInstance().GetMaxOpenLevel())
        {
            LeanTween.scale(btnBigLevel.gameObject, new Vector3(1, 1, 1), 0.1f).setLoopOnce();
        }
        else
        {
            LeanTween.scale(objPanelBtnBig.gameObject, new Vector3(1, 1, 1), 0.1f).setLoopOnce();
        }
    }

    private void Reset11()
    {
        LeanTween.scale(big120.gameObject, new Vector3(1, 1, 1), 0.1f).setLoopOnce();
    }

    private void Reset22()
    {
        LeanTween.scale(big121.gameObject, new Vector3(120, 120, 120), 0.1f).setLoopOnce();
    }


    /// <summary>
    /// 刷新大按钮面板位置，用来纠正位置偏移
    /// </summary>
    private void RefureshPosition()
    {
        if (imgR0.gameObject.activeSelf == false)
        {
            imgR0.SetActive(true);
            imgR1.SetActive(false);
            LeanTween.move(buttonPage.gameObject, new Vector3(buttonPage.transform.position.x, btnBigPanelY - 2.8f, 0), 0.1f);
        }
        else
        {
            imgR0.SetActive(false);
            imgR1.SetActive(true);
            LeanTween.move(buttonPage.gameObject, new Vector3(buttonPage.transform.position.x, btnBigPanelY, 0), 0.1f);
        }
        //Invoke("aaa", 0.1f);
    }

    //public void aaa()
    //{
    //    if (imgR0.gameObject.activeSelf == false)
    //    {
    //        imgR0.SetActive(true);
    //        imgR1.SetActive(false);
    //        LeanTween.move(buttonPage.gameObject, new Vector3(buttonPage.transform.position.x, btnBigPanelY - 2.8f,0), 0.1f);
    //    }
    //    else
    //    {
    //        imgR0.SetActive(false);
    //        imgR1.SetActive(true);
    //        LeanTween.move(buttonPage.gameObject, new Vector3(buttonPage.transform.position.x, btnBigPanelY ,0), 0.1f);
    //    }
    //}

    public void BackClick()
    {
        //if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.SelectLevel)
        //{
        //    AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        //    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, false);
        //    UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.StartMenu, true);
        //}
    }


    public void ClickBig0()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(0);
    }

    public void ClickBig1()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(1);
    }
    public void ClickBig2()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(2);
    }
    public void ClickBig3()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(3);
    }
    public void ClickBig4()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(4);
    }
    public void ClickBig5()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(5);
    }
    public void ClickBig6()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(6);
    }
    public void ClickBig7()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(7);
    }
    public void ClickBig8()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(8);
        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniSuo8.Play("DGQsd-DianJi", PlayMode.StopAll);
            panelBuy.SetActive(true);
        }
    }
    public void ClickBig9()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(9);
        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniSuo9.Play("DGQsd-DianJi", PlayMode.StopAll);
            panelBuy.SetActive(true);
            panelBuy.GetComponent<BuyADPanel>().InitData();
        }
    }
    public void ClickBig10()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(10);
        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniSuo10.Play("DGQsd-DianJi", PlayMode.StopAll);
            panelBuy.SetActive(true); panelBuy.GetComponent<BuyADPanel>().InitData();
        }
    }
    public void ClickBig11()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(11);
        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniSuo11.Play("DGQsd-DianJi", PlayMode.StopAll);
            panelBuy.SetActive(true); panelBuy.GetComponent<BuyADPanel>().InitData();
        }
    }
    public void ClickBig12()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(12);
        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniSuo12.Play("DGQsd-DianJi", PlayMode.StopAll);
            panelBuy.SetActive(true); panelBuy.GetComponent<BuyADPanel>().InitData();
        }
    }
    public void ClickBig13()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(13);
        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniSuo13.Play("DGQsd-DianJi", PlayMode.StopAll);
            panelBuy.SetActive(true); panelBuy.GetComponent<BuyADPanel>().InitData();
        }
    }
    public void ClickBig14()
    {
        if (GameController.GetInstance().hasNew)
        {
            return;
        }
        InitCicleImage(14);
        if (LocalData.GetInstance().stateBuy == 0)
        {
            aniSuo14.Play("DGQsd-DianJi", PlayMode.StopAll);
            panelBuy.SetActive(true); panelBuy.GetComponent<BuyADPanel>().InitData();
        }
    }

    private void ClickLevelButton(int _levelID)
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.SelectLevel)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            if (LocalData.GetInstance().GetMaxOpenLevel() > _levelID)
            {
                //进行精细划分，看广告，还是买游戏，还是进游戏                
                GameController.GetInstance().currentLevel = _levelID;
                if (_levelID <= 79)
                {
                    if (LocalData.GetInstance().timePlayGame >= 5)
                    {
                        panelStartGame.SetActive(true);
                        panelStartGame.GetComponent<StartGamePanel>().InitData();
                    }
                    else
                    {
                        panelStartGame.SetActive(true);
                        panelStartGame.GetComponent<StartGamePanel>().InitData();
                    }
                }
                else
                {
                    if (LocalData.GetInstance().stateBuy == 1)
                    {
                        panelStartGame.SetActive(true);
                        panelStartGame.GetComponent<StartGamePanel>().InitData();
                    }
                    else
                    {
                        panelBuy.SetActive(true); panelBuy.GetComponent<BuyADPanel>().InitData();
                    }
                }
                //UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, true);
                //AudioManager.GetInstance().PlayMusic(AudioManager.MusicGame);
            }
            else
            {
                UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "Levels not unlocked yet");
            }
        }
    }


    private void ChangeState(int _state)
    {
        if (playState != _state)
        {
            playState = _state;
        }
        else
        {
            return;
        }
        switch (playState)
        {
            case 0:

                break;
            case 1:


                break;
            case 2:
                if (GameController.GetInstance().hasNewLast == false)
                {
                    ButtonLevel _cicle = panelBtn.transform.
                    GetChild((LocalData.GetInstance().GetMaxOpenLevel() - 1) % 10).GetComponent<ButtonLevel>();
                    _cicle.Play1();
                    AudioManager.GetInstance().PlaySound(AudioManager.SoundSmallLevel);
                }
                break;
            case 3:
                if (GameController.GetInstance().hasNewLast == false)//没有新大关卡解锁，直接显示开始游戏面板
                {
                    GameController.GetInstance().hasNew = false;
                    GameController.GetInstance().currentLevel++;
                    isCanClick = true;
                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.SetActive(true);
                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.GetComponent<StartGamePanel>().InitData();
                }
                else
                {
                    //点亮石像和关卡的遮罩
                    mask0.SetActive(true);
                    mask1.SetActive(false);
                    mask2.SetActive(true);
                    mask0.transform.position = btnPosition[10];
                    mask0.GetComponent<ParticleSystem>().Stop();
                    mask0.GetComponent<ParticleSystem>().Play();
                    if (currentPage == 9)
                    {
                        mask1.SetActive(true);
                        mask1.transform.position = big120.transform.position;
                        mask1.GetComponent<ParticleSystem>().Stop();
                        mask1.GetComponent<ParticleSystem>().Play();
                    }
                    Vector3 vec = new Vector3(0, 0, 0);
                    switch (currentPage)
                    {
                        case 0: vec = btnBig1.transform.position; break;
                        case 1: vec = btnBig2.transform.position; break;
                        case 2: vec = btnBig3.transform.position; break;
                        case 3: vec = btnBig4.transform.position; break;
                        case 4: vec = btnBig5.transform.position; break;
                        case 5: vec = btnBig6.transform.position; break;
                        case 6: vec = btnBig7.transform.position; break;
                        case 7: vec = btnBig8.transform.position; break;
                        case 8: vec = btnBig9.transform.position; break;
                        case 9: vec = btnBig10.transform.position; break;
                        case 10: vec = btnBig11.transform.position; break;
                        case 11: vec = btnBig12.transform.position; break;
                        case 12: vec = btnBig13.transform.position; break;
                        case 13: vec = btnBig14.transform.position; break;
                    }
                    mask2.transform.position = vec;
                    mask2.GetComponent<ParticleSystem>().Stop();
                    mask2.GetComponent<ParticleSystem>().Play();
                }
                break;
            case 4:
                if (GameController.GetInstance().hasNewLast)
                {
                    big120.SetActive(false);
                    big121.SetActive(false);

                    btnBigLevel.gameObject.SetActive(false);
                    objPanelBtnBig.SetActive(true);
                    aniBtnBig[0].gameObject.SetActive(false);
                    aniBtnBig[1].gameObject.SetActive(false);
                    aniBtnBig[2].gameObject.SetActive(false);
                    aniBtnBig[3].gameObject.SetActive(false);
                    aniBtnBig[4].gameObject.SetActive(false);
                    aniBtnBig[5].gameObject.SetActive(false);
                    aniBtnBig[6].gameObject.SetActive(false);
                    aniBtnBig[7].gameObject.SetActive(false);
                    aniBtnBig[8].gameObject.SetActive(false);
                    aniBtnBig[9].gameObject.SetActive(false);
                    aniBtnBig[10].gameObject.SetActive(false);
                    aniBtnBig[11].gameObject.SetActive(false);
                    aniBtnBig[12].gameObject.SetActive(false);
                    aniBtnBig[13].gameObject.SetActive(false);
                    aniBtnBig[14].gameObject.SetActive(false);
                    aniBtnBig[currentPage].gameObject.SetActive(true);
                    objPanelBtnBig.transform.position = btnPosition[10];
                    if (currentPage == 9)
                    {
                        big121.SetActive(true);
                    }

                    switch (currentPage)
                    {
                        case 0: btnBig1.image.sprite = sprBig1[1]; break;
                        case 1: btnBig2.image.sprite = sprBig2[1]; break;
                        case 2: btnBig3.image.sprite = sprBig3[1]; break;
                        case 3: btnBig4.image.sprite = sprBig4[1]; break;
                        case 4: btnBig5.image.sprite = sprBig5[1]; break;
                        case 5: btnBig6.image.sprite = sprBig6[1]; break;
                        case 6: btnBig7.image.sprite = sprBig7[1]; break;
                        case 7: btnBig8.image.sprite = sprBig8[1]; break;
                        case 8: btnBig9.image.sprite = sprBig9[1]; break;
                        case 9: btnBig10.image.sprite = sprBig10[1]; break;
                        case 10: btnBig11.image.sprite = sprBig11[1]; break;
                        case 11: btnBig12.image.sprite = sprBig12[1]; break;
                        case 12: btnBig13.image.sprite = sprBig13[1]; break;
                        case 13: btnBig14.image.sprite = sprBig14[1]; break;
                    }
                    AudioManager.GetInstance().PlaySound(AudioManager.SoundBigLevel);
                }
                break;
            case 5:
                if (GameController.GetInstance().hasNewLast)
                {
                    int next = currentPage + 1;
                    if (next > 14)
                    {
                        GameController.GetInstance().hasNew = false;
                        GameController.GetInstance().hasNewLast = false;
                        winLast.gameObject.SetActive(true);
                        winLast.GetComponent<WinLastPanel>().InitData();
                        isCanClick = true;
                    }
                    else
                    {
                        //GameController.GetInstance().hasNew = false;
                        //GameController.GetInstance().hasNewLast = false;
                        InitCicleImage(next);
                        //GameController.GetInstance().currentLevel++;
                        //Debug.Log("<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>寄了5");
                        //UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.SetActive(true);
                        //UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().
                        //    panelStartGame.GetComponent<StartGamePanel>().InitData();
                        //isCanClick = true;
                    }
                }
                break;
            case 6:
                if (GameController.GetInstance().hasNewLast)
                {
                    //int next = currentPage + 1;
                    //if (next > 14)
                    //{
                    //GameController.GetInstance().hasNew = false;
                    //GameController.GetInstance().hasNewLast = false;
                    //winLast.gameObject.SetActive(true);
                    //winLast.GetComponent<WinLastPanel>().InitData();
                    //isCanClick = true;
                    //}
                    //else
                    //{
                    GameController.GetInstance().hasNew = false;
                    GameController.GetInstance().hasNewLast = false;
                    //InitCicleImage(next);
                    GameController.GetInstance().currentLevel++;
                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelStartGame.SetActive(true);
                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().
                        panelStartGame.GetComponent<StartGamePanel>().InitData();
                    isCanClick = true;
                    //}
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showBook)
        {
            return;
        }
            if (GameController.GetInstance().hasNew && UIManager.GetInstance().panelCloud.activeSelf == false)
        {
            timePlay += Time.deltaTime;
            if (playState == 0)
            {
                if (timePlay > stepTime[0])
                {
                    ChangeState(1);
                }
            }
            else if (playState == 1)
            {
                if (timePlay < stepTime[0] + needStwp * stepTime[1] + stepTime[2])
                {
                    UpdateStep();
                }
                else
                {
                    ChangeState(2);
                }
            }
            else if (playState == 2)
            {
                if (timePlay > stepTime[0] + needStwp * stepTime[1] + stepTime[2] + stepTime[3])
                {
                    ChangeState(3);
                }
            }
            else if (playState == 3)
            {
                if (timePlay > stepTime[0] + needStwp * stepTime[1] + stepTime[2] + stepTime[3] + stepTime[4])
                {
                    ChangeState(4);
                }
            }
            else if (playState == 4)
            {
                if (timePlay > stepTime[0] + needStwp * stepTime[1] + stepTime[2] + stepTime[3] + stepTime[4] + stepTime[5])
                {
                    ChangeState(5);
                }
            }
            else if (playState == 5)
            {
                if (timePlay > stepTime[0] + needStwp * stepTime[1] + stepTime[2] + stepTime[3] + stepTime[4] + stepTime[5] + stepTime[6])
                {
                    ChangeState(6);
                }
            }
        }
    }

    private void InitStepData()
    {
        switch (currentPage)
        {
            case 0: curPageStep = panelStep0; stepNum = new int[] { 3, 6, 4, 3, 3, 3, 2, 3, 3, 3 }; break;
            case 1: curPageStep = panelStep1; stepNum = new int[] { 4, 3, 3, 2, 2, 4, 3, 2, 4, 3 }; break;
            case 2: curPageStep = panelStep2; stepNum = new int[] { 3, 2, 3, 2, 3, 3, 4, 2, 3, 2 }; break;
            case 3: curPageStep = panelStep3; stepNum = new int[] { 3, 3, 3, 3, 3, 4, 3, 3, 3, 3 }; break;
            case 4: curPageStep = panelStep4; stepNum = new int[] { 3, 4, 2, 5, 3, 5, 2, 3, 4, 3 }; break;
            case 5: curPageStep = panelStep5; stepNum = new int[] { 3, 3, 3, 3, 3, 2, 3, 3, 2, 3 }; break;
            case 6: curPageStep = panelStep6; stepNum = new int[] { 3, 3, 3, 3, 3, 4, 3, 3, 4, 2 }; break;
            case 7: curPageStep = panelStep7; stepNum = new int[] { 4, 4, 4, 3, 2, 5, 5, 2, 2, 2 }; break;
            case 8: curPageStep = panelStep8; stepNum = new int[] { 3, 3, 3, 3, 3, 2, 3, 3, 3, 2 }; break;
            case 9: curPageStep = panelStep9; stepNum = new int[] { 3, 2, 2, 2, 3, 3, 4, 3, 3, 6 }; break;
            case 10: curPageStep = panelStep10; stepNum = new int[] { 3, 3, 3, 3, 2, 3, 3, 3, 4, 2 }; break;
            case 11: curPageStep = panelStep11; stepNum = new int[] { 3, 3, 3, 3, 2, 3, 3, 3, 4, 2 }; break;
            case 12: curPageStep = panelStep12; stepNum = new int[] { 3, 3, 3, 4, 3, 3, 3, 2, 2, 3 }; break;
            case 13: curPageStep = panelStep13; stepNum = new int[] { 3, 3, 3, 4, 3, 3, 3, 3, 3, 3 }; break;
            case 14: curPageStep = panelStep14; stepNum = new int[] { 2, 4, 3, 3, 4, 3, 3, 3, 3, 3 }; break;
        }
    }

    /// <summary>
    /// 换页的时候初始化步数显示
    /// </summary>
    public void SetpInitView()
    {

        //需要根据页数、关卡、数组计算出当前需要切换图片的步数数量
        //for循环计算，根据
        InitStepData();
        int needNum = 0;
        if (GameController.GetInstance().hasNew)
        {
            _length = curPageStep.transform.childCount;

            for (int i = 0; i < 10; i++)
            {
                if (currentPage * 10 + i + 2 < LocalData.GetInstance().GetMaxOpenLevel())
                {
                    needNum += stepNum[i];
                }
            }
            upStwp = needNum;
        }
        else
        {
            _length = curPageStep.transform.childCount;
            for (int i = 0; i < 10; i++)
            {
                if (currentPage * 10 + i + 1 < LocalData.GetInstance().GetMaxOpenLevel())
                {
                    needNum += stepNum[i];
                }
            }
        }

        for (int i = 0; i < _length; i++)
        {
            Image img = curPageStep.transform.GetChild(i).gameObject.GetComponent<Image>();
            if (i < needNum)
            {
                img.sprite = stepSprite[1];
            }
            else
            {
                img.sprite = stepSprite[0];
            }
        }

    }

    /// <summary>
    /// 获得需要显示的步数
    /// </summary>
    public int GetSetp()
    {
        InitStepData();
        //需要根据页数、关卡、数组计算出当前需要切换图片的步数数量
        //for循环计算，根据
        int _step = stepNum[(LocalData.GetInstance().GetMaxOpenLevel() - 2) % 10];
        return _step;
    }

    /// <summary>
    /// 获得需要显示的步数
    /// </summary>
    public void UpdateStep()
    {
        InitStepData();
        if (timePlay > 0.2f + 0.2f && timePlay < 0.4f + 0.2f)
        {
            if (isS1 == false)
            {
                isS1 = true; Image img = curPageStep.transform.GetChild(upStwp + currStwp).gameObject.GetComponent<Image>();
                img.sprite = stepSprite[1];
                AudioManager.GetInstance().PlaySound(AudioManager.SoundStep);
            }
        }
        if (timePlay > 0.4f + 0.2f && timePlay < 0.6f + 0.2f)
        {
            if (isS2 == false)
            {
                isS2 = true;
                currStwp++; Image img = curPageStep.transform.GetChild(upStwp + currStwp).gameObject.GetComponent<Image>();
                img.sprite = stepSprite[1];
                AudioManager.GetInstance().PlaySound(AudioManager.SoundStep);
            }
        }
        else if (timePlay > 0.6f + 0.2f && timePlay < 0.8f + 0.2f)
        {
            if (isS3 == false)
            {
                isS3 = true;
                currStwp++; Image img = curPageStep.transform.GetChild(upStwp + currStwp).gameObject.GetComponent<Image>();
                img.sprite = stepSprite[1];
                AudioManager.GetInstance().PlaySound(AudioManager.SoundStep);
            }
        }
        else if (timePlay > 0.8f + 0.2f && timePlay < 1.0f + 0.2f)
        {
            if (isS4 == false)
            {
                isS4 = true;
                currStwp++; Image img = curPageStep.transform.GetChild(upStwp + currStwp).gameObject.GetComponent<Image>();
                img.sprite = stepSprite[1];
                AudioManager.GetInstance().PlaySound(AudioManager.SoundStep);
            }
        }
        else if (timePlay > 1.0f + 0.2f && timePlay < 1.2f + 0.2f)
        {
            if (isS5 == false)
            {
                isS5 = true;
                currStwp++; Image img = curPageStep.transform.GetChild(upStwp + currStwp).gameObject.GetComponent<Image>();
                img.sprite = stepSprite[1];
                AudioManager.GetInstance().PlaySound(AudioManager.SoundStep);
            }
        }
        else if (timePlay > 1.2f + 0.2f && timePlay < 1.4f + 0.2f)
        {
            if (isS6 == false)
            {
                isS6 = true;
                currStwp++; Image img = curPageStep.transform.GetChild(upStwp + currStwp).gameObject.GetComponent<Image>();
                img.sprite = stepSprite[1];
                AudioManager.GetInstance().PlaySound(AudioManager.SoundStep);
            }
        }
    }


    //===========================================
    void OnClickDown(GameObject go)
    {
        isExit = false;
    }

    void OnClickUp(GameObject go)
    {
        isExit = true;
    }

    void OnClickEnter(GameObject go)
    {
        isExit = false;
    }

    void OnClickExit(GameObject go)
    {
        isExit = true;
    }

    public float strrtPos;
    public float startDX;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlayMov == true)
        {
            return;
        }
        if (isCanClick == false)
        {
            return;
        }
        strrtPos = bigBtnPanel.transform.position.x;
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(bigBtnPanel.GetComponent<RectTransform>(), eventData.position, eventData.enterEventCamera, out pos);
        startDX = pos.x;
        if (startDX < -44 || startDX > 44)
        {
            startDX = (float)startDX / 100;
        }
    }

    private bool isLeft;
    float endX;
    public void OnDrag(PointerEventData eventData)
    {
        if (isExit || panelBuy.activeSelf || panelSet.activeSelf || panelStartGame.activeSelf)
        {
            return;
        }

        Vector3 pos1;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(bigBtnPanel.GetComponent<RectTransform>(), eventData.position, eventData.enterEventCamera, out pos1);
        if (startDX < pos1.x)
        {
            isLeft = false;
        }
        else
        {
            isLeft = true;
        }
        endX = strrtPos + (pos1.x - startDX) * 1.8f;
        endX = Mathf.Clamp(endX, -35.5f - testNum / 100, 0 + testNum / 100);
        bigBtnPanel.transform.position = new Vector3(endX, bigBtnPanel.transform.position.y, 0);

        //float _localX = bigBtnPanel.transform.localPosition.x;
        //_localX = Mathf.Clamp(_localX, -4320-testNum, 0);
        //bigBtnPanel.transform.localPosition = new Vector3(_localX, bigBtnPanel.transform.localPosition.y, 0);

        btnDrag.GetComponent<SelectDrag>().ChangePositon2(-endX * 100 / (4400 / 12) * 0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isExit || panelBuy.activeSelf || panelSet.activeSelf || panelStartGame.activeSelf)
        {
            return;
        }
        //拖拽结束后添加惯性
        if (isLeft)
        {
            endX -= 4;
        }
        else
        {
            endX += 4;
        }
        endX = Mathf.Clamp(endX, -35.5f - testNum / 100, 0 + testNum / 100);
        LeanTween.move(bigBtnPanel, new Vector3(endX, bigBtnPanel.transform.position.y, 0), 0.4f);
    }

    private void ShowClear()
    {
        float _t = 0f;//clear动画延迟播放时间
        int _num = 0;
        Tools.StopAnimation(aniClear0, "Clear");
        _num = GetStarNum(0 * 10, 0 * 10 + 10);
        aniClear0.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear0.gameObject.SetActive(false);
            Invoke("PlayClear0", _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear1, "Clear");
        _num = GetStarNum(1 * 10, 1 * 10 + 10);
        aniClear1.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear1.gameObject.SetActive(false);
            Invoke("PlayClear" + 1, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear2, "Clear");
        _num = GetStarNum(2 * 10, 2 * 10 + 10);
        aniClear2.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear2.gameObject.SetActive(false);
            Invoke("PlayClear" + 2, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear3, "Clear");
        _num = GetStarNum(3 * 10, 3 * 10 + 10);
        aniClear3.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear3.gameObject.SetActive(false);
            Invoke("PlayClear" + 3, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear4, "Clear");
        _num = GetStarNum(4 * 10, 4 * 10 + 10);
        aniClear4.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear4.gameObject.SetActive(false);
            Invoke("PlayClear" + 4, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear5, "Clear");
        _num = GetStarNum(5 * 10, 5 * 10 + 10);
        aniClear5.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear5.gameObject.SetActive(false);
            Invoke("PlayClear" + 5, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear6, "Clear");
        _num = GetStarNum(6 * 10, 6 * 10 + 10);
        aniClear6.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear6.gameObject.SetActive(false);
            Invoke("PlayClear" + 6, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear7, "Clear");
        _num = GetStarNum(7 * 10, 7 * 10 + 10);
        aniClear7.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear7.gameObject.SetActive(false);
            Invoke("PlayClear" + 7, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear8, "Clear");
        _num = GetStarNum(8 * 10, 8 * 10 + 10);
        aniClear8.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear8.gameObject.SetActive(false);
            Invoke("PlayClear" + 8, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear9, "Clear");
        _num = GetStarNum(9 * 10, 9 * 10 + 10);
        aniClear9.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear9.gameObject.SetActive(false);
            Invoke("PlayClear" + 9, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear10, "Clear");
        _num = GetStarNum(10 * 10, 10 * 10 + 10);
        aniClear10.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear10.gameObject.SetActive(false);
            Invoke("PlayClear" + 10, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear11, "Clear");
        _num = GetStarNum(11 * 10, 11 * 10 + 10);
        aniClear11.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear11.gameObject.SetActive(false);
            Invoke("PlayClear" + 11, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear12, "Clear");
        _num = GetStarNum(12 * 10, 12 * 10 + 10);
        aniClear12.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear12.gameObject.SetActive(false);
            Invoke("PlayClear" + 12, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear13, "Clear");
        _num = GetStarNum(13 * 10, 13 * 10 + 10);
        aniClear13.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear13.gameObject.SetActive(false);
            Invoke("PlayClear" + 13, _t);
        }
        _num = 0;
        Tools.StopAnimation(aniClear14, "Clear");
        _num = GetStarNum(14 * 10, 14 * 10 + 10);
        aniClear14.gameObject.SetActive(_num == 10 ? true : false);
        if (GameController.GetInstance().hasWan && _num == 10)
        {
            aniClear14.gameObject.SetActive(false);
            Invoke("PlayClear" + 14, _t);
        }
    }

    Tweener tweenPath;

    /// <summary>
    /// 左上角星星一缩一放效果
    /// </summary>
    public void StartScale()
    {
        tweenPath = UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().imgStarLeft.
                   transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.2f).SetLoops(1);

        Invoke("OnWaypointChange2", 0.2f);

    }

    private void OnWaypointChange2()
    {
        tweenPath = UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().imgStarLeft.
                   transform.DOScale(new Vector3(1,1,1), 0.2f).SetLoops(1);
    }
}