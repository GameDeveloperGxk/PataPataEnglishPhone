using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 单元
/// </summary>
public class UnitCicle : MonoBehaviour
{
    // 当前状态
    public enum unitCicleState
    {
        wait,//等待
        shoot,//射击
        end,//结束
        locked,//锁住
        boom,
        dance,//完美状态下跳跃
    }
    //当前状态
    public unitCicleState currentUnitState;

    //穿越特效
    public GameObject effectBlue, effectRed, effectGreen, effectPurple;

    //音符图
    public Image imageSign;
    //音符图集
    public Sprite[] spriteSign;
    //锁住图
    public GameObject gameObjectLock;
    //锁住动画
    public Animation aniLock;

    public GameObject[] GameObejctAni;

    //根据地图ID计算出imageid范围，imageSmallID和imageid成对应关系
    public int imageSmallID;
    /// <summary>
    /// 图片ID 用来控制图片显示imageSprite,每个imageID对应多个imageSmallID（角度不同）
    /// </summary>
    public int imageID = -1;

    /// <summary>
    /// 地图数据=生成的 ID 每个图片根据旋转角度对应多个createID,createID和imageID成对应关系
    /// createID划分段：
    /// 0-99 纯图片 (0)
    /// 100-199穿越蓝色（穿越1+纯图片ID）
    /// 200-299穿越红色（穿越2+纯图片ID）
    /// 300-399穿越绿色（穿越3+纯图片ID）
    /// 400-499穿越紫色（穿越4+纯图片ID）
    /// 1000,2000,3000,4000为穿越门
    /// </summary>
    public int createID = 0;
    //是否锁住
    public bool isLock = false;

    //当前角度
    public int rotateNowAngel = 0;
    // 默认旋转角度
    public int rotateAngel = 0;
    // 当前朝向的状态,根据旋转角度  每个音符会有2 4 8不同个角度
    public int rotateState = 1;

    public int line = 0;
    public int row = 0;

    //发射时间
    public float shootTime = 0;

    public GameObject gameObjectSign;

    public GameObject gameObjectChuan1;
    public GameObject gameObjectChuan2;
    public GameObject gameObjectChuan3;
    public GameObject gameObjectChuan4;

    public GameObject gameObjectChuan11;
    public GameObject gameObjectChuan22;
    public GameObject gameObjectChuan33;
    public GameObject gameObjectChuan44;

    public Animation aniBall0;
    public Animation aniBall1;
    public Animation aniBall2;
    public Animation aniBall3;
    public Animation aniBall4;
    public Animation aniBall5;
    public Animation aniBall6;
    public Animation aniBall7;
    public Animation aniBall8;
    public Animation aniBall9;
    public Animation aniBall10;
    public Animation aniBall11;
    public Animation aniBall12;
    public Animation aniBall13;
    public Animation aniBall14;
    public Animation aniBall15;
    public Animation aniBall16;
    public Animation aniBall17;
    public Animation aniBall18;

    public GameObject parBall0;
    public GameObject parBall1;
    public GameObject parBall2;
    public GameObject parBall3;
    public GameObject parBall4;
    public GameObject parBall5;
    public GameObject parBall5X;
    public GameObject parBall6;
    public GameObject parBall6X;
    public GameObject parBall7;
    public GameObject parBall8;
    public GameObject parBall9;
    public GameObject parBall10;
    public GameObject parBall11;
    public GameObject parBall12;
    public GameObject parBall13;
    public GameObject parBall14;
    public GameObject parBall15;
    public GameObject parBall15X;
    public GameObject parBall16;
    public GameObject parBall17;
    public GameObject parBall18;

    public GameObject parJin1;
    public GameObject parJin2;
    public GameObject parJin3;
    public GameObject parJin4;
    public GameObject parJin11;
    public GameObject parJin22;
    public GameObject parJin33;
    public GameObject parJin44;


    public GameObject parfBall1;
    public GameObject parfBall2;
    public GameObject parfBall3;
    public GameObject parfBall4;
    public GameObject parfBall5;
    public GameObject parfBall5X;
    public GameObject parfBall6;
    public GameObject parfBall6X;
    public GameObject parfBall7;
    public GameObject parfBall8;
    public GameObject parfBall9;
    public GameObject parfBall10;
    public GameObject parfBall11;
    public GameObject parfBall12;
    public GameObject parfBall13;
    public GameObject parfBall14;
    public GameObject parfBall15;
    public GameObject parfBall15X;


    //默认点击旋转角度
    private int[] rotateBaseData =
            new int[] { 0, 90, 90, 90, 90, 45, 45, 90, 90, 90, 90, 90, 90, 90, 90, 45, 0, 0, 0 };
    //默认小球的旋转角度
    private int[] rotateBaseBall =
        new int[] { 0, 0, -45, 0, -45, 90, 90, 0, 45, 0, 45, 0, -45, 0, -45, 0, 0, 0, 0 };
    //最后状态播放时间
    private float endPlayTime = 0;

    public GameObject gameObjectChuan;
    //阴影图
    public Image imgShadow, imAn;

    private float stoneClickTime = 0;
    // Use this for initialization

    public GameObject[] numSize;
    public int numCi = -1;

    public bool isClick;

    public int index;

    public bool isShowFuzhu = false;
    //终止音符播放闲置音效的计时器
    private float endTime;

    public Image imageHui;
    public Sprite[] imgHui;
    /// <summary>
    /// 激活状态
    /// </summary>
    public bool isJH = false;
    //被激活的次数,有锁的需要被激活两次以上才行
    public int jhTimes = 0;
    /// <summary>
    /// 用来计时 激活状态的音符发射检测子弹
    /// </summary>
    public float timeJH = 0;
    /// <summary>
    /// 第一个被击中的，永远不会处于不激活状态;
    /// </summary>
    public bool isFirst;
    /// <summary>
    /// 开始闪烁
    /// </summary>
    public bool isLight;

    public float timeLight;

    public GameObject objMovLightAni,objMovJH,objZD,objZDCiJi;

    public bool isJhOld = false;
    /// <summary>
    /// 初始坐标，用来区分摆动坐标
    /// </summary>
    public Vector3 basePosition;
    void Start()
    {
    }

