using UnityEngine;
using System.Collections;

/// <summary>
/// 数据本地永久存储类
/// </summary>
public class LocalData
{
    private static LocalData instance = null;  
    public static LocalData GetInstance()
    {
        if (instance == null)
        {
            instance = new LocalData();
        }
        return instance;
    }
    private LocalData()
    {
    }
    //[SerializeField]
    /// <summary>
    /// 金币,初始值1000
    /// </summary>
    private int coin = 2000;
    public int GetCoin()
    {
        return coin;
    }
    public void SetCoin(int _value)
    {
        coin += _value;
        if (coin < 0)
        {
            coin = 0;
        }
    }
    /// <summary>
    /// 登录游戏的日期
    /// </summary>
    private string strTodayDate = "0000:00:00";
    /// <summary>
    /// 最大关卡数
    /// </summary>
    public const int MAXLEVELNUM = 150;
    /// <summary>
    /// 最大开启关卡数，默认1
    /// </summary>
    private int maxOpenLevel = 1;
    public int GetMaxOpenLevel()
    {
        return maxOpenLevel;
    }
    public void SetMaxOpenLevel(int _value)
    {
        maxOpenLevel = _value;
        if (maxOpenLevel < 0)
        {
            maxOpenLevel = 0;
        }
        else if (maxOpenLevel > MAXLEVELNUM)
        {
            maxOpenLevel = MAXLEVELNUM;
        }
    }
    ///<summary>
    /// <summary>
    /// 是否完美通关
    /// </summary>
    public int[] levelWmState = new int[] {
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,};
    //星星数量
    public int starNum = 0;
    //是否已经购买游戏
    public int stateBuy = 0;
    //玩游戏次数，用来判断是否弹出广告
    public int timePlayGame = 0;
    //玩游戏的次数
    public int playGameTime = 0;
    //新手引导当前步数
    public int guidCurrentStep = -1;
    /// <summary>
    /// game book sign ball state 0:not show 1:new 2:has show
    /// </summary>
    public int[] bookSignState = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    /// <summary>
    /// 保存数据到本地
    /// </summary>
    public void SaveLocalData()
    {
        Debug.Log("localdata class: SaveLocalData function");
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("max level", maxOpenLevel);
        PlayerPrefs.SetInt("starNum", starNum);
        PlayerPrefs.SetInt("stateBuy", stateBuy);
        PlayerPrefs.SetInt("timePlayGame", timePlayGame);        
        Tools.SavePlayerPrefsArray(levelWmState, "levelWmState");
        PlayerPrefs.SetString("strTodayDate", strTodayDate);
        PlayerPrefs.SetInt("newState", guidCurrentStep);
        Tools.SavePlayerPrefsArray(bookSignState, "bookSignState");
        PlayerPrefs.Save();
    }

   
    /// <summary>
    /// load local data
    /// </summary>
    public void LoadLocalData()
    {
        Debug.Log("localdata class: loadlocaldata function");
        coin = PlayerPrefs.GetInt("coin", coin);
        maxOpenLevel = PlayerPrefs.GetInt("max level", maxOpenLevel);
        starNum = PlayerPrefs.GetInt("starNum", starNum);
        stateBuy = PlayerPrefs.GetInt("stateBuy", stateBuy);
        timePlayGame = PlayerPrefs.GetInt("timePlayGame", timePlayGame);
        Tools.LoadPlayerPrefsArray(levelWmState, "levelWmState");

        strTodayDate = PlayerPrefs.GetString("strTodayDate", strTodayDate);
        if (!strTodayDate.Equals(System.DateTime.Now.ToString("yyyy:MM:dd")))
        {
            strTodayDate = System.DateTime.Now.ToString("yyyy:MM:dd");
        }
        playGameTime = 0;

        guidCurrentStep = PlayerPrefs.GetInt("newState", guidCurrentStep);
        Tools.LoadPlayerPrefsArray(bookSignState, "bookSignState");
        //test

    }
}
