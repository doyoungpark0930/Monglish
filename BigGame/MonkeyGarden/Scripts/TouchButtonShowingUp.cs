using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButtonShowingUp : MonoBehaviour  //범람이 아닐 때에서만 프리팹이 나오도록 하는 스크립트
{

    [SerializeField] private GameObject TouchButton;
    [SerializeField] private GameObject CenterEffect;
    [SerializeField] private GameObject ButterFlyEffect;
    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager에서 오디오클립을 가져오기 위함.


    void Awake()
    {
        TouchButton.SetActive(false); //초기세팅
        CenterEffect.SetActive(true);
        ButterFlyEffect.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {

        if (SituationManager.SituationNum == 2 && other.gameObject.name == "Player") //범람이 아닐 때의 상황에서만 프리팹이 나오게한다. 
        {
            TouchButton.SetActive(true);
            CenterEffect.SetActive(false); //CenterEffect는 눈에띄게해서 가까이가게하기 위함인데. 계속 켜놓으면 복잡해지기 때문에 끈다.
            ButterFlyEffect.SetActive(true); //가까이가면 나비이펙트들 보이게한다.
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (SituationManager.SituationNum == 2 && other.gameObject.name == "Player")
        {
            TouchButton.SetActive(false);
        }
    }


}
