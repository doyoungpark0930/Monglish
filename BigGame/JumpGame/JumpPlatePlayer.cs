using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpPlatePlayer : MonoBehaviour
{       
    [SerializeField] Sprite[] fruits;
    [SerializeField] Image correntimg;
    [SerializeField] Image backgimg;
    [SerializeField] Text fruitName; //화면에 출력할 textUI
    [SerializeField] PlayerExample playerExample; //과일 생성기에서 먹은 과일 수 저장( 방향마다 먹은 과일수가 달라서)

    [SerializeField] Pool transparency;
    [SerializeField] GameObject triggerFloor;
    [SerializeField] Rocket rocket;
    //[SerializeField] Camera cam;

    [SerializeField] AudioClip[] fruitSound; //과일 발음 저장
    public AudioClip itemSound;
    public AudioClip jumpSound;

    string[] fruitStr = {"Apple", "Banana", "Cherry", "Coconut", "Fig", "Grapefruit",
    "Kiwi", "Lemon", "Melon", "Peach", "Pear", "Persimmon", "Watermelon"};
    /*string[] fruitStr = {"Apple", "Banana", "Cherry", "Coconut", "Fig", "Grape", "Grapefruit",
    "Kiwi", "Lemon", "Melon", "Peach", "Peanut", "Pear", "Persimmon", "Pineapple", "Strawberry",
    "Tomato", "Watermelon"};*/
    private AudioSource audioSouce;
    private Rigidbody Rig;
    private SphereCollider sphereCollider;
    private float force = 2000f;
    private float deltaY = 0f;
    Animator animator;

    bool fading = false;
    bool mouseClick = true;

    //Vector3 moduleVal;
    //float xpos;
    //public int countEaten =0;
    private Vector3 LRotation = new Vector3(0, 5, 0);
    private Vector3 RRotation = new Vector3(0, -5, 0);

    //private Quaternion initRotation;
    //[SerializeField] private Animation animation;

    void Awake()
    {
        Screen.SetResolution(2180, 1060, true);
    }

    void Start()
    {
        
        //initRotation = transform.rotation;
        audioSouce = GetComponent<AudioSource>();
        //moduleVal = new Vector3(-2, 0, 0);
        Rig = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        animator.SetInteger("animation", 3);

        //xpos = 0f;
    }

    
   
    void FixedUpdate()
    {
        deltaY = Mathf.Abs(deltaY - transform.position.y);
        

        if (Input.GetMouseButton(0) && fading ==false && mouseClick ==true)
        {

            if (Input.mousePosition.x > 1080f)
            {
                Rig.AddForce(force * Time.deltaTime, 0, 0);

                if (transform.rotation.eulerAngles.y >= 100 && transform.rotation.eulerAngles.y <= 265)
                {
                    //Debug.Log("R");
                    Rig.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                    
                    if(transform.rotation.eulerAngles.y > 110)
                        transform.Rotate(RRotation);

                    //initRotation = transform.rotation;
                    Rig.constraints |= RigidbodyConstraints.FreezeRotationY;
                }
                
                /*transform.position += new Vector3(-1 * force * Time.deltaTime, 0, 0);*/
                if (Mathf.Abs(Rig.velocity.x) > 15f)
                {
                    Rig.AddForce( -1 * force * Time.deltaTime, 0, 0);
                }
            }
            else
            {
                Rig.AddForce(-1 * force * Time.deltaTime, 0, 0);
                //if (transform.rotation == initRotation)
                if (transform.rotation.eulerAngles.y >= 100 && transform.rotation.eulerAngles.y <= 260)
                {
                    //Debug.Log("L");
                    Rig.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                    if(transform.rotation.eulerAngles.y<260)
                        transform.Rotate(LRotation);
                    //initRotation = transform.rotation;
                    Rig.constraints |= RigidbodyConstraints.FreezeRotationY;
                }
                    
                if (Mathf.Abs(Rig.velocity.x) > 15f)
                    {
                        Rig.AddForce(force * Time.deltaTime, 0, 0);
                    }

            }
            deltaY = transform.position.y;
            if (Mathf.Abs(triggerFloor.transform.position.y - transform.position.y) < 1.5f && Mathf.Abs(Rig.velocity.y) < 0.2f)
                transform.position += new Vector3(0, 0.2f, 0);

        }   
        
            //Debug.Log();
        //Debug.Log();
    }


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log((other.transform.position.x - transform.position.x));
        //Debug.Log(sphereCollider.material.bounciness);
        
         
        if (mouseClick == true)
        {
            //Debug.Log(other.name +Vector3.Distance(other.transform.position, transform.position));
            if (other.gameObject.tag == "Item" && (Mathf.Abs(other.transform.position.x - transform.position.x) < 6f
                | Vector3.Distance(other.transform.position, transform.position)<12))
            {
                audioSouce.PlayOneShot(itemSound);
                Time.timeScale = 0.1f;
                
                //Debug.Log("과일");
                //sphereCollider.material.bounciness = 0.9f;
                transform.position += new Vector3(0, 3f, 0);
                //gameObject.GetComponent<sp>
                StartCoroutine(FruitsImageLoader(other.gameObject.name));
                transparency.Opaque(other.gameObject.name);
                Destroy(other.gameObject);

            }
            else if (other.tag == "Wall")
            {
                animator.Play("Jump", -1, 0f);
                /*if (playerExample.eatenCount != 1)
                    
                else
                   animator.SetBool("JumpBool", false);*/
                //Debug.Log("또잉");
                audioSouce.PlayOneShot(jumpSound);

                if ((other.transform.position.x - transform.position.x) > 21f)
                    transform.position += Vector3.right / 2f;
                else if ((other.transform.position.x - transform.position.x) < -21f)
                {
                    //Debug.Log("left");
                    transform.position += Vector3.left / 2f;
                }

                if (Mathf.Abs(Rig.velocity.y) < 5f)
                {
                    //Debug.Log("진입" + Rig.velocity.y);
                    sphereCollider.material.bounciness = 1f;
                    transform.position += new Vector3(0, 0.1f, 0);
                }
                else
                    sphereCollider.material.bounciness = 0.85f;

            }
            
        }
        
    }
    IEnumerator FruitsImageLoader(string str)
    {//10개 먹으면 로켓
        playerExample.EatenCounter(); //먹은 과일 수 더하기
        fruitName.text = str.Substring(0, str.Length - 7); //충돌한 게임오브젝트 이름
        //str = fruitName.text.ToLower();
        //Debug.Log("fn" +fruitName.text);
        int imgnum = 0;
        for (int i = 0; i < fruitStr.Length; i++)
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
            /*Rig.constraints |= RigidbodyConstraints.FreezePosition;// 멈춰

            fading = true;
            backgimg.gameObject.SetActive(true);

            backgColor.a = 0f;
            backgimg.color = backgColor; //0으로 투명도 초기화

            while (backgColor.a < 1f)
            {//backgimg.color.a <1f) <= 1까지 안가는 오류 발생 화면을 계속 클릭하면 fixedUpdate로 넘어가서 생기는 오류
             //코루틴이 돌아가는지 확인하기위해 fading을 사용하였음
                backgColor.a += 0.01f;
                //Debug.Log(backgColor.a);
                backgimg.color = backgColor;
                yield return new WaitForFixedUpdate();
            }
            
*/
            mouseClick = false;
            playerExample.gameObject.SetActive(false); //생성기 종료
            Debug.Log("10개 종료");

            animator.SetBool("JumpBool", false);
            //animator.enabled = true;


            gameObject.transform.localRotation = Quaternion.Euler(0, 110f, 0);
            transform.position = new Vector3(-10, 0.5f, 0);
            animator.SetInteger("animation", 0);
            //animator.enabled = false;
            Rig.velocity = Vector3.zero;
            //yield return new WaitForSeconds(1f);
            //Debug.Log("0");
            rocket.gameObject.SetActive(true);

            /*yield return new WaitForSeconds(2f);
            Rig.AddForce(new Vector3(500, 500, 0));
            animator.Play("Jump", -1, 0);
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
            rocket.gameObject.SetActive(true);*/
        }
    }
}