    /// <summary>
    /// 初始化数据 
    /// </summary>
    public void InitData(int _createID, int _line, int _row, 
        bool _isLock, int _numCi, int _index,bool _jh)
    {
        rotateNowAngel = 0;
        rotateAngel = 0;
        rotateState = 1;
        line = _line;
        row = _row;
        stoneClickTime = 0;
        numCi = _numCi;
        index = _index;
        isJH = _jh;
        isFirst = isJH;
        endTime = 0;

        numSize[0].SetActive(false);
        numSize[1].SetActive(false);
        numSize[2].SetActive(false);
        numSize[3].SetActive(false);
        numSize[4].SetActive(false);
        numSize[5].SetActive(false);
        numSize[6].SetActive(false);
        numSize[7].SetActive(false);
        numSize[8].SetActive(false);
        if (numCi > -1)
        {
            numSize[numCi].SetActive(true);
        }
        //初始化ID数据
        createID = _createID;
        if (createID >= 1000)
        {
            gameObjectChuan.SetActive(true);
            InitChuanSong(createID / 1000);
            transform.GetComponent<CircleCollider2D>().enabled = false;
            imgShadow.gameObject.SetActive(false);
            imAn.gameObject.SetActive(false);
        }
        else
        {
            imageSmallID = createID % 100;
            InitBaseData();
            if (createID >= 0 && createID <= 99)
            {

            }
            else if (createID >= 100 && createID <= 199)
            {
                gameObjectChuan.SetActive(true);
                effectBlue.gameObject.SetActive(true);
                gameObjectChuan1.SetActive(true);
                gameObjectChuan11.SetActive(false);

                parJin1.GetComponent<ParticleSystem>().Stop();
                parJin11.gameObject.SetActive(false);

            }
            else if (createID >= 200 && createID <= 299)
            {
                gameObjectChuan.SetActive(true);
                effectRed.gameObject.SetActive(true);

                parJin2.GetComponent<ParticleSystem>().Stop();
                parJin22.gameObject.SetActive(false);

                gameObjectChuan2.SetActive(true);
                gameObjectChuan22.SetActive(false);
            }
            else if (createID >= 300 && createID <= 399)
            {
                gameObjectChuan.SetActive(true);
                effectGreen.gameObject.SetActive(true);

                parJin3.GetComponent<ParticleSystem>().Stop();
                parJin33.gameObject.SetActive(false);

                gameObjectChuan3.SetActive(true);
                gameObjectChuan33.SetActive(false);
            }
            else if (createID >= 400 && createID <= 499)
            {
                gameObjectChuan.SetActive(true);
                effectPurple.gameObject.SetActive(true);

                parJin4.GetComponent<ParticleSystem>().Stop();
                parJin44.gameObject.SetActive(false);

                gameObjectChuan4.SetActive(true);
                gameObjectChuan44.SetActive(false);
            }
        }

        //初始化锁
        isLock = _isLock;
        gameObjectLock.SetActive(isLock);
        aniLock.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        //刷新引导
        //SetJH(_jh);

        endPlayTime = 0;
        if (GameController.GetInstance().currentLevel == 0 ||
            GameController.GetInstance().currentLevel == 10 ||
            GameController.GetInstance().currentLevel == 20 ||
            GameController.GetInstance().currentLevel == 30 ||
            (GameController.GetInstance().currentLevel == 40
            &&index ==3) ||
            GameController.GetInstance().currentLevel == 50 ||
            GameController.GetInstance().currentLevel == 60 ||
            GameController.GetInstance().currentLevel == 70 ||
            GameController.GetInstance().currentLevel == 80 ||
            (GameController.GetInstance().currentLevel == 90
            &&index == 5) ||
            (GameController.GetInstance().currentLevel == 100
            &&index==5) )
        {
            isShowFuzhu = true;
        }
        else
        {
            isShowFuzhu = false;
            HideFuzhu();
        }

        objMovLightAni.SetActive(false);
        objMovJH.SetActive(false);
        objZD.SetActive(false);
        objZDCiJi.SetActive(false);
        isJhOld = false;

        basePosition = gameObject.transform.position;
        Debug.Log(basePosition+"base");
    }

    private void InitChuanSong(int _idChuan)
    {
        gameObjectSign.SetActive(false);
        effectBlue.gameObject.SetActive(false);
        effectRed.gameObject.SetActive(false);
        effectGreen.gameObject.SetActive(false);
        effectPurple.gameObject.SetActive(false);
        gameObjectChuan1.SetActive(false);
        gameObjectChuan2.SetActive(false);
        gameObjectChuan3.SetActive(false);
        gameObjectChuan4.SetActive(false);
        gameObjectChuan11.SetActive(false);
        gameObjectChuan22.SetActive(false);
        gameObjectChuan33.SetActive(false);
        gameObjectChuan44.SetActive(false);
        switch (_idChuan)
        {
            case 1:
                effectBlue.gameObject.SetActive(true);
                parJin1.gameObject.SetActive(false);
                parJin11.GetComponent<ParticleSystem>().Stop();
                GameController.GetInstance().lineBlue = line;
                GameController.GetInstance().rowBlue = row;
                gameObjectChuan11.SetActive(true);
                break;
            case 2:
                effectRed.gameObject.SetActive(true);
                GameController.GetInstance().lineRed = line;
                GameController.GetInstance().rowRed = row;
                parJin2.gameObject.SetActive(false);
                parJin22.GetComponent<ParticleSystem>().Stop();
                gameObjectChuan22.SetActive(true);
                break;
            case 3:
                effectGreen.gameObject.SetActive(true);
                GameController.GetInstance().lineGreen = line;
                GameController.GetInstance().rowGreen = row;
                parJin3.gameObject.SetActive(false);
                parJin33.GetComponent<ParticleSystem>().Stop();
                gameObjectChuan33.SetActive(true);
                break;
            case 4:
                effectPurple.gameObject.SetActive(true);
                GameController.GetInstance().linePurple = line;
                GameController.GetInstance().rowPurple = row;
                parJin4.gameObject.SetActive(false);
                parJin44.GetComponent<ParticleSystem>().Stop();
                gameObjectChuan44.SetActive(true);
                break;
        }
    }

    private void InitGameObjectAni()
    {
        gameObjectSign.SetActive(true);
        GameObejctAni[imageID].SetActive(true);
        imageSign.gameObject.SetActive(true);
    }

    private void InitAni()
    {
        switch (imageID)
        {
            case 0:
                aniBall0.Play("Ball-ShiTou-XianZhi", PlayMode.StopAll);
                parBall0.GetComponent<ParticleSystem>().Stop();
                break;
            case 1:
                aniBall1.Play("Ball-1-XianZhi", PlayMode.StopAll);
                parBall1.GetComponent<ParticleSystem>().Stop();
                break;
            case 2:
                aniBall2.Play("Ball-1-XianZhi", PlayMode.StopAll);
                parBall2.GetComponent<ParticleSystem>().Stop();
                break;
            case 3:
                aniBall3.Play("Ball-2-XianZhi", PlayMode.StopAll);
                parBall3.GetComponent<ParticleSystem>().Stop();
                break;
            case 4:
                aniBall4.Play("Ball-2-XianZhi", PlayMode.StopAll);
                parBall4.GetComponent<ParticleSystem>().Stop();
                break;
            case 5:
                aniBall5.Play("Ball-3-XianZhi", PlayMode.StopAll);
                parBall5.GetComponent<ParticleSystem>().Stop();
                parBall5X.GetComponent<ParticleSystem>().Stop();
                break;
            case 6:
                aniBall6.Play("Ball-4-XianZhi", PlayMode.StopAll);
                parBall6.GetComponent<ParticleSystem>().Stop();
                parBall6X.GetComponent<ParticleSystem>().Stop();
                break;
            case 7:
                aniBall7.Play("Ball-5-XianZhi", PlayMode.StopAll);
                parBall7.GetComponent<ParticleSystem>().Stop();
                break;
            case 8:
                aniBall8.Play("Ball-5-XianZhi", PlayMode.StopAll);
                parBall8.GetComponent<ParticleSystem>().Stop();
                break;
            case 9:
                aniBall9.Play("Ball-6-XianZhi", PlayMode.StopAll);
                parBall9.GetComponent<ParticleSystem>().Stop();
                break;
            case 10:
                aniBall10.Play("Ball-6-XianZhi", PlayMode.StopAll);
                parBall10.GetComponent<ParticleSystem>().Stop();
                break;
            case 11:
                aniBall11.Play("Ball-7-XianZhi", PlayMode.StopAll);
                parBall11.GetComponent<ParticleSystem>().Stop();
                break;
            case 12:
                aniBall12.Play("Ball-7-XianZhi", PlayMode.StopAll);
                parBall12.GetComponent<ParticleSystem>().Stop();
                break;
            case 13:
                aniBall13.Play("Ball-8-XianZhi", PlayMode.StopAll);
                parBall13.GetComponent<ParticleSystem>().Stop();
                break;
            case 14:
                aniBall14.Play("Ball-8-XianZhi", PlayMode.StopAll);
                parBall14.GetComponent<ParticleSystem>().Stop();
                break;
            case 15:
                aniBall15.Play("Ball-9-XianZhi", PlayMode.StopAll);
                parBall15.GetComponent<ParticleSystem>().Stop();
                parBall15X.GetComponent<ParticleSystem>().Stop();
                break;
            case 16:
                aniBall16.Play("Ball-SuiJi-XianZhi", PlayMode.StopAll);
                parBall16.GetComponent<ParticleSystem>().Stop();
                break;
            case 17:
                aniBall17.Play("ZD-XianZhi", PlayMode.StopAll);
                parBall17.GetComponent<ParticleSystem>().Stop();
                break;
            case 18:
                aniBall18.Play("HeXinZD-XianZhi", PlayMode.StopAll);
                parBall18.GetComponent<ParticleSystem>().Stop();
                break;
        }
    }

