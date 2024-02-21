using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{

    [SerializeField] private GameObject rocketMonkey;
    [SerializeField] private Camera cam;
    [SerializeField] private Image backgimg;
    [SerializeField] private GameObject groundMonkey;
    //[SerializeField] private GameObject torch;
    [SerializeField] private ParticleSystem explosion;

    [SerializeField] ExitManagerJumpGame ExitManager;
    bool ExitManagerOn = false;

    private AudioSource audioSource;
    //private ParticleSystem explosion;
    private Animator anim;
    private Rigidbody rb;
    private Rigidbody Rig; //�� ������
    private bool lastEvent =false;
    private ParticleSystem.MainModule psMain;

    [SerializeField] AudioClip [] rocketVoices;
    bool rocketVoicebool =false;
    
    public AudioClip takeSound;
    public AudioClip LandingSound;
    public AudioClip OperatingSound;

   
    bool takemonkey = false;
    bool landing = true;
    //bool operating = false;
    bool goToGarden = false;
    bool firstLanding = true;
    bool audioPlayed = false;
    void Start()
    {

        anim = groundMonkey.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Rig = groundMonkey.GetComponent<Rigidbody>();
        psMain = explosion.main;
        audioSource.clip = LandingSound;
        audioSource.Play();
        StartCoroutine(Launch());
        StartCoroutine(AudioCor());
        StartCoroutine(RocketVoicer());
    }


    void Update()
    {
        /*if (operating)
            audioSource.clip = OperatingSound;*/

        if (transform.position.y > 30f && transform.position.y < 31f && groundMonkey.activeSelf)
        {
            psMain.gravityModifier = 1.5f;
        }

        if (transform.position.y < 9f)
        {
            //Debug.Log(transform.position.y);
            //audioSource.mute = true;
            landing = false;
            
            explosion.gameObject.SetActive(false);
            if (groundMonkey.activeSelf)
            {
                rb.velocity = Vector3.zero;
                /*if (!takemonkey)
                    audioSource.PlayOneShot(takeSound);*/
                if(!firstLanding)
                    StartCoroutine(MonkeyLauncher());
            }
        }
                
        if (transform.position.y >9f && transform.position.y < 10f && !groundMonkey.activeSelf)
        {
            //Debug.Log(transform.position.y);
            landing = true;
            explosion.gameObject.SetActive(true);
            psMain.gravityModifier = 1.5f;
         }

        if (transform.position.y <50 && transform.position.y>48 && !audioPlayed )
        {
            landing = false;
            audioPlayed = true;
            psMain.gravityModifier = 3.5f;
            cam.orthographicSize = 20;
            cam.transform.position = transform.position + new Vector3(0, 20, -50);
            StartCoroutine(FadeOut()); //�ϴú��̽�
            StartCoroutine(FadeIn());
        }
        if (transform.position.y < 110 && transform.position.y > 108 && !audioPlayed)
        {
            audioPlayed = true;
            cam.transform.position = transform.position + new Vector3(0, 20, -50);
            StartCoroutine(FadeOut()); // ���� ���̽�
            lastEvent = true;
            StartCoroutine(FadeIn());
            
        }
        if(transform.position.y > 180)
        {
            goToGarden = true;
        }
        if(goToGarden)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
            //Debug.Log("����");
            if (!ExitManagerOn) { ExitManagerOn = true; ExitManager.MonkeyGardenScene(); }
        }

    }
    private IEnumerator RocketVoicer()
    {
        int i = 0;
        while(!goToGarden)
        {
            if (rocketVoicebool)
            {
                Debug.Log(i);
                audioSource.PlayOneShot(rocketVoices[i++]);
                //yield return new WaitUntil(() => !audioSource.isPlaying);
                rocketVoicebool = false;
            }
            yield return new WaitUntil(() => rocketVoicebool);
        }
        
    }
    private IEnumerator AudioCor()
    {
        while(true)
        {
            if (takemonkey)
            {//ſ����
                if (landing)
                    audioSource.clip = LandingSound;
                else
                    audioSource.clip = OperatingSound;
            }
            else if(!landing)
            { //�� Ÿ�� ������ �ƴҶ�
                audioSource.clip = takeSound;
            }
            yield return new WaitForFixedUpdate();
        }
        
    }

    public IEnumerator MonkeyLauncher()
    {
        //yield return new WaitForSeconds(1f);
        //anim.Play("Jump", -1, 0);
        //yield return new WaitUntil(() => !audioSource.isPlaying);
        takemonkey = true;
        anim.SetInteger("animation", 3);
        Rig.AddForce(new Vector3(30, 20, 0));
        yield return new WaitUntil(() => groundMonkey.transform.position.x > 0f);
        groundMonkey.SetActive(false);
    }
    private IEnumerator Launch()
    {
        /*float temp = 2.5f;
        psMain.gravityModifier = 1f;
        temp -= 0.1f;*/
        /*psMain.gravityModifier = new ParticleSystem.MainModule().gravityModifier;
        psMain.gravityModifier = 0f;*/
        //audioSource.mute = false;


        //audioSource.loop = true;
/*        gameObject.GetComponent<AudioSource>().enabled = false;
        gameObject.GetComponent<AudioSource>().enabled = true;*/
        audioSource.loop = true;
        rb.AddForce(Vector3.down * 500);
        rocketVoicebool = true; //�����ö�
        /*        for (int i = 0; i < 25; i++)
                {

                    yield return new WaitForSecondsRealtime(0.1f);
                }*/
        //torch.gameObject.SetActive(false); 
        //explosion.main.gravityModifier -= 0.1f;
        //yield return new WaitUntil(() => rb.velocity == Vector3.zero); // ����
        yield return new WaitUntil(() => rb.transform.position.y < 10f);
        yield return new WaitUntil(() => !rocketVoicebool);

        rocketVoicebool = true; //�� �������� Ÿ����
        yield return new WaitUntil(() => !audioSource.isPlaying);
        yield return new WaitUntil(() => !rocketVoicebool);

        audioSource.PlayOneShot(takeSound);
        firstLanding = false;
        yield return new WaitUntil(() => !audioSource.isPlaying);
        
        
        //yield return new WaitUntil(() => !audioSource.isPlaying);

        //yield return new WaitForSecondsRealtime(3f);

        //audioSource.Stop();
        yield return new WaitUntil(() => groundMonkey.transform.position.x > 0f);
        yield return new WaitForSeconds(1f);
        rocketMonkey.SetActive(true);

        rocketVoicebool = true; //�߻� ī��Ʈ
        //yield return new WaitUntil(() => !audioSource.isPlaying);
        yield return new WaitForSecondsRealtime(3.5f);

       
        //audioSource.Play(); ?? �� ��
        audioSource.PlayOneShot(LandingSound);
        rb.AddForce(Vector3.up * 500);
        
        yield return new WaitForSeconds(3f);
        //audioSource.mute = true;
        
    }
    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3.5f);
        


        //audioSource.mute = false;
        backgimg.gameObject.SetActive(true);
        Color backgColor = backgimg.color;

        backgimg.color = backgColor;
        backgColor.a = 0f;
        backgimg.color = backgColor; //0���� ���� �ʱ�ȭ

        

        if (lastEvent)
        {
            while (audioSource.volume > 0f)
            {
                audioSource.volume -= 0.01f;
                //Debug.Log(backgColor.a);
                backgimg.color = backgColor;
                yield return new WaitForFixedUpdate();

            }
            yield return new WaitForSeconds(2f);
            
        }


        while (backgColor.a < 1f)
        {//backgimg.color.a <1f) <= 1���� �Ȱ��� ���� �߻� ȭ���� ��� Ŭ���ϸ� fixedUpdate�� �Ѿ�� ����� ����
         //�ڷ�ƾ�� ���ư����� Ȯ���ϱ����� fading�� ����Ͽ���
            backgColor.a += 0.02f;
            if(audioSource.volume != 0f)
                audioSource.volume -= 0.01f;
            //Debug.Log(backgColor.a);
            backgimg.color = backgColor;
            yield return new WaitForFixedUpdate();
            
        }
        audioPlayed = false;
        audioSource.Stop();
        if(lastEvent)
            Debug.Log("����");
    }
    public IEnumerator FadeOut()
    {
        /*transform.GetComponent<AudioSource>().enabled = false;
        transform.GetComponent<AudioSource>().enabled = true;*/
        //audioSource.Stop();
        rocketVoicebool = true;
        /*audioSource.clip = OperatingSound;
        audioSource.Play();*/
        

        audioSource.volume = 1f;
        audioSource.clip = OperatingSound;
        audioSource.Play();
        backgimg.gameObject.SetActive(true);
        Color backgColor = backgimg.color;
        
        

        backgimg.color = backgColor;
        backgColor.a = 1f;
        backgimg.color = backgColor; //0���� ���� �ʱ�ȭ

        while (backgColor.a >0f)
        {//backgimg.color.a <1f) <= 1���� �Ȱ��� ���� �߻� ȭ���� ��� Ŭ���ϸ� fixedUpdate�� �Ѿ�� ����� ����
         //�ڷ�ƾ�� ���ư����� Ȯ���ϱ����� fading�� ����Ͽ���
            backgColor.a -= 0.01f;
            //Debug.Log(backgColor.a);
            backgimg.color = backgColor;
            yield return new WaitForFixedUpdate();
        }
        
        //audioSource.mute = true;

    }
    
}
