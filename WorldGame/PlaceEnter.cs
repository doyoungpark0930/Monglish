using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceEnter : MonoBehaviour
{
    [SerializeField] private GameObject GameButton;
    [SerializeField] private GameObject TopPrefab;
    [SerializeField] private GameObject PortalEffect;    ParticleSystem PortalEffect_Particle;  //후자는 PortalEffect의 ParticleSystem컴포넌트

    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager에서 오디오클립을 가져오기 위함.
    AudioSource audio;
    bool EnteredToAGame=false;


    bool PrefabShowingUp_Started = false;
    public bool GetEnteredToAGame()
    {
        return EnteredToAGame;
        
    }

    void Start()
    {
        PortalEffect_Particle=PortalEffect.GetComponent<ParticleSystem>(); //게임오브젝트의 ParticleSystem컴포넌트를 가져온다
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
