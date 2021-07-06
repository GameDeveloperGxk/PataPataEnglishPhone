using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 失败页面
/// </summary>
public class LosePanel : MonoBehaviour {

    
    public Button btnGoon;
    public Button btnRestart;

    public Animation ani;
    public float showTime;

    public Text textLevel;


    void Start () {
        
        btnGoon.onClick.AddListener(GoonClick);
        btnRestart.onClick.AddListener(RestartClick);
    }

    public void InitData()
    {
        showTime = 0;
        btnGoon.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
        ani.Play("JieSuan-ShiBai-TanChu", PlayMode.StopAll);
        textLevel.gameObject.SetActive(false);
        textLevel.text = "" + (GameController.GetInstance().currentLevel+1);
        AudioManager.GetInstance().StopMusic();
    }

    private void GoonClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Lose)
        {
            LeanTween.scale(btnGoon.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            

            UIManager.GetInstance().panelCloud.SetActive(true);
            UIManager.GetInstance().panelCloud.GetComponent<PanelCloud>().Play0(3);
            Invoke("ChangeView", 1f);
        }
    }

    void ChangeView()
    {
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Lose, false);
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, false);
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, true);
        AudioManager.GetInstance().PlayMusic(AudioManager.MusicUI);
    }
    

    private void RestartClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Lose)
        {
            LeanTween.scale(btnRestart.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            

            UIManager.GetInstance().panelCloud.SetActive(true);
            UIManager.GetInstance().panelCloud.GetComponent<PanelCloud>().Play0(1.5F);
            Invoke("ChangeView2", 1f);
        }
    }

    void ChangeView2()
    {
        UIManager.GetInstance().currentUIStep = UIManager.UIStep.Game;
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Lose, false);

        //初始化游戏数据，重新开始游戏
        GameController.GetInstance().InitGameData();
        UIManager.GetInstance().game.GetComponent<Game>().InitGame();
    }

    // Update is called once per frame
    void Update () {
        showTime+=Time.deltaTime;
        if (showTime > 0.7f)
        {
            btnGoon.gameObject.SetActive(true);
            btnRestart.gameObject.SetActive(true);
            ani.Play("JieSuan-ShiBai-ChiXu", PlayMode.StopAll);
            textLevel.gameObject.SetActive(true);
        }
    }
}
