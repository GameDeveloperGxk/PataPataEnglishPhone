using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    private float m_LastUpdateShowTime = 0f;    //上一次更新帧率的时间;

    private float m_UpdateShowDeltaTime = 0.01f;//更新帧率的时间间隔;

    private int m_FrameUpdate = 0;//帧数;

    private float m_FPS = 0;

    public Text text1;
    public float showTime;

    void Awake()
    {
        Application.targetFrameRate = 100;
    }

    // Use this for initialization
    void Start()
    {
        m_LastUpdateShowTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //m_FrameUpdate++;
        //if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
        //{
        //    m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
        //    m_FrameUpdate = 0;
        //    m_LastUpdateShowTime = Time.realtimeSinceStartup;
        //}
        //showTime += Time.deltaTime;
        //if(showTime > 1)
        //{
        //    showTime = 0;
        //    text1.text = "Debug:FPS: " + (int)m_FPS;
        //}        
    }
    
}