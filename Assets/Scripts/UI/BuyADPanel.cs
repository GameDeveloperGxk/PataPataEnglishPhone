using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BuyADPanel : MonoBehaviour {

    public Button btnBuy;
    public Button btnRect;
    public Button btnClose;

    public GameObject objRect;
    // Use this for initialization
    void Start () {
        btnBuy.onClick.AddListener(ClickBuy);
        btnRect.onClick.AddListener(ClickRect2);
        btnClose.onClick.AddListener(ClickRect);
    }
    private float _scale;

    public void InitData()
    {
        _scale = 0.2f;
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

    public void ClickBuy()
    {
        LeanTween.scale(btnBuy.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.1f).setLoopPingPong(1);
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        //拉起支付
        if (PayPort.GetInstance().payState == 0)
        {
            PayPort.GetInstance().PayHuaWeiAppTouch();
        }        
    }
    public void ClickRect()
    {
        LeanTween.scale(btnClose.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.1f).setLoopPingPong(1);
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        gameObject.SetActive(false);
        _scale = 0.2f;
    }
    public void ClickRect2()
    {
       // LeanTween.scale(btnBuy.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.05f).setLoopPingPong(1);
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        gameObject.SetActive(false);
        _scale = 0.2f;
    }
}
