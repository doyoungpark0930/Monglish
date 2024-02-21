using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //��ġ��������


//https://www.youtube.com/watch?v=S-0muRsfwzk ����
public class JoyStickAndMove : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField] private RectTransform rect_JoyStickBackGround;
    [SerializeField] private RectTransform rect_JoyStick;

    private float radius;//JoyStickBackGround�� ������

    [SerializeField] private GameObject Player; //������ ĳ����
    [SerializeField] private float moveSpeed; //ĳ������ ������ �ӵ�
    [SerializeField] private float rotateSpeed;

    private bool isTouch = false; //��ġ �ɶ� �����̵���
    private Vector3 movePosition; //������ ��ǥ
    Vector3 dir; //ĳ���Ͱ� �����̷��� �ϴ� �������� ����

    Animator monkeyAnimator;

    public AudioSource audio;  //���ڱ� �Ҹ���������. ���ڱ��Ҹ��� ���� ��Ȳ���� �޶����⿡ �� ������ SituationManager��������

    void Awake()
    {
        monkeyAnimator = Player.GetComponent<Animator>();
        radius = rect_JoyStickBackGround.rect.width * 0.5f; //������ ���

        audio = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        if(isTouch && !monkeyAnimator.GetBool("Motion") ) //��ġ�� �ؾ��ϰ�, Player�� run�� ������ � ����� ���� ���� �������� �ʴ´�.������� �� ���� �ô� ���.
        {
            Player.transform.position += Time.fixedDeltaTime*movePosition;
            Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed); //���� �ٲٱ� �Լ�, ������ ��

            if (!audio.isPlaying)
            { 
                audio.Play();   //���ڱ� �Ҹ�
            }
        }
        else
        {
            audio.Stop(); //��ġ���� ���� ������ �ٷ� ���ڱ� �Ҹ�  stop�ϵ���
        }

        //�� ���� if���� �и��߳ĸ�, ���� if���� ���� monkeyAnimator.SetBool("isRun", movePosition != Vector3.zero); �� �ڵ带 �־��ٸ�, ��ġ �� �Ŀ� isRun�� false�� �ȵǱ⶧��
        //�׸��� if���ȿ� !monkeyAnimator.GetBool("Motion")�̰� ���� ������, �ٸ����� �����ϰ� isRun�ϸ� �ȵǴϱ�
        //Monkey1�ִϸ����� Ư���� isRun Transition�� �켱������ ���Ƽ� isRun�� �ٸ��� �� ����
        if (!monkeyAnimator.GetBool("Motion")) monkeyAnimator.SetBool("isRun", movePosition != Vector3.zero); //�������� 0�� �ƴ϶�� isRun
        else {  //�� else���� ���ٸ�, Motion�� true�ε� isRun�� True�� �����϶� ��� isRun�� true���Ǿ�����Ƿ�.. ���� Motion�����϶� �׻� isRun�� false�� �д�
            monkeyAnimator.SetBool("isRun", false); 
        }
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_JoyStickBackGround.position; //��ġ�� ��ġ-background�� ��ġ ����
        value = Vector2.ClampMagnitude(value, radius); //ClampMagnitue�� ���α�, �ش簪 value�� radius��ŭ ���д�
        rect_JoyStick.localPosition = value; //localPosition�� �θ�Ʒ��� ������ġ. 

        float distance = Vector2.Distance(rect_JoyStickBackGround.position, rect_JoyStick.position) / radius; //���̽�ƽ��ġ�� ���� �ӵ������ϱ� ����. ������ �ִ밡 radius�̹Ƿ� distance�� �ִ�1�̵�
        value = value.normalized; //������ ���Ѵ�
        movePosition = new Vector3(value.x * moveSpeed * Time.fixedDeltaTime * distance, 0f, value.y * moveSpeed * Time.fixedDeltaTime * distance);
        dir = new Vector3(value.x, 0, value.y); //ĳ���Ͱ� �����̷��� �ϴ� �������� ����




    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true; //��ġ�� ���۵ȴٸ� true
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_JoyStick.localPosition = Vector3.zero; //��ġ�� ������ �ٽ� ������ġ�� (0,0,0)���� �ʱ�ȭ
        movePosition = Vector3.zero; //�ٽ� �����϶� �̵����� �ʱ�ȭ ���������
    }


}
