using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * 品牌LOGO页面、遥控器页面
 * @date 20200328 21:59 * 
 * **/
public class LogoMenu : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Invoke("ShowRemoteControlImg", 2.5f);
    }

    public void ShowRemoteControlImg()
    {
        SceneManager.LoadScene("Game");
    }
    
    // Update is called once per frame
    void Update()
    {        

    }
}
