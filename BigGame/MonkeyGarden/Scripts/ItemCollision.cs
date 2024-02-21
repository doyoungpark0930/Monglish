using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] AudioManagerMonkeyGarden AudioManager;
    [SerializeField] SliderMiniScene SliderMiniScene;


    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.fixedDeltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            switch (gameObject.name)
            {
                case "Cheese":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);  //������ ���� �� ȿ����. �Ҹ�ũ�� 0.4f
                    AudioManager.Getaudio().clip = AudioManager.Cheese;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Shrimp":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Shrimp;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Meat":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Meat;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Burger":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Burger;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Toast":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Toast;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Pineapple":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Pineapple;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "FrenchFries":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.FrenchFries;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Pepper":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Pepper;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Drink":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Drink;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Omelette":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Omelette;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Sausage":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Sausage;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Sushi":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Sushi;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Rice":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Rice;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Candy":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Candy;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Pizza":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Pizza;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Melon":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Melon;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Icecream":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Icecream;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Hotdog":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Hotdog;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Doughnut":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Doughnut;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Cookie":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Cookie;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Cake":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Cake;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                case "Whale":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Whale;
                    AudioManager.Getaudio().Play();     //�� ���⼭ ������� �÷����ϳĸ�, �� ��ü�� �÷��̽�Ű��,�� ��ü�� ����� �� �ٷ� ������ҽ��� ������� ����
                    break;
                default:
                    break;
            }
            if (SituationManager.SituationNum == 2) //���� ��Ȳ�� �ƴ� ��
            {
                SliderMiniScene.StartCoroutine(SliderMiniScene.ContinuousUp(0.15f)); //���⼭ StartCoroutine�ع����� gameObject�� �ٷ� �������鼭 �ڷ�ƾ�� ���������� SliderMiniscene�� StartCoroutine�� ��
                gameObject.SetActive(false);
            }
            else if (SituationManager.SituationNum == 1) //���� ��Ȳ�� ��
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("��Ȳ����");
            }
        }
    }
}
