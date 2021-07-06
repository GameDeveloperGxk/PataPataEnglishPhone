using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WinLastPanel : MonoBehaviour
{
    public Button btnGrad;
    public Animation aniBack;
    public Animation aniHead;
    public GameObject objStar;

    // Use this for initialization
    void Start()
    {
        btnGrad.onClick.AddListener(ClickGrad);
    }

    public void InitData()
    {
        Tools.PlayAnimation(aniBack, "SLXG-TanChu");
        btnGrad.gameObject.SetActive(false);
        aniHead.gameObject.SetActive(false);
        objStar.SetActive(false);
        Invoke("Show", 0.12f);
        Invoke("ShowBtn", 1f);
    }

    private void Show()
    {
        Tools.PlayAnimation(aniBack, "SLXG-ChiXu");
        aniHead.gameObject.SetActive(true);
        objStar.SetActive(true);
    }

    private void ShowBtn()
    {
        btnGrad.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickGrad()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        gameObject.SetActive(false);
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isCanClick = true;
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isPlayMov = false;
        GameController.GetInstance().hasNew = false;
        GameController.GetInstance().hasNewLast = false;
    }
}
