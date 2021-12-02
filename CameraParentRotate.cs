using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParentRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //是否旋转相机的开关
    public bool startRotate;
    //向左向右旋转的指向
    public bool lockWiseOrAnticrlockWise;

    public float speed=0f;
    public float timer;
    // Update is called once per frame
    void Update()
    {
        #region 缓冲 插值
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
