using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTile : MonoBehaviour
{// ���ĺ� ������ ���� ������� ���� �ٴ��� ������ ���� ����
    float speed =10f;
    Vector3 initPos = new Vector3(0, 0.2f, 209.1f); //�� ������ ������
    //Vector3 initPos = new Vector3(0, 0.2f, 199.30f); //�� ������ ������

    /*IEnumerator Start()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
    }*/
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale >0.5f)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
            if (transform.position.z <= -23.5f)
            {
                //Time.timeScale = 0f;
                //Debug.Log(transform.position);
                transform.position = initPos;
            }
        }
        else if(Time.timeScale > 0.3f)
        {
            transform.Translate(0, 0, -speed * 0.95f * Time.deltaTime);
            if (transform.position.z <= -23.5f)
            {
                //Time.timeScale = 0f;
                //Debug.Log(transform.position);
                transform.position = initPos;
            }
        }
        else if(Time.timeScale >0.1f)
        {
            transform.Translate(0, 0, -speed *0.8f * Time.deltaTime);
            if (transform.position.z <= -23.5f)
            {
                //Time.timeScale = 0f;
                //Debug.Log(transform.position);
                transform.position = initPos;
            }
        }


    }
    /*if (Camera.main.ScreenToWorldPoint(touch.position).x < 0)
    {
        Debug.Log("LL");
        Player.Lposi();
    }*/

    /*        if (Camera.main.ScreenToWorldPoint(touch.position).x > 0)
                Player.Rposi();*/
}

