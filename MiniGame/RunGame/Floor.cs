using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{// 알파벳 나오는 곳을 배경으로 가려 바닥이 움직는 것을 가림
    float speed = 10f;
    Vector3 initPos = new Vector3(0, 0.2f, 60); //두 바닥이 첫번째 바닥이 넘은 만큼 겹치게 함

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

