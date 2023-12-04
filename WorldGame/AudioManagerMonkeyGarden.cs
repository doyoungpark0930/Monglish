using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMonkeyGarden : MonoBehaviour
{

    public AudioClip[] DangerousSignal_ad; //강에 완전히 잠겼을 때의 오디오클립. //  "물에서는 숨쉬기가 힘들어요", "물 밖에 나가고 싶어", "방향키를 따라가보자", "help me"
    public AudioClip[] Evacuate_ad; //강의 범람이 시작됐지만, 완전히 잠기지 않았을 때의 오디오 클립.  //"강이 올라오고 있어요, 위로 대피하세요" , "물이 깊어지고 있어요, 위험해요"
    public AudioClip NewGameShowingUp; //"You can go to the new Game"
    public AudioClip[] CantGoOut; //더이상 나가면 위험해. 여긴 절벽이야.

    public AudioClip PinkFlowersShowingUp; //PinkFlowers가 자라났어요. 꽃에 다가가봐요
    public AudioClip BlueFlowersShowingUp; //BlueFlowers가 자라났어요. 꽃에 다가가봐요
    public AudioClip[] FlowersMotion; //원숭이가 꽃을 파헤치고 있어요. 원숭이가 꽃 냄새를 맡고있어요. 원숭이가 꽃으로 놀고있네요

    public AudioClip MushroomsShowingUp; //버섯들이 자라났어요. 버섯들에 다가가봐요
    public AudioClip[] MushroomsMotion; //버섯을 먹으려고 하고 있어요. 빨간색파란색 머쉬룸이네요.

    public AudioClip WatermelonShowingUp; //수박이 자라났어요. 수박에게 다가가봐요
    public AudioClip[] WatermelonMotion; //여기는 수박 밭이에요. 수박을 먹으려하고있어요.

    public AudioClip TomatoShowingUp; //토마토가 자라났어요. 토마토에게 다가가봐요
    public AudioClip[] TomatoMotion; //여기는 토마토 밭이에요. 토마토를 먹으려하고있어요.

    public AudioClip MelonShowingUp; //멜론이 자라났어요. 멜론에게 다가가봐요
    public AudioClip[] MelonMotion; //여기는 멜론 밭이에요. 멜론을 먹으려하고있어요.

    public AudioClip LettuceShowingUp; //상추가 자라났어요. 상추에게 다가가봐요
    public AudioClip[] LettuceMotion; //여기는 상추 밭이에요. 상추를 먹으려하고있어요.

    public AudioClip[] JumpingToGetBananaMotion; //바나나를 얻어보자. 바나나가 나온다
    public AudioClip[] JumpingToGetAppleMotion; //사과를 얻어보자. 사과가 나온다
    public AudioClip[] JumpingToGetGrapeMotion; //포도를 얻어보자. 포도가 나온다
    public AudioClip[] JumpingToGetPeachMotion; //복숭아를 얻어보자. 복숭아가 나온다
    public AudioClip[] AttackToGetStrawberryMotion; //딸기를 얻어보자. 딸기가 나온다
    public AudioClip[] AttackToGetPotatoMotion; //감자를 얻어보자 .감자가 나온다.
    public AudioClip[] AttackToGetCarrotMotion; //당근을 얻어보자. 당근이 나온다.
    public AudioClip[] AttackToGetOnionMotion; //양파를 얻어보자. 양파가 나온다.

    public AudioClip Cheese; //치즈
    public AudioClip Shrimp;
    public AudioClip Meat;
    public AudioClip Burger;
    public AudioClip Toast;
    public AudioClip Pineapple;
    public AudioClip FrenchFries;
    public AudioClip Pepper;
    public AudioClip Drink;
    public AudioClip Omelette;
    public AudioClip Sausage;
    public AudioClip Sushi;
    public AudioClip Rice;
    public AudioClip Candy;
    public AudioClip Pizza;
    public AudioClip Melon;
    public AudioClip Icecream;
    public AudioClip Hotdog;
    public AudioClip Doughnut;
    public AudioClip Cookie;
    public AudioClip Cake;
    public AudioClip Whale;

    public AudioClip ItemSound; //아이템 먹을 때 나는 "뾱~" 효과음
    public AudioClip MotionSound; //Player가 모션 취할 때 나는 "샤랄라~" 효과음

    public AudioClip StepSound;     //그냥 땅 위를 걸을 때 나는 소리
    public AudioClip WaterStepSound;    //물 위를 걸을 때 나는 소리
    public AudioClip UnderWaterStepSound;   //물에 잠겼을 때 나는 소리

    AudioSource audio; //제한 적으로 사용. 일단 Item객체 먹을 때 날 떄 사용
    public AudioSource Getaudio()
    {
        return audio;
    }


    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }



    //이전에 이용했던 값을 제외한 랜덤 값
    public int RandomValueExceptBefore(int Clip_Length, int BeforeValue) //오디오클립 배열의 요소수와, 이전의 값
    {
        int newNumber;
        do
        {
            newNumber = Random.Range(0, Clip_Length);

        } while (newNumber == BeforeValue); //이전의 값과 같다면 다른게 나올때까지 반복돌리고 새 값을 받는다

        return newNumber;
    }
}
