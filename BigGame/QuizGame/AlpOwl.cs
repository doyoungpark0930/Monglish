using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AlpOwl : MonoBehaviour
{
    [SerializeField] private GameObject[] owls= new GameObject[3]; 
    [SerializeField] private GameObject[] monkeys= new GameObject[2]; 
     

    [SerializeField] public Text[] owltexts = new Text[3]; //부엉이 알파벳
    [SerializeField] GameObject backImage; //글자 배경
    [SerializeField] ImageBox picFrame; //사진 저장된 곳
    [SerializeField] ResultTextImage resTextImage; //채점 텍스트이미지 출력
    [SerializeField] AudioController audioController; //오디오 컨트롤러
    
    [SerializeField] Camera quizCamera;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera monkeyCamera;
    [SerializeField] RewardCamera rewardCamera;

    [SerializeField] Button[] buttons = new Button[3];
    [SerializeField] TreeMonkey treeMokey;

    AudioSource currentAudio;
    Animator []owlAnim = new Animator[3];
    Animator[] monkeyAnim = new Animator[2];
    
    int quizNum = 4;
    public int correntquiz = 0; //현재 풀고있는 문제 번호
    [SerializeField] bool buttonOn = false;
    string[] words;
    int[] quizOrder; //words에서 퀴즈에 사용할 단어의 순서를 저장하는 배열 start에서 초기화
    
    RectTransform frameRect;
    Vector3 initFrameScale;
    Vector2 initFramePosition;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        words = new string[] {"Bear", "Dear", "Dog", "Elephant", "Frog"
        ,"Mouse", "Octopus", "Owl", "Panda", "Penguin", "Rabbit", "Seal", "Tiger"};

        quizOrder = new int[quizNum];

        for (int i = 0; i < quizNum; i++)
        {
            quizOrder[i] = (words.Length - Random.Range(1, words.Length + 1)); //1부터 words.length까지 -> 0~words.Length-1까지
            for (int j = 0; j < i; j++)
                if (quizOrder[j] == quizOrder[i])
                {
                    i--;
                    break;

                }
        }

        currentAudio = audioController.GetComponent<AudioSource>();

        quizCamera = quizCamera.GetComponent<Camera>();
        for (int i = 0; i < owls.Length; i++)
            owlAnim[i] = owls[i].GetComponent<Animator>();

        for (int i = 0; i < monkeys.Length; i++)
            monkeyAnim[i] = monkeys[i].GetComponent<Animator>();

        frameRect = picFrame.GetComponent<RectTransform>(); // 프레임의 rect transform

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].GetComponent<Button>();

        initFrameScale = frameRect.localScale;
        initFramePosition = frameRect.anchoredPosition;


        picFrame.gameObject.SetActive(false);


        yield return new WaitForSeconds(2f);
        StartCoroutine(AlpOwlController(quizOrder[correntquiz])); 
    }
    IEnumerator AlpOwlController(int i)
    {
        if (correntquiz == quizNum) yield break;
        yield return StartCoroutine(CallWord(i)); // CallWord() 끝날때까지 대기
        yield return StartCoroutine(OwlAlp(words[i]));
        yield return StartCoroutine(Quiz());
        
        //Result(true);
    }
    private IEnumerator CallWord(int i)
    {//사진하고 발음

        picFrame.gameObject.SetActive(true);
        picFrame.ChangeImage(i);
        picFrame.OnImage();
        owltexts[1].text = words[i];
        backImage.gameObject.SetActive(true); //글자 배경
        yield return new WaitUntil(() => !currentAudio.isPlaying); //재생 중이지 않으면
            audioController.playAnimals(quizOrder[correntquiz]);
        
        yield return new WaitForSeconds(1.5f);
        
        //
        
        while (frameRect.anchoredPosition.y < 90 || frameRect.localScale.y >initFrameScale.y/2)
        {//이미지 축소 및 이동
            //Debug.Log(Time.deltaTime *50f);
            yield return new WaitForFixedUpdate();
            if (frameRect.localScale.y >initFrameScale.y/2)
                frameRect.localScale /= Time.deltaTime * 50.7f;
            //frameRect.localScale /= 1.015f; 
            if (frameRect.anchoredPosition.y < 90)
                frameRect.anchoredPosition += new Vector2(0, 100* Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
            //yield return new WaitForFixedUpdate();
        }

        backImage.gameObject.SetActive(false);
        owltexts[1].text = "";
        
    }

    private IEnumerator OwlAlp(string inp)
    {// 부엉이가 알파벳 불러줌 끝나면 퀴즈 불러옴 <- 알파벳 읽어 주기 추가
        
        for (int m =0; m < 1; m++)
        {
            int k = 0;
            while (k < inp.Length)
            {
                for (int i = 0; i < owls.Length; i++)
                {
                    owlAnim[i].SetInteger("animation", 3);
                    owltexts[i].text = inp[k].ToString();

                    //Debug.Log(WordMatcher(inp[k]) + "번째 알파벳");
                    //yield return new WaitUntil(() => !currentAudio.isPlaying);
                    audioController.playAlp(WordMatcher(inp[k])); // 알파벳 발음 출력
                    yield return new WaitForSeconds(1f);
                    owltexts[i].text = "";
                    owlAnim[i].SetInteger("animation", 0);
                    k++;
                    if (k == inp.Length)
                        break;
                }
            }
            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 3);

            owltexts[1].text = inp;
            backImage.gameObject.SetActive(true);
            //yield return new WaitUntil(() => !currentAudio.isPlaying);
            audioController.playAnimals(quizOrder[correntquiz]);
            
            yield return new WaitForSeconds(2f);
            backImage.gameObject.SetActive(false);
            owltexts[1].text = "";

            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 0);

           
            yield return new WaitForSeconds(1f);
            
        }
        
    }
    private IEnumerator Quiz()
    {// 플레이어가 단어를 고르는 퀴즈 불러옴
     // 맞추면 동물들이 점프하고 카메라 변경후 원숭이가 나무 올라감
       
        quizCamera.fieldOfView = 10f;
        //yield return new WaitUntil(() => !currentAudio.isPlaying);
        audioController.playGuessAns(); //맞춰봐 보이스

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.GetComponentInChildren<Text>().text = words[quizOrder[Random.Range(0, quizNum)]];
            for(int j = 0; j < i; j++) /// 중복 단어 있는지 확인
                if(buttons[j].gameObject.GetComponentInChildren<Text>().text ==
                    buttons[i].gameObject.GetComponentInChildren<Text>().text)
                {
                    i--;
                    break;
                }
            if (i == buttons.Length - 1)
                for (int k = 0; k < buttons.Length; k++)
                {// 버튼에 정답있는지 확인
                    if (buttons[k].gameObject.GetComponentInChildren<Text>().text == words[quizOrder[correntquiz]])
                        break;
                    if (k == buttons.Length - 1)
                        i = 0;
                }
                    
            buttons[i].gameObject.SetActive(true);
        }        
        yield return new WaitUntil(() => buttonOn );//ui에서 On Click()을 이용 누를때까지 대기
        if(currentAudio.isPlaying)
        {
            currentAudio.Stop();
        }
        buttonOn = false;

        
        GameObject gameObject = EventSystem.current.currentSelectedGameObject; //눌린 ui
        string ans = gameObject.GetComponentInChildren<Text>().text;
        
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(false);

        //yield return new WaitUntil(() => !currentAudio.isPlaying);
        picFrame.gameObject.SetActive(false); //사진 끄기   
        if (ans == words[quizOrder[correntquiz]])
            StartCoroutine(Result(true));
        else
            StartCoroutine(Result(false));

        quizCamera.fieldOfView = 60f;
    }
    private IEnumerator Result(bool b)
    {
        
        if (b)
        {//성공 글자 보여주고 원숭이 나무 올라감

            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 3);
            for (int i = 0; i < monkeys.Length; i++)
                monkeyAnim[i].SetInteger("animation", 3);

            StartCoroutine(resTextImage.CallResWord(true));
                
            yield return new WaitUntil(() => !currentAudio.isPlaying);

            mainCamera.gameObject.SetActive(true); //올빼미하고 같이 원숭이 보여줌
            quizCamera.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(2f);
            
            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 0);
            for (int i = 0; i < monkeys.Length; i++)
                monkeyAnim[i].SetInteger("animation", 0);

            monkeyCamera.gameObject.SetActive(true);// 나무에 올라가는 원숭이 보여줌
            mainCamera.gameObject.SetActive(false);
            yield return new WaitUntil(() => !currentAudio.isPlaying);
            audioController.playTreeAudio(correntquiz); //나무 올라가는 보이스

            yield return new WaitUntil(() => !currentAudio.isPlaying);
            yield return StartCoroutine(treeMokey.MoveMonkey());

            yield return new WaitUntil(() => !currentAudio.isPlaying);
            yield return new WaitForSeconds(1.5f);

            

            quizCamera.gameObject.SetActive(true); //퀴즈로 시야 바꿈
            monkeyCamera.gameObject.SetActive(false);

            
            frameRect.anchoredPosition = initFramePosition;
            frameRect.localScale = initFrameScale;
            ++correntquiz;
            if (correntquiz < quizNum)
            {
                StartCoroutine(AlpOwlController(quizOrder[correntquiz]));
            }

            else
            {
                rewardCamera.gameObject.SetActive(true);
                quizCamera.gameObject.SetActive(false);
                yield return new WaitUntil(() => !currentAudio.isPlaying);
                audioController.playClear_0();
                yield return  StartCoroutine(rewardCamera.Clear()); //애니메이션

                yield return new WaitUntil(() => !currentAudio.isPlaying);
                audioController.playClear_1();
                StartCoroutine(resTextImage.Clear()); //clear 출력
                //Debug.Log("Success");
            }
        }

        else
        {//실패 글자 보여주고 원숭이 슬퍼함
            //Debug.Log("false");
            currentAudio.Stop();
            audioController.playFail();

            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 8);
            for (int i = 0; i < monkeys.Length; i++)
                monkeyAnim[i].SetInteger("animation", 8);

            StartCoroutine(resTextImage.CallResWord(false));

            

            yield return new WaitUntil(() => !currentAudio.isPlaying);
            mainCamera.gameObject.SetActive(true); //올빼미하고 같이 원숭이 보여줌
            quizCamera.gameObject.SetActive(false);

            yield return new WaitForSeconds(2f);


            //yield return new WaitUntil(() => !currentAudio.isPlaying);
            audioController.playTreeWait();
            monkeyCamera.gameObject.SetActive(true);// 가만히 있는 원숭이 보여줌
            mainCamera.gameObject.SetActive(false);
            yield return new WaitUntil(() => !currentAudio.isPlaying);
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 0);
            for (int i = 0; i < monkeys.Length; i++)
                monkeyAnim[i].SetInteger("animation", 0);

            yield return new WaitUntil(() => !currentAudio.isPlaying);
            quizCamera.gameObject.SetActive(true); //퀴즈로 시야 바꿈
            monkeyCamera.gameObject.SetActive(false);

            StartCoroutine(AlpOwlController(quizOrder[correntquiz]));
   
        }
    }
    private void ButtonClicked()
    {
        buttonOn = true;
        
        //Debug.Log(buttonOn);
    }
    private int WordMatcher(char c)
    {// RunGame에서 가져옴

        /*string WordsArr = Words.ToString();*/
        int order = 0;
        if ('a' <= c && c <= 'z')
        {//소문자이면
            order = (int)(c - 'a');//+ 26;
            /*Debug.Log("소문자" + order);*/
        }
        else if ('A' <= c && c <= 'Z')
        {//대문자 이면
            order = (int)(c - 'A');
            /*Debug.Log("대문자" + order);*/
        }
        return order;
    }
}

