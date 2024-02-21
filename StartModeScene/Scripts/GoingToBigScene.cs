using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToBigScene : MonoBehaviour
{
    public GameObject SecondLock;
    public GameObject ThirdLock;
    public GameObject FourthLock;
    public GameObject FifthLock;
    public GameObject SixthLock;

    public void MonkeyGardenScene()
    {
        InitPos.SetInitPos = new Vector3(-77.8f, 1.5f, -10.4f);   //이거 되나 확인해야함.
        SituationManager.SituationNum = 2; //몽키가든에 입장할때 강의 범람이 아닌 상황으로 시작
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/monkeyGarden.unity");
    }

    public void SecondScene() 
    {
        if (SecondLock.activeSelf == false) //자물쇠가 풀려있다면(없다면)
        {
            Debug.Log("Going to Second Scene");
        }
        else
        {

        }
    }


}
