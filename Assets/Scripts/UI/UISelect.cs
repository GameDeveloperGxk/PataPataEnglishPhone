using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 遥控小手 
/// @date 2020/06/05
/// </summary>
public class UISelect : MonoBehaviour
{
    /// <summary>
    /// 操作种类 0 手机 1电视
    /// </summary>
    private int handleType = 0;
    public StartMenu startMenu;
    public ExitPanel exitPanel;
    public SelectLevel selectLevel;
    public Game game;
    public TipsPanel tipsPanel;
    public PausePanel pausePanel;
    public WinPanel winPanel;
    public LosePanel losePanel;
    public ShopPanel shopPanel;

    private Button[] allButton;
    private int currentButtonIndex = 0;
    private float timer = 0;
    private bool isHaveClick = false;
    public Image hand;
    
    void Start()
    {
        if (handleType == 0)
        {
            return;
        }
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setLoopPingPong();

        hand.gameObject.SetActive(!GameController.PHONE_VERSION);

        InitHandIndex();
    }

    void InitAllButton()
    {
        if (handleType == 0)
        {
            return;
        }
        allButton = new Button[] {startMenu.buttonSound, startMenu.buttonStart,startMenu.buttonExit,  //0  1  2
        exitPanel.ButtonYes,exitPanel.ButtonNo,// 4 5        
        pausePanel.btnYse,pausePanel.btnNo,//6  7
        winPanel.btnOK,// 8  9  10  11
        losePanel.btnGoon,losePanel.btnRestart,//12  13 14
        pausePanel.btnYse,pausePanel.btnNo,//15 16
        shopPanel.buttonBack,shopPanel.buttonGet0,shopPanel.buttonGet1,shopPanel.buttonGet2,//17 18 19 20
        };
    }

    public void RefurshSelectLevelButton()
    {
        InitAllButton();
    }

    /// <summary>
    /// 初始化按钮的索引，切换界面的时候调用
    /// </summary>
    public void InitHandIndex()
    {
        if (handleType == 0)
        {
            return;
        }
        InitAllButton();
        hand.gameObject.SetActive(true);
        switch (UIManager.GetInstance().currentUIStep)
        {
            case UIManager.UIStep.StartMenu:
                currentButtonIndex = 1;
                break;
            case UIManager.UIStep.ExitPanel:
                currentButtonIndex = 4;
                break;
            case UIManager.UIStep.SelectLevel:
                currentButtonIndex = 23 + LocalData.GetInstance().GetMaxOpenLevel();
                break;
            case UIManager.UIStep.Game:
                hand.gameObject.SetActive(false);
                break;
            case UIManager.UIStep.Win:
                currentButtonIndex = 9;
                break;
            case UIManager.UIStep.Lose:
                currentButtonIndex = 13;
                break;
            case UIManager.UIStep.TipsPanel:
                hand.gameObject.SetActive(false);
                break;
            case UIManager.UIStep.Pause:
                currentButtonIndex = 15;
                break;
            case UIManager.UIStep.Shop:
                currentButtonIndex = 18;
                break;
        }

        this.transform.position = allButton[currentButtonIndex].transform.localPosition;
    }

    void Update()
    {
        if (handleType == 0)
        {
            return;
        }
        ClickUp();
        ClickDown();
        ClickLeft();
        ClickRight();
        ClickSureDown();
        ClickSureUp();
        ClickReturn();
        if (isHaveClick == true)
        {
            timer += Time.deltaTime;
            if (timer >= 0.00001f)
            {
                timer = 0;
                isHaveClick = false;
            }
        }
    }

