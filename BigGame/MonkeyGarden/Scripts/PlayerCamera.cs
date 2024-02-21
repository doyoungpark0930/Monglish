using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player; //target���� ���� player
    Vector3 offset;      //cam ���Ϳ��� player ���͸� �� ��

    void Start()
    {
        offset = transform.position - player.position; //start�� �� offset�� ������ 
        transform.position = player.position + offset;

    }
    void Update()
    {
        transform.position = player.position + offset;
    }
}
