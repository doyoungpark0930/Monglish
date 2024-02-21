using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{


    public static int SituationNum=2; //1�� ������ ��Ȳ, 2�� ������ �ƴ� ���� ��Ȳ.
    public static int NoRiverSituationNum = 1; //No���� ��Ȳ�� 2���̻��̸� �ݵ�ô����� ���� ��Ȳ. ó�� ���� ���۽� ������ no���� ��Ȳ���� �����ϴϱ� default�� 1


    [SerializeField] private GameObject player;
    [SerializeField] private GameObject River;
    [SerializeField] private GameObject FloodedUI;  //���� �� ����� ��, �ϴû� UI
    [SerializeField] private GameObject DangerousSignalUI;
    [SerializeField] private Vector3 FloodSpeed;    //���� ���� �ӵ� �ν����� â���� ����
    [SerializeField] private GameObject RunGameEnter; //AGame���� ��Ż
    [SerializeField] private GameObject ItemsOnlyRiver; //River ��Ȳ�� ���� ������

    [SerializeField] private DirectionManagerMonkeyGarden DirectionManager;
    [SerializeField] private PlaceEnter PlaceEnterManager; //����Ű �ڷ�ƾ�Լ��� ���߱� ����.

    [SerializeField] private AudioManagerMonkeyGarden AudioManager; //AudioManaer���� �����Ŭ���� �������� ����.

    [SerializeField] private GameObject Potals; //�������� ���� ���� ��Ȳ���� ���� ��Ż�� (PinkFlowers�����͵�) Ȱ��ȭ

    [SerializeField] private GameObject SliderMiniSceneObject; //�� ���� ��Ȳ�� ���� �����̴� �����




    public AudioSource audio; //��� ��ũ��Ʈ�� audio clip�� AudioManagerMonkeyGarden�� �ְ� �ʿ��� �� AudioSource�� �־ ����

    AudioSource BackgroundAudio;    //BGM �����
    [SerializeField] private GameObject BgmGameObject;
    [SerializeField] private AudioClip[] BgmAudioClip;

    [SerializeField] private JoyStickAndMove JoyStickManager;



    float RiverHeight;

    bool FloodingStarted = false;
    bool PlayerSinked = false;
    bool PlayerSinkedFunction_isrunning = false;
    bool DirectionSignal_Started = false;
    bool EvacuateSignal_isrunning = false;

    IEnumerator var_PlayerSinkedFunction; //�ڷ�ƾ ���� ���� ��. �̷��� �ؾ� stopCoroutine()�ϰ� StartCoroutine()���� �� ó������ ����� �� �� �ִ�
    IEnumerator var_DangerousSignal;
    IEnumerator var_SetOnDirection;
    IEnumerator var_EvacuateSignal;



    void Awake()
    {
        BackgroundAudio = BgmGameObject.GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
        Time.timeScale = 1f;        //�̰ž����� ������ �ӵ� ������ ����. �̴ϰ��ӿ��� �̰� �ǵ�ȱ⶧���� ���⼭ �ʱ�ȭ �������
    }

    void Start()
    {
        var_PlayerSinkedFunction = PlayerSinkedFunction(); //�ڷ�ƾ ������ �ڷ�ƾ�� �����Ѵ�. �ʹݿ� �̷��� ���س����� stopcoroutin�������� ������
        var_DangerousSignal = DangerousSignal();
        var_SetOnDirection = DirectionManager.SetOnDirection();
        var_EvacuateSignal = EvacuateSignal();


        BackgroundAudio.clip = BgmAudioClip[0];   //�⺻�� '�����ڴ°����'(no�� ��Ȳ)
        BackgroundAudio.Play();

        if (SituationNum == 1) //���� �����ϴ� ��Ȳ
        {
            Potals.SetActive(false);
            RunGameEnter.SetActive(false);    //AGamePotal�� �����Ȳ ���Ŀ� ������������
            SliderMiniSceneObject.SetActive(false);
            ItemsOnlyRiver.SetActive(true);
            StartCoroutine(FloodingSituation());
        }
        else if(SituationNum==2) //���� �������� ���� ���� ��Ȳ
        {
           Potals.SetActive(true); //��Ż�� ��Ҹ� ��Ÿ���ִ� ������Ʈ�� Ȱ��ȭ
            RunGameEnter.SetActive(false);
           SliderMiniSceneObject.SetActive(true); //�� ������Ȳ�� �ƴ� ���� �����̴� ����
           ItemsOnlyRiver.SetActive(false);
        }
    }



    IEnumerator FloodingSituation() //���� �����ϴ� �ڷ�ƾ
    {

        yield return new WaitForSecondsRealtime(8f); //8�ʵ� ���� �����ϱ� ������. ���� �ʰ� �ʹ� ª�ٸ�, ������ �ȳ�������� ����� 8�ʷ���

        BackgroundAudio.clip = BgmAudioClip[1]; //�� ������ ���۵Ǹ� bgm�ٲ��
        BackgroundAudio.volume = 0.4f;
        BackgroundAudio.Play();

        RunGameEnter.SetActive(true); //�������� �� �̴ϰ������� �� �� �ְ��Ѵ�.

        FloodingStarted = true;
       

        while (RiverHeight <= 1.9) //���� ���� ������ �������� �ݺ����� �������´�.
        {
            yield return new WaitForFixedUpdate();

            
            River.transform.position += FloodSpeed*Time.fixedDeltaTime; //���� �ö������ �Ѵ�. �� �������� ��ġ����.

        } 
        
        yield break;
    }

    IEnumerator PlayerSinkedFunction() //Player�� �� ����� ��
    {
        PlayerSinkedFunction_isrunning = true; //PlayerSinkedFunction �Լ��� ����ǰ� �ִٴ� ��
        yield return new WaitForSecondsRealtime(3f);    //�� 5�ʵ��� �߰���  PlayerSinked=false�Ǹ� �ٷ� �Լ��������� . FixedUpdate���� "PlayerSinkedFunction �Լ�����"�� ����


        //�����ȣ(��������+������)    
        var_DangerousSignal = DangerousSignal();
        StartCoroutine(var_DangerousSignal);

        //����Ű �߰��ϴ� �ڷ�ƾ. 
        if (!DirectionSignal_Started) { StartCoroutine(DirectionSignal()); } //����Ű�� ��� ���ÿ� AGameEnter�� ���.


        //��� �����¶�� �ڷ�ƾ�� ���������� ���ϰ�
        while (PlayerSinked)
        {
            yield return null;
        }
        PlayerSinkedFunction_isrunning = false; //PlayerSinkedFunction �Լ��� �����ٴ� ��
        yield break;
        
    }

    int Dangerous_AudioNum; //DangerousSignal_ad�� ����� ����
    IEnumerator DangerousSignal()
    {

        while (PlayerSinked)
        {
            Debug.Log("������� ������,Die����: �������� �����Ⱑ ������");
            if (!audio.isPlaying) 
            {
                Dangerous_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.DangerousSignal_ad.Length, Dangerous_AudioNum);
                audio.clip = AudioManager.DangerousSignal_ad[Dangerous_AudioNum];
                audio.Play();
            }
            DangerousSignalUI.SetActive(true);
            yield return new WaitForSecondsRealtime(0.6f);
            DangerousSignalUI.SetActive(false);
            yield return new WaitForSecondsRealtime(5f);
        }
        //PlayerSinked=false�� �Ǹ� �ڷ�ƾ ��ž

        yield break;
        
    }

    IEnumerator DirectionSignal()
    {
        DirectionSignal_Started = true;
        var_SetOnDirection = DirectionManager.SetOnDirection();
        StartCoroutine(var_SetOnDirection); 
        
        


        yield break;
    }



    int Evacuate_AudioNum; //Evacuate_ad�� ����� ����
    IEnumerator EvacuateSignal() //���� ������ ���۵�����, ����� �ʾ��� ��
    {
        EvacuateSignal_isrunning = true;

        while (!PlayerSinked)
        {
            
            yield return new WaitForSecondsRealtime(6f);
            if (!audio.isPlaying) 
            {
                Evacuate_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.Evacuate_ad.Length, Evacuate_AudioNum); //������ ���� ������ �����Ŭ���� ��ȣ�� ����
                audio.clip = AudioManager.Evacuate_ad[Evacuate_AudioNum];
                audio.Play();
            }
            Debug.Log("�����ؿ�. ���̿ö�����־��");

        }
        //�ڷ�ƾ �Լ��� ����� FixedUpdate������ ȣ���Ѵ�.
        
    }


    void FixedUpdate()
    {
        RiverHeight = River.transform.position.y;       //���� ����
        if (player.transform.position.y + 0.19 < RiverHeight) { PlayerSinked = true; } //�����̰� �� ���ٸ� PlayerSinked = true;
        else { PlayerSinked = false; }

        if(PlayerSinked)    //�����̰� ���� �� ���ٸ�
        {
            //��� ������ ���ڱ� �Ҹ�
            JoyStickManager.audio.clip = AudioManager.UnderWaterStepSound;
            JoyStickManager.audio.volume = 0.3f;
        }
        else if (RiverHeight+1.7f>player.transform.position.y)   //������� �ʾ�����, ���� ���̰� player���� ���� �� ÷��÷���Ҹ�
        {
            JoyStickManager.audio.clip = AudioManager.WaterStepSound;
            JoyStickManager.audio.volume = 0.7f;
        }
        else        //�׳� �ܵ� ���� ���� �� ���� �Ҹ�
        {
            JoyStickManager.audio.clip = AudioManager.StepSound;
            JoyStickManager.audio.volume = 0.3f;
        }



        if (SituationNum==1) //���� ���� ��Ȳ�� ��
        {

            if (PlayerSinked)    //�����̰� �� ���ٸ�
            {

                River.SetActive(false);
                FloodedUI.SetActive(true);

                {
                    StopCoroutine(var_EvacuateSignal);  //�� ����� �ʾ��� �� ���Ǵ� EvacuateSignal�� stop�Ѵ�
                    EvacuateSignal_isrunning = false;
                }
            }
            else
            {
                River.SetActive(true);
                FloodedUI.SetActive(false);
                DangerousSignalUI.SetActive(false);

                {   //������ ����������  PlayerSinkedFunction �ڷ�ƾ ��������. �ڷ�ƾ �ߺ��ؼ� ������ �ȵǴϱ�. �׸��� ���ӵ� �ڷ�ƾ�� ����Ű �ڷ�ƾ �����ϰ� ����.
                    StopCoroutine(var_PlayerSinkedFunction);
                    PlayerSinkedFunction_isrunning = false; //PlayerSinkedFunction �Լ��� �����ٴ� ��
                    StopCoroutine(var_DangerousSignal);
                }
            }

            //���� ������ ���۵��� ��
            if (FloodingStarted)
            {
                if (PlayerSinked && !PlayerSinkedFunction_isrunning) //���� �����ϰ� ������ �������� ��.  �ٵ� �̹� ����� ���� �Լ��� �����ִٸ� �������� �ʴ´�(���� �ڷ�ƾ �ߺ�����)
                {
                    var_PlayerSinkedFunction = PlayerSinkedFunction();
                    StartCoroutine(var_PlayerSinkedFunction);
                }
                else if (!PlayerSinked && player.transform.position.y < 4.2f) //���� �����ϰ�, ������ �����°� �ƴҶ�. �׸��� ����Ⱑ �ƴ� ��. ������϶� ���ǽ�ȣ ����ȴ�.
                {




                    //������ ����� �ʾ��� ��
                    if (!EvacuateSignal_isrunning) { var_EvacuateSignal = EvacuateSignal(); StartCoroutine(var_EvacuateSignal); }




                }
                else if (player.transform.position.y >= 4.2f) //���� �����ϰ�, ������ �����°� �ƴ� ��, �׸��� ����� ���� ��. �� ���� ���� ����� ������, PlayerSinked�Ǵ� ��
                {


                    {   //����� �뿡 �ö�Դٸ�, ���ǽ�ȣ����
                        StopCoroutine(var_EvacuateSignal);
                        EvacuateSignal_isrunning = false;
                    }

                    //����Ű�ڷ�ƾ�� ������ ���� �����ϰ� ���� �����ϴϱ�. �� ����⿡ �ö󰡸� ����Ű �ڷ�ƾ�� �����.               
                    if (PlaceEnterManager.GetEnteredToAGame()) { StopCoroutine(var_SetOnDirection); DirectionManager.GetDirection().SetActive(false); }
                }
            }
        }
        else if(SituationNum==2) //���� ���� ��Ȳ�� �ƴ� ��
        {

        }
        else
        {
            Debug.LogError("��Ȳ�� �־����� ����");
        }
    }
    
}
