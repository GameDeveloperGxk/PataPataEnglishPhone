using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 发射的子弹
/// </summary>
public class ShootJH : MonoBehaviour
{
    public int ID;
    public Image imageSign0, imageSign1;

    private int signID;

    //0实心实线 1空心虚线
    private int type = 0;

    private float moveTime = 0;

    public int hitTime = 0;

    //触发下一个音符的时间
    private float shootTime = 1.5f;

    private int lines = -1;
    private int rows = -1;

    private float startY = 0;
    private float startX = 0;

    private float startTime;


    // Use this for initialization
    void Start()
    {

    }

    public void InitData(int _signID, int _rotate, int _id)
    {
        startTime = 0;
        ID = _id;
        signID = _signID;
        transform.localPosition = transform.position;
        startY = transform.localPosition.y;
        startX = transform.localPosition.x;
        moveTime = 0;
        switch (signID)
        {
            case 1:
            case 2:
            case 5:
            case 7:
            case 8:
            case 11:
            case 12:
            case 15:
            case 16:
                type = 1;
                imageSign0.gameObject.SetActive(false);
                shootTime = 5f;
                break;
            case 3:
            case 4:
            case 6:
            case 9:
            case 10:
            case 13:
            case 14:
                type = 2;
                imageSign1.gameObject.SetActive(false);
                shootTime = 7f;
                break;
        }
        float _d1 = 1.8f, _d2 = 2.5f;//5 7旧值
        shootTime = new float[] { _d1, _d2, _d1*2, _d2*2, _d1, _d1 * 2, _d1, _d2,
            _d1 * 2, _d2*2, _d1, _d2, _d1 * 2, _d2*2, _d1, _d1 }[signID - 1];
        shootTime *= 2f;//2.2-3.0 

        hitTime = type;
        transform.localEulerAngles = new Vector3(0, 0, _rotate);
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(true);
        lines = -1;
        rows = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePause)
        {
            return;
        }
        startTime += Time.deltaTime;
        if (startTime < 0.2f)
        {
            return;
        }
        transform.Translate(Vector3.up * Time.deltaTime * shootTime, Space.Self);
        moveTime += Time.deltaTime;
        if (moveTime > 2.0f)
        {
            moveTime = 0;
            GameObject.Destroy(gameObject);
        }
        if (Mathf.Abs(startY - transform.localPosition.y) > 190 * type || Mathf.Abs(startX - transform.localPosition.x) > 190 * type)
        {
            GameObject.Destroy(gameObject);
        }
    }

    /// <summary>
    /// 碰撞
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (startTime < 0.2f)
        {
            return;
        }
        string _tag = collision.gameObject.tag;
        switch (_tag)
        {
            case "unit":
                if (collision.gameObject.GetComponent<UnitCicle>().currentUnitState != UnitCicle.unitCicleState.wait)
                {
                    return;
                }
                int line = collision.gameObject.GetComponent<UnitCicle>().line;
                int row = collision.gameObject.GetComponent<UnitCicle>().row;

                if (lines == line && rows == row)
                {
                    return;
                }
                else
                {
                    lines = line;
                    rows = row;
                }
                hitTime--;
                if (hitTime <= 0 || Mathf.Abs(startY - transform.localPosition.y) > 200 || Mathf.Abs(startX - transform.localPosition.x) > 200)
                {
                    //Debug.Log("设置");
                    //collision.gameObject.GetComponent<UnitCicle>().SetJH(true,0);
                    GameObject.Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }

}
