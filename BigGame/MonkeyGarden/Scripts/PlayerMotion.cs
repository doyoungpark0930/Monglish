using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private AudioManagerMonkeyGarden AudioManager;//AudioManager���� �����Ŭ���� �������� ����

    [SerializeField] private GameObject BananaParticle; //�ٳ����� ������ ��ƼŬ ����Ʈ
    [SerializeField] private GameObject AppleParticle; //����� ������ ��ƼŬ ����Ʈ
    [SerializeField] private GameObject GrapeParticle; //������ ������ ��ƼŬ ����Ʈ
    [SerializeField] private GameObject PeachParticle; //�����ư� ������ ��ƼŬ ����Ʈ
    [SerializeField] private GameObject StrawberryParticle; //���Ⱑ ������ ��ƼŬ ����Ʈ
    [SerializeField] private GameObject OnionParticle; //���İ� ������ ��ƼŬ ����Ʈ
    [SerializeField] private GameObject CarrotParticle; //����� ������ ��ƼŬ ����Ʈ
    [SerializeField] private GameObject PotatoParticle; //���ڰ� ������ ��ƼŬ ����Ʈ


    [SerializeField] SliderMiniScene SliderMiniScene; //����ѹ� ���� ������ �����̴��� ����


    


    Animator monkeyAnimator;
    AudioSource audio;

    void Awake()
    {
       


        monkeyAnimator = Player.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        BananaParticle.SetActive(false);    //���ӽ��۽ÿ��� ���ʿ��� ParticleEffect ����
        AppleParticle.SetActive(false); 
        GrapeParticle.SetActive(false); 
         PeachParticle.SetActive(false); 
        StrawberryParticle.SetActive(false); 
        OnionParticle.SetActive(false); 
        CarrotParticle.SetActive(false); 
        PotatoParticle.SetActive(false);
    }


    int FlowersMotion_AudioNum;

    public void FlowersMotion() //PinkFlowers�� �ٰ����� ��ġ ��ư�� �����ٸ� �ؿ� �ڵ� ����
    {
        Debug.Log("�����̰� ���� ����ġ�� �־��. �����̰� �� ������ �ð��־��. �����̰� ������ ����ֳ׿�");
        if (!audio.isPlaying && !monkeyAnimator.GetBool("Motion")) //������� ������, ������ �������� �����, ���� �׸��� Sliderä�� �ٽ� ������ �� �ִ�
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

            FlowersMotion_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.FlowersMotion.Length, FlowersMotion_AudioNum);
            audio.clip = AudioManager.FlowersMotion[FlowersMotion_AudioNum];
            audio.Play();
            StartCoroutine(EatingMotion());
        }
           
    }


    int Mushrooms_AudioNum;
    public void MushroomsMotion() //PinkFlowers�� �ٰ����� ��ġ ��ư�� �����ٸ� �ؿ� �ڵ� ����
    {
        Debug.Log("������ �������� �ϰ� �־��. �������Ķ��� �ӽ����̳׿�");
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
    public void WatermelonMotion() //PinkFlowers�� �ٰ����� ��ġ ��ư�� �����ٸ� �ؿ� �ڵ� ����
    {
        Debug.Log("������ �����ϰ� �־��. ������ �������ϰ��־��.");
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
    public void TomatoMotion() //PinkFlowers�� �ٰ����� ��ġ ��ư�� �����ٸ� �ؿ� �ڵ� ����
    {
        Debug.Log("�丶�並 �����ϰ� �־��. �丶�並 �������ϰ��־��.");
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
    public void MelonMotion() //PinkFlowers�� �ٰ����� ��ġ ��ư�� �����ٸ� �ؿ� �ڵ� ����
    {
        Debug.Log("����� �����ϰ� �־��. ����� �������ϰ��־��.");
        if (!audio.isPlaying && !monkeyAnimator.GetBool("Motion"))
        {
            AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);    //Motionȿ����, PlayOneShot�� ���� ��ø ����
           
            Melon_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.MelonMotion.Length, Melon_AudioNum);
            audio.clip = AudioManager.MelonMotion[Melon_AudioNum];
            audio.Play();
            StartCoroutine(EatingMotion());
        }
    }

    int Lettuce_AudioNum;
    public void LettuceMotion() //PinkFlowers�� �ٰ����� ��ġ ��ư�� �����ٸ� �ؿ� �ڵ� ����
    {
        Debug.Log("���߸� �����ϰ� �־��. ���߸� �������ϰ��־��.");
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
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
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
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
        BananaParticle.SetActive(true); 
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isJump", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false); //Motion �������� Player�� ������ �� ����. JoyStickAndMove�� �ڵ忡 �����ִ�
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
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
        AppleParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isJump", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion �������� Player�� ������ �� ����. JoyStickAndMove�� �ڵ忡 �����ִ�
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
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
        GrapeParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isJump", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);  //Motion �������� Player�� ������ �� ����. JoyStickAndMove�� �ڵ忡 �����ִ�
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
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
        PeachParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isJump", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion �������� Player�� ������ �� ����. JoyStickAndMove�� �ڵ忡 �����ִ�
        PeachParticle.SetActive(false);
        JumpingToGetPeachMotion_isrunning = false;


        yield break;
    }

    IEnumerator AttackToGetStrawberryMotion()
    {
        AttackToGetStrawberryMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f)); //River�̴Ͼ����� ������ �� �ٷκ��̹Ƿ�, �����̴� ������ õõ�� ������

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isAttack", true);
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
        StrawberryParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isAttack", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion �������� Player�� ������ �� ����. JoyStickAndMove�� �ڵ忡 �����ִ�
        StrawberryParticle.SetActive(false);
        AttackToGetStrawberryMotion_isrunning = false;

        yield break;
    }

    IEnumerator AttackToGetOnionMotion()  //����
    {
        AttackToGetOnionMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isAttack", true);
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
        OnionParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isAttack", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion �������� Player�� ������ �� ����. JoyStickAndMove�� �ڵ忡 �����ִ�
        OnionParticle.SetActive(false);
        AttackToGetOnionMotion_isrunning = false;

        yield break;
    }

    IEnumerator AttackToGetCarrotMotion()  //���
    {
        AttackToGetCarrotMotion_isrunning = true;

        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isAttack", true);
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
        CarrotParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isAttack", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion �������� Player�� ������ �� ����. JoyStickAndMove�� �ڵ忡 �����ִ�
        CarrotParticle.SetActive(false);
        AttackToGetCarrotMotion_isrunning = false;

        yield break;
    }

    IEnumerator AttackToGetPotatoMotion()  //����
    {
        AttackToGetPotatoMotion_isrunning = true;

        
        StartCoroutine(SliderMiniScene.ContinuousUp(0.25f));

        AudioManager.Getaudio().PlayOneShot(AudioManager.MotionSound, 0.2f);

        monkeyAnimator.SetBool("isAttack", true);
        monkeyAnimator.SetBool("Motion", true); //Run�� ������ � ����� ���� ���� Motion�� true�� �ǵ��� �Ѵ�
        PotatoParticle.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        monkeyAnimator.SetBool("isAttack", false);
        yield return new WaitForSecondsRealtime(2f);
        monkeyAnimator.SetBool("Motion", false);    //Motion �������� Player�� ������ �� ����. JoyStickAndMove�� �ڵ忡 �����ִ�
        PotatoParticle.SetActive(false);
        AttackToGetPotatoMotion_isrunning = false;

        yield break;
    }
}
