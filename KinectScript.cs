using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KinectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    float oldX = 0;
    float oldZ = 0;
    //float oldX;
    public Text _text;

    //定义了四个bool 类型值
    public bool IsForwardIng = false;
    public bool IsBackIng = false;
    public bool IsLeftIng = false;
    public bool IsRightIng = false;



    public float ForwaradBackTimer = 0;
    public float LeftRightTimer = 0;
    

    // Update is called once per frame
    void Update()
    {
        KinectManager manager = KinectManager.Instance;
        Vector3 MyPos = manager.GetUserPosition(manager.GetPrimaryUserID());
        #region 判断向左还是向右

        //print(MyPos);
        if (Mathf.Abs((MyPos.x - oldX)) > 0.2f)
        {
            LeftRightTimer = 0;
            if ((MyPos.x - oldX) > 0)
            {
                IsRightIng = true;
                print("向右");
                //  _text.text = "右";
                oldX = MyPos.x;
            }
            if ((MyPos.x - oldX) < 0)
            {
                IsLeftIng = true;
                print("向左");
                //  _text.text = "左";
                oldX = MyPos.x;
            }
        }
        else
        {

            LeftRightTimer += Time.deltaTime;
            if (LeftRightTimer > 0.5f)
            {
                IsRightIng = false;
                IsLeftIng = false;
                LeftRightTimer = 0;
            }
        }
        #endregion


        #region 判断向前还是向后
        if (Mathf.Abs((MyPos.z - oldZ)) > 0.1f)
        {
            ForwaradBackTimer = 0;
            if ((MyPos.z - oldZ) > 0)
            {
                IsBackIng = true;
                print("向后");
                _text.text = "后";
                oldZ = MyPos.z;
            }
            if ((MyPos.z - oldZ) < 0)
            {
                IsForwardIng = true;
                print("向前");
                _text.text = "前";
                oldZ = MyPos.z;
            }
        }
        else
        {
            ForwaradBackTimer += Time.deltaTime;
            if (ForwaradBackTimer > 0.5f)
            {
                _text.text = "";
                IsBackIng = false;
                IsForwardIng = false;
                ForwaradBackTimer = 0;
            }

        }
        #endregion

        #region 声音的控制频率 角度差
        //获取人体旋转方向
        Quaternion Drictions = manager.GetUserOrientation(manager.GetPrimaryUserID(), true);
        //获取人体旋转角度
        float angle = Drictions.eulerAngles.y;
        //print(_Center.transform.rotation.eulerAngles.y);

        //得到旋转角度值 左转<0 右转>0
        RotateAngle = CheckAngle(angle);

        //计算 1000毫秒时间内 旋转角度差
        timer += Time.deltaTime;
        if (timer < 0.1f)
        {
            startAngle = RotateAngle;
        }
        if (timer > 0.2f)
        {
            endAngle = RotateAngle;


            timer = 0;
        }
        //                      旋转的角度
        speedVulome = Mathf.Abs(endAngle - startAngle) * 100;

        #endregion

        #region 获取双手距离值
        leftHandPos = manager.GetJointKinectPosition(manager.GetPrimaryUserID(), 7);
        rightHandPos = manager.GetJointKinectPosition(manager.GetPrimaryUserID(), 11);
        handDistance = Vector3.Distance(leftHandPos, rightHandPos);

        #endregion
        // _text.text = handDistance.ToString();
    }
    //定义双手位置信息
    public Vector3 leftHandPos;
    public Vector3 rightHandPos;
    //定义双手距离
    public float handDistance = 0f;
    //定义旋转角度
    public float RotateAngle;
    //定义时间 简易计时器
    public float timer = 0;
    //初始角度值
    public float startAngle = 0;
    //结束角度值
    public float endAngle = 0;
    public float speedVulome = 0;

    /// <summary>
    /// 处理角度数据  使角度 分为正负 从而分向左或向右转动
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float CheckAngle(float value) // 将大于180度角进行以负数形式输出 {
    {
        float angle = value - 180;

        if (angle > 0)
        {
            return angle - 180;
        }

        if (value == 0)
        {
            return 0;
        }

        return angle + 180;
    }
}
