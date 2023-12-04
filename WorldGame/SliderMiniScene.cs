using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderMiniScene : MonoBehaviour
{
    [SerializeField] private Slider slider; //SliderMiniScene오브젝트
    [SerializeField] GameObject[] MiniGameButton; //미니게임버튼. static으로 선언한 이유는 ExitManager미니씬Game에서 받기위해

    public static int NoRiverMiniScene_Num=0; //미니씬에서 나올 때 이 변수에 값을 할당해줘야함 . 일단 QuizGame=0,PlateGame=1. 첫 게임은 무조건 1로 시작해야함. //D게임이 plate게임으로 대체될 것

    void Awake()
    {
        slider.value = 0; //초기값은 0
    }
    void Start()
    {
        Debug.Log(NoRiverMiniScene_Num);

        for (int i=0;i<MiniGameButton.Length;i++)
        {
            MiniGameButton[i].SetActive(false);         //게임 시작 전 미니게임 버튼은 다 끈다
        }

       
      
    }

    void FixedUpdate()
    {
        if(slider.value>=1.0f) //슬라이드바가 다 채워지면, 랜덤으로 미니게임 버튼이 뜨도록 한다
        {
            MiniGameButton[NoRiverMiniScene_Num].SetActive(true);
        }
    }
    float currentDotTime=1f;
    public IEnumerator ContinuousUp(float UpValue) //게이지바가 연속적으로 상승되도록. 절대 이 코루틴이 두번 시행되지 않도록한다
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
