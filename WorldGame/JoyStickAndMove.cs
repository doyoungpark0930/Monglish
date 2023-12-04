using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //터치관련정보


//https://www.youtube.com/watch?v=S-0muRsfwzk 참고
public class JoyStickAndMove : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField] private RectTransform rect_JoyStickBackGround;
    [SerializeField] private RectTransform rect_JoyStick;

    private float radius;//JoyStickBackGround의 반지름

    [SerializeField] private GameObject Player; //움직일 캐릭터
    [SerializeField] private float moveSpeed; //캐릭터의 움직임 속도
    [SerializeField] private float rotateSpeed;

    private bool isTouch = false; //터치 될때 움직이도록
    private Vector3 movePosition; //움직일 좌표
    Vector3 dir; //캐릭터가 움직이려고 하는 쪽으로의 방향

    Animator monkeyAnimator;

    public AudioSource audio;  //발자국 소리내기위해. 발자국소리는 강의 상황마다 달라지기에 이 변수는 SituationManager에서쓰임

    void Awake()
    {
        monkeyAnimator = Player.GetComponent<Animator>();
        radius = rect_JoyStickBackGround.rect.width * 0.5f; //반지름 계산

        audio = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        if(isTouch && !monkeyAnimator.GetBool("Motion") ) //터치를 해야하고, Player가 run을 제외한 어떤 모션을 취할 때는 움직이지 않는다.예를들어 꽃 냄새 맡는 모션.
        {
            Player.transform.position += Time.fixedDeltaTime*movePosition;
            Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed); //방향 바꾸기 함수, 원리는 모름

            if (!audio.isPlaying)
            { 
                audio.Play();   //발자국 소리
            }
        }
        else
        {
            audio.Stop(); //터치에서 손을 땐순간 바로 발자국 소리  stop하도록
        }

        //왜 위의 if문과 분리했냐면, 위의 if문에 밑의 monkeyAnimator.SetBool("isRun", movePosition != Vector3.zero); 이 코드를 넣었다면, 터치 땐 후에 isRun이 false로 안되기때문
        //그리고 if문안에 !monkeyAnimator.GetBool("Motion")이거 넣은 이유는, 다른동작 무시하고 isRun하면 안되니까
        //Monkey1애니메이터 특성상 isRun Transition이 우선순위가 높아서 isRun이 다른거 다 씹음
        if (!monkeyAnimator.GetBool("Motion")) monkeyAnimator.SetBool("isRun", movePosition != Vector3.zero); //움직임이 0이 아니라면 isRun
        else {  //이 else문이 없다면, Motion이 true인데 isRun이 True의 상태일때 계속 isRun이 true가되어버리므로.. 따라서 Motion상태일땐 항상 isRun을 false로 둔다
            monkeyAnimator.SetBool("isRun", false); 
        }
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_JoyStickBackGround.position; //터치한 위치-background의 위치 벡터
        value = Vector2.ClampMagnitude(value, radius); //ClampMagnitue는 가두기, 해당값 value를 radius만큼 가둔다
        rect_JoyStick.localPosition = value; //localPosition은 부모아래의 지역위치. 

        float distance = Vector2.Distance(rect_JoyStickBackGround.position, rect_JoyStick.position) / radius; //조이스틱위치에 따른 속도조절하기 위함. 어차피 최대가 radius이므로 distance는 최대1이됨
        value = value.normalized; //방향을 구한다
        movePosition = new Vector3(value.x * moveSpeed * Time.fixedDeltaTime * distance, 0f, value.y * moveSpeed * Time.fixedDeltaTime * distance);
        dir = new Vector3(value.x, 0, value.y); //캐릭터가 움직이려고 하는 쪽으로의 방향




    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true; //터치가 시작된다면 true
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_JoyStick.localPosition = Vector3.zero; //터치가 때지면 다시 지역위치를 (0,0,0)으로 초기화
        movePosition = Vector3.zero; //다시 움직일때 이동방향 초기화 시켜줘야함
    }


}
