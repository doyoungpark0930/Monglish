using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI����Ҷ� ��ư�̵��� ����, �� �������ֱ�
using System.IO; //������ �ؽ�Ʈ �������� ���� �� �� �ְ� ����

public class StartGameManager : MonoBehaviour
{
    public GameObject ExitMenuObject;

    public GameObject SecondLock; //�ι�° �� ���� �ڹ��� 
    public GameObject ThirdLock;
    public GameObject FourthLock;
    public GameObject FifthLock;
    public GameObject SixthLock;
    int unlockNumber=1;

    //���ڿ����� �ڹ��� �̹����� setoff�ǵ����϶�. ������� 1�̶�� �ڹ����̹��� 2~6set on.�Ƹ� buutton�� �ڷ�ƾ���� ���°�������.
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)) //escŰ�� �ȵ���̵� ����Ͽ��� �ڷΰ��� ������ ��, ����� ���� ��� xǥ�ÿ� ��ư������ ExitMenu�ߵ��� ����
        {
            ExitMenu();
        }
    }

    public void ExitMenu() //�н��� �����Ͻðڽ��ϱ�? ����ϱ�, ����. �� ������ ���ϴ� ��
    {
        ExitMenuObject.SetActive(true);
    }
    public void GameExit()
    {
        Application.Quit(); //����� ,pc�ۿ��� ���������� Editor�󿡼��� �ȵ�
    }
    public void GameContinue()
    {
        ExitMenuObject.SetActive(false);
    }

    public void UnLockButton()
    {
        //��ư������ unlockNumber �ϳ��� ����. int 1�̸� �ڹ��� 2~6���, 2�� 3~6, 3�� 4~6, 4�� 5~6, 5�� 6, 6�̻��� ��Ǯ��.
        unlockNumber++;
        StartCoroutine(UnLockCoroutine());
    }
    public void initializeButton()
    {
        unlockNumber = 1;
        StartCoroutine(UnLockCoroutine());
    }
    private IEnumerator UnLockCoroutine()
    {
        switch (unlockNumber)
        {
            case 1:
                SecondLock.SetActive(true);
                ThirdLock.SetActive(true);
                FourthLock.SetActive(true);
                FifthLock.SetActive(true);
                SixthLock.SetActive(true);
                break;
            case 2:
                SecondLock.SetActive(false);
                break;
            case 3:
                ThirdLock.SetActive(false);
                break;
            case 4:
                FourthLock.SetActive(false);
                break;
            case 5:
                FifthLock.SetActive(false);
                break;
            case 6:
                SixthLock.SetActive(false);
                break;
            default:
                break;
        }
        yield break;
    }


}
