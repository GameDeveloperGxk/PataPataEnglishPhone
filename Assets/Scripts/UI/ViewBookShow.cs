using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ViewBookShow : MonoBehaviour
{
    public Button btnBack;
    public GameObject[] objIcon;
    public Text textName, textInfo;
    public int ID;
    public GameObject[] obj0;
    public GameObject[] obj1;
    public GameObject[] obj2;
    public GameObject[] obj3;
    public GameObject[] obj4;
    public GameObject[] obj5;
    public GameObject[] obj6;
    public GameObject[] obj7;
    public GameObject[] obj8;
    public GameObject[] obj9;
    public GameObject[] obj10;
    public GameObject[] obj11;
    public GameObject[] obj12;
    public GameObject[] obj13;
    public GameObject[] obj14;
    public GameObject[] obj15;
    public GameObject[] obj16;
    public GameObject[] obj17;
    public GameObject[] obj18;
    public GameObject[] obj19;

    public GameObject[] panel;

    float timeMov = 0;
    int state = 0;

    int ranIndex = 0;

    public GameObject randomParent,randomPre;

    // Use this for initialization
    void Start()
    {
        btnBack.onClick.AddListener(ClickBack);

        //Tools.ScaleParticleSystem(obj9[6], 0.8f);
        //Tools.ScaleParticleSystem(obj9[7], 0.8f);
        //Tools.ScaleParticleSystem(obj9[8], 0.8f);
        //Tools.ScaleParticleSystem(obj9[9], 0.8f);

        Tools.ScaleParticleSystem(objIcon[9], 1.3f);
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData(int _id)
    {
        ID = _id;
        objIcon[ID].SetActive(true);
        textName.text = new string[] {
            "Big eye elves",
            "Pata elves",
            "The elantra elves",
            "Stone guard",
            "Numbers elves",
            "Jump elves",
            "HeeHee elves",
            "Prison",
            "Portal",
            "Fury elves",
            "Kaka elves",
            "Grandpa Winter elves",
            "Little Jiro elves",
            "Nina elves",
            "Niba elves",
            "Nobita elves",
            "Songsong elves",
            "Qiqi elves",
            "QuiQuick elves",
            "Cocoa elves",
        }[ID];
        textInfo.text = new string[] {
            "Attacks the first elves touched directly above",
            "Attacks a neighboring elves",
            "Hit it to win the game",
            "Can not attack, can only be destroyed by the elantra elves",
            "Hit it to get a lot of notes",
            "Jump to attack an elves",
            "Attack two neighboring elves on the left and right",
            "Jump to attack two elves on the left and right",
            "Destroy after being hit once",
            "When a elves at a portal portal is attacked, it fires at the corresponding portal exit",
            "Attack both elves above the diagonal simultaneously",
            "Jump to attack two elves above the diagonal.",
            "Attack three adjacent elves",
            "Jump to attack three elves",
            "Attack elves in all four directions at once",
            "Random attacks on elves that exist in the field",
            "The guardian elve of levels 111-120",
            "The guardian elve of levels 121-130",
            "The guardian elve of levels 131-140",
            "The guardian elve of levels 141-149",
        }[ID];
        panel[ID].SetActive(true);
        timeMov = 0;
        InitMov();
        Tools.RemoveAllChildren(RewardPanel);
        AudioManager.GetInstance().PlaySound(AudioManager.SoundUIShow);
    }

    void InitMov()
    {
        
        timeMov = 0;
        state = 0;
        switch (ID)
        {
            case 0:
                PlayAnimation(obj0[0], "KaiShi-XianZhi-1");
                StopParticleSystem(obj0[1]);
                PlayAnimation(obj0[2], "Ball-1-XianZhi");
                StopParticleSystem(obj0[3]);
                obj0[4].SetActive(false);
                break;
            case 1:
                PlayAnimation(obj1[0], "Ball-1-XianZhi");
                StopParticleSystem(obj1[1]);
                PlayAnimation(obj1[2], "Ball-1-XianZhi");
                StopParticleSystem(obj1[3]);
                break;
            case 2:
                PlayAnimation(obj2[0], "HeXinZD-XianZhi");
                StopParticleSystem(obj2[1]);
                PlayAnimation(obj2[2], "Ball-1-XianZhi");
                StopParticleSystem(obj2[3]);
                break;
            case 3:
                PlayAnimation(obj3[0], "Ball-ShiTou-XianZhi");
                PlayAnimation(obj3[1], "Ball-1-XianZhi");
                StopParticleSystem(obj3[2]);
                break;
            case 4:
                PlayAnimation(obj4[0], "Ball-1-XianZhi");
                StopParticleSystem(obj4[1]);
                PlayAnimation(obj4[2], "ZD-XianZhi");
                StopParticleSystem(obj4[3]);
                break;
            case 5:
                PlayAnimation(obj5[0], "Ball-2-XianZhi");
                StopParticleSystem(obj5[1]);
                PlayAnimation(obj5[2], "Ball-1-XianZhi");
                StopParticleSystem(obj5[3]);
                break;
            case 6:
                PlayAnimation(obj6[0], "Ball-3-XianZhi");
                StopParticleSystem(obj6[1]);
                PlayAnimation(obj6[2], "Ball-1-XianZhi");
                StopParticleSystem(obj6[3]);
                PlayAnimation(obj6[4], "Ball-1-XianZhi");
                StopParticleSystem(obj6[5]);
                break;
            case 7:
                PlayAnimation(obj7[0], "Ball-4-XianZhi");
                StopParticleSystem(obj7[1]);
                PlayAnimation(obj7[2], "Ball-1-XianZhi");
                StopParticleSystem(obj7[3]);
                PlayAnimation(obj7[4], "Ball-1-XianZhi");
                StopParticleSystem(obj7[5]);
                break;
            case 8:
                PlayAnimation(obj8[0], "Ball-1-XianZhi");
                StopParticleSystem(obj8[1]);
                PlayAnimation(obj8[2], "Ball-1-XianZhi");
                StopParticleSystem(obj8[3]);
                PlayAnimation(obj8[4], "Ball-1-XianZhi");
                StopParticleSystem(obj8[5]);
                obj8[6].SetActive(false);
                obj8[7].SetActive(true);
                break;
            case 9:
                PlayAnimation(obj9[0], "Ball-1-XianZhi");
                StopParticleSystem(obj9[1]);

                obj9[2].SetActive(true);
                PlayAnimation(obj9[2], "Ball-1-XianZhi");
                obj9[6].SetActive(true);
                obj9[7].SetActive(false);
                StopParticleSystem(obj9[7]);

                obj9[8].SetActive(true);
                obj9[9].SetActive(false);
                obj9[3].SetActive(false);
                StopParticleSystem(obj9[3]);
                StopParticleSystem(obj9[9]);

                PlayAnimation(obj9[4], "Ball-1-XianZhi");
                StopParticleSystem(obj9[5]);
                break;
            case 10:
                PlayAnimation(obj10[0], "Ball-5-XianZhi");
                StopParticleSystem(obj10[1]);
                PlayAnimation(obj10[2], "Ball-1-XianZhi");
                StopParticleSystem(obj10[3]);
                PlayAnimation(obj10[4], "Ball-1-XianZhi");
                StopParticleSystem(obj10[5]);
                break;
            case 11:
                PlayAnimation(obj11[0], "Ball-6-XianZhi");
                StopParticleSystem(obj11[1]);
                PlayAnimation(obj11[2], "Ball-1-XianZhi");
                StopParticleSystem(obj11[3]);
                PlayAnimation(obj11[4], "Ball-1-XianZhi");
                StopParticleSystem(obj11[5]);
                break;
            case 12:
                PlayAnimation(obj12[0], "Ball-7-XianZhi");
                StopParticleSystem(obj12[1]);
                PlayAnimation(obj12[2], "Ball-1-XianZhi");
                StopParticleSystem(obj12[3]);
                PlayAnimation(obj12[4], "Ball-1-XianZhi");
                StopParticleSystem(obj12[5]);
                PlayAnimation(obj12[6], "Ball-1-XianZhi");
                StopParticleSystem(obj12[7]);
                break;
            case 13:
                PlayAnimation(obj13[0], "Ball-8-XianZhi");
                StopParticleSystem(obj13[1]);
                PlayAnimation(obj13[2], "Ball-1-XianZhi");
                StopParticleSystem(obj13[3]);
                PlayAnimation(obj13[4], "Ball-1-XianZhi");
                StopParticleSystem(obj13[5]);
                PlayAnimation(obj13[6], "Ball-1-XianZhi");
                StopParticleSystem(obj13[7]);
                break;
            case 14:
                PlayAnimation(obj14[0], "Ball-9-XianZhi");
                StopParticleSystem(obj14[1]);
                PlayAnimation(obj14[2], "Ball-1-XianZhi");
                StopParticleSystem(obj14[3]);
                PlayAnimation(obj14[4], "Ball-1-XianZhi");
                StopParticleSystem(obj14[5]);
                PlayAnimation(obj14[6], "Ball-1-XianZhi");
                StopParticleSystem(obj14[7]);
                PlayAnimation(obj14[8], "Ball-1-XianZhi");
                StopParticleSystem(obj14[9]);
                break;
            case 15:
                PlayAnimation(obj15[0], "Ball-SuiJi-XianZhi");
                StopParticleSystem(obj15[1]);
                PlayAnimation(obj15[2], "Ball-1-XianZhi");
                StopParticleSystem(obj15[3]);
                PlayAnimation(obj15[4], "Ball-1-XianZhi");
                StopParticleSystem(obj15[5]);
                PlayAnimation(obj15[6], "Ball-1-XianZhi");
                StopParticleSystem(obj15[7]);
                obj15[8].SetActive(true);
                obj15[9].SetActive(true);
                obj15[10].SetActive(true);
                ranIndex = Random.Range(0, 3);
                if (ranIndex == 0)
                {
                    obj15[8].SetActive(false);
                }
                else if (ranIndex == 1)
                {
                    obj15[9].SetActive(false);
                }
                else
                {
                    ranIndex = 2;
                    obj15[10].SetActive(false);
                }
                Tools.RemoveAllChildren(randomParent);
                break;
            case 16: break;
            case 17: break;
            case 18: break;
            case 19: break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timeMov += Time.deltaTime;
        switch (ID)
        {
            case 0:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.8f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                if (state == 1)
                {
                    obj0[4].transform.Translate(Vector3.up * Time.deltaTime * 10, Space.Self);
                }
                break;
            case 1:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.8f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 2:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)//因为终点球动画时间长
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 3:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 4:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 5:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 6:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 7:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 8:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.8f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.1f)
                {
                    SetState(3);
                }
                if (state == 3 && timeMov >= 2.4f)
                {
                    SetState(4);
                }
                if (state == 4 && timeMov >= 3.5f)
                {
                    SetState(0);
                }
                break;
            case 9:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.8f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.1f)
                {
                    SetState(3);
                }
                if (state == 3 && timeMov >= 2.4f)
                {
                    SetState(4);
                }
                if (state == 4 && timeMov >= 3.5f)
                {
                    SetState(0);
                }
                break;
            case 10:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 11:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 12:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 13:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 14:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 15:
                if (state == 0 && timeMov >= 1.5f)
                {
                    SetState(1);
                }
                if (state == 1 && timeMov >= 1.9f)
                {
                    SetState(2);
                }
                if (state == 2 && timeMov >= 2.5f)
                {
                    SetState(0);
                }
                break;
            case 16: break;
            case 17: break;
            case 18: break;
            case 19: break;
            default:
                break;
        }
    }
    /// <summary>
    ///  切换状态
    /// </summary>
    /// <param name="_state"></param>
    void SetState(int _state)
    {
        if (state != _state)
        {
            state = _state;
        }
        else
        {
            return;
        }
        switch (ID)
        {
            case 0:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj0[0], "KAISHI-BaoZha-1");
                    PlayParticleSystem(obj0[1]);
                    obj0[4].SetActive(true);
                    obj0[4].transform.localPosition = new Vector3(0, -389, 0);
                }
                if (state == 2)
                {
                    obj0[4].SetActive(false);
                    PlayAnimation(obj0[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj0[3]);
                }
                break;
            case 1:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 2)
                {
                    PlayAnimation(obj1[0], "Ball-1-BaoZha");
                    PlayParticleSystem(obj1[1]);
                }
                if (state == 1)
                {
                    PlayAnimation(obj1[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj1[3]);
                }
                break;
            case 2:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 2)
                {
                    PlayAnimation(obj2[0], "HeXinZD-BaoZha");
                    PlayParticleSystem(obj2[1]);
                }
                if (state == 1)
                {
                    PlayAnimation(obj2[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj2[3]);
                    CreateYinfu(obj4[2].transform.position, 6);
                }
                break;
            case 3:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 2)
                {
                    PlayAnimation(obj3[0], "Ball-ShiTou-Dianji");
                }
                if (state == 1)
                {
                    PlayAnimation(obj3[1], "Ball-1-BaoZha");
                    PlayParticleSystem(obj3[2]);
                }
                break;
            case 4:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj4[0], "Ball-1-BaoZha");
                    PlayParticleSystem(obj4[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj4[2], "ZD-BaoZha");
                    PlayParticleSystem(obj4[3]);
                    CreateYinfu(obj4[2].transform.position, 6);
                }
                break;
            case 5:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj5[0], "Ball-2-BaoZha");
                    PlayParticleSystem(obj5[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj5[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj5[3]);
                }
                break;
            case 6:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj6[0], "Ball-3-BaoZha");
                    PlayParticleSystem(obj6[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj6[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj6[3]);
                    PlayAnimation(obj6[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj6[5]);
                }
                break;
            case 7:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj7[0], "Ball-4-BaoZha");
                    PlayParticleSystem(obj7[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj7[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj7[3]);
                    PlayAnimation(obj7[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj7[5]);
                }
                break;
            case 8:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj8[0], "Ball-1-BaoZha");
                    PlayParticleSystem(obj8[1]);
                }
                if (state == 2)
                {
                    obj8[6].SetActive(true);
                    obj8[7].SetActive(false);
                    PlayAnimation(obj8[6], "Suo-BaoZha");
                }
                if (state == 3)
                {
                    PlayAnimation(obj8[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj8[5]);
                }
                if (state == 4)
                {
                    PlayAnimation(obj8[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj8[3]);
                }
                break;
            case 9:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj9[0], "Ball-1-BaoZha");
                    PlayParticleSystem(obj9[1]);
                }
                if (state == 2)
                {
                    obj9[2].SetActive(false);
                    obj9[6].SetActive(false);
                    obj9[7].SetActive(true);
                    PlayParticleSystem(obj9[7]);
                }
                if (state == 3)
                {
                    obj9[8].SetActive(false);
                    obj9[9].SetActive(true);
                    obj9[3].SetActive(true);
                    PlayParticleSystem(obj9[3]);
                    PlayParticleSystem(obj9[9]);
                }
                if (state == 4)
                {
                    PlayAnimation(obj9[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj9[5]);
                }
                break;
            case 10:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj10[0], "Ball-5-BaoZha");
                    PlayParticleSystem(obj10[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj10[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj10[3]);
                    PlayAnimation(obj10[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj10[5]);
                }
                break;
            case 11:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj11[0], "Ball-6-BaoZha");
                    PlayParticleSystem(obj11[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj11[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj11[3]);
                    PlayAnimation(obj11[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj11[5]);
                }
                break;
            case 12:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj12[0], "Ball-7-BaoZha");
                    PlayParticleSystem(obj12[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj12[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj12[3]);
                    PlayAnimation(obj12[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj12[5]);
                    PlayAnimation(obj12[6], "Ball-1-BaoZha");
                    PlayParticleSystem(obj12[7]);
                }
                break;
            case 13:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj13[0], "Ball-8-BaoZha");
                    PlayParticleSystem(obj13[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj13[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj13[3]);
                    PlayAnimation(obj13[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj13[5]);
                    PlayAnimation(obj13[6], "Ball-1-BaoZha");
                    PlayParticleSystem(obj13[7]);
                }
                break;
            case 14:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj14[0], "Ball-9-BaoZha");
                    PlayParticleSystem(obj14[1]);
                }
                if (state == 2)
                {
                    PlayAnimation(obj14[2], "Ball-1-BaoZha");
                    PlayParticleSystem(obj14[3]);
                    PlayAnimation(obj14[4], "Ball-1-BaoZha");
                    PlayParticleSystem(obj14[5]);
                    PlayAnimation(obj14[6], "Ball-1-BaoZha");
                    PlayParticleSystem(obj14[7]);
                    PlayAnimation(obj14[8], "Ball-1-BaoZha");
                    PlayParticleSystem(obj14[9]);
                }
                break;
            case 15:
                if (state == 0)
                {
                    InitMov();
                }
                if (state == 1)
                {
                    PlayAnimation(obj15[0], "Ball-SuiJi-BaoZha");
                    PlayParticleSystem(obj15[1]);
                    Vector3 _ranP = new Vector3(-2, -428-308, 0);
                    GameObject rand = GameObject.Instantiate(randomPre,
                        _ranP, Quaternion.identity) as GameObject;
                    rand.transform.SetParent(randomParent.transform);
                    rand.transform.localScale = new Vector3(1, 1, 1);
                    rand.transform.localPosition = _ranP;
                    Vector3 _vec;
                    if (ranIndex == 0)
                    {
                        _vec = obj15[8].transform.position;
                    }
                    else if (ranIndex == 1)
                    {
                        _vec = obj15[9].transform.position;
                    }
                    else
                    {
                        _vec = obj15[10].transform.position;
                    }
                    rand.GetComponent<RandomJian>().InitData(_vec);
                }
                if (state == 2)
                {
                    if(ranIndex == 0)
                    {
                        PlayAnimation(obj15[2], "Ball-1-BaoZha");
                        PlayParticleSystem(obj15[3]);
                    }
                    else if(ranIndex==1)
                    {
                        PlayAnimation(obj15[4], "Ball-1-BaoZha");
                        PlayParticleSystem(obj15[5]);
                    }
                    else
                    {
                        PlayAnimation(obj15[6], "Ball-1-BaoZha");
                        PlayParticleSystem(obj15[7]);
                    }
                }
                break;
            case 16: break;
            case 17: break;
            case 18: break;
            case 19: break;
            default:
                break;
        }
    }

    public GameObject rewardCoinPrefab;
    public GameObject RewardPanel;
    public GameObject imgTarget;

    /// <summary>
    /// 生成音符奖励 
    /// </summary>
    /// <param name="_vec"></param>
    public void CreateYinfu(Vector3 _vec, int _num)
    {
        Tools.RemoveAllChildren(RewardPanel);
        for (int i = 0; i < _num; i++)
        {
            GameObject _unit = GameObject.Instantiate(rewardCoinPrefab, _vec, Quaternion.identity) as GameObject;
            _unit.transform.SetParent(RewardPanel.transform);
            _unit.transform.position = _vec;
            _unit.transform.localScale = new Vector3(1, 1, 1);
            _unit.GetComponent<BookReward>().InitData(_vec, imgTarget,
                Random.Range(0, 10) < 5 ? 0 : 10, _num, i);
        }
    }

    void ClickBack()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundButtonClick);
        Tools.RemoveAllChildren(RewardPanel);
        objIcon[ID].SetActive(false);
        panel[ID].SetActive(false);
        gameObject.SetActive(false);
        UIManager.GetInstance().viewBook.GetComponent<ViewBook>().objPar.SetActive(true);
    }
    /// <summary>
    /// 播放animation动画
    /// </summary>
    void PlayAnimation(GameObject ani, string stateName)
    {
        AnimationState state = ani.GetComponent<Animation>()[stateName];
        state.time = 0;
        ani.GetComponent<Animation>().Stop();
        ani.GetComponent<Animation>().Play(stateName, PlayMode.StopAll);

        ani.transform.localScale = Vector3.one;
    }
    /// <summary>
    /// 暂停播放animation动画
    /// </summary>
    void StopAnimation(GameObject ani, string stateName)
    {
        AnimationState state = ani.GetComponent<Animation>()[stateName];
        state.time = 0;
        ani.GetComponent<Animation>().Stop();
    }
    void PlayParticleSystem(GameObject par)
    {
        par.GetComponent<ParticleSystem>().Stop();
        par.GetComponent<ParticleSystem>().Play();
    }
    void StopParticleSystem(GameObject par)
    {
        par.GetComponent<ParticleSystem>().Stop();
    }
}
