using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabShowingUpScript : MonoBehaviour  //������ �ƴ� �������� �������� �������� �ϴ� ��ũ��Ʈ
{
    [SerializeField] private GameObject Prefab; //����Ʈ �߸鼭 ������ �������� ����
    [SerializeField] private GameObject PortalEffect; ParticleSystem PortalEffect_Particle;  //���ڴ� PortalEffect�� ParticleSystem������Ʈ
    [SerializeField] private GameObject MotionButtonPotal;       //Prefab�� �����̰��� �� ��ư�� �ߴµ�, �� ��Ż
    [SerializeField] private GameObject CenterEffect; //��Ż �߾ӿ��ִ� ����Ʈ. 

    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager���� �����Ŭ���� �������� ����.


    
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
        PortalEffect_Particle = PortalEffect.GetComponent<ParticleSystem>(); //���ӿ�����Ʈ�� ParticleSystem������Ʈ�� �����´�
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
        CenterEffect.SetActive(false); //��Ż �߾ӿ� �ִ� ����Ʈ�� ���� ���� �ȶ� �Ǵϱ� ����


        audio.Play();
        PrefabShowingUp_Started = true;
        PortalEffect.SetActive(true);
        PortalEffect_Particle.Play();
        yield return new WaitForSecondsRealtime(1.6f);
        Prefab.SetActive(true);
        MotionButtonPotal.SetActive(true);
        yield return new WaitForSecondsRealtime(1.4f); //1.6+1.4�� 3�ʰ� ���� �Ŀ� PortalEffect�� ��������.
        PortalEffect_Particle.Pause();
        PortalEffect.SetActive(false);

        yield break;
    }
    void OnTriggerStay(Collider other)
    {

        if (SituationManager.SituationNum==2 && other.gameObject.name == "Player") //������ �ƴ� ���� ��Ȳ������ �������� �������Ѵ�. 
        {
            if (!PrefabShowingUp_Started) StartCoroutine(PrefabShowingUp()); //�̹� �������� �����Ǿ��ٸ� �ٽ� �� �ٽ� �������� �ʴ´�
        }
    }
  
    
}
