using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpPlatePlayer : MonoBehaviour
{       
    [SerializeField] Sprite[] fruits;
    [SerializeField] Image correntimg;
    [SerializeField] Image backgimg;
    [SerializeField] Text fruitName; //ȭ�鿡 ����� textUI
    [SerializeField] PlayerExample playerExample; //���� �����⿡�� ���� ���� �� ����( ���⸶�� ���� ���ϼ��� �޶�)

    [SerializeField] Pool transparency;
    [SerializeField] GameObject triggerFloor;
    [SerializeField] Rocket rocket;
    //[SerializeField] Camera cam;

    [SerializeField] AudioClip[] fruitSound; //���� ���� ����
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
                
                //Debug.Log("����");
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
                //Debug.Log("����");
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
                    //Debug.Log("����" + Rig.velocity.y);
                    sphereCollider.material.bounciness = 1f;
                    transform.position += new Vector3(0, 0.1f, 0);
                }
                else
                    sphereCollider.material.bounciness = 0.85f;

            }
            
        }
        
    }
    IEnumerator FruitsImageLoader(string str)
    {//10�� ������ ����
        playerExample.EatenCounter(); //���� ���� �� ���ϱ�
        fruitName.text = str.Substring(0, str.Length - 7); //�浹�� ���ӿ�����Ʈ �̸�
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

        Time.timeScale = 0f; //������ ���� Ÿ�� ������ ����

        Color backgColor = backgimg.color;
        backgColor.a = 0.65f;
        backgimg.color = backgColor;

        backgimg.gameObject.SetActive(true);
        fruitName.gameObject.SetActive(true);

        correntimg.sprite = fruits[imgnum];
        correntimg.gameObject.SetActive(true);

        audioSouce.PlayOneShot(fruitSound[imgnum]);
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f; //������ ���� Ÿ�� ������ ����

        backgColor.a = 1f;
        backgimg.color = backgColor;
        backgimg.gameObject.SetActive(false);
        correntimg.gameObject.SetActive(false);
        fruitName.gameObject.SetActive(false);

        if (playerExample.eatenCount == 10)
        {// 10�� ������ ���� 
            /*Rig.constraints |= RigidbodyConstraints.FreezePosition;// ����

            fading = true;
            backgimg.gameObject.SetActive(true);

            backgColor.a = 0f;
            backgimg.color = backgColor; //0���� ���� �ʱ�ȭ

            while (backgColor.a < 1f)
            {//backgimg.color.a <1f) <= 1���� �Ȱ��� ���� �߻� ȭ���� ��� Ŭ���ϸ� fixedUpdate�� �Ѿ�� ����� ����
             //�ڷ�ƾ�� ���ư����� Ȯ���ϱ����� fading�� ����Ͽ���
                backgColor.a += 0.01f;
                //Debug.Log(backgColor.a);
                backgimg.color = backgColor;
                yield return new WaitForFixedUpdate();
            }
            
*/
            mouseClick = false;
            playerExample.gameObject.SetActive(false); //������ ����
            Debug.Log("10�� ����");

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
