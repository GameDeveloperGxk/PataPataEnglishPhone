using UnityEngine;
using System.Collections;

public class PanelCloud : MonoBehaviour
{
    public Animation ani;
    private float time;
    private bool isStart;

    // Use this for initialization
    void Start()
    {
        Tools.PlayAnimation(ani, "JiaZaiZhong-JingRu");
        isStart = false;
    }
    

    public void Play0(float _t)
    {
        if(isStart == false)
        {
            isStart = true;
            ani.transform.localPosition = new Vector3(0, 0, 0);
            time = _t;
            AnimationState state = ani["JiaZaiZhong-JingRu"];
            state.time = 0;
            ani.Stop();
            ani.Play("JiaZaiZhong-JingRu", PlayMode.StopAll);
            Invoke("Play1", 1f);
        }
        
    }

    private void Play1()
    {
        ani.transform.localPosition = new Vector3(0, 0, 0);
        ani.Stop();
        ani.Play("JiaZaiZhong-ChiXu", PlayMode.StopAll);
        Invoke("Play2", time);
    }
    private void Play2()
    {
        //ani.transform.localPosition = new Vector3(0, 0, 0);
        //AnimationState state = ani["JiaZaiZhong-Chu"];
        //state.time = 0;
        ani.Stop();
        ani.Play("JiaZaiZhong-Chu", PlayMode.StopAll);
        Invoke("Hide", 0.5f);
    }

    public void Hide()
    {
        isStart = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
