using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabShowingUpScript : MonoBehaviour  //범람이 아닐 때에서만 프리팹이 나오도록 하는 스크립트
{
    [SerializeField] private GameObject Prefab; //이펙트 뜨면서 나오는 프리팹을 뜻함
    [SerializeField] private GameObject PortalEffect; ParticleSystem PortalEffect_Particle;  //후자는 PortalEffect의 ParticleSystem컴포넌트
    [SerializeField] private GameObject MotionButtonPotal;       //Prefab에 가까이가면 또 버튼이 뜨는데, 그 포탈
    [SerializeField] private GameObject CenterEffect; //포탈 중앙에있는 이펙트. 

    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager에서 오디오클립을 가져오기 위함.


    
    AudioSource audio;

    bool PrefabShowingUp_Started = false;

    void Awake()
    {
        CenterEffect.SetActive(true);
        Prefab.SetActive(false);
        MotionButtonPotal.SetActive(false);
        PortalEffect.SetActive(false);
    }
    void Start()
    {
        PortalEffect_Particle = PortalEffect.GetComponent<ParticleSystem>(); //게임오브젝트의 ParticleSystem컴포넌트를 가져온다
        audio = GetComponent<AudioSource>();

        switch (Prefab.name)
        {
          case "PinkFlowers":
              Debug.Log("PinkFLowers");
              audio.clip = AudioManager.PinkFlowersShowingUp;
              break;
          case "BlueFlowers":
              Debug.Log("BlueFLowers");
              audio.clip = AudioManager.BlueFlowersShowingUp;
              break;
          case "Mushrooms":
              Debug.Log("Mushrooms");
              audio.clip = AudioManager.MushroomsShowingUp;
              break;
          case "Watermelon":
              Debug.Log("Watermelon");
              audio.clip = AudioManager.WatermelonShowingUp;
              break;
          case "Tomato":
              Debug.Log("Tomato");
              audio.clip = AudioManager.TomatoShowingUp;
              break;
          case "Melon":
              Debug.Log("Melon");
              audio.clip = AudioManager.MelonShowingUp;
              break;
          case "Lettuce":
              Debug.Log("Lettuce");
              audio.clip = AudioManager.LettuceShowingUp;
              break;
            default:
              break;
        }
    }
    IEnumerator PrefabShowingUp()
    {
        CenterEffect.SetActive(false); //포탈 중앙에 있는 이펙트는 이제 눈에 안띄어도 되니까 해제


        audio.Play();
        PrefabShowingUp_Started = true;
        PortalEffect.SetActive(true);
        PortalEffect_Particle.Play();
        yield return new WaitForSecondsRealtime(1.6f);
        Prefab.SetActive(true);
        MotionButtonPotal.SetActive(true);
        yield return new WaitForSecondsRealtime(1.4f); //1.6+1.4인 3초가 지난 후에 PortalEffect는 없어진다.
        PortalEffect_Particle.Pause();
        PortalEffect.SetActive(false);

        yield break;
    }
    void OnTriggerStay(Collider other)
    {

        if (SituationManager.SituationNum==2 && other.gameObject.name == "Player") //범람이 아닐 때의 상황에서만 프리팹이 나오게한다. 
        {
            if (!PrefabShowingUp_Started) StartCoroutine(PrefabShowingUp()); //이미 프리팹이 형성되었다면 다시 또 다시 형성하지 않는다
        }
    }
  
    
}
