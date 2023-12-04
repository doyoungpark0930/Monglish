using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager에서 오디오클립을 가져오기 위함

    [SerializeField] private GameObject BananaParticle; //바나나가 나오는 파티클 이펙트
    [SerializeField] private GameObject AppleParticle; //사과가 나오는 파티클 이펙트
    [SerializeField] private GameObject GrapeParticle; //포도가 나오는 파티클 이펙트
    [SerializeField] private GameObject PeachParticle; //복숭아가 나오는 파티클 이펙트
    [SerializeField] private GameObject StrawberryParticle; //딸기가 나오는 파티클 이펙트
    [SerializeField] private GameObject OnionParticle; //양파가 나오는 파티클 이펙트
    [SerializeField] private GameObject CarrotParticle; //당근이 나오는 파티클 이펙트
    [SerializeField] private GameObject PotatoParticle; //감자가 나오는 파티클 이펙트


    [SerializeField] SliderMiniScene SliderMiniScene; //모션한번 취할 때마다 슬라이더값 증가


    


    Animator monkeyAnimator;
    AudioSource audio;

    void Awake()
    {
       


        monkeyAnimator = Player.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        BananaParticle.SetActive(false);    //게임시작시에는 불필요한 ParticleEffect 끈다
        AppleParticle.SetActive(false); 
        GrapeParticle.SetActive(false); 
         PeachParticle.SetActive(false); 
        StrawberryParticle.SetActive(false); 
        OnionParticle.SetActive(false); 
        CarrotParticle.SetActive(false); 
        PotatoParticle.SetActive(false);
    }


    int FlowersMotion_AudioNum;

    public void FlowersMotion() //PinkFlowers에 다가가서 터치 버튼을 누른다면 밑에 코드 실행
    {
        Debug.Log("원숭이가 꽃을 파헤치고 있어요. 원숭이가 꽃 냄새를 맡고있어요. 원숭이가 꽃으로 놀고있네요");
        if (!audio.isPlaying && !monkeyAnimator.GetBool("Motion")) //오디오가 끝나고, 동작이 끝나야지 오디오, 동작 그리고 Slider채움도 다시 시작할 수 있다
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            FlowersMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.FlowersMotion.Length, FlowersMotion_AudioNum);
            audio.clip = AudioManager.FlowersMotion[FlowersMotion_AudioNum];
            audio.Play();
            StartCoroutine(EatingMotion());
        }
           
    }


    int Mushrooms_AudioNum;
    public void MushroomsMotion() //PinkFlowers에 다가가서 터치 버튼을 누른다면 밑에 코드 실행
    {
        Debug.Log("버섯을 먹으려고 하고 있어요. 빨간색파란색 머쉬룸이네요");
        if (!audio.isPlaying && !monkeyAnimator.GetBool("Motion"))
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            Mushrooms_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.MushroomsMotion.Length, Mushrooms_AudioNum);
            audio.clip = AudioManager.MushroomsMotion[Mushrooms_AudioNum];
            audio.Play();
            StartCoroutine(EatingMotion());
        }

    }

    int Watermelon_AudioNum;
    public void WatermelonMotion() //PinkFlowers에 다가가서 터치 버튼을 누른다면 밑에 코드 실행
    {
        Debug.Log("수박을 따려하고 있어요. 수박을 먹으려하고있어요.");
        if (!audio.isPlaying && !monkeyAnimator.GetBool("Motion"))
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            Watermelon_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.WatermelonMotion.Length, Watermelon_AudioNum);
            audio.clip = AudioManager.WatermelonMotion[Watermelon_AudioNum];
            audio.Play();
            StartCoroutine(EatingMotion());
        }


    }

    int Tomato_AudioNum;
    public void TomatoMotion() //PinkFlowers에 다가가서 터치 버튼을 누른다면 밑에 코드 실행
    {
        Debug.Log("토마토를 따려하고 있어요. 토마토를 먹으려하고있어요.");
        if (!audio.isPlaying && !monkeyAnimator.GetBool("Motion"))
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            Tomato_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.TomatoMotion.Length, Tomato_AudioNum);
            audio.clip = AudioManager.TomatoMotion[Tomato_AudioNum];
            audio.Play();
            StartCoroutine(EatingMotion());
        }

    }

    int Melon_AudioNum;
    public void MelonMotion() //PinkFlowers에 다가가서 터치 버튼을 누른다면 밑에 코드 실행
    {
        Debug.Log("멜론을 따려하고 있어요. 멜론을 먹으려하고있어요.");
        if (!audio.isPlaying && !monkeyAnimator.GetBool("Motion"))
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);    //Motion효과음, PlayOneShot은 음성 중첩 가능
           
            Melon_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.MelonMotion.Length, Melon_AudioNum);
            audio.clip = AudioManager.MelonMotion[Melon_AudioNum];
            audio.Play();
            StartCoroutine(EatingMotion());
        }
    }

    int Lettuce_AudioNum;
    public void LettuceMotion() //PinkFlowers에 다가가서 터치 버튼을 누른다면 밑에 코드 실행
    {
        Debug.Log("상추를 따려하고 있어요. 상추를 먹으려하고있어요.");
        if (!audio.isPlaying && !monkeyAnimator.GetBool("Motion"))
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);
           
            Lettuce_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.LettuceMotion.Length, Lettuce_AudioNum);
            audio.clip = AudioManager.LettuceMotion[Lettuce_AudioNum];
            audio.Play();
            StartCoroutine(EatingMotion());
        }
    }


    int JumpingBananaMotion_AudioNum;
    bool JumpingToGetBananaMotion_isrunning=false;
    public void JumpingToGetBanana()
    {
        if (!JumpingToGetBananaMotion_isrunning)
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f); 
            
            JumpingBananaMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.JumpingToGetBananaMotion.Length, JumpingBananaMotion_AudioNum);
            audio.clip = AudioManager.JumpingToGetBananaMotion[JumpingBananaMotion_AudioNum];
            audio.Play();
            
            StartCoroutine(JumpingToGetBananaMotion());
        }
    }

    int JumpingAppleMotion_AudioNum;
    bool JumpingToGetAppleMotion_isrunning = false;
    public void JumpingToGetApple()
    {
        if (!JumpingToGetAppleMotion_isrunning)
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f); 

            JumpingAppleMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.JumpingToGetAppleMotion.Length, JumpingAppleMotion_AudioNum);
            audio.clip = AudioManager.JumpingToGetAppleMotion[JumpingAppleMotion_AudioNum];
            audio.Play();
            
            StartCoroutine(JumpingToGetAppleMotion());
        }
    }


    int JumpingGrapeMotion_AudioNum;
    bool JumpingToGetGrapeMotion_isrunning = false;
    public void JumpingToGetGrape()
    {
       
        if (!JumpingToGetGrapeMotion_isrunning)
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            JumpingGrapeMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.JumpingToGetGrapeMotion.Length, JumpingGrapeMotion_AudioNum);
            audio.clip = AudioManager.JumpingToGetGrapeMotion[JumpingGrapeMotion_AudioNum];
            audio.Play();
            
            StartCoroutine(JumpingToGetGrapeMotion());
        }
    }

    int JumpingPeachMotion_AudioNum;
    bool JumpingToGetPeachMotion_isrunning = false;
    public void JumpingToGetPeach()
    {
        if (!JumpingToGetPeachMotion_isrunning)
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            JumpingPeachMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.JumpingToGetPeachMotion.Length, JumpingPeachMotion_AudioNum);
            audio.clip = AudioManager.JumpingToGetPeachMotion[JumpingPeachMotion_AudioNum];
            audio.Play();
            
            StartCoroutine(JumpingToGetPeachMotion());
        }
    }


    int AttackStrawberryMotion_AudioNum;
    bool AttackToGetStrawberryMotion_isrunning = false;
    public void AttackToGetStrawberry()
    {
        if (!AttackToGetStrawberryMotion_isrunning)
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            AttackStrawberryMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.AttackToGetStrawberryMotion.Length, AttackStrawberryMotion_AudioNum);
            audio.clip = AudioManager.AttackToGetStrawberryMotion[AttackStrawberryMotion_AudioNum];
            audio.Play();
            
            StartCoroutine(AttackToGetStrawberryMotion());
        }
    }

    int AttackOnionMotion_AudioNum;
    bool AttackToGetOnionMotion_isrunning = false;
    public void AttackToGetOnion()
    {
        if (!AttackToGetOnionMotion_isrunning)
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            AttackOnionMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.AttackToGetOnionMotion.Length, AttackOnionMotion_AudioNum);
            audio.clip = AudioManager.AttackToGetOnionMotion[AttackOnionMotion_AudioNum];
            audio.Play();
            
            StartCoroutine(AttackToGetOnionMotion());
        }
    }

    int AttackCarrotMotion_AudioNum;
    bool AttackToGetCarrotMotion_isrunning = false;
    public void AttackToGetCarrot()
    {
        if (!AttackToGetCarrotMotion_isrunning)
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            AttackCarrotMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.AttackToGetCarrotMotion.Length, AttackCarrotMotion_AudioNum);
            audio.clip = AudioManager.AttackToGetCarrotMotion[AttackCarrotMotion_AudioNum];
            audio.Play();
            
            StartCoroutine(AttackToGetCarrotMotion());
        }
    }


    int AttackPotatoMotion_AudioNum;
    bool AttackToGetPotatoMotion_isrunning = false;
    public void AttackToGetPotato()
    {
        if (!AttackToGetPotatoMotion_isrunning)
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            AttackPotatoMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.AttackToGetPotatoMotion.Length, AttackPotatoMotion_AudioNum);
            audio.clip = AudioManager.AttackToGetPotatoMotion[AttackPotatoMotion_AudioNum];
            audio.Play();
            
            StartCoroutine(AttackToGetPotatoMotion());
        }
    }

    IEnumerator EatingMotion()
    {
        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));
        monkeyAnimator.SetBool("isEat", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("isEat", false);
        monkeyAnimator.SetBool("Motion", false);
        yield break;
    }

  
    IEnumerator JumpingToGetBananaMotion() 
    {
        JumpingToGetBananaMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isJump", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        BananaParticle.SetActive(true); 
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isJump", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false); //Motion 기준으로 Player가 움직일 수 있음. JoyStickAndMove의 코드에 나와있다
        BananaParticle.SetActive(false);
        JumpingToGetBananaMotion_isrunning = false;

        yield break;
    }

    IEnumerator JumpingToGetAppleMotion()
    {
        JumpingToGetAppleMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);


        monkeyAnimator.SetBool("isJump", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        AppleParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isJump", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion 기준으로 Player가 움직일 수 있음. JoyStickAndMove의 코드에 나와있다
        AppleParticle.SetActive(false);
        JumpingToGetAppleMotion_isrunning = false;


        yield break;
    }

    IEnumerator JumpingToGetGrapeMotion()
    {
        JumpingToGetGrapeMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isJump", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        GrapeParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isJump", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);  //Motion 기준으로 Player가 움직일 수 있음. JoyStickAndMove의 코드에 나와있다
        GrapeParticle.SetActive(false);
        JumpingToGetGrapeMotion_isrunning = false;


        yield break;
    }



    IEnumerator JumpingToGetPeachMotion()
    {
        JumpingToGetPeachMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);


        monkeyAnimator.SetBool("isJump", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        PeachParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isJump", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion 기준으로 Player가 움직일 수 있음. JoyStickAndMove의 코드에 나와있다
        PeachParticle.SetActive(false);
        JumpingToGetPeachMotion_isrunning = false;


        yield break;
    }

    IEnumerator AttackToGetStrawberryMotion()
    {
        AttackToGetStrawberryMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f)); //River미니씬에서 나왔을 때 바로보이므로, 슬라이더 게이지 천천히 차도록

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isAttack", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        StrawberryParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isAttack", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion 기준으로 Player가 움직일 수 있음. JoyStickAndMove의 코드에 나와있다
        StrawberryParticle.SetActive(false);
        AttackToGetStrawberryMotion_isrunning = false;

        yield break;
    }

    IEnumerator AttackToGetOnionMotion()  //양파
    {
        AttackToGetOnionMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isAttack", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        OnionParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isAttack", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion 기준으로 Player가 움직일 수 있음. JoyStickAndMove의 코드에 나와있다
        OnionParticle.SetActive(false);
        AttackToGetOnionMotion_isrunning = false;

        yield break;
    }

    IEnumerator AttackToGetCarrotMotion()  //당근
    {
        AttackToGetCarrotMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isAttack", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        CarrotParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isAttack", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion 기준으로 Player가 움직일 수 있음. JoyStickAndMove의 코드에 나와있다
        CarrotParticle.SetActive(false);
        AttackToGetCarrotMotion_isrunning = false;

        yield break;
    }

    IEnumerator AttackToGetPotatoMotion()  //감자
    {
        AttackToGetPotatoMotion_isrunning = true;

        
        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isAttack", true);
        monkeyAnimator.SetBool("Motion", true); //Run을 제외한 어떤 모션을 취할 때는 Motion이 true가 되도록 한다
        PotatoParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isAttack", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion 기준으로 Player가 움직일 수 있음. JoyStickAndMove의 코드에 나와있다
        PotatoParticle.SetActive(false);
        AttackToGetPotatoMotion_isrunning = false;

        yield break;
    }
}
