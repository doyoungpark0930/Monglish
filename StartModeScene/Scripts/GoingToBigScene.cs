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
        InitPos.SetInitPos = new Vector3(-77.8f, 1.5f, -10.4f);   //�̰� �ǳ� Ȯ���ؾ���.
        SituationManager.SituationNum = 2; //��Ű���翡 �����Ҷ� ���� ������ �ƴ� ��Ȳ���� ����
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/monkeyGarden.unity");
    }

    public void SecondScene() 
    {
        if (SecondLock.activeSelf == false) //�ڹ��谡 Ǯ���ִٸ�(���ٸ�)
        {
            Debug.Log("Going to Second Scene");
        }
        else
        {

        }
    }


}
