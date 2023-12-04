using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitManagerRunGame : MonoBehaviour
{
    [SerializeField] GameObject RunGameAllObject;
    void SetOffRunGame()
    {
        RunGameAllObject.SetActive(false);

    }
    public void MonkeyGardenScene()
    {

        InitPos.SetInitPos = new Vector3(-3.85f, 6.21f, 60.0f);//나올때 AGame이 입장한 산 꼭대기에서 
        SituationManager.SituationNum = 2; //AGame에서 나올 때 강의 범람이 아닌 상황으로 시작하도록. 왜냐면 AGame은 강의 범람 상황에서 입장했으니
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/monkeyGarden.unity");

        //로딩 씬과 겹치지 않게 RunGame의 모든 오브젝트를 SetActive(false)한다.
        Invoke("SetOffRunGame", 0.5f);  //바로끄면 유니티 기본 배경보임으로 0.5초후에 시작하도록 .로딩프리팹이 나타나는데 시간이 좀 걸리므로 0.5초넣은것
    }

}
