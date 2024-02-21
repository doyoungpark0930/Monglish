using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceEnter : MonoBehaviour
{
    [SerializeField] private GameObject GameButton;
    [SerializeField] private GameObject TopPrefab;
    [SerializeField] private GameObject PortalEffect;    ParticleSystem PortalEffect_Particle;  //���ڴ� PortalEffect�� ParticleSystem������Ʈ

    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager���� �����Ŭ���� �������� ����.
    AudioSource audio;
    bool EnteredToAGame=false;


    bool PrefabShowingUp_Started = false;
    public bool GetEnteredToAGame()
    {
        return EnteredToAGame;
        
    }

    void Start()
    {
        PortalEffect_Particle=PortalEffect.GetComponent<ParticleSystem>(); //���ӿ�����Ʈ�� ParticleSystem������Ʈ�� �����´�
        audio = GetComponent<AudioSource>();
        audio.clip = AudioManager.NewGameShowingUp;
    }
    IEnumerator PrefabShowingUp()
    {
        audio.Play();
        PrefabShowingUp_Started = true;
        PortalEffect.SetActive(true);
        PortalEffect_Particle.Play();
        yield return new WaitForSecondsRealtime(1.6f);
        TopPrefab.SetActive(true);
        GameButton.SetActive(true);
        yield return new WaitForSecondsRealtime(1.4f);
        PortalEffect_Particle.Pause();
        PortalEffect.SetActive(false);
        
        yield break;
    }
    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.name == "Player") 
        {
            EnteredToAGame = true;
            if(!PrefabShowingUp_Started) StartCoroutine(PrefabShowingUp());
        }
    }

}
