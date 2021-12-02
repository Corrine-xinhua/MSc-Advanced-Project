using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{

    //��ȡkinectScript����ز���
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
        //ǰ��
        if (_KS.IsForwardIng==true)
        {
            _pcs1.startMoveSwitch = true;
            _pcs1.moveForwardOrBack = false;

        }//����
        else if (_KS.IsBackIng==true)
        {
            _pcs1.startMoveSwitch = true;
            _pcs1.moveForwardOrBack = true;
        }
        else
        {
            _pcs1.startMoveSwitch = false;
        }


        //������ת ˳ʱ��
        if (_KS.IsLeftIng==true)
        {
            _CPR.startRotate = true;
            _CPR.lockWiseOrAnticrlockWise = true;
        }//������ת ��ʱ��
        else if(_KS.IsRightIng == true)
        {
            _CPR.startRotate = true;
            _CPR.lockWiseOrAnticrlockWise = false;
        }
        else
        {
            _CPR.startRotate = false;
        }

        //˫�־���
        _pcs1.handDistance = _KS.handDistance;
        danceAudioSource.pitch = Mathf.Lerp(danceAudioSource.pitch, _pcs1.handDistance,2f*Time.deltaTime);

        //���ݽǶȲ� ��Ϊ�ļ� �������ε���

        if (_KS.speedVulome < 500)
        {
            
            AudioVulome = 0f;
            //����Ƶ������ ��� ����AudioVulome���������ֵ
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
    //���������
    public AudioSource windAudioSource;
    public AudioSource danceAudioSource;
}