    /// <summary>
    /// 隐藏辅助线
    /// </summary>
    public void HideFuzhu()
    {
        parfBall1.SetActive(false);
        parfBall2.SetActive(false);
        parfBall3.SetActive(false);
        parfBall4.SetActive(false);
        parfBall5.SetActive(false);
        parfBall5X.SetActive(false);
        parfBall6.SetActive(false);
        parfBall6X.SetActive(false);
        parfBall7.SetActive(false);
        parfBall8.SetActive(false);
        parfBall9.SetActive(false);
        parfBall10.SetActive(false);
        parfBall11.SetActive(false);
        parfBall12.SetActive(false);
        parfBall13.SetActive(false);
        parfBall14.SetActive(false);
        parfBall15.SetActive(false);
        parfBall15X.SetActive(false);
    }
    /// <summary>
    /// 显示辅助线
    /// </summary>
    /// <param name="_imageID"></param>
    public void InitFuZhu(int _imageID)
    {
        HideFuzhu();
        if (GameController.GetInstance().fuzhuState == 1&&createID<100)
        {
            ShowChuanFuZhu(_imageID);
        }
    }

    public void ShowChuanFuZhu(int _imageID)
    {
        switch (_imageID)
        {
            case 1: parfBall1.SetActive(true); break;
            case 2: parfBall2.SetActive(true); break;
            case 3: parfBall3.SetActive(true); break;
            case 4: parfBall4.SetActive(true); break;
            case 5:
                if (Mathf.Abs(rotateNowAngel) % 10 == 0)
                {
                    parfBall5.SetActive(true); 
                    parfBall5X.SetActive(false);
                }
                else
                {
                    parfBall5X.SetActive(true);
                    parfBall5.SetActive(false);
                }
                break;
            case 6:
                //Debug.Log("rotateNowAngel = "+ rotateNowAngel);
                if (Mathf.Abs(rotateNowAngel) % 10 == 0)
                {
                    parfBall6.SetActive(true);
                    parfBall6X.SetActive(false);
                }
                else
                {
                    parfBall6X.SetActive(true);
                    parfBall6.SetActive(false);
                }
                break;
            case 7: parfBall7.SetActive(true); break;
            case 8: parfBall8.SetActive(true); break;
            case 9: parfBall9.SetActive(true); break;
            case 10: parfBall10.SetActive(true); break;
            case 11: parfBall11.SetActive(true); break;
            case 12: parfBall12.SetActive(true); break;
            case 13: parfBall13.SetActive(true); break;
            case 14: parfBall14.SetActive(true); break;
            case 15: parfBall15.SetActive(true); break;
        }
    }

    /// <summary>
    /// 初始化基础数据,图片
    /// </summary>
    private void InitBaseData()
    {
        switch (imageSmallID)
        {
            case -1:
                gameObject.SetActive(false);
                break;
            case 0:
                imageID = 0;
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                imageID = 1;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID;
                rotateNowAngel = -rotateAngel * (imageSmallID - 1);
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                imageID = 2;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 4;
                rotateNowAngel = -rotateAngel * (imageSmallID - 5);
                break;
            case 9:
            case 10:
            case 11:
            case 12:
                imageID = 3;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 8;
                rotateNowAngel = -rotateAngel * (imageSmallID - 9);
                break;
            case 13:
            case 14:
            case 15:
            case 16:
                imageID = 4;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 12;
                rotateNowAngel = -rotateAngel * (imageSmallID - 13);
                break;
            case 17:
            case 18:
            case 19:
            case 20:
                imageID = 5;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 16;
                rotateNowAngel = -rotateAngel * (imageSmallID - 17);
                break;
            case 21:
            case 22:
            case 23:
            case 24:
                imageID = 6;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 20;
                rotateNowAngel = -rotateAngel * (imageSmallID - 21);
                break;
            case 25:
            case 26:
            case 27:
            case 28:
                imageID = 7;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 24;
                rotateNowAngel = -rotateAngel * (imageSmallID - 25);
                break;
            case 29:
            case 30:
            case 31:
            case 32:
                imageID = 8;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 28;
                rotateNowAngel = -rotateAngel * (imageSmallID - 29);
                break;
            case 33:
            case 34:
            case 35:
            case 36:
                imageID = 9;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 32;
                rotateNowAngel = -rotateAngel * (imageSmallID - 33);
                break;
            case 37:
            case 38:
            case 39:
            case 40:
                imageID = 10;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 36;
                rotateNowAngel = -rotateAngel * (imageSmallID - 37);
                break;
            case 41:
            case 42:
            case 43:
            case 44:
                imageID = 11;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 40;
                rotateNowAngel = -rotateAngel * (imageSmallID - 41);
                break;
            case 45:
            case 46:
            case 47:
            case 48:
                imageID = 12;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 44;
                rotateNowAngel = -rotateAngel * (imageSmallID - 45);
                break;
            case 49:
            case 50:
            case 51:
            case 52:
                imageID = 13;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 48;
                rotateNowAngel = -rotateAngel * (imageSmallID - 49);
                break;
            case 53:
            case 54:
            case 55:
            case 56:
                imageID = 14;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 52;
                rotateNowAngel = -rotateAngel * (imageSmallID - 53);
                break;
            case 57:
            case 58:
                imageID = 15;
                rotateAngel = rotateBaseData[imageID];
                rotateState = imageSmallID - 56;
                rotateNowAngel = -rotateAngel * (imageSmallID - 57);
                break;
            case 59:
                imageID = 16;
                break;
            case 60:
                imageID = 17;
                break;
            case 61:
                imageID = 18;
                GameController.GetInstance().targetPosition = transform.localPosition;
                UIManager.GetInstance().game.GetComponent<Game>().endIndex = index-1;
                break;
            default:
                break;
        }
        imageHui.gameObject.SetActive(!isJH);
        if(imageID>0&&imageID < 17)
        {
            imageHui.sprite = imgHui[imageID - 1];
        }
        else
        {
            imageHui.gameObject.SetActive(false);
        }   

        transform.localEulerAngles = new Vector3(0, 0, rotateNowAngel);
        imageSign.sprite = spriteSign[imageID];
        InitGameObjectAni();
        InitAni();
        InitFuZhu(imageID);
        if (createID >= 100 && createID <= 199)
        {
            GameController.GetInstance().chuanFuZhuID0 = imageID;
        }
        else if (createID >= 200 && createID <= 299)
        {
            GameController.GetInstance().chuanFuZhuID1 = imageID;
        }
        else if (createID >= 300 && createID <= 399)
        {
            GameController.GetInstance().chuanFuZhuID2 = imageID;
        }
        else if (createID >= 400 && createID <= 499)
        {
            GameController.GetInstance().chuanFuZhuID3 = imageID;
        }
        if (createID >= 1000)
        {
            switch (createID) {
                case 1000:
                    InitFuZhu(GameController.GetInstance().chuanFuZhuID0); break;
                case 2000:
                    InitFuZhu(GameController.GetInstance().chuanFuZhuID1); break;
                case 3000:
                    InitFuZhu(GameController.GetInstance().chuanFuZhuID2); break;
                case 4000:
                    InitFuZhu(GameController.GetInstance().chuanFuZhuID3); break;
            }            
        }
        isClick = false;
        GameController.GetInstance().scoreTotal += GameController.GetInstance().scoreSign[imageID];
        jhTimes = 0;
    }

