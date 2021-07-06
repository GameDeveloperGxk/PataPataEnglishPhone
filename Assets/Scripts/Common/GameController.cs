using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    /// 是否是测试版本
    public static bool DEBUG = true;
    /// 是否是手机版本
    public static bool PHONE_VERSION = true;

    //================================================================游戏关卡数据变量===============================================================
    /// 当前关卡
    /// <returns></returns>    
    public int currentLevel = 0;
    /// 当前殺怪數量
    public int currentLevelKillNum = 0;
    /// 当前关卡时间
    public int currentLevelTime = 0;
    /// 当前关卡得分
    public int currentLevelScore = 0;
    /// 当前关卡获得金币
    public int currentLevelCoinNum = 0;

    //================================================================游戏关卡数据===============================================================
    public int gameTotalSignNum = 0;
    public int gameDestroySignNum = 0;

    public int gameCurrentJHNum = 0;


    //逻辑运算用到的变量

    //石头索引，如果当前位置有石头就更改值为1
    public int[] stoneIndex = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    //传送门出的行列
    public int lineBlue = 0;
    public int rowBlue = 0;
    public int lineRed = 0;
    public int rowRed = 0;
    public int lineGreen = 0;
    public int rowGreen = 0;
    public int linePurple = 0;
    public int rowPurple = 0;

    public int index1 = 0;
    public int index2 = 0;
    public int index3 = 0;
    public int index4 = 0;

    public int chuanFuZhuID0 = -1;
    public int chuanFuZhuID1 = -1;
    public int chuanFuZhuID2 = -1;
    public int chuanFuZhuID3 = -1;

    public int chuanFZRotation0 = 0;
    public int chuanFZRotation1 = 0;
    public int chuanFZRotation2 = 0;
    public int chuanFZRotation3 = 0;



    public Vector3 targetPosition = new Vector3(0,0,0);

    //是否开启震动
    public int stateZhen = 1;

    public int fuzhuState = 1;

    public bool hasNew = false;
    public bool hasWan = false;
    public bool hasNewLast = false;
    public bool hasNotOpenNext = false;

    public int playPage = 0;
    //关卡音符爆炸后获得的积分
    public int[] scoreSign = new int[] {0,1,1,1,1,2,2,3,3,3,3,4,4,4,4,5,5,6,12};
    //关卡总积分
    public int scoreTotal = 0;
    //新手引导点击次数
    public int clickTime = 0;

    public int[] starNum = new int[] { 5, 11, 17, 24, 30, 36, 42, 48, 56, 63, 75, 88, 100, 120, 150 };
    
    public int SoundType = 0;
    /// <summary>
    /// 单例
    /// </summary>
    /// <returns></returns>
    public static GameController GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {

    }

    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    public void InitGameData()
    {
        currentLevelKillNum = 0;
        currentLevelTime = 0;
        currentLevelScore = 0;
        currentLevelCoinNum = 0;
        gameTotalSignNum = 0;
        gameDestroySignNum = 0;
        fuzhuState = 1;
        hasNew = false;
        hasWan = false;
        hasNewLast = false;
        hasNotOpenNext = false;
        index1 = -1;
        index2 = -1;
        index3 = -1;
        index4 = -1;
        clickTime = 0;
        scoreTotal = 0;
        gameCurrentJHNum = 0;
    }

}