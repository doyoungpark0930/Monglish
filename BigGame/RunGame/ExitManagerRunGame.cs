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

        InitPos.SetInitPos = new Vector3(-3.85f, 6.21f, 60.0f);//���ö� AGame�� ������ �� ����⿡�� 
        SituationManager.SituationNum = 2; //AGame���� ���� �� ���� ������ �ƴ� ��Ȳ���� �����ϵ���. �ֳĸ� AGame�� ���� ���� ��Ȳ���� ����������
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/monkeyGarden.unity");

        //�ε� ���� ��ġ�� �ʰ� RunGame�� ��� ������Ʈ�� SetActive(false)�Ѵ�.
        Invoke("SetOffRunGame", 0.5f);  //�ٷβ��� ����Ƽ �⺻ ��溸������ 0.5���Ŀ� �����ϵ��� .�ε��������� ��Ÿ���µ� �ð��� �� �ɸ��Ƿ� 0.5�ʳ�����
    }

}
