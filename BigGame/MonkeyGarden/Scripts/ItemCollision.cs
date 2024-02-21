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
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);  //아이템 먹을 때 효과음. 소리크기 0.4f
                    AudioManager.Getaudio().clip = AudioManager.Cheese;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Shrimp":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Shrimp;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Meat":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Meat;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Burger":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Burger;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Toast":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Toast;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Pineapple":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Pineapple;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "FrenchFries":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.FrenchFries;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Pepper":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Pepper;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Drink":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Drink;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Omelette":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Omelette;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Sausage":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Sausage;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Sushi":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Sushi;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Rice":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Rice;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Candy":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Candy;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Pizza":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Pizza;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Melon":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Melon;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Icecream":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Icecream;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Hotdog":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Hotdog;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Doughnut":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Doughnut;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Cookie":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Cookie;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Cake":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Cake;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                case "Whale":
                    AudioManager.Getaudio().PlayOneShot(AudioManager.ItemSound, 0.3f);
                    AudioManager.Getaudio().clip = AudioManager.Whale;
                    AudioManager.Getaudio().Play();     //왜 여기서 오디오를 플레이하냐면, 이 객체에 플레이시키면,이 객체가 사라질 때 바로 오디오소스도 사라지기 때문
                    break;
                default:
                    break;
            }
            if (SituationManager.SituationNum == 2) //강의 상황이 아닐 때
            {
                SliderMiniScene.StartCoroutine(SliderMiniScene.ContinuousUp(0.15f)); //여기서 StartCoroutine해버리면 gameObject가 바로 없어지면서 코루틴도 중지됨으로 SliderMiniscene의 StartCoroutine을 씀
                gameObject.SetActive(false);
            }
            else if (SituationManager.SituationNum == 1) //강의 상황일 때
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("상황오류");
            }
        }
    }
}
