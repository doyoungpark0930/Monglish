using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI사용할땐 버튼이든지 뭐든, 꼭 선언해주기
using System.IO; //파일을 텍스트 형식으로 저장 할 수 있게 해줌

public class StartGameManager : MonoBehaviour
{
    public GameObject ExitMenuObject;

    public GameObject SecondLock; //두번째 씬 가는 자물쇠 
    public GameObject ThirdLock;
    public GameObject FourthLock;
    public GameObject FifthLock;
    public GameObject SixthLock;
    int unlockNumber=1;

    //숫자에따라 자물쇠 이미지가 setoff되도록하라. 예를들어 1이라면 자물쇠이미지 2~6set on.아마 buutton의 코루틴만들어서 쓰는게좋을듯.
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)) //esc키나 안드로이드 모바일에서 뒤로가기 눌렀을 때, 모바일 왼쪽 상단 x표시에 버튼눌러도 ExitMenu뜨도록 했음
        {
            ExitMenu();
        }
    }

    public void ExitMenu() //학습을 종료하시겠습니까? 계속하기, 종료. 그 페이지 말하는 것
    {
        ExitMenuObject.SetActive(true);
    }
    public void GameExit()
    {
        Application.Quit(); //모바일 ,pc앱에선 가능하지만 Editor상에서는 안됨
    }
    public void GameContinue()
    {
        ExitMenuObject.SetActive(false);
    }

    public void UnLockButton()
    {
        //버튼누르면 unlockNumber 하나씩 증가. int 1이면 자물쇠 2~6잠금, 2면 3~6, 3은 4~6, 4는 5~6, 5는 6, 6이상은 다풀림.
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
