using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour
{


    public static int SituationNum=2; //1은 범람의 상황, 2는 범람이 아닐 때의 상황.
    public static int NoRiverSituationNum = 1; //No강의 상황이 2번이상이면 반드시다음은 강의 상황. 처음 게임 시작시 어차피 no강의 상황으로 시작하니까 default는 1


    [SerializeField] private GameObject player;
    [SerializeField] private GameObject River;
    [SerializeField] private GameObject FloodedUI;  //강에 다 잠겼을 때, 하늘색 UI
    [SerializeField] private GameObject DangerousSignalUI;
    [SerializeField] private Vector3 FloodSpeed;    //강의 범람 속도 인스펙터 창에서 조절
    [SerializeField] private GameObject RunGameEnter; //AGame입장 포탈
    [SerializeField] private GameObject ItemsOnlyRiver; //River 상황일 때의 아이템

    [SerializeField] private DirectionManagerMonkeyGarden DirectionManager;
    [SerializeField] private PlaceEnter PlaceEnterManager; //방향키 코루틴함수를 멈추기 위해.

    [SerializeField] private AudioManagerMonkeyGarden AudioManager; //AudioManaer에서 오디오클립을 가져오기 위함.

    [SerializeField] private GameObject Potals; //범람하지 않을 때의 상황에서 쓰일 포탈들 (PinkFlowers같은것들) 활성화

    [SerializeField] private GameObject SliderMiniSceneObject; //강 범람 상황일 때는 슬라이더 지우기




    public AudioSource audio; //모든 스크립트의 audio clip은 AudioManagerMonkeyGarden에 넣고 필요할 때 AudioSource에 넣어서 쓴다

    AudioSource BackgroundAudio;    //BGM 오디오
    [SerializeField] private GameObject BgmGameObject;
    [SerializeField] private AudioClip[] BgmAudioClip;

    [SerializeField] private JoyStickAndMove JoyStickManager;



    float RiverHeight;

    bool FloodingStarted = false;
    bool PlayerSinked = false;
    bool PlayerSinkedFunction_isrunning = false;
    bool DirectionSignal_Started = false;
    bool EvacuateSignal_isrunning = false;

    IEnumerator var_PlayerSinkedFunction; //코루틴 변수 받을 곳. 이렇게 해야 stopCoroutine()하고 StartCoroutine()했을 때 처음부터 재시작 할 수 있다
    IEnumerator var_DangerousSignal;
    IEnumerator var_SetOnDirection;
    IEnumerator var_EvacuateSignal;



    void Awake()
    {
        BackgroundAudio = BgmGameObject.GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
        Time.timeScale = 1f;        //이거없으면 프레임 속도 느려질 수도. 미니게임에서 이걸 건드렸기때문에 여기서 초기화 해줘야함
    }

    void Start()
    {
        var_PlayerSinkedFunction = PlayerSinkedFunction(); //코루틴 변수에 코루틴을 생성한다. 초반에 이렇게 안해놓으면 stopcoroutin켜졌을떄 에러뜸
        var_DangerousSignal = DangerousSignal();
        var_SetOnDirection = DirectionManager.SetOnDirection();
        var_EvacuateSignal = EvacuateSignal();


        BackgroundAudio.clip = BgmAudioClip[0];   //기본은 '낮잠자는고양이'(no강 상황)
        BackgroundAudio.Play();

        if (SituationNum == 1) //강이 범람하는 상황
        {
            Potals.SetActive(false);
            RunGameEnter.SetActive(false);    //AGamePotal은 위험상황 이후에 켜져야함으로
            SliderMiniSceneObject.SetActive(false);
            ItemsOnlyRiver.SetActive(true);
            StartCoroutine(FloodingSituation());
        }
        else if(SituationNum==2) //강이 범람하지 않을 때의 상황
        {
           Potals.SetActive(true); //포탈의 장소를 나타내주는 오브젝트들 활성화
            RunGameEnter.SetActive(false);
           SliderMiniSceneObject.SetActive(true); //강 범람상황이 아닐 때는 슬라이더 띄우기
           ItemsOnlyRiver.SetActive(false);
        }
    }



    IEnumerator FloodingSituation() //강이 범람하는 코루틴
    {

        yield return new WaitForSecondsRealtime(8f); //8초뒤 강이 범람하기 시작함. 여기 초가 너무 짧다면, 밑으로 안내려갈까봐 좀길게 8초로함

        BackgroundAudio.clip = BgmAudioClip[1]; //강 범람이 시작되면 bgm바뀌도록
        BackgroundAudio.volume = 0.4f;
        BackgroundAudio.Play();

        RunGameEnter.SetActive(true); //강범람할 때 미니게임으로 들어갈 수 있게한다.

        FloodingStarted = true;
       

        while (RiverHeight <= 1.9) //강이 거의 끝까지 차오르면 반복문을 빠져나온다.
        {
            yield return new WaitForFixedUpdate();

            
            River.transform.position += FloodSpeed*Time.fixedDeltaTime; //강이 올라오도록 한다. 다 잠기기전의 위치까지.

        } 
        
        yield break;
    }

    IEnumerator PlayerSinkedFunction() //Player가 다 잠겼을 때
    {
        PlayerSinkedFunction_isrunning = true; //PlayerSinkedFunction 함수가 실행되고 있다는 뜻
        yield return new WaitForSecondsRealtime(3f);    //이 5초동안 중간에  PlayerSinked=false되면 바로 함수꺼지도록 . FixedUpdate문의 "PlayerSinkedFunction 함수관련"에 있음


        //위험신호(음성파일+깜빡이)    
        var_DangerousSignal = DangerousSignal();
        StartCoroutine(var_DangerousSignal);

        //방향키 뜨게하는 코루틴. 
        if (!DirectionSignal_Started) { StartCoroutine(DirectionSignal()); } //방향키가 뜸과 동시에 AGameEnter도 뜬다.


        //계속 잠긴상태라면 코루틴을 빠져나가지 못하게
        while (PlayerSinked)
        {
            yield return null;
        }
        PlayerSinkedFunction_isrunning = false; //PlayerSinkedFunction 함수가 꺼진다는 뜻
        yield break;
        
    }

    int Dangerous_AudioNum; //DangerousSignal_ad의 오디오 순서
    IEnumerator DangerousSignal()
    {

        while (PlayerSinked)
        {
            Debug.Log("오디오와 깜빡이,Die동작: 물에서는 숨쉬기가 힘들어요");
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
        //PlayerSinked=false가 되면 코루틴 스탑

        yield break;
        
    }

    IEnumerator DirectionSignal()
    {
        DirectionSignal_Started = true;
        var_SetOnDirection = DirectionManager.SetOnDirection();
        StartCoroutine(var_SetOnDirection); 
        
        


        yield break;
    }



    int Evacuate_AudioNum; //Evacuate_ad의 오디오 순서
    IEnumerator EvacuateSignal() //강의 범람이 시작됐지만, 잠기지 않았을 때
    {
        EvacuateSignal_isrunning = true;

        while (!PlayerSinked)
        {
            
            yield return new WaitForSecondsRealtime(6f);
            if (!audio.isPlaying) 
            {
                Evacuate_AudioNum = AudioManager.RandomValueExceptBefore(AudioManager.Evacuate_ad.Length, Evacuate_AudioNum); //이전의 값을 제외한 오디오클립의 번호를 받음
                audio.clip = AudioManager.Evacuate_ad[Evacuate_AudioNum];
                audio.Play();
            }
            Debug.Log("위험해요. 강이올라오고있어요");

        }
        //코루틴 함수의 종료는 FixedUpdate문에서 호출한다.
        
    }


    void FixedUpdate()
    {
        RiverHeight = River.transform.position.y;       //강의 높이
        if (player.transform.position.y + 0.19 < RiverHeight) { PlayerSinked = true; } //원숭이가 다 잠겼다면 PlayerSinked = true;
        else { PlayerSinked = false; }

        if(PlayerSinked)    //원숭이가 강에 다 잠겼다면
        {
            //잠긴 원숭이 발자국 소리
            JoyStickManager.audio.clip = AudioManager.UnderWaterStepSound;
            JoyStickManager.audio.volume = 0.3f;
        }
        else if (RiverHeight+1.7f>player.transform.position.y)   //잠기지는 않았지만, 강의 높이가 player보다 높을 때 첨벙첨벙소리
        {
            JoyStickManager.audio.clip = AudioManager.WaterStepSound;
            JoyStickManager.audio.volume = 0.7f;
        }
        else        //그냥 잔디 위를 걸을 때 나는 소리
        {
            JoyStickManager.audio.clip = AudioManager.StepSound;
            JoyStickManager.audio.volume = 0.3f;
        }



        if (SituationNum==1) //강의 범람 상황일 때
        {

            if (PlayerSinked)    //원숭이가 다 잠겼다면
            {

                River.SetActive(false);
                FloodedUI.SetActive(true);

                {
                    StopCoroutine(var_EvacuateSignal);  //다 잠기지 않았을 때 사용되는 EvacuateSignal은 stop한다
                    EvacuateSignal_isrunning = false;
                }
            }
            else
            {
                River.SetActive(true);
                FloodedUI.SetActive(false);
                DangerousSignalUI.SetActive(false);

                {   //물에서 빠져나오면  PlayerSinkedFunction 코루틴 꺼버린다. 코루틴 중복해서 켜지면 안되니까. 그리고 종속된 코루틴도 방향키 코루틴 제외하고 제거.
                    StopCoroutine(var_PlayerSinkedFunction);
                    PlayerSinkedFunction_isrunning = false; //PlayerSinkedFunction 함수가 꺼진다는 뜻
                    StopCoroutine(var_DangerousSignal);
                }
            }

            //강의 범람이 시작됐을 때
            if (FloodingStarted)
            {
                if (PlayerSinked && !PlayerSinkedFunction_isrunning) //강이 범람하고 완전히 잠긴상태일 때.  근데 이미 잠겼을 때의 함수가 켜져있다면 실행하지 않는다(같은 코루틴 중복방지)
                {
                    var_PlayerSinkedFunction = PlayerSinkedFunction();
                    StartCoroutine(var_PlayerSinkedFunction);
                }
                else if (!PlayerSinked && player.transform.position.y < 4.2f) //강이 범람하고, 완전히 잠긴상태가 아닐때. 그리고 꼭대기가 아닐 때. 꼭대기일땐 대피신호 없어도된다.
                {




                    //완전히 잠기지 않았을 때
                    if (!EvacuateSignal_isrunning) { var_EvacuateSignal = EvacuateSignal(); StartCoroutine(var_EvacuateSignal); }




                }
                else if (player.transform.position.y >= 4.2f) //강이 범람하고, 완전히 잠긴상태가 아닐 때, 그리고 꼭대기 쯤일 때. 이 때는 절대 잠기지 않으니, PlayerSinked판단 뺌
                {


                    {   //꼭대기 쯤에 올라왔다면, 대피신호끄기
                        StopCoroutine(var_EvacuateSignal);
                        EvacuateSignal_isrunning = false;
                    }

                    //방향키코루틴은 어차피 강이 범람하고 나서 시작하니까. 산 꼭대기에 올라가면 방향키 코루틴을 멈춘다.               
                    if (PlaceEnterManager.GetEnteredToAGame()) { StopCoroutine(var_SetOnDirection); DirectionManager.GetDirection().SetActive(false); }
                }
            }
        }
        else if(SituationNum==2) //강의 범람 상황이 아닐 때
        {

        }
        else
        {
            Debug.LogError("상황이 주어지지 않음");
        }
    }
    
}
