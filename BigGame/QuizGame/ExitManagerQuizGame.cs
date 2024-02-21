using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitManagerQuizGame : MonoBehaviour
{
    [SerializeField] GameObject QuizGameAllObject;
    void SetOffQuizGame()
    {
        QuizGameAllObject.SetActive(false);

    }


    public void MonkeyGardenScene()
    {
        InitPos.SetInitPos = new Vector3(61.6f, 1.5f, 29f); //나올 때 해당 미니씬 옆에 Player 위치시킴
        SituationManager.SituationNum = Random.Range(1, 3);  //1부터 2까지값. 강의상황과 강이아닌상황 50%확률
        if (SituationManager.SituationNum == 1) //강의 상황
        {
            SituationManager.NoRiverSituationNum = 0;
        }
        else if (SituationManager.SituationNum == 2) //No강의 상황
        {
            if (SituationManager.NoRiverSituationNum >= 2) //no강 상황이 2번 이상 실행됐다면, 강 상황으로
            {
                SituationManager.SituationNum = 1; //강 상황으로
                SituationManager.NoRiverSituationNum = 0; //강 상황으로 갔으므로, No강 상황횟수 0으로 초기화
            }
            else { SituationManager.NoRiverSituationNum++; }    //no강 상황이 2번보다 적다면,no강 상황 그대로 실행시키고 횟수 하나를 센다
        }
        else
        {
            Debug.LogError("상황이 잘 안주어짐");
        }
        SliderMiniScene.NoRiverMiniScene_Num = 0;  //미니씬에서 나오고 해당 미니씬의 번호를 알려줌. QuizGame은 0

        //미니게임 버튼 뭐 뜰지 랜덤 값 받는다. 이전에 들어갔던 미니씬을 제외한 다른 미니씬을 랜덤으로
        SliderMiniScene.NoRiverMiniScene_Num = RandomValueExceptBefore(2, SliderMiniScene.NoRiverMiniScene_Num); //숫자 2는 no강의 미니게임 씬의 수
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/monkeyGarden.unity");


        //로딩 씬과 겹치지 않게 RunGame의 모든 오브젝트를 SetActive(false)한다.
        Invoke("SetOffQuizGame", 0.5f);  //바로끄면 유니티 기본 배경보임으로 0.5초후에 시작하도록 .로딩프리팹이 나타나는데 시간이 좀 걸리므로 0.5초넣은것
    }

    int RandomValueExceptBefore(int MiniSceneNum, int BeforeValue) //MiniGameButton 배열의 요소수와, 이전의 값
    {
        int newNumber;
        do
        {
            newNumber = Random.Range(0, MiniSceneNum);

        } while (newNumber == BeforeValue); //이전의 값과 같다면 다른게 나올때까지 반복돌리고 새 값을 받는다

        return newNumber;
    }

}
