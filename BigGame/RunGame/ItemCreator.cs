using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour
{//���� ���̾ƿ� �� �̹��� ���, ���� ��üȭ
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
    [SerializeField] private Image transparent; // ���� ����

    [SerializeField] private RectTransform layoutRect;

    Queue<Item> AlpQueue = new Queue<Item>();// �÷��̾�� ���� ���ĺ� Ǯ��
    
    
    
    private Item item;
    public string correntString; //���� �������ִ� ���ڿ�
    public int[] checkingArray;//���� �񱳸� ���� ���� �迭 ���� ���ڸ� 1�ְ� ���ڿ� ���̶� ����.
    public int checkingSum;
    public Queue<Item> objQueue; //���ڵ� ����Ǵ� ť
    
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
        StartCoroutine(itemcreate); //�÷��̾ ���� �ܾ� ����

        yield return new WaitUntil(() =>correntString.Length == checkingSum);
        //gameCoroutinebool = false; //�ȵ� success�Լ����� �ذ������� ���� ������ �𸥴�.

        yield return StartCoroutine(ShowUI(i)); //�� ������ �ٽ� �����ֱ�

        
        StopCoroutine(itemcreate);
        //**end 
        foreach(Item item in objQueue)
        {//���� �ܾ� ������ Ǯ ��ü ���ֱ�
            Destroy(item.gameObject);
        }
        objQueue.Clear(); //�����ָ� ���� �߻�
        gameCoroutinebool = true;
        yield break;
    }
    
    private IEnumerator ShowUI(int i)
    {
        float temptime = Time.timeScale;
        Time.timeScale = 0f;
        var tempRunningSound = runningSound; //���� �޸��� �Ҹ� ����
        runningSound.mute = true;
        /*if (runningSound == null)
            tempRunningSound.clip = runSound;
        runningSound = null;*/
        anim.SetInteger("animation", 0);
        transparent.gameObject.SetActive(true);

        //Debug.Log(wordLayoutMaker.shown);
        
        //audioSource.PlayOneShot(electronicsPronunci[quizOrder[i]]); �Ʒ� �ڷ�ƾ���� �÷����ؾߵ�
        yield return StartCoroutine(wordLayoutMaker.MakeLayout(correntString, 2));

        if (checkingSum == correntString.Length)
        {
            //yield return new WaitUntil(() => !audioSource.isPlaying);
            audioSource.PlayOneShot(oneWordClear);
        }

        //�̹��� ���
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
            /*inputitem.transform.Rotate(new Vector3(0, Time.unscaledDeltaTime * 50, 0), Space.World); //ȸ��*/
            if (!gameCoroutinebool)
            {//gameCoroutine���� false�� ������ �ȵǴ� ���� ��ã�Ƽ� success�Լ����� ������.
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
                inputitem.gameObject.SetActive(false); //����
                yield break;
            }

        }

    }
    IEnumerator ItemCreate(string str)
    {
        objQueue = AlpPoolMaker(AlphaQueueMaker(str)); //���ĺ� ������ ť ���� �� ������Ʈ Ǯ��
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
                //objQueue.Enqueue(item); //���ڸ�
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
        int arrSize = inp.Length * 2; // ������ ������ �� ����
        char[] chars = new char[arrSize];
        int k = 0;
        foreach (char c in inp)
        {
            chars[k++] = c;
        }
        for (; k < arrSize; k++)
        {// ó�� �빮�ڰ� �ҹ��ڶ� �ߺ��ȵǰ� ����
            int temp = Random.Range('a', 'z');
            chars[k] = (char)temp;
            /*Debug.Log((char)(chars[0] -('A' - 'a')));*/
            while (temp == (int)(chars[0] - ('A' - 'a')))
            {
                temp = Random.Range('a', 'z');
                chars[k] = (char)temp;
            }
        }

        //���̱� ���� �빮�ڶ� ���� ���� �ҹ��� Ȯ��
        
        for (int i = 1; i < chars.Length; i++)
        {// �빮�� �ڿ� ���� �����ִ��� Ȯ��
            
            if (chars[0].ToString().ToLower()[0] == chars[i])
            {
                runPlayer.firstCharChecking = true;
                break;
            }  
        }

        for (int i = 0; i < chars.Length; i++)
        {// ���� ���� ���� �÷��̾�� ���� ���ڿ� ����
            /*Debug.Log(arr.Length);*/
            int temp = Random.Range(0, arrSize);
            char arrtemp = chars[i];
            chars[i] = chars[temp];
            chars[temp] = arrtemp;

        }
        //Debug.Log(string.Join("", chars));



        Queue<char> alphaQueue = new Queue<char>(); //������ �ܾ�� ���� ������ deque

        foreach (char i in chars)
        {//ť ����
            alphaQueue.Enqueue(i);
            /*Debug.Log(i);*/
        }

        //ť ���
        int Queuesize = alphaQueue.Count;

        for (int i = 0; i < Queuesize; i++)
        {//���� ť���� ���ĺ� ������
            var temp = alphaQueue.Dequeue();//��������
            alphaQueue.Enqueue(temp); //ó�� ���·� �����
        }

        return alphaQueue;

    }
    Queue<Item> AlpPoolMaker(Queue<char> objects)
    {
        int size = objects.Count;
        for (int i = 0; i < size; i++)
        {
            var newobj = Instantiate(AlphabetArr[WordMatcher(objects.Dequeue())]).GetComponent<Item>();//���� ���ڰ� ����� objects�� ��üȭ
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
        {//�ҹ����̸�
            order = (int)(c - 'a') + 26;
            /*Debug.Log("�ҹ���" + order);*/
        }
        else if ('A' <= c && c <= 'Z')
        {//�빮�� �̸�
            order = (int)(c - 'A');
            /*Debug.Log("�빮��" + order);*/
        }
        return order;
    }


    //public IEnumerator CompareString(char inp)
    public void CompareString(char inp)
    {
        int protemp = WordMatcher(inp);

        if (audioSource.isPlaying) //�Ҹ����� ������ 
        {
            Debug.Log(Time.timeScale); //1.5 �̻�
            alreadyeating = true;
            if(Time.timeScale> 1.5f)
            {
                gameObject.GetComponent<AudioSource>().enabled = false;
                gameObject.GetComponent<AudioSource>().enabled = true;
            }
                
        }
        else 
            alreadyeating=false;

        if (protemp > 25)//�ҹ���
            audioSource.PlayOneShot(AlphabetPronunci[protemp - 26]);
        else//�빮��
            audioSource.PlayOneShot(AlphabetPronunci[protemp]);
        
        //Debug.Log(inp);
        LayoutCor = wordLayoutMaker.ShowLayout();
        for (int i = 0; i < correntString.Length; i++)
        {
            if (inp == correntString[i])
            {// ���� ��
                runningSound.clip = runSound; //�޸��� ȿ����
                runningSound.Play();
                if (checkingArray[i] != 1)
                {// Ȯ�� �� �� ���ڸ� 
                    checkingArray[i] = 1;
                    checkingSum += 1;
                    //Debug.Log(i + "��° ����");
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
                {// Ȯ�ε� ���ڸ� checkingArray[i] =1 
                    int k = i + 1;
                    for (; k < correntString.Length; k++)
                    {//�ڿ��� ���� ���� �ִ��� Ȯ��
                        if (correntString[k] == inp)
                        {
                            //Debug.Log("�ڿ� �ߺ��� ���� ����");
                            k = 0; //�������� �ߺ��Ǵ� ���� ����ؼ� ������
                            break;
                        }
                    }
                    if (k == correntString.Length)
                    {// �� Ȯ�������� �ߺ� ���ڰ� ���� ���
                     //Debug.Log("�̹� Ȯ�ε� " + i + "��° ����");
                     
                        
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
            {//�ٸ� �� Ȯ�α��� �� ������
                
                Time.timeScale *= 0.5f;
                runningSound.pitch *= 0.95f;
                //Time.timeScale = 0.4f;
                if (Time.timeScale < 0.6f)
                {
                    Time.timeScale = 0.6f;
                    /*runningSound.clip = walkSound; //�ȱ� ȿ����
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
        
        if (preLayoutCor != null)// ������ ���̾ƿ��� ������ 
            StopCoroutine(preLayoutCor); //�� ���̾ƿ��� ����
        
        StartCoroutine(LayoutCor); //���̾ƿ� ���
        preLayoutCor = LayoutCor; //����� ���̾ƿ��� �ڷ�ƾ�� ���� ���� ���
        
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
