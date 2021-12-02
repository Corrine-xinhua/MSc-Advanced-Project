using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParentRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //�Ƿ���ת����Ŀ���
    public bool startRotate;
    //����������ת��ָ��
    public bool lockWiseOrAnticrlockWise;

    public float speed=0f;
    public float timer;
    // Update is called once per frame
    void Update()
    {
        #region ���� ��ֵ
        if (startRotate)
        {
            if (speed != 10)
            {
                timer += Time.deltaTime;
                if (timer > 0.1f)
                {
                    speed += 2;
                    timer = 0;
                }
            }
            else
            {
                timer = 0;
            }
        }
        else
        {
            if (speed != 0)
            {
                timer += Time.deltaTime;
                if (timer > 0.1f)
                {
                    speed -= 2;
                    timer = 0;
                }
            }
            else
            {
                timer = 0;
            }
        } 
        #endregion


        if (lockWiseOrAnticrlockWise)
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward, -speed * Time.deltaTime);
        }
    }
}
