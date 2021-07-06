using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonBookSign : MonoBehaviour {
    public int ID;
    public int type;
    public Image imgIcon;
    public Sprite[] sprImg0;

    public GameObject[] objMov;
    public GameObject objBookShow;

    public Text textNew;

    public bool isNew;
    public float timeNew;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(ClickSign);
	}
	
    public void InitData(int _id)
    {
        ID = _id;
        type = 0;
        switch (ID)
        {
                //发射按钮
            case 0: type = 1; break;
                //单箭头
            case 1: type = 1; break;
            case 2: type = 1; break;
                //石头
            case 3: if (LocalData.GetInstance().GetMaxOpenLevel() > 2) { type = 1; } break;
                //次级核心
            case 4: if (LocalData.GetInstance().GetMaxOpenLevel() > 21) { type = 1; } break;
                //单箭头-跳跃
            case 5: if (LocalData.GetInstance().GetMaxOpenLevel() > 11) { type = 1; } break;
            case 6: if (LocalData.GetInstance().GetMaxOpenLevel() > 21) { type = 1; } break;
            case 7: if (LocalData.GetInstance().GetMaxOpenLevel() > 31) { type = 1; } break;
            case 8: if (LocalData.GetInstance().GetMaxOpenLevel() > 41) { type = 1; } break;
            case 9: if (LocalData.GetInstance().GetMaxOpenLevel() > 51) { type = 1; } break;
            case 10: if (LocalData.GetInstance().GetMaxOpenLevel() > 61) { type = 1; } break;
            case 11: if (LocalData.GetInstance().GetMaxOpenLevel() > 71) { type = 1; } break;
            case 12: if (LocalData.GetInstance().GetMaxOpenLevel() > 81) { type = 1; } break;
            case 13: if (LocalData.GetInstance().GetMaxOpenLevel() > 91) { type = 1; } break;
            case 14: if (LocalData.GetInstance().GetMaxOpenLevel() > 101) { type = 1; } break;
            case 15: if (LocalData.GetInstance().GetMaxOpenLevel() > 106) { type = 1; } break;
            case 16: if (LocalData.GetInstance().GetMaxOpenLevel() > 111) { type = 1; } break;
            case 17: if (LocalData.GetInstance().GetMaxOpenLevel() > 121) { type = 1; } break;
            case 18: if (LocalData.GetInstance().GetMaxOpenLevel() > 131) { type = 1; } break;
            case 19: if (LocalData.GetInstance().GetMaxOpenLevel() > 141) { type = 1; } break;
        }
        imgIcon.sprite = sprImg0[ID];
        
        if ( type==1)
        {
            GetComponent<Image>().color = Color.clear;
            objMov[ID].SetActive(true);
        }
        textNew.gameObject.SetActive(false);
        isNew = false;
        timeNew = 0;
        switch (ID)
        {
            case 0: 
            case 1:
            case 2:
                if (LocalData.GetInstance().GetMaxOpenLevel() == 2) { SetNewText(); }
                break;
            //石头
            case 3: if (LocalData.GetInstance().GetMaxOpenLevel() ==3) { SetNewText(); } break;
            //次级核心
            case 4: if (LocalData.GetInstance().GetMaxOpenLevel() ==22) { SetNewText(); } break;
            //单箭头-跳跃
            case 5: if (LocalData.GetInstance().GetMaxOpenLevel() == 12) { SetNewText(); } break;
            case 6: if (LocalData.GetInstance().GetMaxOpenLevel() == 22) { SetNewText(); } break;
            case 7: if (LocalData.GetInstance().GetMaxOpenLevel() == 32) { SetNewText(); } break;
            case 8: if (LocalData.GetInstance().GetMaxOpenLevel() == 42) { SetNewText(); } break;
            case 9: if (LocalData.GetInstance().GetMaxOpenLevel() == 52) { SetNewText(); } break;
            case 10: if (LocalData.GetInstance().GetMaxOpenLevel() == 62) { SetNewText(); } break;
            case 11: if (LocalData.GetInstance().GetMaxOpenLevel() == 72) { SetNewText(); } break;
            case 12: if (LocalData.GetInstance().GetMaxOpenLevel() == 82) { SetNewText(); } break;
            case 13: if (LocalData.GetInstance().GetMaxOpenLevel() == 92) { SetNewText(); } break;
            case 14: if (LocalData.GetInstance().GetMaxOpenLevel() == 102) { SetNewText(); } break;
            case 15: if (LocalData.GetInstance().GetMaxOpenLevel() == 107) { SetNewText(); } break;
            case 16: if (LocalData.GetInstance().GetMaxOpenLevel() == 112) { SetNewText(); } break;
            case 17: if (LocalData.GetInstance().GetMaxOpenLevel() == 122) { SetNewText(); } break;
            case 18: if (LocalData.GetInstance().GetMaxOpenLevel() == 132) { SetNewText(); } break;
            case 19: if (LocalData.GetInstance().GetMaxOpenLevel() == 142) { SetNewText(); } break;
        }
    }

    private void SetNewText()
    {
        textNew.gameObject.SetActive(true);
        isNew = true;
        timeNew = 0;
        if(LocalData.GetInstance().bookSignState[ID] == 2)
        {
            textNew.gameObject.SetActive(false);
            isNew = false;
        }
    }
    void ClickSign()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        if (type == 1)
        {
            LocalData.GetInstance().bookSignState[ID] = 2;
            textNew.gameObject.SetActive(false);
            isNew = false;
            UIManager.GetInstance().viewBook.GetComponent<ViewBook>().ShowBookMov(ID);
        }
        else
        {
            UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.TipsPanel, true, "unlocked");
        }        
        UIManager.GetInstance().viewBook.GetComponent<ViewBook>().HideZhiYin();
    }

    private void Update()
    {
        if (isNew)
        {
            timeNew += Time.deltaTime;
            if(timeNew < 0.5f)
            {
                textNew.gameObject.SetActive(true);
            }
            if (timeNew > 0.5f && timeNew < 1.0)
            {
                textNew.gameObject.SetActive(false);
            }
            if (timeNew > 1.0f)
            {
                timeNew = 0;
            }
        }
    }
}
