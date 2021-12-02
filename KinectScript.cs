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

    //�������ĸ�bool ����ֵ
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
        #region �ж�����������

        //print(MyPos);
        if (Mathf.Abs((MyPos.x - oldX)) > 0.2f)
        {
            LeftRightTimer = 0;
            if ((MyPos.x - oldX) > 0)
            {
                IsRightIng = true;
                print("����");
                //  _text.text = "��";
                oldX = MyPos.x;
            }
            if ((MyPos.x - oldX) < 0)
            {
                IsLeftIng = true;
                print("����");
                //  _text.text = "��";
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


        #region �ж���ǰ�������
        if (Mathf.Abs((MyPos.z - oldZ)) > 0.1f)
        {
            ForwaradBackTimer = 0;
            if ((MyPos.z - oldZ) > 0)
            {
                IsBackIng = true;
                print("���");
                _text.text = "��";
                oldZ = MyPos.z;
            }
            if ((MyPos.z - oldZ) < 0)
            {
                IsForwardIng = true;
                print("��ǰ");
                _text.text = "ǰ";
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

        #region �����Ŀ���Ƶ�� �ǶȲ�
        //��ȡ������ת����
        Quaternion Drictions = manager.GetUserOrientation(manager.GetPrimaryUserID(), true);
        //��ȡ������ת�Ƕ�
        float angle = Drictions.eulerAngles.y;
        //print(_Center.transform.rotation.eulerAngles.y);

        //�õ���ת�Ƕ�ֵ ��ת<0 ��ת>0
        RotateAngle = CheckAngle(angle);

        //���� 1000����ʱ���� ��ת�ǶȲ�
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
        //                      ��ת�ĽǶ�
        speedVulome = Mathf.Abs(endAngle - startAngle) * 100;

        #endregion

        #region ��ȡ˫�־���ֵ
        leftHandPos = manager.GetJointKinectPosition(manager.GetPrimaryUserID(), 7);
        rightHandPos = manager.GetJointKinectPosition(manager.GetPrimaryUserID(), 11);
        handDistance = Vector3.Distance(leftHandPos, rightHandPos);

        #endregion
        // _text.text = handDistance.ToString();
    }
    //����˫��λ����Ϣ
    public Vector3 leftHandPos;
    public Vector3 rightHandPos;
    //����˫�־���
    public float handDistance = 0f;
    //������ת�Ƕ�
    public float RotateAngle;
    //����ʱ�� ���׼�ʱ��
    public float timer = 0;
    //��ʼ�Ƕ�ֵ
    public float startAngle = 0;
    //�����Ƕ�ֵ
    public float endAngle = 0;
    public float speedVulome = 0;

    /// <summary>
    /// ����Ƕ�����  ʹ�Ƕ� ��Ϊ���� �Ӷ������������ת��
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float CheckAngle(float value) // ������180�Ƚǽ����Ը�����ʽ��� {
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
