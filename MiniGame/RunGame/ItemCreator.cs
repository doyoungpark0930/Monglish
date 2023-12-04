using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour
{//글자 레이아웃 및 이미지 출력, 문자 객체화
    public GameObject[] AlphabetArr = new GameObject[52];
    [SerializeField] private string[] electronicProd = {"Smartphone", "Laptop", "Microwave", "Vacuum", "Television",
        "Refrigerator", "Camera", "Oven", "Hairdryer", "Earphones","Desktop" }; //"Monitor",

    public AudioSource audioSource;
    public AudioSource runningSound;
    public AudioClip[] AlphabetPronunci = new AudioClip[26];
    public AudioClip[] electronicsPronunci;
    public AudioClip alreadyEaten;
    public AudioClip notInclude;
    public AudioClip oneWordClear;
    //public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip readyGoSound;


    [SerializeField] private Sprite[] itemImage;
    [SerializeField] private Image itemUIImage;
    [SerializeField] private GameObject itemImageLayout;
    [SerializeField] private Image transparent; // 투명도 조절

    [SerializeField] private RectTransform layoutRect;

    Queue<Item> AlpQueue = new Queue<Item>();// 플레이어에게 보낼 알파벳 풀링
    
    
    
    private Item item;
    public string correntString; //현재 보내고있는 문자열
    public int[] checkingArray;//문자 비교를 위해 만든 배열 같은 문자면 1넣고 문자열 길이랑 비교함.
    public int checkingSum;
    public Queue<Item> objQueue; //문자들 저장되는 큐
    
    [SerializeField] 
    private WordLayoutMaker wordLayoutMaker;
    private IEnumerator LayoutCor;
    private IEnumerator preLayoutCor;
        
    public int[] quizOrder;
    public int correntOrder= 0;
    private Vector3 initSize;
    [SerializeField] Vehicle vehicle;
    [SerializeField] private GameObject monkey;
    private Animator anim;
    private bool gameCoroutinebool = true;
    [SerializeField] RunPlayer runPlayer;

    bool alreadyeating =false;

    /*private GameObject ItemPrefab;*/
    IEnumerator Start()
    {
        runningSound = GetComponentsInChildren <AudioSource>()[1];
        //Debug.Log(runningSound);
        runningSound.clip = runSound;
        runningSound.loop = true;
        audioSource = GetComponent<AudioSource>();
        anim = monkey.GetComponent<Animator>();
        initSize = layoutRect.localScale;
        Time.timeScale = 0f;

        quizOrder = new int[electronicProd.Length];

        for (int i=0; i< electronicProd.Length; i++)
        {
            quizOrder[i] = Random.Range(0, electronicProd.Length);
            
            for(int k =0; k< i; k++)
            {
                if (i > 0 && quizOrder[i].Equals(quizOrder[k]))
                {
                    i--;
                    break;
                }                
            }   
        }
        /*foreach (int n in quizOrder)
            Debug.Log(n);*/
        audioSource.PlayOneShot(readyGoSound);
        yield return new WaitUntil(() => !audioSource.isPlaying);

        for (correntOrder =0; correntOrder < 2; correntOrder++)
            yield return StartCoroutine(GameCoroutin(correntOrder));
        StopAllCoroutines();
        runningSound.pitch = 0.5f;
        yield return StartCoroutine(vehicle.TakeMonkey());
        Time.timeScale = 1f;
        StopAllCoroutines();
        yield break;
    }
    
    IEnumerator GameCoroutin(int i)
    {
        //**init
       
        correntString = electronicProd[quizOrder[i]];
        checkingArray = new int[correntString.Length];
        checkingSum = 0;
        
        yield return StartCoroutine(ShowUI(i));

        var itemcreate = ItemCreate(correntString);
        StartCoroutine(itemcreate); //플레이어가 먹을 단어 생성

        yield return new WaitUntil(() =>correntString.Length == checkingSum);
        //gameCoroutinebool = false; //안됨 success함수에서 해결했으나 아직 이유를 모른다.

        yield return StartCoroutine(ShowUI(i)); //다 모으고 다시 보여주기

        
        StopCoroutine(itemcreate);
        //**end 
        foreach(Item item in objQueue)
        {//먹을 단어 저장한 풀 객체 없애기
            Destroy(item.gameObject);
        }
        objQueue.Clear(); //안해주면 오류 발생
        gameCoroutinebool = true;
        yield break;
    }
    
    private IEnumerator ShowUI(int i)
    {
        float temptime = Time.timeScale;
        Time.timeScale = 0f;
        var tempRunningSound = runningSound; //이전 달리기 소리 저장
        runningSound.mute = true;
        /*if (runningSound == null)
            tempRunningSound.clip = runSound;
        runningSound = null;*/
        anim.SetInteger("animation", 0);
        transparent.gameObject.SetActive(true);

        //Debug.Log(wordLayoutMaker.shown);
        
        //audioSource.PlayOneShot(electronicsPronunci[quizOrder[i]]); 아래 코루틴에서 플레이해야됨
        yield return StartCoroutine(wordLayoutMaker.MakeLayout(correntString, 2));

        if (checkingSum == correntString.Length)
        {
            //yield return new WaitUntil(() => !audioSource.isPlaying);
            audioSource.PlayOneShot(oneWordClear);
        }

        //이미지 출력
        itemUIImage.sprite = itemImage[quizOrder[i]];
        itemImageLayout.gameObject.SetActive(true);
        while (layoutRect.localScale.x < 9f)
        {
            layoutRect.localScale *= 1.03f;
            yield return new WaitForSecondsRealtime(0.02f);
        }

        audioSource.PlayOneShot(electronicsPronunci[quizOrder[i]]);
        yield return new WaitForSecondsRealtime(2f);

        transparent.gameObject.SetActive(false);
        itemImageLayout.gameObject.SetActive(false);
        layoutRect.localScale = initSize;

        /*runningSound = tempRunningSound;
        runningSound.Play();*/
        runningSound.mute = false;
        runningSound.Play();
        anim.SetInteger("animation", 2);
        if (temptime != 0f)
            Time.timeScale = temptime;
        else
            Time.timeScale = 1f;
        
        yield break;
    }

    IEnumerator MoveItem(Item inputitem)
    {

        while (true)
        {
            yield return new WaitForFixedUpdate();
            /*inputitem.transform.Rotate(new Vector3(0, Time.unscaledDeltaTime * 50, 0), Space.World); //회전*/
            if (!gameCoroutinebool)
            {//gameCoroutine에서 false로 했으나 안되는 원인 못찾아서 success함수에서 변경함.
                Destroy(inputitem.gameObject);
                yield break;
            }
            if (inputitem == null)
            {
                //Debug.Log("error");
                yield break;
            }
            inputitem.transform.Translate(0, 0, -10 * Time.deltaTime, Space.World);
            if(runPlayer.doNotEnqueue)
            {
                Destroy(inputitem.gameObject);
                runPlayer.doNotEnqueue = false;
            }
                

            else if (inputitem.transform.position.z<-10)
            {
                objQueue.Enqueue(inputitem);
                inputitem.gameObject.SetActive(false); //끄기
                yield break;
            }

        }

    }
    IEnumerator ItemCreate(string str)
    {
        objQueue = AlpPoolMaker(AlphaQueueMaker(str)); //알파벳 저장한 큐 생성 및 오브젝트 풀링
        int size = objQueue.Count;
        while (true)
        {
            for (int i = 0; i < size; i++)
            {
                yield return new WaitForSeconds(2f);
                item = objQueue.Dequeue();  
                item.gameObject.SetActive(true);
                //Rigidbody rg = item[i].GetComponent<Rigidbody>();
                item.transform.position = new Vector3(7f * Random.Range(-1, 2), 2, 50);

                StartCoroutine(MoveItem(item));
                if (runPlayer.doNotEnqueue)
                    size--;
                //objQueue.Enqueue(item); //제자리
            }

        }
    }

    IEnumerator ItemReturn(Item item)
    {
        /*yield return new WaitForSeconds(6f);*/
        item.gameObject.SetActive(false);
        yield break;
    }
   
    public Queue<char> AlphaQueueMaker(string inp)
    {
        int arrSize = inp.Length * 2; // 보내줄 문자의 총 길이
        char[] chars = new char[arrSize];
        int k = 0;
        foreach (char c in inp)
        {
            chars[k++] = c;
        }
        for (; k < arrSize; k++)
        {// 처음 대문자가 소문자랑 중복안되게 만듦
            int temp = Random.Range('a', 'z');
            chars[k] = (char)temp;
            /*Debug.Log((char)(chars[0] -('A' - 'a')));*/
            while (temp == (int)(chars[0] - ('A' - 'a')))
            {
                temp = Random.Range('a', 'z');
                chars[k] = (char)temp;
            }
        }

        //섞이기 전에 대문자랑 같은 순서 소문자 확인
        
        for (int i = 1; i < chars.Length; i++)
        {// 대문자 뒤에 같은 글자있는지 확인
            
            if (chars[0].ToString().ToLower()[0] == chars[i])
            {
                runPlayer.firstCharChecking = true;
                break;
            }  
        }

        for (int i = 0; i < chars.Length; i++)
        {// 글자 순서 섞기 플레이어에게 보낼 문자열 생성
            /*Debug.Log(arr.Length);*/
            int temp = Random.Range(0, arrSize);
            char arrtemp = chars[i];
            chars[i] = chars[temp];
            chars[temp] = arrtemp;

        }
        //Debug.Log(string.Join("", chars));



        Queue<char> alphaQueue = new Queue<char>(); //보내줄 단어들 생성 먹으면 deque

        foreach (char i in chars)
        {//큐 생성
            alphaQueue.Enqueue(i);
            /*Debug.Log(i);*/
        }

        //큐 사용
        int Queuesize = alphaQueue.Count;

        for (int i = 0; i < Queuesize; i++)
        {//만든 큐에서 알파벳 보내기
            var temp = alphaQueue.Dequeue();//내보내기
            alphaQueue.Enqueue(temp); //처음 상태로 만들기
        }

        return alphaQueue;

    }
    Queue<Item> AlpPoolMaker(Queue<char> objects)
    {
        int size = objects.Count;
        for (int i = 0; i < size; i++)
        {
            var newobj = Instantiate(AlphabetArr[WordMatcher(objects.Dequeue())]).GetComponent<Item>();//보낼 문자가 저장된 objects로 객체화
            newobj.gameObject.SetActive(false);
            AlpQueue.Enqueue(newobj);
        }
        return AlpQueue;
    }
    int WordMatcher(char c)
    {// 
       
        /*string WordsArr = Words.ToString();*/
        int order= 0;
        if ('a' <= c && c <= 'z')
        {//소문자이면
            order = (int)(c - 'a') + 26;
            /*Debug.Log("소문자" + order);*/
        }
        else if ('A' <= c && c <= 'Z')
        {//대문자 이면
            order = (int)(c - 'A');
            /*Debug.Log("대문자" + order);*/
        }
        return order;
    }


    //public IEnumerator CompareString(char inp)
    public void CompareString(char inp)
    {
        int protemp = WordMatcher(inp);

        if (audioSource.isPlaying) //소리나고 있을때 
        {
            Debug.Log(Time.timeScale); //1.5 이상
            alreadyeating = true;
            if(Time.timeScale> 1.5f)
            {
                gameObject.GetComponent<AudioSource>().enabled = false;
                gameObject.GetComponent<AudioSource>().enabled = true;
            }
                
        }
        else 
            alreadyeating=false;

        if (protemp > 25)//소문자
            audioSource.PlayOneShot(AlphabetPronunci[protemp - 26]);
        else//대문자
            audioSource.PlayOneShot(AlphabetPronunci[protemp]);
        
        //Debug.Log(inp);
        LayoutCor = wordLayoutMaker.ShowLayout();
        for (int i = 0; i < correntString.Length; i++)
        {
            if (inp == correntString[i])
            {// 같을 때
                runningSound.clip = runSound; //달리기 효과음
                runningSound.Play();
                if (checkingArray[i] != 1)
                {// 확인 안 된 문자면 
                    checkingArray[i] = 1;
                    checkingSum += 1;
                    //Debug.Log(i + "번째 문자");
                    anim.SetInteger("animation", 2);

                    if(runningSound.pitch <0.9f)
                        runningSound.pitch = 0.9f;
                    else
                    {
                        runningSound.pitch *= 1.03f;
                        if(runningSound.pitch >1.1f)
                            runningSound.pitch = 1.1f;
                    }

                    if (Time.timeScale < 1f)
                        Time.timeScale = 1f;

                    else if (Time.timeScale < 1.8f)
                        Time.timeScale *= 1.1f;

                    wordLayoutMaker.SetPregfabAlphaval(i, 1);

                    break;
                }
                else
                {// 확인된 문자면 checkingArray[i] =1 
                    int k = i + 1;
                    for (; k < correntString.Length; k++)
                    {//뒤에도 같은 문자 있는지 확인
                        if (correntString[k] == inp)
                        {
                            //Debug.Log("뒤에 중복된 문자 있음");
                            k = 0; //마지막에 중복되는 것을 고려해서 값변경
                            break;
                        }
                    }
                    if (k == correntString.Length)
                    {// 다 확인했지만 중복 문자가 없는 경우
                     //Debug.Log("이미 확인된 " + i + "번째 문자");
                     
                        
                        if (Time.timeScale < 1f)
                        {
                            Time.timeScale = 1f;
                            runningSound.pitch = 0.9f; 
                        }    
                        else
                        {
                            Time.timeScale *= 0.8f;
                            runningSound.pitch *= 0.97f;
                        }
                            
                        anim.SetInteger("animation", 2);
                        //yield return new WaitUntil(() => !audioSource.isPlaying);
                        /*yield return new WaitForSeconds(0.5f);
                        audioSource.PlayOneShot(alreadyEaten);*/
                        StartCoroutine(DelayAudioPlayer(alreadyEaten));
                        break;
                    }
                }
            }
            else if (i == correntString.Length - 1)
            {//다를 때 확인까지 다 했으면
                
                Time.timeScale *= 0.5f;
                runningSound.pitch *= 0.95f;
                //Time.timeScale = 0.4f;
                if (Time.timeScale < 0.6f)
                {
                    Time.timeScale = 0.6f;
                    /*runningSound.clip = walkSound; //걷기 효과음
                    runningSound.Play();
                    runningSound.Play();*/
                    runningSound.pitch = 0.5f;
                    anim.SetInteger("animation", 1);
                    /*if (Time.timeScale < 0.4f)
                        Time.timeScale = 0.4f;*/
                }

                //Debug.Log("Wrong");
                //yield return new WaitUntil(() => !audioSource.isPlaying);
                //yield return new WaitForSeconds(0.5f);
                //audioSource.PlayOneShot(notInclude);
                StartCoroutine(DelayAudioPlayer(notInclude));
                /*audioSource.clip = notInclude;
                audioSource.Play();*/
            }
        }
        
        if (preLayoutCor != null)// 이전에 레이아웃이 있으면 
            StopCoroutine(preLayoutCor); //그 레이아웃을 종료
        
        StartCoroutine(LayoutCor); //레이아웃 출력
        preLayoutCor = LayoutCor; //사용한 레이아웃을 코루틴을 끄기 위해 기록
        
        if (checkingSum == correntString.Length)
        {
            Success();
            //yield return new WaitUntil(() => !audioSource.isPlaying);
            //audioSource.PlayOneShot(oneWordClear);
        }
        //yield return new WaitForFixedUpdate();
    }
    
    private IEnumerator DelayAudioPlayer(AudioClip clip)
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(clip);
    }
    public void Success()
    {
        //wordLayoutMaker.shown = false;
        //Debug.Log("Sucess: " + correntString);
        
        gameCoroutinebool = false;
        wordLayoutMaker.DestroyLayout();
    }

}
