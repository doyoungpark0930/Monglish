using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager에서 오디오클립을 가져오기 위함.
    [SerializeField] private SituationManager situationManager; //SituationManager에 있는 오디오를 가져오기 위함. SituationManager의 오디오가 강의 상황에서 위험상황을 말해주는 오디오인데
                                                                //벽에 부딪힐 때랑 겹치지 않게하기 위하여, 여기 오디오랑 같이 씀.
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
                CantGoOut_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.CantGoOut.Length, CantGoOut_AudioNum); //이전에 사용했던 값 제외한 다른 랜덤값
                situationManager.audio.clip = AudioManager.CantGoOut[CantGoOut_AudioNum];
                situationManager.audio.Play(); 
            }
        }
    }
}
