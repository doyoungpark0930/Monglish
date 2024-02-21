using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButtonShowingUp : MonoBehaviour  //������ �ƴ� �������� �������� �������� �ϴ� ��ũ��Ʈ
{

    [SerializeField] private GameObject TouchButton;
    [SerializeField] private GameObject CenterEffect;
    [SerializeField] private GameObject ButterFlyEffect;
    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager���� �����Ŭ���� �������� ����.


    void Awake()
    {
        TouchButton.SetActive(false); //�ʱ⼼��
        CenterEffect.SetActive(true);
        ButterFlyEffect.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {

        if (SituationManager.SituationNum == 2 && other.gameObject.name == "Player") //������ �ƴ� ���� ��Ȳ������ �������� �������Ѵ�. 
        {
            TouchButton.SetActive(true);
            CenterEffect.SetActive(false); //CenterEffect�� ��������ؼ� �����̰����ϱ� �����ε�. ��� �ѳ����� ���������� ������ ����.
            ButterFlyEffect.SetActive(true); //�����̰��� ��������Ʈ�� ���̰��Ѵ�.
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
