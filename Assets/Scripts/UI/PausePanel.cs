using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 暂停页面
/// </summary>
public class PausePanel : MonoBehaviour
{
    public Button btnYse;
    public Button btnNo;

    public Game.GameState upState;

    // Use this for initialization
    void Start()
    {
        btnYse.onClick.AddListener(YseClick);
        btnNo.onClick.AddListener(NoClick);
    }

    private void YseClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Pause)
        {
            LeanTween.scale(btnYse.gameObject, new Vector3(0.6f,0.6f,0.6f), 0.1f).setLoopPingPong(1);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Pause, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Game, false);
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, true);
            AudioManager.GetInstance().PlayMusic(AudioManager.MusicUI);
            //退出游戏
            UIManager.GetInstance().game.GetComponent<Game>().Clear();
            //UIManager.GetInstance().panelCloud.SetActive(true);
            //UIManager.GetInstance().panelCloud.GetComponent<PanelCloud>().InitShow();
        }
    }

    private void NoClick()
    {
        if (UIManager.GetInstance().currentUIStep == UIManager.UIStep.Pause)
        {
            LeanTween.scale(btnNo.gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.1f).setLoopPingPong(1);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
            UIManager.GetInstance().currentUIStep = UIManager.UIStep.Game;
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.Pause, false);
            //继续游戏
            UIManager.GetInstance().game.GetComponent<Game>().currentGameState = upState;
        }
    }
       
    void Update()
    {

    }
}
