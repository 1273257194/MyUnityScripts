using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI; // 

public class TimelineController : MonoBehaviour
{
    /// <summary>
    /// Playable Director组件的物件，Inspector视图下Playable Director组件的Wrap Mode为Hold
    /// </summary>
    public PlayableDirector director;

    public Slider slider;
    private void Start()
    { 
        pause(); //先暂停
        slider.onValueChanged.AddListener((x) =>
        { 
            pause(); //先暂停
            director.time = x; 
            director.Evaluate(); 
        });
    }

    void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.A))
        {
            pause(); //先暂停
            StartCoroutine("tRewind"); //倒播
        }
    }

//倒播 ================================================================
    public float speed;
    public IEnumerator tRewind()
    {
        yield return new WaitForSeconds(0.001f * Time.deltaTime);
        director.time -=speed * Time.deltaTime; //1.0f是倒帶速度
        director.Evaluate();
        if (director.time < 0f)
        {
            director.time = 0f;
            director.Evaluate();
        }
        else
        {
            StartCoroutine("tRewind");
        }

        slider.value = (float)director.time;
    }


    /// <summary>
    /// 暂停
    /// </summary>
    public void pause()
    {
        if (director != null)
        {
            //Director.Pause();
            director.playableGraph.GetRootPlayable(0).SetSpeed(0);
            //Director.Stop();//Director.Stop();也可以暂停
        }
    } //

    /// <summary>
    /// 恢复播放
    /// </summary>
    public void resume()
    {
        if (director != null)
        {
            //Director.Resume();
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }
}