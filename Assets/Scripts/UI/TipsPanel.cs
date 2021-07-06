using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 系统提示弹框
/// </summary>
public class TipsPanel : MonoBehaviour
{

    /// <summary>
    /// 提示信息
    /// </summary>
    public Text textInfo;

    /// <summary>
    /// 关闭时间
    /// </summary>
    public float timeClose = 0;
    /// <summary>
    /// 提示页面下边的页面,提示页面显示关闭后需要回到的页面
    /// </summary>
    public UIManager.UIStep currentUpStep;

    // Use this for initialization
    void Start()
    {

    }

    public void InitBaseData(string _textInfo, UIManager.UIStep _upStep)
    {
        textInfo.text = _textInfo;
        timeClose = 0;
        currentUpStep = _upStep;
        AudioManager.GetInstance().PlaySound(AudioManager.SoundTips);
    }

    private void ClosePanel()
    {
        UIManager.GetInstance().currentUIStep = currentUpStep;
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, false, "");
    }

    void Update()
    {
        timeClose+=Time.deltaTime;
        if (timeClose >=3f)
        {
            timeClose = 0;
            ClosePanel();
        }
        //点击任意位置关闭本界面
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            ClosePanel();
        }
    }

}
