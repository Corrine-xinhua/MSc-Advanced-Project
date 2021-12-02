using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{

    //获取kinectScript的相关参数
    public KinectScript _KS;
    public CameraParentRotate _CPR;
    public ParticleCtlScript _pcs1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //前进
        if (_KS.IsForwardIng==true)
        {
            _pcs1.startMoveSwitch = true;
            _pcs1.moveForwardOrBack = false;

        }//后退
        else if (_KS.IsBackIng==true)
        {
            _pcs1.startMoveSwitch = true;
            _pcs1.moveForwardOrBack = true;
        }
        else
        {
            _pcs1.startMoveSwitch = false;
        }


        //画面左转 顺时针
        if (_KS.IsLeftIng==true)
        {
            _CPR.startRotate = true;
            _CPR.lockWiseOrAnticrlockWise = true;
        }//画面右转 逆时针
        else if(_KS.IsRightIng == true)
        {
            _CPR.startRotate = true;
            _CPR.lockWiseOrAnticrlockWise = false;
        }
        else
        {
            _CPR.startRotate = false;
        }

        //双手距离
        _pcs1.handDistance = _KS.handDistance;
        danceAudioSource.pitch = Mathf.Lerp(danceAudioSource.pitch, _pcs1.handDistance,2f*Time.deltaTime);

        //根据角度差 分为四级 音量依次递增

        if (_KS.speedVulome < 500)
        {
            
            AudioVulome = 0f;
            //让音频的音量 变成 上面AudioVulome这个参数的值
            windAudioSource.volume = Mathf.Lerp(windAudioSource.volume, AudioVulome, 10f * Time.deltaTime);
        }
        else if (_KS.speedVulome < 1000 && _KS.speedVulome > 500)
        {
           
            AudioVulome = 0.3f;
            windAudioSource.volume = Mathf.Lerp(windAudioSource.volume, AudioVulome, 10f * Time.deltaTime);
        }
        else if (_KS.speedVulome < 1500 && _KS.speedVulome > 1000)
        {
           
            AudioVulome = 0.6f;
            windAudioSource.volume = Mathf.Lerp(windAudioSource.volume, AudioVulome, 10f * Time.deltaTime);
        }
        else if (_KS.speedVulome > 1500)
        {
            AudioVulome = 1f;
            windAudioSource.volume = Mathf.Lerp(windAudioSource.volume, AudioVulome, 10f * Time.deltaTime);
        }

    }
    public float AudioVulome;
    //定义的音乐
    public AudioSource windAudioSource;
    public AudioSource danceAudioSource;
}
