using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
/// <summary>
/// 游戏金币奖励
/// </summary>
public class StarLevel : MonoBehaviour
{
    private Vector3 mubiao;

    private bool isFly;

    private float timeDelay;

    public Image img;
    public GameObject par;

    
    // Use this for initialization
    void Start()
    {

    }

    public void InitData(Vector3 _mubiao)
    {
        img.gameObject.SetActive(false);
        par.SetActive(false);
        par.GetComponent<ParticleSystem>().Stop();
        isFly = false;
        mubiao = _mubiao;
        transform.localScale = new Vector3(.1f, .1f, .1f);
        timeDelay = 1.0f;//1秒后播放飞星
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isCanClick = false;
    }

    private void Update()
    {
        if (UIManager.GetInstance().panelCloud.activeSelf == false && timeDelay > 0)
        {
            timeDelay -= Time.deltaTime;
            if (timeDelay < 0.4f && timeDelay > 0 && par.activeSelf == false)
            {
                par.SetActive(true);
                par.GetComponent<ParticleSystem>().Stop();
                par.GetComponent<ParticleSystem>().Play();
            }
            if (timeDelay <= 0f && img.gameObject.activeSelf == false)
            {
                img.gameObject.SetActive(true);
                par.SetActive(false);
                par.GetComponent<ParticleSystem>().Stop();
            }

            if (timeDelay <= 0 && isFly == false)
            {
                isFly = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (isFly)
            {
                Tweener t = transform.DOMove(mubiao, 1.0f).SetEase(Ease.InFlash);
                t.SetAutoKill(false);
                t.Pause();
                transform.DOPlayForward();
                t.onComplete = delegate ()
                {
                    AudioManager.GetInstance().PlaySound(AudioManager.SoundStarUI);
                    LocalData.GetInstance().starNum++;
                    LocalData.GetInstance().SaveLocalData();
                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().textStarNum.text = LocalData.GetInstance().starNum + "/150";
                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().text1.text = LocalData.GetInstance().starNum + "";
                    if (LocalData.GetInstance().starNum ==
                   GameController.GetInstance().starNum[UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().currentPage])
                    {
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().text1.color = Color.white;
                        //如果是有完美通关星星火的，则检测是否达到解锁大关卡条件，达到的话开启石像和大关卡
                        if ( LocalData.GetInstance().GetMaxOpenLevel()== UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().currentPage * 10 + 10)
                        {
                            GameController.GetInstance().hasNew = true;
                            GameController.GetInstance().hasNewLast = true;
                            LocalData.GetInstance().SetMaxOpenLevel(LocalData.GetInstance().GetMaxOpenLevel() + 1);
                            GameController.GetInstance().currentLevel = 9 +
                            UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().currentPage * 10;
                            UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().InitData(100);
                        }                         
                    }
                    else if (LocalData.GetInstance().starNum >= 
                    GameController.GetInstance().starNum[UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().currentPage])
                    {
                        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().text1.color = Color.white;
                    }

                    UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().StartScale();

                   GameObject.Destroy(gameObject);
                };
                UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().isCanClick = true;
                isFly = false;
            }

        }

    }

   
}
