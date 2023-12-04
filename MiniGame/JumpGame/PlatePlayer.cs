using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlatePlayer : MonoBehaviour
{
    [SerializeField] GameObject[] avatar = new GameObject[2];
    
    [SerializeField] Sprite[] fruits;
    [SerializeField] Image correntimg;
    [SerializeField] Image backgimg;
    [SerializeField] Text fruitName; //화면에 출력할 textUI
    [SerializeField] PlayerExample playerExample; //과일 생성기에서 먹은 과일 수 저장( 방향마다 먹은 과일수가 달라서)
    
    [SerializeField] AudioClip[] fruitSound; //과일 발음 저장
    [SerializeField] Pool transparency;
    [SerializeField] Camera cam;

    string[] fruitStr = { "Apple", "Avocado", "Banana", "Cherry", "Lemon", "Peach", "Peanut", "Pear",
            "Pineapple", "Strawberry", "Tomato", "Watermelon"};
    private AudioSource audioSouce;
    Rigidbody Rig;
    float force = 2000f;
    Animator animator;

    bool fading = false;
    //Vector3 moduleVal;
    Vector3 initVelo;
    //float xpos;
    //public int countEaten =0;
    
    void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);
        Screen.SetResolution(2180, 1060, true);
    }
    // Start is called before the first frame update

    void Start()
    {
        audioSouce = GetComponent<AudioSource>();
        //moduleVal = new Vector3(-2, 0, 0);
        Rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetInteger("animation", 0);
        initVelo = Vector3.zero;
        //xpos = 0f;
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(Rig.velocity.x) < 5)
            animator.SetInteger("animation", 0);
        
        if (Input.GetMouseButton(0) && fading ==false)
        {
            //Debug.Log(Input.mouse);
            //xpos = cam.ScreenToWorldPoint(Input.mousePosition).x;
            //xpos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            animator.SetInteger("animation", 2);
                
            //if (xpos > 0f)
            if (Input.mousePosition.x > 1080f)
            {
                Rig.AddForce(force * Time.deltaTime, 0, 0);

                if (avatar[0].activeSelf)
                {
                    Rig.velocity = initVelo;
                    avatar[0].SetActive(false);
                    avatar[1].transform.position = avatar[0].transform.position; //+ moduleVal;
                }
                avatar[1].SetActive(true);
                
                /*transform.position += new Vector3(-1 * force * Time.deltaTime, 0, 0);*/
                if (Mathf.Abs(Rig.velocity.x) > 15f)
                {
                    Rig.AddForce( -1 * force * Time.deltaTime, 0, 0);
                }
            }
            if (Input.mousePosition.x < 1080f)
            {
                if (avatar[1].activeSelf)
                {
                    Rig.velocity = initVelo;
                    avatar[1].SetActive(false);
                    avatar[0].transform.position = avatar[1].transform.position;
                }
                avatar[0].SetActive(true);
                
                Rig.AddForce(-1 * force * Time.deltaTime, 0, 0);
                //transform.position += new Vector3(1 * force * Time.deltaTime, 0, 0);
                /*Rig.velocity = new Vector3(-200 * Time.deltaTime, 0, 0);*/
                if (Mathf.Abs(Rig.velocity.x) > 15f)
                {
                    Rig.AddForce(  force * Time.deltaTime, 0, 0);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(Mathf.Abs(other.transform.position.x - transform.position.x));
        if (other.gameObject.tag == "Item" 
            && Mathf.Abs(other.transform.position.x - transform.position.x)<4.6f)
        {
            /*collision.gameObject.GetComponent<Pool>().Enqueue(collision.gameObject);*/
            //other.gameObject.SetActive(false);
            
            StartCoroutine(FruitsImageLoader(other.gameObject.name));
            transparency.Opaque(other.gameObject.name);
            Destroy(other.gameObject);
            
        }
    }
    IEnumerator FruitsImageLoader(string str)
    {
        playerExample.EatenCounter(); //먹은 과일 수 더하기
        fruitName.text = str.Substring(0, str.Length - 7); //충돌한 게임오브젝트 이름
        //str = fruitName.text.ToLower();
        //Debug.Log("fn" +fruitName.text);
        int imgnum = 0;
        for(int i =0; i<fruitStr.Length; i++)
        {
            if (fruitStr[i] == fruitName.text)
            {
                //Debug.Log("fs" + fruitStr[i]);
                imgnum = i;
                break;
            }
        }
        
        Time.timeScale = 0f; //정지를 위해 타임 스케일 수정

        Color backgColor = backgimg.color;
        backgColor.a = 0.65f;
        backgimg.color = backgColor;

        backgimg.gameObject.SetActive(true);
        fruitName.gameObject.SetActive(true);

        correntimg.sprite = fruits[imgnum];
        correntimg.gameObject.SetActive(true);
        
        audioSouce.PlayOneShot(fruitSound[imgnum]);
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f; //정지를 위해 타임 스케일 수정
        
        backgColor.a = 1f;
        backgimg.color = backgColor;
        backgimg.gameObject.SetActive(false);
        correntimg.gameObject.SetActive(false);
        fruitName.gameObject.SetActive(false);

        if (playerExample.eatenCount == 10)
        {// 10개 먹으면 종료 
            fading = true;
            backgimg.gameObject.SetActive(true);

            backgColor.a = 0f;
            backgimg.color = backgColor; //0으로 투명도 초기화

            while (backgColor.a < 1f)
            {//backgimg.color.a <1f) <= 1까지 안가는 오류 발생 화면을 계속 클릭하면 fixedUpdate로 넘어가서 생기는 오류
             //코루틴이 돌아가는지 확인하기위해 fading을 사용하였음
                backgColor.a += 0.01f;
                /*Debug.Log(backgColor.a);*/
                backgimg.color = backgColor;
                yield return new WaitForFixedUpdate();
            }
            playerExample.gameObject.SetActive(false); //생성기 종료
            Debug.Log("종료" );
        }   
    }
}
