using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderMiniScene : MonoBehaviour
{
    [SerializeField] private Slider slider; //SliderMiniScene������Ʈ
    [SerializeField] GameObject[] MiniGameButton; //�̴ϰ��ӹ�ư. static���� ������ ������ ExitManager�̴Ͼ�Game���� �ޱ�����

    public static int NoRiverMiniScene_Num=0; //�̴Ͼ����� ���� �� �� ������ ���� �Ҵ�������� . �ϴ� QuizGame=0,PlateGame=1. ù ������ ������ 1�� �����ؾ���. //D������ plate�������� ��ü�� ��

    void Awake()
    {
        slider.value = 0; //�ʱⰪ�� 0
    }
    void Start()
    {
        Debug.Log(NoRiverMiniScene_Num);

        for (int i=0;i<MiniGameButton.Length;i++)
        {
            MiniGameButton[i].SetActive(false);         //���� ���� �� �̴ϰ��� ��ư�� �� ����
        }

       
      
    }

    void FixedUpdate()
    {
        if(slider.value>=1.0f) //�����̵�ٰ� �� ä������, �������� �̴ϰ��� ��ư�� �ߵ��� �Ѵ�
        {
            MiniGameButton[NoRiverMiniScene_Num].SetActive(true);
        }
    }
    float currentDotTime=1f;
    public IEnumerator ContinuousUp(float UpValue) //�������ٰ� ���������� ��µǵ���. ���� �� �ڷ�ƾ�� �ι� ������� �ʵ����Ѵ�
    {
        currentDotTime = 1f;
        while (currentDotTime >= 0f)
        {
            
                currentDotTime -= Time.fixedDeltaTime;
                slider.value += Time.fixedDeltaTime*UpValue;
                yield return new WaitForFixedUpdate();
            
          
        }
        yield break;
    }
}
