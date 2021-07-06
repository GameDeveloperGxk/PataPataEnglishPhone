using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonStart : MonoBehaviour
{
    private bool isDown;
    private bool isUp;
    private bool isExit;
    private bool isEnter;

    public Animation aniButtonStart;
    public GameObject parButtonStart;
    
    public GameObject parLine;
    public Animation aniBlack;

    public float timeClick;

    public float downTime;

    void Start()
    {
        EventTriggerListener.Get(gameObject).onDown += OnClickDown;
        EventTriggerListener.Get(gameObject).onUp += OnClickUp;
        EventTriggerListener.Get(gameObject).onEnter += OnClickEnter;
        EventTriggerListener.Get(gameObject).onExit += OnClickExit;
        aniButtonStart.Play("KaiShi-XianZhi-1", PlayMode.StopAll);
    }

    public void InitData()
    {
        isDown = false;
        isUp = true;
        isExit = false;
        isEnter = false;
        aniButtonStart.transform.localScale = new Vector3(1,1,1);
        aniButtonStart.Play("KaiShi-XianZhi-1", PlayMode.StopAll);
        parButtonStart.GetComponent<ParticleSystem>().Stop();
        aniButtonStart.transform.localScale = new Vector3(1,1,1);
        parLine.SetActive(false);
        aniBlack.gameObject.SetActive(false);
        timeClick = 0;
    }

    void OnClickDown(GameObject go)
    {
        if(GameController.GetInstance().currentLevel == 0 &&
            LocalData.GetInstance().GetMaxOpenLevel() == 1 &&
            LocalData.GetInstance().guidCurrentStep != 2)
        {
            return;
        }
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState != Game.GameState.GameWait)
        {
            return;
        }
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonStartHandler);
        isDown = true;
        isEnter = true;
        isUp = false;
        aniBlack.gameObject.SetActive(true);
        parLine.SetActive(true);
        Tools.PlayAnimation(aniBlack, "KSYY-XL-Dianji");
        Tools.PlayAnimation(aniButtonStart, "KAISHI-DianJi-1");

        downTime = 0;
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonStartDowning);
        //UIManager.GetInstance().game.GetComponent<Game>().GetComponent<Animation>().Play("GameScale",PlayMode.StopAll);
        //if (UIManager.GetInstance().game.GetComponent<Game>().mapImdex != 1)
        //{
        //    Tools.ScaleParticleSystem(UIManager.GetInstance().game.GetComponent<Game>()
        //    .effectBack[UIManager.GetInstance().game.GetComponent<Game>().mapImdex], 1.5f);
        //}
        Invoke("ChangeAnZhu", 0.1f);       
    }

    private void ChangeAnZhu()
    {
        if(isDown)
        {
            Tools.PlayAnimation(aniButtonStart, "KAISHI-AnZhu-1");
        }
    }

    void OnClickUp(GameObject go)
    {
        if (GameController.GetInstance().currentLevel == 0 &&
            LocalData.GetInstance().GetMaxOpenLevel() == 1 &&
            LocalData.GetInstance().guidCurrentStep != 2)
        {
            return;
        }
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState != Game.GameState.GameWait)
        {
            return;
        }
        if (isDown && isEnter)
        {
            Shoot();
            UIManager.GetInstance().game.GetComponent<Game>().hand.SetActive(false);
            if (LocalData.GetInstance().guidCurrentStep == 2)
            {
                UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
            }
            parLine.SetActive(false);
            aniBlack.gameObject.SetActive(false);

            AudioManager.GetInstance().PlayMusic(AudioManager.MusicGame1);

            // UIManager.GetInstance().game.GetComponent<Game>().GetComponent<Animation>().Play("GameScaleSmall", PlayMode.StopAll);
            // if (UIManager.GetInstance().game.GetComponent<Game>().mapImdex != 1)
            // {
            //     Tools.ScaleParticleSystem(UIManager.GetInstance().game.GetComponent<Game>()
            //.effectBack[UIManager.GetInstance().game.GetComponent<Game>().mapImdex], 1 / 1.5f);
            // }

        }        
    }

    void OnClickEnter(GameObject go)
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState != Game.GameState.GameWait)
        {
            return;
        }
        isEnter = true;
    }

    void OnClickExit(GameObject go)
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState != Game.GameState.GameWait)
        {
            return;
        }
        if (isDown)
        {
            isExit = true;
            isDown = false;
            isUp = false;
            isEnter = false;
            Tools.PlayAnimation(aniButtonStart, "KAISHI-YiChu-1");
            parLine.SetActive(false);
            Tools.PlayAnimation(aniBlack, "KSYY-XL-YiChu");
            
            //UIManager.GetInstance().game.GetComponent<Game>().unitCiclePanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //UIManager.GetInstance().game.GetComponent<Game>().imgMapUp.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //UIManager.GetInstance().game.GetComponent<Game>().GetComponent<Animation>().Play("GameScaleSmall", PlayMode.StopAll);
            //if (UIManager.GetInstance().game.GetComponent<Game>().mapImdex != 1)
            //{
            //    Tools.ScaleParticleSystem(UIManager.GetInstance().game.GetComponent<Game>()
            //.effectBack[UIManager.GetInstance().game.GetComponent<Game>().mapImdex], 1 / 1.5f);
            //}
            Invoke("Reset", 0.1f);
        }        
    }

    private void ResetXian()
    {
        if (isDown==false)
        {
            Tools.PlayAnimation(aniButtonStart, "KaiShi-XianZhi-1");
        }
    }

    private void Reset()
    {
        Tools.PlayAnimation(aniButtonStart, "KaiShi-XianZhi-1");
        aniButtonStart.transform.localScale = new Vector3(1,1,1);
        AudioManager.GetInstance().StopSound(AudioManager.SoundButtonStartDowning);
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonStartHandler);
    }

    private void Update()
    {
        if (isDown && isEnter&& GameController.GetInstance().stateZhen == 1&& isExit==false)
        {
            timeClick += Time.deltaTime;
            if(timeClick >= 0.5f)
            {
                Handheld.Vibrate();
                timeClick = 0;
            }
        }
        if (isDown)
        {
            downTime += Time.deltaTime;
            if (downTime > 3.0f)
            {
                downTime = 0;
                Shoot();
                UIManager.GetInstance().game.GetComponent<Game>().hand.SetActive(false);
                if (LocalData.GetInstance().guidCurrentStep == 2)
                {
                   //UIManager.GetInstance().game.GetComponent<Game>().SetGuidCurrentStep(3);
                    UIManager.GetInstance().game.GetComponent<Game>().HideGuid();
                }

                parLine.SetActive(false);
                aniBlack.gameObject.SetActive(false);

                AudioManager.GetInstance().PlayMusic(AudioManager.MusicGame1);
            }
        }

    }

    private void Shoot()
    {
        UIManager.GetInstance().game.GetComponent<Game>().ResetAllBallPositionBase();
       AudioManager.GetInstance().StopSound(AudioManager.SoundButtonStartDowning);
        UIManager.GetInstance().game.GetComponent<Game>().ShootBall();
        isDown = false;
        isUp = true;
        Tools.PlayAnimation(aniButtonStart, "KAISHI-BaoZha-1");
        parButtonStart.GetComponent<ParticleSystem>().Stop();
        parButtonStart.GetComponent<ParticleSystem>().Play();        
    } 
    

}