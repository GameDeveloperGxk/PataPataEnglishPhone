using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 发射的子弹(初始子弹)
/// </summary>
public class ShootSign : MonoBehaviour {

    public bool isMove = false;
    public int MoveSpeed = 10;

    // Use this for initialization
    void Start () {
	
	}

    public void InitData()
    {
        gameObject.SetActive(false);
        transform.localPosition = new Vector3(0,-638,0);
        isMove = false;
    }

    public void StartMove()
    {
        gameObject.SetActive(true);
        isMove = true;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePause)
        {
            return;
        }
        if (isMove)
        {
            transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed, Space.Self);
            if (transform.position.y > 1000)
            {
                InitData();
            }
        }
    }


    ///// <summary>
    ///// 碰撞
    ///// </summary>
    ///// <param name="collision"></param>
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    string _tag = collision.gameObject.tag;
    //    switch (_tag)
    //    {
    //        case "unit":
    //            isMove = false;
    //            InitData();
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
