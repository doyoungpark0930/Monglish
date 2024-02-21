using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionButtonShowingUp : MonoBehaviour
{

    [SerializeField] private GameObject MotionButton;





  
    void OnTriggerStay(Collider other)
    {

        if (SituationManager.SituationNum == 2 && other.gameObject.name == "Player") //������ �ƴ� ���� ��Ȳ������ �������� �������Ѵ�. 
        {
            MotionButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
     {
         if (SituationManager.SituationNum == 2 && other.gameObject.name == "Player")
         {
            MotionButton.SetActive(false);
        }
     }
}
