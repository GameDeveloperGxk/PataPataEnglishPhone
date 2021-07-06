using UnityEngine;
using System.Collections;

/// <summary>
/// 支付SDK类，用来拉起支付，处理支付成功失败的逻辑
/// </summary>
public class PayPort : MonoBehaviour {

    private static PayPort instance;
    private AndroidJavaObject m_androidObj = null;
    /// <summary>
    /// 支付状态
    /// </summary>
    public int payState = 0;

    public static PayPort GetInstance()
    {
        if (instance == null)
        {
            instance = new PayPort();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
        //AndroidJavaClass androidClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //m_androidObj = androidClass.GetStatic<AndroidJavaObject>("currentActivity");
    }

    

    // Use this for initialization
    void Start() {
        payState = 0;
    }

    /// <summary>
    /// 拉起支付
    /// </summary>
    public void PayHuaWeiAppTouch()
    {
        //if (m_androidObj != null)
        //{
        //    Debug.Log("拉起支付unity");
        //    payState = 1;
        //    m_androidObj.Call("PayHuaWeiAppTouch", "PayHuaWeiAppTouch");
        //}
        PaySuccess("");
    }

    /// <summary>
    /// 支付成功
    /// </summary>
    public void PaySuccess(string str)
    {
        //Debug.Log("支付成功unity");
        payState = 2;
        LocalData.GetInstance().stateBuy = 1;
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().panelBuy.SetActive(false);
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().aniShop0.gameObject.SetActive(false);
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().aniShop1.gameObject.SetActive(true);
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().aniShop1.Play("SC-01-XianZhi", PlayMode.StopAll);
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "Buy success");
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().PlayLockOpen();
        LocalData.GetInstance().SaveLocalData();
    }

    /// <summary>
    /// 支付失败
    /// </summary>
    public void PayFaild(string str)
    {
        //Debug.Log("支付失败unity");
        payState = 0;
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "Buy faild");
    }     
	
}
