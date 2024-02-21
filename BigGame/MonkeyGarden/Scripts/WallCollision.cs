using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager���� �����Ŭ���� �������� ����.
    [SerializeField] private SituationManager situationManager; //SituationManager�� �ִ� ������� �������� ����. SituationManager�� ������� ���� ��Ȳ���� �����Ȳ�� �����ִ� ������ε�
                                                                //���� �ε��� ���� ��ġ�� �ʰ��ϱ� ���Ͽ�, ���� ������� ���� ��.
    //AudioSource audio;

    void Start()
    {      
        //audio = GetComponent<AudioSource>();
    }

    int CantGoOut_AudioNum;
    void OnCollisionEnter(Collision other)
    {
              
        if (other.gameObject.name == "Player")
        {
            if (!situationManager.audio.isPlaying)
            {
                CantGoOut_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.CantGoOut.Length, CantGoOut_AudioNum); //������ ����ߴ� �� ������ �ٸ� ������
                situationManager.audio.clip = AudioManager.CantGoOut[CantGoOut_AudioNum];
                situationManager.audio.Play(); 
            }
        }
    }
}
