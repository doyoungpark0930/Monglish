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
    [SerializeField] Text fruitName; //ȭ�鿡 ����� textUI
    [SerializeField] PlayerExample playerExample; //���� �����⿡�� ���� ���� �� ����( ���⸶�� ���� ���ϼ��� �޶�)
    
    [SerializeField] AudioClip[] fruitSound; //���� ���� ����
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
        playerExample.EatenCounter(); //���� ���� �� ���ϱ�
        fruitName.text = str.Substring(0, str.Length - 7); //�浹�� ���ӿ�����Ʈ �̸�
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
            fading = true;
            backgimg.gameObject.SetActive(true);

            backgColor.a = 0f;
            backgimg.color = backgColor; //0���� ���� �ʱ�ȭ

            while (backgColor.a < 1f)
            {//backgimg.color.a <1f) <= 1���� �Ȱ��� ���� �߻� ȭ���� ��� Ŭ���ϸ� fixedUpdate�� �Ѿ�� ����� ����
             //�ڷ�ƾ�� ���ư����� Ȯ���ϱ����� fading�� ����Ͽ���
                backgColor.a += 0.01f;
                /*Debug.Log(backgColor.a);*/
                backgimg.color = backgColor;
                yield return new WaitForFixedUpdate();
            }
            playerExample.gameObject.SetActive(false); //������ ����
            Debug.Log("����" );
        }   
    }
}
