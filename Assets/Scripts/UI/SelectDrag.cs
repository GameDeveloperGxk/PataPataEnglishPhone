using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    public bool isExit=true;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        EventTriggerListener.Get(gameObject).onDown += OnClickDown;
        EventTriggerListener.Get(gameObject).onUp += OnClickUp;
        EventTriggerListener.Get(gameObject).onEnter += OnClickEnter;
        EventTriggerListener.Get(gameObject).onExit += OnClickExit;
        isExit = true;
    }

    void OnClickDown(GameObject go)
    {
        isExit = false;
    }

    void OnClickUp(GameObject go)
    {
        isExit = true;
    }

    void OnClickEnter(GameObject go)
    {
        isExit = false;
    }

    void OnClickExit(GameObject go)
    {
        isExit = true;
    }

   

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isExit)
        {
            return;
        }
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);

        float _x = pos.x;
        if (_x < -2.6)
        {
            _x = -2.6f;
        }
        if (_x > 2.6)
        {
            _x = 2.6f;
        }
        rectTransform.position = new Vector3(_x, rectTransform.position.y, rectTransform.position.z);
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().Refrush(_x*100, 1);
    }

    public void ChangePositon(float _changeX)
    {
        float _x = -2.6f+_changeX;
        if (_x < -2.6)
        {
            _x = -2.6f;
        }
        if (_x > 2.6)
        {
            _x = 2.6f;
        }
        rectTransform.position = new Vector3(_x, rectTransform.position.y, 0);
        UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().Refrush(_x * 100, 0);
    }

    public void ChangePositon2(float _changeX)
    {
        float _x = -2.6f + _changeX;
        if (_x < -2.6)
        {
            _x = -2.6f;
        }
        if (_x > 2.6)
        {
            _x = 2.6f;
        }
        rectTransform.position = new Vector3(_x, rectTransform.position.y, rectTransform.position.z);
        //UIManager.GetInstance().selectLevel.GetComponent<SelectLevel>().Refrush(rectTransform.localPosition.x, 0);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
    }

}
