using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CubeScript : MonoBehaviour
{
   public bool isrotatorA;
   public bool isrotatorD;

    // Start is called before the first frame update
    void Start()
    {



        // Use this for initialization

        isrotatorA = true;
        isrotatorD = true;


    }
    float oldX = 0;
    float oldZ = 0;
    //float oldX;
    public Text _text;


    public bool IsForwardIng = false;
    public bool IsBackIng = false;
    public bool IsLeftIng = false;
    public bool IsRightIng = false;

    public float handDistance = 0f;


    public float ForwaradBackTimer = 0;
    public float LeftRightTimer = 0;

    public GameObject _Center;

    // Update is called once per frame
    void Update()
    {

        Vector3 MyPos = transform.position;
        //print(MyPos);
        if (Mathf.Abs((MyPos.x - oldX)) > 0.2f)
        {
            LeftRightTimer = 0;
            if ((MyPos.x - oldX) > 0)
            {
                IsRightIng = true;
                IsLeftIng = false;
                print("向右");
                //  _text.text = "右";
                oldX = MyPos.x;
            }
            if ((MyPos.x - oldX) < 0)
            {

                IsRightIng = false;
                IsLeftIng = true;
                print("向左");
                //  _text.text = "左";
                oldX = MyPos.x;
            }
        }
        else
        {

            LeftRightTimer += Time.deltaTime;
            if (LeftRightTimer > 1f)
            {
                IsRightIng = false;
                IsLeftIng = false;
                LeftRightTimer = 0;
            }
        }


        if (Mathf.Abs((MyPos.z - oldZ)) > 0.1f)
        {
            ForwaradBackTimer = 0;
            if ((MyPos.z - oldZ) > 0)
            {
                IsBackIng = true;
                IsForwardIng = false;
                print("向后");
                _text.text = "后";
                oldZ = MyPos.z;
            }
            if ((MyPos.z - oldZ) < 0)
            {
                IsBackIng = false;
                IsForwardIng = true;
                print("向前");
                _text.text = "前";
                oldZ = MyPos.z;
            }
        }
        else
        {
            ForwaradBackTimer += Time.deltaTime;
            if (ForwaradBackTimer > 1f)
            {
                _text.text = "";
                IsBackIng = false;
                IsForwardIng = false;
                ForwaradBackTimer = 0;
            }

        }




    }


}
