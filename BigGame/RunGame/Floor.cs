using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{// ���ĺ� ������ ���� ������� ���� �ٴ��� ������ ���� ����
    float speed = 10f;
    Vector3 initPos = new Vector3(0, 0.2f, 60); //�� �ٴ��� ù��° �ٴ��� ���� ��ŭ ��ġ�� ��

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(0, 0, -speed * Time.deltaTime);
        /*Touch touch = Input.GetTouch(0);*/
        if(transform.position.z <= -14.5f) 
            transform.position = initPos;
        }
        /*if (Camera.main.ScreenToWorldPoint(touch.position).x < 0)
        {
            Debug.Log("LL");
            Player.Lposi();
        }*/
            
/*        if (Camera.main.ScreenToWorldPoint(touch.position).x > 0)
            Player.Rposi();*/
}

