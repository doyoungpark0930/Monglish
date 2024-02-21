using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionButtonShowingUp : MonoBehaviour
{

    [SerializeField] private GameObject MotionButton;





  
    void OnTriggerStay(Collider other)
    {

        if (SituationManager.SituationNum == 2 && other.gameObject.name == "Player") //범람이 아닐 때의 상황에서만 프리팹이 나오게한다. 
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
