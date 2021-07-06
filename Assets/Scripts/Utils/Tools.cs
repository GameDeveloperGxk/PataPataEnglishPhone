using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

/// <summary>
/// 工具类
/// </summary>
public class Tools {

    public static void LoadPlayerPrefsArray(int[] _array, string _arrName)
    {
        int _length = _array.Length;
        for (int i = 0; i < _length; i++)
        {
            _array[i] = PlayerPrefs.GetInt(_arrName + i, _array[i]);
        }
    }

    public static void SavePlayerPrefsArray(int[] _array, string _arrName)
    {
        int _length = _array.Length;
        for (int i = 0; i < _length; i++)
        {
            PlayerPrefs.SetInt(_arrName + i, _array[i]);
        }
    }

    /// <summary>
    /// 缩放粒子
    /// </summary>
    /// <param name="gameObj">粒子节点</param>
    /// <param name="scale">绽放系数</param>
    public static void ScaleParticleSystem(GameObject gameObj, float scale)
    {
        var hasParticleObj = false;
        var particles = gameObj.GetComponentsInChildren<ParticleSystem>(true);
        var max = particles.Length;
        for (int idx = 0; idx < max; idx++)
        {
            var particle = particles[idx];
            if (particle == null) continue;
            hasParticleObj = true;
            particle.startSize *= scale;
            particle.startSpeed *= scale;
            particle.startRotation *= scale;
            particle.transform.localScale *= scale;
        }
        if (hasParticleObj)
        {
            gameObj.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    /// <summary>
    /// 移除对象的全部子对象
    /// </summary>
    /// <param name="parent"></param>
    public static void RemoveAllChildren(GameObject parent)
    {
        Transform transform;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            transform = parent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }

    /// <summary>
    /// 播放animation动画
    /// </summary>
    public static void PlayAnimation(Animation ani,string stateName)
    {
        AnimationState state = ani[stateName];
        state.time = 0;
        ani.Stop();
        ani.Play(stateName ,PlayMode.StopAll);
    }

    /// <summary>
    /// 暂停播放animation动画
    /// </summary>
    public static void StopAnimation(Animation ani, string stateName)
    {
        AnimationState state = ani[stateName];
        state.time = 0;
        ani.Stop();
    }

    public static void PlayParticleSystem(GameObject par)
    {
        par.GetComponent<ParticleSystem>().Stop();
        par.GetComponent<ParticleSystem>().Play();
    }

    public static void StopParticleSystem(GameObject par)
    {
        par.GetComponent<ParticleSystem>().Stop();
    }


    /// <summary>
    /// 存储字符串为文本
    /// </summary>
    /// <param name="html"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string writTxt(string html, string file)
    {
        FileStream fileStream = new FileStream(System.Environment.CurrentDirectory + "\\" + file+".txt", FileMode.Append);
        StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
        streamWriter.Write(html + "\r\n");
        streamWriter.Flush();
        streamWriter.Close();
        fileStream.Close();
        return "ture";
    }

    /// <summary>
    /// 获得2D平面内两个点的角度
    /// </summary>
    /// <returns></returns>
    public static float GetForRotation(Vector3 vecA, Vector3 vecB)
    {
        float targetAngle = 0;
        Vector3 targetVec;
        float AtanTarget = 0;
        
        //Vector3 direction = vecB - vecA;                                    ///< 终点减去起点（AB方向与X轴的夹角）
        Vector3 direction = vecA - vecB;                                  ///< （BA方向与X轴的夹角）
        float angle = Vector3.Angle(direction, Vector3.right);              ///< 计算旋转角度
        direction = Vector3.Normalize(direction);                           ///< 向量规范化
        float dot = Vector3.Dot(direction, Vector3.up);                  ///< 判断是否Vector3.right在同一方向
        if (dot < 0)
            angle = 360 - angle;

        targetAngle = angle;
        targetVec = new Vector3(0, 0, angle);
        //< 补充点1： 通过Atan2与方向向量的两条边可以计算出转向的角度，通过计算结果可以看到targetAngle与-AtanTarget相加正好是360°，即二者都指向同一方向。具体使用场景需要根据具体需求分析。
        AtanTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.LogWarning("vecA：" + vecA.ToString() + ", vecB：" + vecB.ToString() + ", targetAngle: " + targetAngle.ToString() + ", AtanTarget: " + AtanTarget.ToString());
        //arrow.GetComponent<Transform>().Rotate(0, 0, angle);

        //< 补充点2： 使用欧拉角来控制物体的旋转
        //arrow.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, angle);
        return targetAngle;
    }

}
