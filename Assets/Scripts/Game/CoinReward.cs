using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// 游戏奖励
/// </summary>
public class CoinReward : MonoBehaviour
{
    public GameObject img;
    Vector3[] path1 = new Vector3[3];
    Tweener tweenPath;
    //飞行时间
    private float speed = 1.6f;


    //用来区分是不是终点球的飞音符:0不是 1是
    private int typeScale = 0;
    /// <summary>
    /// 终点球--状态
    /// </summary>
    private int flyState = 0;
    //终点球两个阶段的飞行时间
    private float flyTime0 = 0.6f, flyTime1 = 1.55f;
    private Vector3 targetVec;

    private int indexs = 0;

    // Use this for initialization
    void Start()
    {
    }

    public void InitData(Vector3 _startPosition, GameObject _target, int _type, int _num, int _index)
    {
        indexs = _index;
        if (_num > 8)
        {
            path1 = new Vector3[2];
            if (_index == 1)
            {
                typeScale = 1;
            }
            path1 = new Vector3[3];
            path1[0] = _startPosition;//起始点
            targetVec = _target.transform.position;//终点
            int _t = 1;
            if (path1[2].x < path1[0].x)//ui在右侧
            {
                _t = -1;
            }
            else
            {
                _t = 1;
            }
            if (_type < 10)//左侧
            {
                _t = -1;
            }
            else//右侧
            {
                _type -= 10;
                _t = 1;
            }
            float _rx = Random.Range(-6, 6.0f);
            float _ry = Random.Range(-5f, 6f);
            path1[1] = new Vector3(path1[0].x + _t * _rx, path1[0].y - _ry, 0);
            tweenPath = img.transform.DOPath(path1, flyTime0, PathType.CatmullRom).SetLoops(1)
                    .SetEase(Ease.OutExpo).OnWaypointChange(OnWaypointChange2);
        }
        else
        {            
            path1 = new Vector3[6];
            path1[0] = _startPosition;//起始点
            path1[5] = _target.transform.position;//终点
            int _t = 1;
            if (path1[5].x < path1[0].x)//ui在右侧
            {
                _t = -1;
            }
            else
            {
                _t = 1;
            }
            float _xx = Mathf.Abs(path1[5].x - path1[0].x) / 5;
            float _yy = (Mathf.Abs(path1[5].y - path1[0].y) + 1.5f + _type * 0.2f) / 4;
            if (_num <= 1)//生成一个音符的时候，就近原则生成曲线运动轨迹
            {
                path1[1] = new Vector3(path1[0].x + _xx * _t, path1[0].y - 1.5f - _type * 0.2f, 0);
                path1[2] = new Vector3(path1[0].x + _xx * _t * 2, path1[1].y + _yy, 0);
                path1[3] = new Vector3(path1[0].x + _xx * _t * 3, path1[1].y + _yy * 2, 0);
                path1[4] = new Vector3(path1[0].x + _xx * _t * 4, path1[1].y + _yy * 3, 0);
            }
            else//多个音符的时候左右等分
            {
                if (_num > 8)//终点音符
                {

                }
                else
                {
                    speed = 1.4f;
                    speed += _index * 0.1f;//音符时间间隔
                }
                int _xFen = 2;//X分成的分数
                if (_type < 10)//左侧
                {
                    _t = -1;
                }
                else//右侧
                {
                    _type -= 10;
                    _t = 1;
                    _xFen = 4;
                }
                float _rx = Random.Range(-3, 3.0f);
                float _ry = Random.Range(-2f, 3f);
                _xx = (Mathf.Abs(path1[5].x - path1[0].x) + Random.Range(0, 2.0f - _t)) / _xFen;
                _yy = (Mathf.Abs(path1[5].y - path1[0].y) + _ry) / 4;

                path1[1] = new Vector3(path1[0].x + _t * _rx, path1[0].y - _ry, 0);
                path1[2] = new Vector3(path1[0].x + _xx * _t * 1, path1[1].y + _yy, 0);
                path1[3] = new Vector3(path1[0].x + _xx * _t * 2, path1[1].y + _yy * 2, 0);
                path1[4] = new Vector3(path1[0].x + _xx * _t * 2, path1[1].y + _yy * 3, 0);

                //_xx = (Mathf.Abs(path1[5].x - path1[0].x) + 0.5f * _type) / 2;
                //_yy = (Mathf.Abs(path1[5].y - path1[0].y) + 1.5f + _type * 0.5f) / 4;
                //path1[1] = new Vector3(path1[0].x, path1[0].y - 1.5f - _type * 0.5f, 0);
                //path1[2] = new Vector3(path1[1].x + _xx * _t * 1, path1[1].y + _yy, 0);
                //path1[3] = new Vector3(path1[1].x + _xx * _t * 2, path1[1].y + _yy * 2, 0);
                //path1[4] = new Vector3(path1[1].x + _xx * _t * 2, path1[1].y + _yy * 3, 0);
            }
            tweenPath = img.transform.DOPath(path1, speed, PathType.CatmullRom).SetLoops(1, LoopType.Restart)
                .SetEase(Ease.InQuad).OnWaypointChange(OnWaypointChange);
        }

    }

    public void OnWaypointChange(int index)
    {
        if (index == path1.Length - 1)
        {
            tweenPath.Kill();

            UIManager.GetInstance().game.GetComponent<Game>().MoveRect(typeScale);
            GameObject.Destroy(gameObject);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundGetSound);
        }
    }

    public void OnWaypointChange2(int index)
    {
        if (index == 1)
        {
            tweenPath.Kill();
            //二次曲线
            path1 = new Vector3[2];
            path1[0] = img.transform.position;//起始点 
            path1[1] = targetVec;
            tweenPath = img.transform.DOPath(path1, flyTime1, PathType.CatmullRom).SetLoops(1)
                    .SetEase(Ease.InCubic).OnWaypointChange(OnWaypointChange3);

        }
    }

    public void OnWaypointChange3(int index)
    {
        if (index == 1)
        {
            tweenPath.Kill();

            UIManager.GetInstance().game.GetComponent<Game>().MoveRect(typeScale);
            GameObject.Destroy(gameObject);
            if(indexs == 0)
            {
                AudioManager.GetInstance().PlaySound(AudioManager.SoundGetSoundMost);
            }
        }
    }

}
