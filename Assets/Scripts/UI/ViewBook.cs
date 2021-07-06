using UnityEngine;
using UnityEngine.UI;
public class ViewBook : MonoBehaviour {
    public Button btnBack;

    public GameObject btnSign;
    public GameObject objPar;
    // Use this for initialization

    public GameObject zhiyin;
	void Start () {
        btnBack.onClick.AddListener(ClickBack);
	}

    public void InitData()
    {       
        Tools.RemoveAllChildren(objPar);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int _id = i * 4 + j;
                Vector3 _vec = new Vector3(-400+j*270, 500 - i * 250, 0);
                GameObject _unit = GameObject.Instantiate(btnSign, _vec, Quaternion.identity) as GameObject;
                _unit.transform.SetParent(objPar.transform);
                _unit.transform.localScale = Vector3.one*1.2f;
                _unit.transform.localPosition = _vec;
                _unit.GetComponent<ButtonBookSign>().InitData(_id);
            }
        }
        //HideZhiYin();
        if (LocalData.GetInstance().GetMaxOpenLevel() == 2 )
        {
            zhiyin.SetActive(true);
            zhiyin.transform.position =
                new Vector3(
                objPar.transform.GetChild(1).transform.position.x+1f,
                objPar.transform.GetChild(1).transform.position.y-.4f,0
                );
            //zhiyin.transform.localPosition = new Vector3(-Screen.width / 10,-250,0)/100;
        }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 3)
        //{
        //    zhiyin1.SetActive(true);
        //}
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 12) { zhiyin2.SetActive(true); }

        //if (LocalData.GetInstance().GetMaxOpenLevel() == 22) { zhiyin3.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 32) { zhiyin4.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 42) { zhiyin5.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 52) { zhiyin6.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 62) { zhiyin7.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 72) { zhiyin8.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 82) { zhiyin9.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 92) { zhiyin10.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 102) { zhiyin11.SetActive(true); }
        //if (LocalData.GetInstance().GetMaxOpenLevel() == 106) { zhiyin12.SetActive(true); }
        AudioManager.GetInstance().PlaySound(AudioManager.SoundUIShow);
    }
	
    public void HideZhiYin()
    {
        zhiyin.SetActive(false);
    }
    void ClickBack()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().showBook = false;
        gameObject.SetActive(false);
        UIManager.GetInstance().ShowOrHideUI(UIManager.UIStep.SelectLevel, true);
    }

    public GameObject objShow;
    public void ShowBookMov(int _id)
    {
        objPar.SetActive(false);
        objShow.SetActive(true);
        objShow.GetComponent<ViewBookShow>().InitData(_id);
    }
    // Update is called once per frame
    void Update () {
	
	}
}