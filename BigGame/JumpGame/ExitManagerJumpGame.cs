using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManagerJumpGame : MonoBehaviour
{

    [SerializeField] GameObject JumpGameAllObject;
    void SetOffJumpGame()
    {
        JumpGameAllObject.SetActive(false);

    }

    public void MonkeyGardenScene()
    {
        InitPos.SetInitPos = new Vector3(9.06f, 4f, 30.5f);//���� �� �ش� �̴Ͼ� ���� Player ��ġ��Ŵ
        SituationManager.SituationNum = Random.Range(1, 3);  //1���� 2������. ���ǻ�Ȳ�� ���̾ƴѻ�Ȳ 50%Ȯ��
        if (SituationManager.SituationNum == 1) //���� ��Ȳ
        {
            SituationManager.NoRiverSituationNum = 0;
        }
        else if (SituationManager.SituationNum == 2) //No���� ��Ȳ
        {
            if (SituationManager.NoRiverSituationNum >= 2) //no�� ��Ȳ�� 2�� �̻� ����ƴٸ�, �� ��Ȳ����
            {
                SituationManager.SituationNum = 1; //�� ��Ȳ����
                SituationManager.NoRiverSituationNum = 0; //�� ��Ȳ���� �����Ƿ�, No�� ��ȲȽ�� 0���� �ʱ�ȭ
            }
            else { SituationManager.NoRiverSituationNum++; }    //no�� ��Ȳ�� 2������ ���ٸ�,no�� ��Ȳ �״�� �����Ű�� Ƚ�� �ϳ��� ����
        }
        else
        {
            Debug.LogError("��Ȳ�� �� ���־���");
        }
        SliderMiniScene.NoRiverMiniScene_Num = 1;  //�̴Ͼ����� ������ �ش� �̴Ͼ��� ��ȣ�� �˷���. JumpGame�� 1
                                                   //�̴ϰ��� ��ư �� ���� ���� �� �޴´�. ������ ���� �̴Ͼ��� ������ �ٸ� �̴Ͼ��� ��������
        SliderMiniScene.NoRiverMiniScene_Num = RandomValueExceptBefore(2, SliderMiniScene.NoRiverMiniScene_Num);//���� 2�� no���� �̴ϰ��� ���� ��
        AddLoadingSceneController.Instance.LoadScene("Assets/Scene/monkeyGarden.unity");

        //�ε� ���� ��ġ�� �ʰ� RunGame�� ��� ������Ʈ�� SetActive(false)�Ѵ�.
        Invoke("SetOffJumpGame", 0.5f);  //�ٷβ��� ����Ƽ �⺻ ��溸������ 0.5���Ŀ� �����ϵ��� .�ε��������� ��Ÿ���µ� �ð��� �� �ɸ��Ƿ� 0.5�ʳ�����
    }

    int RandomValueExceptBefore(int MiniSceneNum, int BeforeValue) //MiniGameButton �迭�� ��Ҽ���, ������ ��
    {
        int newNumber;
        do
        {
            newNumber = Random.Range(0, MiniSceneNum);

        } while (newNumber == BeforeValue); //������ ���� ���ٸ� �ٸ��� ���ö����� �ݺ������� �� ���� �޴´�

        return newNumber;
    }
}