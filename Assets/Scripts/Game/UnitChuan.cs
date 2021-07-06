using UnityEngine;
using System.Collections;

public class UnitChuan : MonoBehaviour {


    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    
    public GameObject jin11;
    public GameObject jin22;
    public GameObject jin33;
    public GameObject jin44;

    private float destroyTime;
    void Start () {
	
	}

    public void InitData(int _id)
    {
        if(_id >= 100 && _id < 199)
        {
            _id = 1;
        }else if (_id >= 200 && _id < 299)
        {
            _id = 2;
        }
        else if (_id >= 300 && _id < 399)
        {
            _id = 3;
        }
        else if (_id >=400 && _id < 599)
        {
            _id = 4;
        }
        switch (_id)
        {
            case 1:
                obj1.SetActive(true);
                jin11.SetActive(true);
                jin11.GetComponent<ParticleSystem>().Stop();
                jin11.GetComponent<ParticleSystem>().Play();
                break;
            case 2:
                obj2.SetActive(true);
                jin11.GetComponent<ParticleSystem>().Stop();
                jin11.GetComponent<ParticleSystem>().Play();
                break;
            case 3:
                obj3.SetActive(true);
                jin11.GetComponent<ParticleSystem>().Stop();
                jin11.GetComponent<ParticleSystem>().Play();
                break;
            case 4:
                obj4.SetActive(true);
                jin11.GetComponent<ParticleSystem>().Stop();
                jin11.GetComponent<ParticleSystem>().Play();
                break;
        }

        destroyTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (UIManager.GetInstance().game.GetComponent<Game>().currentGameState == Game.GameState.GamePause)
        {
            return;
        }
        destroyTime += Time.deltaTime;
        if (destroyTime > 2.0f)
        {
            Destroy(gameObject);
        }
	}
}
