using UnityEngine;
using System.Collections;
using DG.Tweening;
/// <summary>
/// 随机球飞的箭头 
/// </summary>
public class RandomJian : MonoBehaviour
{
    private float speed = 1000;

    private float delta = 1.0f;

    private Vector3 mubiao;
    // Use this for initialization
    void Start()
    {

    }

    public void InitData(Vector3 _mubiao)
    {
        mubiao = new Vector3(_mubiao.x * 100, _mubiao.y * 100, _mubiao.z * 100);
        float _boomAngle = Tools.GetForRotation(transform.localPosition, mubiao);
        transform.transform.rotation = Quaternion.Euler(0, 0, _boomAngle-90-180);

        Tweener t = transform.DOLocalMove(mubiao, 0.36f)
             .SetEase(Ease.InFlash);
        t.SetAutoKill(false);
        t.Pause();
        transform.DOPlayForward();
        t.onComplete = delegate ()
        {
            GameObject.Destroy(gameObject);
        };

    }

}

