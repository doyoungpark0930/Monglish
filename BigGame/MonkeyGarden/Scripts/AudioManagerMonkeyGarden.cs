using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMonkeyGarden : MonoBehaviour
{

    public AudioClip[] DangerousSignal_ad; //���� ������ ����� ���� �����Ŭ��. //  "�������� �����Ⱑ ������", "�� �ۿ� ������ �;�", "����Ű�� ���󰡺���", "help me"
    public AudioClip[] Evacuate_ad; //���� ������ ���۵�����, ������ ����� �ʾ��� ���� ����� Ŭ��.  //"���� �ö���� �־��, ���� �����ϼ���" , "���� ������� �־��, �����ؿ�"
    public AudioClip NewGameShowingUp; //"You can go to the new Game"
    public AudioClip[] CantGoOut; //���̻� ������ ������. ���� �����̾�.

    public AudioClip PinkFlowersShowingUp; //PinkFlowers�� �ڶ󳵾��. �ɿ� �ٰ�������
    public AudioClip BlueFlowersShowingUp; //BlueFlowers�� �ڶ󳵾��. �ɿ� �ٰ�������
    public AudioClip[] FlowersMotion; //�����̰� ���� ����ġ�� �־��. �����̰� �� ������ �ð��־��. �����̰� ������ ����ֳ׿�

    public AudioClip MushroomsShowingUp; //�������� �ڶ󳵾��. �����鿡 �ٰ�������
    public AudioClip[] MushroomsMotion; //������ �������� �ϰ� �־��. �������Ķ��� �ӽ����̳׿�.

    public AudioClip WatermelonShowingUp; //������ �ڶ󳵾��. ���ڿ��� �ٰ�������
    public AudioClip[] WatermelonMotion; //����� ���� ���̿���. ������ �������ϰ��־��.

    public AudioClip TomatoShowingUp; //�丶�䰡 �ڶ󳵾��. �丶�信�� �ٰ�������
    public AudioClip[] TomatoMotion; //����� �丶�� ���̿���. �丶�並 �������ϰ��־��.

    public AudioClip MelonShowingUp; //����� �ڶ󳵾��. ��п��� �ٰ�������
    public AudioClip[] MelonMotion; //����� ��� ���̿���. ����� �������ϰ��־��.

    public AudioClip LettuceShowingUp; //���߰� �ڶ󳵾��. ���߿��� �ٰ�������
    public AudioClip[] LettuceMotion; //����� ���� ���̿���. ���߸� �������ϰ��־��.

    public AudioClip[] JumpingToGetBananaMotion; //�ٳ����� ����. �ٳ����� ���´�
    public AudioClip[] JumpingToGetAppleMotion; //����� ����. ����� ���´�
    public AudioClip[] JumpingToGetGrapeMotion; //������ ����. ������ ���´�
    public AudioClip[] JumpingToGetPeachMotion; //�����Ƹ� ����. �����ư� ���´�
    public AudioClip[] AttackToGetStrawberryMotion; //���⸦ ����. ���Ⱑ ���´�
    public AudioClip[] AttackToGetPotatoMotion; //���ڸ� ���� .���ڰ� ���´�.
    public AudioClip[] AttackToGetCarrotMotion; //����� ����. ����� ���´�.
    public AudioClip[] AttackToGetOnionMotion; //���ĸ� ����. ���İ� ���´�.

    public AudioClip Cheese; //ġ��
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

    public AudioClip ItemSound; //������ ���� �� ���� "�s~" ȿ����
    public AudioClip MotionSound; //Player�� ��� ���� �� ���� "������~" ȿ����

    public AudioClip StepSound;     //�׳� �� ���� ���� �� ���� �Ҹ�
    public AudioClip WaterStepSound;    //�� ���� ���� �� ���� �Ҹ�
    public AudioClip UnderWaterStepSound;   //���� ����� �� ���� �Ҹ�

    AudioSource audio; //���� ������ ���. �ϴ� Item��ü ���� �� �� �� ���
    public AudioSource Getaudio()
    {
        return audio;
    }


    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }



    //������ �̿��ߴ� ���� ������ ���� ��
    public int RandomValueExceptBefore(int Clip_Length, int BeforeValue) //�����Ŭ�� �迭�� ��Ҽ���, ������ ��
    {
        int newNumber;
        do
        {
            newNumber = Random.Range(0, Clip_Length);

        } while (newNumber == BeforeValue); //������ ���� ���ٸ� �ٸ��� ���ö����� �ݺ������� �� ���� �޴´�

        return newNumber;
    }
}
