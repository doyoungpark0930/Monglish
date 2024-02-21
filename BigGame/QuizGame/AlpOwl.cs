using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AlpOwl : MonoBehaviour
{
    [SerializeField] private GameObject[] owls= new GameObject[3]; 
    [SerializeField] private GameObject[] monkeys= new GameObject[2]; 
     

    [SerializeField] public Text[] owltexts = new Text[3]; //�ξ��� ���ĺ�
    [SerializeField] GameObject backImage; //���� ���
    [SerializeField] ImageBox picFrame; //���� ����� ��
    [SerializeField] ResultTextImage resTextImage; //ä�� �ؽ�Ʈ�̹��� ���
    [SerializeField] AudioController audioController; //����� ��Ʈ�ѷ�
    
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
    public int correntquiz = 0; //���� Ǯ���ִ� ���� ��ȣ
    [SerializeField] bool buttonOn = false;
    string[] words;
    int[] quizOrder; //words���� ��� ����� �ܾ��� ������ �����ϴ� �迭 start���� �ʱ�ȭ
    
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
            quizOrder[i] = (words.Length - Random.Range(1, words.Length + 1)); //1���� words.length���� -> 0~words.Length-1����
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

        frameRect = picFrame.GetComponent<RectTransform>(); // �������� rect transform

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
        yield return StartCoroutine(CallWord(i)); // CallWord() ���������� ���
        yield return StartCoroutine(OwlAlp(words[i]));
        yield return StartCoroutine(Quiz());
        
        //Result(true);
    }
    private IEnumerator CallWord(int i)
    {//�����ϰ� ����

        picFrame.gameObject.SetActive(true);
        picFrame.ChangeImage(i);
        picFrame.OnImage();
        owltexts[1].text = words[i];
        backImage.gameObject.SetActive(true); //���� ���
        yield return new WaitUntil(() => !currentAudio.isPlaying); //��� ������ ������
            audioController.playAnimals(quizOrder[correntquiz]);
        
        yield return new WaitForSeconds(1.5f);
        
        //
        
        while (frameRect.anchoredPosition.y < 90 || frameRect.localScale.y >initFrameScale.y/2)
        {//�̹��� ��� �� �̵�
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
    {// �ξ��̰� ���ĺ� �ҷ��� ������ ���� �ҷ��� <- ���ĺ� �о� �ֱ� �߰�
        
        for (int m =0; m < 1; m++)
        {
            int k = 0;
            while (k < inp.Length)
            {
                for (int i = 0; i < owls.Length; i++)
                {
                    owlAnim[i].SetInteger("animation", 3);
                    owltexts[i].text = inp[k].ToString();

                    //Debug.Log(WordMatcher(inp[k]) + "��° ���ĺ�");
                    //yield return new WaitUntil(() => !currentAudio.isPlaying);
                    audioController.playAlp(WordMatcher(inp[k])); // ���ĺ� ���� ���
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
    {// �÷��̾ �ܾ ���� ���� �ҷ���
     // ���߸� �������� �����ϰ� ī�޶� ������ �����̰� ���� �ö�
       
        quizCamera.fieldOfView = 10f;
        //yield return new WaitUntil(() => !currentAudio.isPlaying);
        audioController.playGuessAns(); //����� ���̽�

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.GetComponentInChildren<Text>().text = words[quizOrder[Random.Range(0, quizNum)]];
            for(int j = 0; j < i; j++) /// �ߺ� �ܾ� �ִ��� Ȯ��
                if(buttons[j].gameObject.GetComponentInChildren<Text>().text ==
                    buttons[i].gameObject.GetComponentInChildren<Text>().text)
                {
                    i--;
                    break;
                }
            if (i == buttons.Length - 1)
                for (int k = 0; k < buttons.Length; k++)
                {// ��ư�� �����ִ��� Ȯ��
                    if (buttons[k].gameObject.GetComponentInChildren<Text>().text == words[quizOrder[correntquiz]])
                        break;
                    if (k == buttons.Length - 1)
                        i = 0;
                }
                    
            buttons[i].gameObject.SetActive(true);
        }        
        yield return new WaitUntil(() => buttonOn );//ui���� On Click()�� �̿� ���������� ���
        if(currentAudio.isPlaying)
        {
            currentAudio.Stop();
        }
        buttonOn = false;

        
        GameObject gameObject = EventSystem.current.currentSelectedGameObject; //���� ui
        string ans = gameObject.GetComponentInChildren<Text>().text;
        
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(false);

        //yield return new WaitUntil(() => !currentAudio.isPlaying);
        picFrame.gameObject.SetActive(false); //���� ����   
        if (ans == words[quizOrder[correntquiz]])
            StartCoroutine(Result(true));
        else
            StartCoroutine(Result(false));

        quizCamera.fieldOfView = 60f;
    }
    private IEnumerator Result(bool b)
    {
        
        if (b)
        {//���� ���� �����ְ� ������ ���� �ö�

            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 3);
            for (int i = 0; i < monkeys.Length; i++)
                monkeyAnim[i].SetInteger("animation", 3);

            StartCoroutine(resTextImage.CallResWord(true));
                
            yield return new WaitUntil(() => !currentAudio.isPlaying);

            mainCamera.gameObject.SetActive(true); //�û����ϰ� ���� ������ ������
            quizCamera.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(2f);
            
            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 0);
            for (int i = 0; i < monkeys.Length; i++)
                monkeyAnim[i].SetInteger("animation", 0);

            monkeyCamera.gameObject.SetActive(true);// ������ �ö󰡴� ������ ������
            mainCamera.gameObject.SetActive(false);
            yield return new WaitUntil(() => !currentAudio.isPlaying);
            audioController.playTreeAudio(correntquiz); //���� �ö󰡴� ���̽�

            yield return new WaitUntil(() => !currentAudio.isPlaying);
            yield return StartCoroutine(treeMokey.MoveMonkey());

            yield return new WaitUntil(() => !currentAudio.isPlaying);
            yield return new WaitForSeconds(1.5f);

            

            quizCamera.gameObject.SetActive(true); //����� �þ� �ٲ�
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
                yield return  StartCoroutine(rewardCamera.Clear()); //�ִϸ��̼�

                yield return new WaitUntil(() => !currentAudio.isPlaying);
                audioController.playClear_1();
                StartCoroutine(resTextImage.Clear()); //clear ���
                //Debug.Log("Success");
            }
        }

        else
        {//���� ���� �����ְ� ������ ������
            //Debug.Log("false");
            currentAudio.Stop();
            audioController.playFail();

            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 8);
            for (int i = 0; i < monkeys.Length; i++)
                monkeyAnim[i].SetInteger("animation", 8);

            StartCoroutine(resTextImage.CallResWord(false));

            

            yield return new WaitUntil(() => !currentAudio.isPlaying);
            mainCamera.gameObject.SetActive(true); //�û����ϰ� ���� ������ ������
            quizCamera.gameObject.SetActive(false);

            yield return new WaitForSeconds(2f);


            //yield return new WaitUntil(() => !currentAudio.isPlaying);
            audioController.playTreeWait();
            monkeyCamera.gameObject.SetActive(true);// ������ �ִ� ������ ������
            mainCamera.gameObject.SetActive(false);
            yield return new WaitUntil(() => !currentAudio.isPlaying);
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < owls.Length; i++)
                owlAnim[i].SetInteger("animation", 0);
            for (int i = 0; i < monkeys.Length; i++)
                monkeyAnim[i].SetInteger("animation", 0);

            yield return new WaitUntil(() => !currentAudio.isPlaying);
            quizCamera.gameObject.SetActive(true); //����� �þ� �ٲ�
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
    {// RunGame���� ������

        /*string WordsArr = Words.ToString();*/
        int order = 0;
        if ('a' <= c && c <= 'z')
        {//�ҹ����̸�
            order = (int)(c - 'a');//+ 26;
            /*Debug.Log("�ҹ���" + order);*/
        }
        else if ('A' <= c && c <= 'Z')
        {//�빮�� �̸�
            order = (int)(c - 'A');
            /*Debug.Log("�빮��" + order);*/
        }
        return order;
    }
}