    /// <summary>
    ///  延迟调用
    /// </summary>
    private void ChangeGuidStep()
    {
        UIManager.GetInstance().game.GetComponent<Game>().SetGuidCurrentStep(1);
    } 
       
    /// <summary>
    /// 点击音符
    /// </summary>
    public void ClickUnit()
    {       
        if (LocalData.GetInstance().GetMaxOpenLevel() == 1)
        {
            if(GameController.GetInstance().currentLevel == 0)
            {
                if( index == 2&&LocalData.GetInstance().guidCurrentStep==0)
                {
                    Invoke("ChangeGuidStep", 0.3f);
                }
                else
                {
                    return;
                }
            }
        }else if (LocalData.GetInstance().GetMaxOpenLevel() == 2)
        {
            if (GameController.GetInstance().currentLevel == 1)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid&&
                    index != 6 && LocalData.GetInstance().guidCurrentStep == 3)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 6 && LocalData.GetInstance().guidCurrentStep == 3)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().SetGuidCurrentStep(4);                    
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&LocalData.GetInstance().guidCurrentStep == 4)
                {
                    return;
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 11)
        {
            if (GameController.GetInstance().currentLevel == 10)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 3 && LocalData.GetInstance().guidCurrentStep == 5)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 3 && LocalData.GetInstance().guidCurrentStep == 5)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 21)
        {
            if (GameController.GetInstance().currentLevel == 20)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 2 && LocalData.GetInstance().guidCurrentStep == 6)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 2 && LocalData.GetInstance().guidCurrentStep == 6)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 31)
        {
            if (GameController.GetInstance().currentLevel == 30)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 3 && LocalData.GetInstance().guidCurrentStep == 7)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 3 && LocalData.GetInstance().guidCurrentStep == 7)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 41)
        {
            if (GameController.GetInstance().currentLevel == 40)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 3 && LocalData.GetInstance().guidCurrentStep == 8)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 3 && LocalData.GetInstance().guidCurrentStep == 8)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 51)
        {
            if (GameController.GetInstance().currentLevel == 50)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 8 && LocalData.GetInstance().guidCurrentStep == 9)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 8 && LocalData.GetInstance().guidCurrentStep == 9)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 61)
        {
            if (GameController.GetInstance().currentLevel == 60)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 3 && LocalData.GetInstance().guidCurrentStep == 10)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 3 && LocalData.GetInstance().guidCurrentStep == 10)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 71)
        {
            if (GameController.GetInstance().currentLevel == 70)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 5 && LocalData.GetInstance().guidCurrentStep == 11)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 5 && LocalData.GetInstance().guidCurrentStep == 11)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 81)
        {
            if (GameController.GetInstance().currentLevel == 80)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 3 && LocalData.GetInstance().guidCurrentStep == 12)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 3 && LocalData.GetInstance().guidCurrentStep == 12)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 91)
        {
            if (GameController.GetInstance().currentLevel == 90)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 5 && LocalData.GetInstance().guidCurrentStep == 13)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 5 && LocalData.GetInstance().guidCurrentStep == 13)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 101)
        {
            if (GameController.GetInstance().currentLevel == 100)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 5 && LocalData.GetInstance().guidCurrentStep == 14)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 5 && LocalData.GetInstance().guidCurrentStep == 14)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        else if (LocalData.GetInstance().GetMaxOpenLevel() == 106)
        {
            if (GameController.GetInstance().currentLevel == 105)
            {
                if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index != 7 && LocalData.GetInstance().guidCurrentStep == 15)
                {
                    return;
                }
                else if (UIManager.GetInstance().game.GetComponent<Game>().isShowGuid &&
                    index == 7 && LocalData.GetInstance().guidCurrentStep == 15)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }
            }
        }
        

        if (isClick)
        {
            return;
        }
        //UIManager.GetInstance().game.GetComponent<Game>().CreateYinfu(transform.position, imageID);

        isClick = true;
        Invoke("ResetClick", 0.1f);
        if (UIManager.GetInstance().game.GetComponent<Game>().gameTime < 1)
        {
            return;
        }
        if (transform.localScale.x != 1)
        {
            return;
        }
        if (isLock)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundLockClick);
            Tools.PlayAnimation(aniLock,"Suo-Dianji");
            //gameObjectLock.transform.localScale = Vector3.one*(100f/80) ;
            return;
        }
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState != Game.GameState.GameWait)
        {
            return;
        }
        if (imageID == 0)
        {
            if (stoneClickTime == 0)
            {
                stoneClickTime = 1.0f;
                aniBall0.Stop();
                aniBall0.Play("Ball-ShiTou-Dianji", PlayMode.StopAll);
                Invoke("StoneXianZhi", 1.0f);
                AudioManager.GetInstance().PlaySound(AudioManager.SoundStoneClickHit);
            }
            return;
        }
        else if (imageID == 17)
        {
            aniBall17.Play("ZD-DianJi", PlayMode.StopAll);

            LeanTween.scale(imgShadow.gameObject, new Vector3(0.7f, 0.7f, 0.7f), 0.1f).setLoopPingPong(1);

            AudioManager.GetInstance().PlaySound(AudioManager.SoundClickBall);
            return;
        }
        else if (imageID == 16 || imageID == 18)//这三个没有点击需要处理
        {
            return;
        }
        if (createID < 1000)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundClickBall);
        }
        rotateState++;
        if (imageID == 15)
        {
            if (rotateState > 2)
            {
                rotateState = 1;
                if (isShowFuzhu)
                {
                    parfBall15.SetActive(true);
                    parfBall15X.SetActive(false);
                }

            }
            else
            {
                if (isShowFuzhu)
                {
                    parfBall15X.SetActive(true);
                    parfBall15.SetActive(false);
                }

            }
        }
        else
        {
            if (rotateState > 4)
            {
                rotateState = 1;
            }
        }
        rotateNowAngel -= rotateAngel;
        if (rotateNowAngel <= -360)
        {
            rotateNowAngel = 0;
        }
        //物体旋转角度（自身角度）
        LeanTween.rotate(gameObject, new Vector3(0, 0, rotateNowAngel), 0.1f);
        LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f).setLoopPingPong(1);
        LeanTween.scale(imgShadow.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);

        UIManager.GetInstance().game.GetComponent<Game>().ResetJH();
        UIManager.GetInstance().game.GetComponent<Game>().ResetJHFirst(1);

        if (isShowFuzhu)
        {
            //Debug.Log("imageID = "+ imageID);
            if ( imageID == 5)
            {
                parfBall5X.SetActive(false);
                parfBall5.SetActive(false);
                if (Mathf.Abs(rotateNowAngel) % 10 == 0)
                {
                    parfBall5.SetActive(true);
                }
                else
                {
                    parfBall5X.SetActive(true);
                }
            }
            else if (imageID == 6)
            {
                parfBall6.SetActive(false);
                parfBall6X.SetActive(false);
                if (Mathf.Abs(rotateNowAngel) % 10 == 0)
                {
                    parfBall6.SetActive(true);
                }
                else
                {
                    parfBall6X.SetActive(true);
                }
            }
        }

        if (createID >= 100 && createID < 1000)
        {
            if (createID >= 100 && createID <= 199)
            {
                Tools.ScaleParticleSystem(gameObjectChuan1, 1.2f);
                UIManager.GetInstance().game.GetComponent<Game>().RotateChuanFuZhu(GameController.GetInstance().index1, rotateNowAngel);
            }
            else if (createID >= 200 && createID <= 299)
            {
                Tools.ScaleParticleSystem(gameObjectChuan2, 1.2f);
                UIManager.GetInstance().game.GetComponent<Game>().RotateChuanFuZhu(GameController.GetInstance().index2, rotateNowAngel);
            }
            else if (createID >= 300 && createID <= 399)
            {
                Tools.ScaleParticleSystem(gameObjectChuan3, 1.2f);
                UIManager.GetInstance().game.GetComponent<Game>().RotateChuanFuZhu(GameController.GetInstance().index3, rotateNowAngel);
            }
            else if (createID >= 400 && createID <= 499)
            {
                Tools.ScaleParticleSystem(gameObjectChuan4, 1.2f);
                UIManager.GetInstance().game.GetComponent<Game>().RotateChuanFuZhu(GameController.GetInstance().index4, rotateNowAngel);
            }
            Invoke("ResetChuan", 0.1f);
        }

    }
    private void ResetChuan()
    {
        if (createID >= 100 && createID <= 199)
        {
            Tools.ScaleParticleSystem(gameObjectChuan1, 1 / 1.2f);
        }
        else if (createID >= 200 && createID <= 299)
        {
            Tools.ScaleParticleSystem(gameObjectChuan2, 1 / 1.2f);
        }
        else if (createID >= 300 && createID <= 399)
        {
            Tools.ScaleParticleSystem(gameObjectChuan3, 1 / 1.2f);
        }
        else if (createID >= 400 && createID <= 499)
        {
            Tools.ScaleParticleSystem(gameObjectChuan4, 1 / 1.2f);
        }
    }
    
    public void ResetClick()
    {
        isClick = false;
    }
       
    public void StoneXianZhi()
    {
        if (imageID == 0)
        {
            aniBall0.Stop();
            aniBall0.Play("Ball-ShiTou-XianZhi", PlayMode.StopAll);

            return;
        }
    }
    public void Stone()
    {
        float _boomAngle = Tools.GetForRotation(transform.localPosition, GameController.GetInstance().targetPosition);
        parBall0.transform.transform.rotation = Quaternion.Euler(0, 0, _boomAngle);

        imgShadow.gameObject.SetActive(false);
        imAn.gameObject.SetActive(false);
        aniBall0.Play("Ball-ShiTou-BaoZha", PlayMode.StopAll);
        parBall0.GetComponent<ParticleSystem>().Stop();
        parBall0.GetComponent<ParticleSystem>().Play();        
    }
       
    private void PlayerSound()
    {
        int _add = new int[] { 0, 27, 37 , 49  }[GameController.GetInstance().SoundType];
        switch (imageID)
        {
            case 1:
            case 2:
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF1 + _add);
                break;
            case 3:
            case 4:
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF2 + _add);
                break;
            case 5:
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF3 + _add);
                break;
            case 6:
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF4 + _add);
                break;
            case 7:
            case 8:
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF5 + _add);
                break;
            case 9:
            case 10:
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF6 + _add);
                break;
            case 11:
            case 12:
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF7 + _add);
                break;
            case 13:
            case 14:
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF8 + _add);
                break;
            case 15://四向球
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF9 + _add);
                break;
            case 16://随机球
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF10 + _add);
                break;
            case 17://刺激终点
                AudioManager.GetInstance().PlaySound(AudioManager.SoundEnd2Boom);
                break;
            case 18://最终终点
                AudioManager.GetInstance().PlaySound(AudioManager.SoundYF10 + _add);
                AudioManager.GetInstance().PlaySound(AudioManager.SoundBigo);
                UIManager.GetInstance().game.GetComponent<Game>().SetWanMeiMov(false);
                break;
        }
    }

    /// <summary>
    /// 设置激活状态
    /// </summary>
    /// <param name="_jh"></param>
    public void SetJH(bool _jh,int _type)
    {       
        if (isJH != _jh)
        {
            if( _jh )
            {
                jhTimes++;
                if (isLock&& jhTimes<2)
                {
                      return;
                }
                Debug.Log("********第" + index + "球被激活");
                isJH = _jh;
                GameController.GetInstance().gameCurrentJHNum++;
                if (imageID > 0 && imageID < 17)
                {
                    SetHui(1);
                }                   
                RefushAroundJH(true);
                isLight = false;
                objMovLightAni.SetActive(false);
                if (imageID == 18&& objZD.activeSelf==false)
                {
                    objZD.SetActive(true);
                    Tools.PlayParticleSystem(objZD);
                    Tools.PlayAnimation(objZD.GetComponent<Animation>(), "QieHuan-ZD");
                }
                if (imageID == 17 && objZDCiJi.activeSelf == false)
                {
                    objZDCiJi.SetActive(true);
                    Tools.PlayParticleSystem(objZDCiJi);
                    Tools.PlayAnimation(objZDCiJi.GetComponent<Animation>(), "ZTQH-ZD");
                }
            }
            else
            {
                Debug.Log("^^^^^^^第" + index + "球被熄灭"+ imageID);
                jhTimes = 0;
                isJH = _jh;
                if (imageID > 0 && imageID < 17)
                {
                    SetHui(2);
                }
                if (imageID == 18)
                {
                    objZD.SetActive(false);
                }
                else if (imageID == 17)
                {
                    objZDCiJi.SetActive(false);
                }
                isLight = false;
                objMovLightAni.SetActive(false);                
            }
            UIManager.GetInstance().game.GetComponent<Game>().jhTime = 0;
        }
    }

    public void ResetOldAndPlayMov()
    {
        if (isJhOld != isJH && imageID > 0 && isFirst == false)
        {           
            isJhOld = isJH;
            if (isJH)
            {
                objMovJH.SetActive(true);
                Tools.PlayParticleSystem(objMovJH);
                if (imageID > 0 && imageID < 17)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().jhNum++;
                }else if (imageID == 17)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().jhNum+=1000;
                }
                else if (imageID == 18)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().jhNum += 100000;
                }
            }
            else
            {
                if (imageID == 18)
                {
                    objZD.SetActive(false);
                }
                else if (imageID == 17)
                {
                    objZDCiJi.SetActive(false);
                }
            }
        }        
    }

    /// <summary>
    /// 灰色状态 0不显示不更新 1显示并更新 2显示并更新
    /// </summary>
    private int huiState = 0;
    private float alp = 0;

    public void SetHui(int _huiState)
    {
        if(huiState!= _huiState)
        {
            huiState = _huiState;
            if (huiState == 2)
            {
                imageHui.gameObject.SetActive(true);
                alp = 0;
                imageHui.color = new Color(1, 1, 1, alp);
            }
            else if (huiState == 1)
            {
                imageHui.gameObject.SetActive(false);
                alp = 255;
                imageHui.color = new Color(1, 1, 1, alp);
            }            
        }
    }

    public void UpdateHui()
    {
        if(huiState == 2)
        {
            alp += 15;
            if(alp > 255)
            {
                alp = 255;
                SetHui(0);
            }
            imageHui.color = new Color(1, 1, 1, alp/255);
        }
        else if (huiState == 1)
        {
            alp -= 15;
            if (alp < 0)
            {
                alp = 0;
                SetHui(0);
            }
            imageHui.color = new Color(255, 255, 255, alp/255);
        }
    }

    /// <summary>
    /// 刷新周围音符球的激活状态
    /// 参数用不到，会重置所有未false 然后刷新，所以——jh这个参数暂时用不到
    /// </summary>
    public void RefushAroundJH(bool _jh)
    {
        int _line = line;
        int _row = row;
        if (createID >= 100 && createID <= 199)
        {
            _line = GameController.GetInstance().lineBlue;
            _row = GameController.GetInstance().rowBlue;
        }
        else if (createID >= 200 && createID <= 299)
        {
            _line = GameController.GetInstance().lineRed;
            _row = GameController.GetInstance().rowRed;
        }
        else if (createID >= 300 && createID <= 399)
        {
            _line = GameController.GetInstance().lineGreen;
            _row = GameController.GetInstance().rowGreen;
        }
        else if (createID >= 400 && createID <= 499)
        {
            _line = GameController.GetInstance().linePurple;
            _row = GameController.GetInstance().rowPurple;
        }
        

        switch (imageID)
        {
            case 1:
                switch( rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line-1,_row,_jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line , _row+1, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line , _row-1, _jh);
                        break;
                }
                break;
            case 2:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row+1, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row + 1, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row - 1, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line -1 , _row - 1, _jh);
                        break;
                }
                break;
            case 3:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 2, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 2, _jh);
                        break;
                }
                break;
            case 4:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row + 2, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row + 2, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row - 2, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row - 2, _jh);
                        break;
                }
                break;
            case 5:
                switch (rotateState)
                {
                    case 1:
                    case 5:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row, _jh);
                        break;
                    case 2:
                    case 6:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row+1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row-1, _jh);
                        break;
                    case 3:
                    case 7:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line , _row-1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line , _row+1, _jh);
                        break;
                    case 4:
                    case 8:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row-1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row+1, _jh);
                        break;                    
                }
                break;
            case 6:
                switch (rotateState)
                {
                    case 1:
                    case 5:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row, _jh);
                        break;
                    case 2:
                    case 6:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row - 2, _jh);
                        break;
                    case 3:
                    case 7:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 2, _jh);
                        break;
                    case 4:
                    case 8:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row + 2, _jh);
                        break;
                }
                break;
            case 7:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line , _row+1, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line , _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row , _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line+1, _row , _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line , _row-1 , _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row, _jh);
                        break;
                }
                break;
            case 8:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row + 1, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row + 1, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row + 1, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row - 1, _jh);
                        break;
                }
                break;
            case 9:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 2, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row, _jh);
                        break;
                }
                break;
            case 10:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row + 2, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row + 2, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row + 2, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row - 2, _jh);
                        break;
                }
                break;
            case 11:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 1, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line-1, _row , _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 1, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line+1, _row , _jh);
                        break;
                }
                break;
            case 12:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row + 1, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row - 1, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row - 1, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row + 1, _jh);
                        break;
                }
                break;
            case 13:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 2, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 2, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row, _jh);
                        break;
                }
                break;
            case 14:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row + 2, _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row - 2, _jh);
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row + 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row - 2, _jh);
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 2, _row - 2, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 2, _row + 2, _jh);
                        break;
                }
                break;
            case 15:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row-1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line, _row + 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line-1, _row , _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line+1, _row , _jh);
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line-1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line+1, _row - 1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line - 1, _row+1, _jh);
                        UIManager.GetInstance().game.GetComponent<Game>().RefushJH(_line + 1, _row+1, _jh);
                        break;                    
                }
                break;
            case 16:
                UIManager.GetInstance().game.GetComponent<Game>().SetLight();
                break;
        }
    }

    /// <summary>
    /// 设置闪烁
    /// </summary>
    /// <param name="_light"></param>
    public void SetLignt(bool _light)
    {
        isLight = _light;
        if (isLight)
        {
            if (imageID >= 17)
            {
                objMovLightAni.SetActive(true);
                Tools.PlayAnimation(objMovLightAni.GetComponent<Animation>(), "ZTQH-SJ");
            }
        }
    }
    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="_state"></param>
    public void SetUnitState(unitCicleState _state)
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().isHitHeXinSign)
        {
            return;
        }
        if (isLock)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundLockBoom);
            aniLock.Play("Suo-BaoZha", PlayMode.StopAll);
            isLock = false;
            return;
        }
        if (_state == currentUnitState)
        {
            return;
        }
        else
        {
            currentUnitState = _state;
        }
        switch (currentUnitState)
        {
            case unitCicleState.wait:

                break;
            case unitCicleState.shoot:
                objMovLightAni.SetActive(false);
                objMovJH.SetActive(false);
                objZD.SetActive(false);
                objZDCiJi.SetActive(false);
                GameController.GetInstance().gameDestroySignNum++;
                UIManager.GetInstance().game.GetComponent<Game>().SetLianji(imageID);
                imgShadow.gameObject.SetActive(false);
                imAn.gameObject.SetActive(false);
                HideFuzhu();
                //发射虚拟子弹
                shootTime = 0;
                UIManager.GetInstance().game.GetComponent<Game>().ResetShootWaitTime();
                switch (imageID)
                {
                    case 0:
                        AudioManager.GetInstance().PlaySound(AudioManager.SoundStoneClickHit);
                        aniBall0.Stop();
                        aniBall0.Play("Ball-ShiTou-Dianji", PlayMode.StopAll);
                        Invoke("StoneXianZhi", 1.0f);
                        break;
                    case 17:
                    case 18:
                        break;
                    default:
                        //生成替代传送门，修改位置，发射子弹，播放动画
                        if (createID >= 100 && createID < 1000)
                        {
                            effectBlue.gameObject.SetActive(false);
                            effectRed.gameObject.SetActive(false);
                            effectGreen.gameObject.SetActive(false);
                            effectPurple.gameObject.SetActive(false);
                            //原位置生成替代传送门
                            UIManager.GetInstance().game.GetComponent<Game>().CreateChuan(createID, transform.position);
                            UIManager.GetInstance().game.GetComponent<Game>().PlayChuanJin(createID);
                            AudioManager.GetInstance().PlaySound(AudioManager.SoundChuanBoom);
                        }
                        CreateShootBullet();
                        Invoke("ShootOtherBall",0.36f);
                        break;
                }
                PlayAniPar();
                PlayerSound();      
                if (imageID != 0)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().CreateYinfu(transform.position, imageID);
                }
                break;
            case unitCicleState.end:
                break;
            case unitCicleState.boom:
                break;
            case unitCicleState.dance:
                break;
        }
    }

    /// <summary>
    /// 新增，用其他逻辑判断攻击路径
    /// </summary>
    private void ShootOtherBall()
    {
        int _line = line;
        int _row = row;
        if (createID >= 100 && createID <= 199)
        {
            _line = GameController.GetInstance().lineBlue;
            _row = GameController.GetInstance().rowBlue;
        }
        else if (createID >= 200 && createID <= 299)
        {
            _line = GameController.GetInstance().lineRed;
            _row = GameController.GetInstance().rowRed;
        }
        else if (createID >= 300 && createID <= 399)
        {
            _line = GameController.GetInstance().lineGreen;
            _row = GameController.GetInstance().rowGreen;
        }
        else if (createID >= 400 && createID <= 499)
        {
            _line = GameController.GetInstance().linePurple;
            _row = GameController.GetInstance().rowPurple;
        }
        switch (imageID)
        {
            case 1:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 1 );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 1 );
                        break;
                }
                break;
            case 2:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row + 1 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row + 1 );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row - 1 );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row - 1 );
                        break;
                }
                break;
            case 3:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 2 );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 2 );
                        break;
                }
                break;
            case 4:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row + 2 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row + 2 );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row - 2 );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row - 2 );
                        break;
                }
                break;
            case 5:
                switch (rotateState)
                {
                    case 1:
                    case 5:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row );
                        break;
                    case 2:
                    case 6:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row - 1 );
                        break;
                    case 3:
                    case 7:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 1 );
                        break;
                    case 4:
                    case 8:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row + 1 );
                        break;
                }
                break;
            case 6:
                switch (rotateState)
                {
                    case 1:
                    case 5:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row );
                        break;
                    case 2:
                    case 6:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row - 2 );
                        break;
                    case 3:
                    case 7:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 2 );
                        break;
                    case 4:
                    case 8:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row + 2 );
                        break;
                }
                break;
            case 7:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 1 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row );
                        break;
                }
                break;
            case 8:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row + 1 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row + 1 );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row + 1 );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row - 1 );
                        break;
                }
                break;
            case 9:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 2 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row );
                        break;
                }
                break;
            case 10:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row + 2 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row + 2 );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row + 2 );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row - 2 );
                        break;
                }
                break;
            case 11:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 1 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 1 );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row );
                        break;
                }
                break;
            case 12:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row + 1 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row - 1 );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row - 1 );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row + 1 );
                        break;
                }
                break;
            case 13:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 2 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 2 );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row );
                        break;
                }
                break;
            case 14:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row + 2 );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row - 2 );
                        break;
                    case 3:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row + 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row - 2 );
                        break;
                    case 4:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 2, _row - 2 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 2, _row + 2 );
                        break;
                }
                break;
            case 15:
                switch (rotateState)
                {
                    case 1:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row );
                        break;
                    case 2:
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row - 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line - 1, _row + 1 );
                        UIManager.GetInstance().game.GetComponent<Game>().RefushBallState(_line + 1, _row + 1 );
                        break;
                }
                break;
            case 16:
                UIManager.GetInstance().game.GetComponent<Game>().SetLight();
                break;
        }
    }

    //播放箭头发射和粒子特效
    private void PlayAniPar()
    {
        if (createID >= 1000)
        {
            return;
        }
        switch (imageID)
        {
            case 0:
                break;
            case 1:
                aniBall1.Play("Ball-1-BaoZha", PlayMode.StopAll);
                parBall1.GetComponent<ParticleSystem>().Stop();
                parBall1.GetComponent<ParticleSystem>().Play();
                break;
            case 2:
                aniBall2.Play("Ball-1-BaoZha", PlayMode.StopAll);
                parBall2.GetComponent<ParticleSystem>().Stop();
                parBall2.GetComponent<ParticleSystem>().Play();
                break;
            case 3:
                aniBall3.Play("Ball-2-BaoZha", PlayMode.StopAll);
                parBall3.GetComponent<ParticleSystem>().Stop();
                parBall3.GetComponent<ParticleSystem>().Play();
                break;
            case 4:
                aniBall4.Play("Ball-2-BaoZha", PlayMode.StopAll);
                parBall4.GetComponent<ParticleSystem>().Stop();
                parBall4.GetComponent<ParticleSystem>().Play();
                break;
            case 5:
                aniBall5.Play("Ball-3-BaoZha", PlayMode.StopAll);
                if ((int)(transform.localEulerAngles.z) % 10 == 0)
                {
                    parBall5.GetComponent<ParticleSystem>().Stop();
                    parBall5.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    parBall5X.GetComponent<ParticleSystem>().Stop();
                    parBall5X.GetComponent<ParticleSystem>().Play();
                }
                break;
            case 6:
                aniBall6.Play("Ball-4-BaoZha", PlayMode.StopAll);
                if ((int)(transform.localEulerAngles.z) % 10 == 0)
                {
                    parBall6.GetComponent<ParticleSystem>().Stop();
                    parBall6.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    parBall6X.GetComponent<ParticleSystem>().Stop();
                    parBall6X.GetComponent<ParticleSystem>().Play();
                }
                break;
            case 7:
                aniBall7.Play("Ball-5-BaoZha", PlayMode.StopAll);
                parBall7.GetComponent<ParticleSystem>().Stop();
                parBall7.GetComponent<ParticleSystem>().Play();
                break;
            case 8://已做
                aniBall8.Play("Ball-5-BaoZha", PlayMode.StopAll);
                parBall8.GetComponent<ParticleSystem>().Stop();
                parBall8.GetComponent<ParticleSystem>().Play();
                break;
            case 9:
                aniBall9.Play("Ball-6-BaoZha", PlayMode.StopAll);
                parBall9.GetComponent<ParticleSystem>().Stop();
                parBall9.GetComponent<ParticleSystem>().Play();
                break;
            case 10:
                aniBall10.Play("Ball-6-BaoZha", PlayMode.StopAll);
                parBall10.GetComponent<ParticleSystem>().Stop();
                parBall10.GetComponent<ParticleSystem>().Play();
                break;
            case 11:
                aniBall11.Play("Ball-7-BaoZha", PlayMode.StopAll);
                parBall11.GetComponent<ParticleSystem>().Stop();
                parBall11.GetComponent<ParticleSystem>().Play();
                break;
            case 12:
                aniBall12.Play("Ball-7-BaoZha", PlayMode.StopAll);
                parBall12.GetComponent<ParticleSystem>().Stop();
                parBall12.GetComponent<ParticleSystem>().Play();
                break;
            case 13:
                aniBall13.Play("Ball-8-BaoZha", PlayMode.StopAll);
                parBall13.GetComponent<ParticleSystem>().Stop();
                parBall13.GetComponent<ParticleSystem>().Play();
                break;
            case 14:
                aniBall14.Play("Ball-8-BaoZha", PlayMode.StopAll);
                parBall14.GetComponent<ParticleSystem>().Stop();
                parBall14.GetComponent<ParticleSystem>().Play();
                break;
            case 15:
                aniBall15.Play("Ball-9-BaoZha", PlayMode.StopAll);
                if ((int)(transform.localEulerAngles.z) % 10 == 0)
                {
                    parBall15X.GetComponent<ParticleSystem>().Stop();
                    parBall15X.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    parBall15.GetComponent<ParticleSystem>().Stop();
                    parBall15.GetComponent<ParticleSystem>().Play();
                }
                break;
            case 16:
                aniBall16.Play("Ball-SuiJi-BaoZha", PlayMode.StopAll);
                parBall16.GetComponent<ParticleSystem>().Stop();
                parBall16.GetComponent<ParticleSystem>().Play();
                break;
            case 17:
                aniBall17.Play("ZD-BaoZha", PlayMode.StopAll);
                parBall17.GetComponent<ParticleSystem>().Stop();
                parBall17.GetComponent<ParticleSystem>().Play();
                break;
            case 18:
                aniBall18.Play("HeXinZD-BaoZha", PlayMode.StopAll);
                parBall18.GetComponent<ParticleSystem>().Stop();
                parBall18.GetComponent<ParticleSystem>().Play();
                UIManager.GetInstance().game.GetComponent<Game>().DestroyStone();
                break;
        }
    }
        
    void Update()
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePause)
        {
            return;
        }
        switch (currentUnitState)
        {
            case unitCicleState.wait:
                imgShadow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                imAn.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                if (imageID == 0)
                {
                    if (stoneClickTime > 0)
                    {
                        stoneClickTime -= Time.deltaTime;
                        if (stoneClickTime < 0)
                        {
                            stoneClickTime = 0;
                        }
                    }
                }
                else if (imageID > 0&& imageID <=16)
                {
                    //if(isJH)
                    //{
                    //    timeJH += Time.deltaTime;
                    //    if(timeJH > 0.1f)
                    //    {
                    //        //Debug.Log("发射子弹检测激活");
                    //        timeJH = 0;
                    //        //发射检测子弹
                    //        CreateShootBulletJH();
                    //    }
                    //}
                }

                if(isLight)//闪烁
                {
                    timeLight+= Time.deltaTime;
                    if (timeLight > 1)
                    {
                        imageHui.gameObject.SetActive(true);
                    }
                    if (timeLight > 2+Random.Range(0,10)*0.1f)
                    {
                        timeLight = 0;
                        imageHui.gameObject.SetActive(false);
                    }
                }
                UpdateHui();
                //更新摆动坐标
                if (imageID!=0&& imageID != 17 && imageID != 18 
                    && UIManager.GetInstance().game.GetComponent<Game>().
                    objWanMeiMov.activeSelf&& UIManager.GetInstance().game.GetComponent<Game>().
                    currentGameState == Game.GameState.GameWait)
                {
                    gameObject.transform.position = basePosition +
                        UIManager.GetInstance().game.GetComponent<Game>().objMoveBD.transform.position;
                }
                break;
            case unitCicleState.shoot:
                shootTime += Time.deltaTime;
                if (shootTime >= 1.0f)
                {
                    shootTime = 0;
                    SetUnitState(unitCicleState.end);
                }
                if (imageID == 18)//震动
                {
                    if (shootTime > 0.1f)
                    {
                        if (GameController.GetInstance().stateZhen == 1)
                        {
                            UIManager.GetInstance().game.GetComponent<Game>().startZhen();
                        }
                    }
                }
                break;
            case unitCicleState.end:
                if (imageID == 18)
                {
                    UIManager.GetInstance().game.GetComponent<Game>().isHitHeXinSign = true;
                    //endPlayTime += Time.deltaTime;
                    //if (endPlayTime >= 3.0f)
                    //{
                    //    UIManager.GetInstance().game.GetComponent<Game>().SetGameState(Game.GameState.GameWin);
                    //}
                }
                break;
        }
    }

    public void ResetBasePosition()
    {
        gameObject.transform.position = basePosition;
    }

    /// <summary>
    /// 生成虚拟子弹激活逻辑使用《暂时遗弃》
    /// </summary>
    private void CreateShootBulletJH()
    {
        Vector3 _position = transform.localPosition;
        //传送门内的音符
        if (createID >= 100 && createID <= 199)
        {
            _position = new Vector3(-360 + GameController.GetInstance().rowBlue * 180, 658 - GameController.GetInstance().lineBlue * 180, 0);

        }
        else if (createID >= 200 && createID <= 299)
        {
            _position = new Vector3(-360 + GameController.GetInstance().rowRed * 180, 658 - GameController.GetInstance().lineRed * 180, 0);

        }
        else if (createID >= 300 && createID <= 399)
        {
            _position = new Vector3(-360 + GameController.GetInstance().rowGreen * 180, 658 - GameController.GetInstance().lineGreen * 180, 0);

        }
        else if (createID >= 400 && createID <= 499)
        {
            _position = new Vector3(-360 + GameController.GetInstance().rowPurple * 180, 658 - GameController.GetInstance().linePurple * 180, 0);

        }

        transform.localPosition = _position;

        switch (imageID)
        {
            case 1:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                break;
            case 2:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                break;
            case 3:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                break;
            case 4:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                break;
            case 5:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 270, line);
                break;
            case 6:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 270, line);
                break;
            case 7:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                break;
            case 8:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                break;
            case 9:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                break;
            case 10:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                   (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                break;
            case 11:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                   (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
                break;
            case 12:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                   (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
                break;
            case 13:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                   (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
                break;
            case 14:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                   (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
                break;
            case 15:
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                   (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 180, line);
                UIManager.GetInstance().game.GetComponent<Game>().CreateShootSignJH
                    (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
                break;
            case 16://随机
                UIManager.GetInstance().game.GetComponent<Game>().RandomStartLight();
                break;
            case 17:

                break;
            case 18:
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 生成虚拟子弹
    /// </summary>
    private void CreateShootBullet()
    {
        Vector3 _position = transform.localPosition;
        //传送门内的音符
        if (createID >= 100 && createID <= 199)
        {
            _position = new Vector3(-360 + GameController.GetInstance().rowBlue * 180, 658 - GameController.GetInstance().lineBlue * 180, 0);

        }
        else if (createID >= 200 && createID <= 299)
        {
            _position = new Vector3(-360 + GameController.GetInstance().rowRed * 180, 658 - GameController.GetInstance().lineRed * 180, 0);

        }
        else if (createID >= 300 && createID <= 399)
        {
            _position = new Vector3(-360 + GameController.GetInstance().rowGreen * 180, 658 - GameController.GetInstance().lineGreen * 180, 0);

        }
        else if (createID >= 400 && createID <= 499)
        {
            _position = new Vector3(-360 + GameController.GetInstance().rowPurple * 180, 658 - GameController.GetInstance().linePurple * 180, 0);

        }

        transform.localPosition = _position;

        //switch (imageID)
        //{
        //    case 1:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        break;
        //    case 2:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        break;
        //    case 3:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        break;
        //    case 4:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        break;
        //    case 5:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 270, line);
        //        break;
        //    case 6:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 270, line);
        //        break;
        //    case 7:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        break;
        //    case 8:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        break;
        //    case 9:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        break;
        //    case 10:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //           (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        break;
        //    case 11:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //           (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
        //        break;
        //    case 12:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //           (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
        //        break;
        //    case 13:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //           (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
        //        break;
        //    case 14:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //           (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
        //        break;
        //    case 15:
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //           (imageID, _position, rotateNowAngel + rotateBaseBall[imageID], line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 90, line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] - 180, line);
        //        UIManager.GetInstance().game.GetComponent<Game>().CreateShootSign
        //            (imageID, _position, rotateNowAngel + rotateBaseBall[imageID] + 90, line);
        //        break;
        //    case 16://随机
        //        UIManager.GetInstance().game.GetComponent<Game>().ShootRandom(transform.localPosition);
        //        break;
        //    case 17:

        //        break;
        //    case 18:
        //        break;
        //    default:
        //        break;
        //}
    }

    /// <summary>
    /// 生成飞行音符
    /// </summary>
    private void CreateFlySign()
    {
        //生成音符数量
        int _num = GameController.GetInstance().scoreSign[imageID];
        //dotween动态生成

    }

    /// <summary>
    /// 碰撞
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentUnitState != unitCicleState.wait)
        {
            return;
        }
        string _tag = collision.gameObject.tag;
        switch (_tag)
        {
            case "startshoot":
                collision.gameObject.GetComponent<ShootSign>().isMove = false;
                collision.gameObject.GetComponent<ShootSign>().InitData();
                SetUnitState(unitCicleState.shoot);
                UIManager.GetInstance().game.GetComponent<Game>().startLine = line;
                UIManager.GetInstance().game.GetComponent<Game>().startRow = row;
                break;
            default:
                break;
        }
    }


}