    public void ClickSureDown()
    {
        //if (isHaveClick == false)
        //{
            KeyCode MIOkKeyCode = GameController.DEBUG ? KeyCode.Return : (KeyCode)10;//小米遥控器确认键
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(MIOkKeyCode))
            {
                isHaveClick = true;
                switch (UIManager.GetInstance().currentUIStep)
                {
                    case UIManager.UIStep.Game:
                        break;
                    default:
                        break;
                }


            }
        //}
    }

    public void ClickSureUp()
    {
        //if (isHaveClick == false)
        //{
            KeyCode MIOkKeyCode = GameController.DEBUG ? KeyCode.Return : (KeyCode)10;//小米遥控器确认键
            if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyUp(MIOkKeyCode))
            {
                isHaveClick = true;
                switch (UIManager.GetInstance().currentUIStep)
                {
                    case UIManager.UIStep.Game:
                    break;
                    default:
                        allButton[currentButtonIndex].onClick.Invoke();
                        break;
                }

                
            }
        //}
    }
    public void ClickUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (UIManager.GetInstance().currentUIStep)
            {
                case UIManager.UIStep.StartMenu:
                    switch (currentButtonIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                            currentButtonIndex = 3;
                            break;
                        case 3:
                            currentButtonIndex = 1;
                            break;
                    }
                    break;
                case UIManager.UIStep.ExitPanel:
                    currentButtonIndex = currentButtonIndex == 4 ? 5 : 4;
                    break;
                case UIManager.UIStep.SelectLevel:
                    switch (currentButtonIndex)
                    {
                        case 21:
                            currentButtonIndex = 34;
                            break;
                        case 22:
                        case 23:
                            currentButtonIndex = 21;
                            break;
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                            currentButtonIndex = 21;
                            break;
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                            currentButtonIndex -= 5;
                            break;
                    }
                    break;
                case UIManager.UIStep.Game:
                    break;
                case UIManager.UIStep.Win:
                    switch (currentButtonIndex)
                    {
                        case 8:
                        case 9:
                        case 10:
                            currentButtonIndex = 11;
                            break;
                        case 11:
                            currentButtonIndex = 10;
                            break;
                    }
                    break;
                case UIManager.UIStep.Lose:
                    currentButtonIndex--;
                    if (currentButtonIndex < 12)
                    {
                        currentButtonIndex = 14;
                    }
                    break;
                case UIManager.UIStep.Pause:
                    currentButtonIndex = currentButtonIndex == 15 ? 16 : 15;
                    break;
                case UIManager.UIStep.Shop:
                    switch (currentButtonIndex)
                    {
                        case 17:
                            currentButtonIndex = 18;
                            break;
                        case 18:
                        case 19:
                        case 20:
                            currentButtonIndex = 17;
                            break;
                    }
                    break;
            }
            this.transform.position = allButton[currentButtonIndex].transform.position;
        }
    }

    public void ClickDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (UIManager.GetInstance().currentUIStep)
            {
                case UIManager.UIStep.StartMenu:
                    switch (currentButtonIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                            currentButtonIndex = 3;
                            break;
                        case 3:
                            currentButtonIndex = 1;
                            break;
                    }
                    break;
                case UIManager.UIStep.ExitPanel:
                    currentButtonIndex = currentButtonIndex == 4 ? 5 : 4;
                    break;
                case UIManager.UIStep.SelectLevel:
                    switch (currentButtonIndex)
                    {
                        case 21:
                            currentButtonIndex = 24;
                            break;
                        case 22:
                        case 23:
                            currentButtonIndex = 21;
                            break;
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                            currentButtonIndex += 5;
                            break;
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                            currentButtonIndex = 21;
                            break;
                    }
                    break;
                case UIManager.UIStep.Game:
                    break;
                case UIManager.UIStep.Win:
                    switch (currentButtonIndex)
                    {
                        case 8:
                        case 9:
                        case 10:
                            currentButtonIndex = 11;
                            break;
                        case 11:
                            currentButtonIndex = 10;
                            break;
                    }
                    break;
                case UIManager.UIStep.Lose:
                    currentButtonIndex++;
                    if (currentButtonIndex > 14)
                    {
                        currentButtonIndex = 12;
                    }
                    break;
                case UIManager.UIStep.Pause:
                    currentButtonIndex = currentButtonIndex == 15 ? 16 : 15;
                    break;
                case UIManager.UIStep.Shop:
                    switch (currentButtonIndex)
                    {
                        case 17:
                            currentButtonIndex = 18;
                            break;
                        case 18:
                        case 19:
                        case 20:
                            currentButtonIndex = 17;
                            break;
                    }
                    break;
            }
            this.transform.position = allButton[currentButtonIndex].transform.position;
        }
    }

    public void ClickLeft()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (UIManager.GetInstance().currentUIStep)
            {
                case UIManager.UIStep.StartMenu:
                    switch (currentButtonIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                            currentButtonIndex --;
                            if (currentButtonIndex < 0)
                            {
                                currentButtonIndex = 2;
                            }
                            break;
                        case 3:
                            currentButtonIndex = 1;
                            break;
                    }
                    break;
                case UIManager.UIStep.ExitPanel:
                    currentButtonIndex = currentButtonIndex == 4 ? 5 : 4;
                    break;
                case UIManager.UIStep.SelectLevel:
                    switch (currentButtonIndex)
                    {
                        case 21:
                            currentButtonIndex = 22;
                            break;
                        case 22:
                            currentButtonIndex = 23;
                            break;
                        case 23:
                            currentButtonIndex = 33;
                            break;
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                            currentButtonIndex --;
                            if(currentButtonIndex < 24)
                            {
                                currentButtonIndex = 22;
                            } 
                            break;
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                            currentButtonIndex --;
                            if (currentButtonIndex < 29)
                            {
                                currentButtonIndex = 22;
                            }
                            break;
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                            currentButtonIndex --;
                            if (currentButtonIndex < 34)
                            {
                                currentButtonIndex = 22;
                            }
                            break;
                    }
                    break;
                case UIManager.UIStep.Game:
                    break;
                case UIManager.UIStep.Win:
                    switch (currentButtonIndex)
                    {
                        case 8:
                        case 9:
                        case 10:
                            currentButtonIndex--;
                            if (currentButtonIndex < 8)
                            {
                                currentButtonIndex = 10;
                            }
                            break;
                        case 11:
                            currentButtonIndex = 10;
                            break;
                    }                   
                    break;
                case UIManager.UIStep.Lose:
                    currentButtonIndex--;
                    if (currentButtonIndex < 12)
                    {
                        currentButtonIndex = 14;
                    }
                    break;
                case UIManager.UIStep.Pause:
                    currentButtonIndex = currentButtonIndex == 15 ? 16 : 15;
                    break;
                case UIManager.UIStep.Shop:
                    switch (currentButtonIndex)
                    {
                        case 17:
                            currentButtonIndex = 18;
                            break;
                        case 18:
                        case 19:
                        case 20:
                            currentButtonIndex --;
                            if (currentButtonIndex < 18)
                            {
                                currentButtonIndex = 20;
                            }
                            break;
                    }
                    break;
            }
            this.transform.position = allButton[currentButtonIndex].transform.position;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            switch (UIManager.GetInstance().currentUIStep)
            {
                case UIManager.UIStep.Game:
                    break;
            }
            this.transform.position = allButton[currentButtonIndex].transform.position;
        }
    }

    public void ClickRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (UIManager.GetInstance().currentUIStep)
            {
                case UIManager.UIStep.StartMenu:
                    switch (currentButtonIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                            currentButtonIndex++;
                            if (currentButtonIndex > 2)
                            {
                                currentButtonIndex = 0;
                            }
                            break;
                        case 3:
                            currentButtonIndex = 1;
                            break;
                    }
                    break;
                case UIManager.UIStep.ExitPanel:
                    currentButtonIndex = currentButtonIndex == 4 ? 5 : 4;
                    break;
                case UIManager.UIStep.SelectLevel:
                    switch (currentButtonIndex)
                    {
                        case 21:
                            currentButtonIndex = 24;
                            break;
                        case 22:
                            currentButtonIndex = 24;
                            break;
                        case 23:
                            currentButtonIndex = 22;
                            break;
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                            currentButtonIndex++;
                            if (currentButtonIndex > 28)
                            {
                                currentButtonIndex = 23;
                            }
                            break;
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                            currentButtonIndex--;
                            if (currentButtonIndex > 33)
                            {
                                currentButtonIndex = 23;
                            }
                            break;
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                            currentButtonIndex++;
                            if (currentButtonIndex > 38)
                            {
                                currentButtonIndex = 23;
                            }
                            break;
                    }
                    break;
                case UIManager.UIStep.Game:
                    break;
                case UIManager.UIStep.Win:
                    switch (currentButtonIndex)
                    {
                        case 8:
                        case 9:
                        case 10:
                            currentButtonIndex++;
                            if (currentButtonIndex > 10)
                            {
                                currentButtonIndex = 8;
                            }
                            break;
                        case 11:
                            currentButtonIndex = 8;
                            break;
                    }
                    break;
                case UIManager.UIStep.Lose:
                    currentButtonIndex++;
                    if (currentButtonIndex > 14)
                    {
                        currentButtonIndex = 12;
                    }
                    break;
                case UIManager.UIStep.Pause:
                    currentButtonIndex = currentButtonIndex == 15 ? 16 : 15;
                    break;
                case UIManager.UIStep.Shop:
                    switch (currentButtonIndex)
                    {
                        case 17:
                            currentButtonIndex = 18;
                            break;
                        case 18:
                        case 19:
                        case 20:
                            currentButtonIndex++;
                            if (currentButtonIndex > 20)
                            {
                                currentButtonIndex = 18;
                            }
                            break;
                    }
                    break;
            }
            this.transform.position = allButton[currentButtonIndex].transform.position;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            switch (UIManager.GetInstance().currentUIStep)
            {
                case UIManager.UIStep.Game:
                    break;
            }
            this.transform.position = allButton[currentButtonIndex].transform.position;
        }
    }
    public void ClickReturn()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (UIManager.GetInstance().currentUIStep)
            {
                case UIManager.UIStep.StartMenu:
                    ClickBack(startMenu.buttonExit);
                    break;
                case UIManager.UIStep.ExitPanel:
                    ClickBack(exitPanel.ButtonNo);
                    break;
                case UIManager.UIStep.SelectLevel:
                    ClickBack(selectLevel.ButtonBack);
                    break;
                case UIManager.UIStep.Game:
                    break;
                case UIManager.UIStep.Pause:
                    ClickBack(pausePanel.btnYse);
                    break;
                case UIManager.UIStep.Win:
                    break;
                case UIManager.UIStep.Lose:
                    break;
                case UIManager.UIStep.Shop:
                    ClickBack(shopPanel.buttonBack);
                    break;
                default:
                    break;
            }
        }

    }

    private void ClickBack(Button btn)
    {
        btn.onClick.Invoke();
    }

}
