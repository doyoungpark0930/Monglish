using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayer : MonoBehaviour
{
    private  Rigidbody rg;
    private Animator anim;
    public bool animCorBool;
    public char collectedChar;
    // Start is called before the first frame update
    //public ItemCreator AlphaCreater;
    public bool firstCharChecking = false; // ù���ڿ� ���� ���� �ִ��� Ȯ��
    public bool doNotEnqueue = false;

    [SerializeField] private ItemCreator itemCreator;
    IEnumerator Start()
    {
        Screen.SetResolution(2160, 1080, true);

        rg = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        /*rg.velocity = Vector3.forward;*/
        yield return new WaitUntil(() => Time.timeScale != 0);

        StartCoroutine(CallAnim());
        yield break;
 
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
        {
            /*Debug.Log("clicked");*/
            /*var xposi = Input.mousePosition.x;*/
            var xposi = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)).x;
            //ScreenToWorldPoint�� �� �� z���� ���� ���� �Ű������� �ָ� ī�޶� ��ġ�� ��ȯ�Ѵ�.
            /*Debug.Log("input:"+ Input.mousePosition);*/

            /*Debug.Log("stw:" + xposi);*/
            /*Debug.Log("screenposint:" + Camera.main.ScreenToWorldPoint(Input.mousePosition).x);*/

            if (xposi < 0)
            {
                Lposi();
            }

            if (xposi > 0)
                Rposi();
        }
    }

    IEnumerator CallAnim()
    {
        anim.SetInteger("animation", 2);
        //anim.SetBool("Run", true);
        yield break;
    }

    public void Lposi()
    {
        if(rg.position.x > -6)
            rg.position += new Vector3(-6, 0, 0);
    }

    public void Rposi()
    {
        if(rg.position.x < 6)
            rg.position += new Vector3(6, 0, 0);
    }
    void OnTriggerEnter(Collider other)
    {

        /*        collectedChars += other.name[0];*/


        if (firstCharChecking == true && itemCreator.checkingArray[0] != 1
            && other.name[0] == itemCreator.correntString.ToLower()[0])
        {// �빮�ڶ� ���� ������ �ҹ��� �����鼭 �빮�� ���� ���� �����̰� ���� ���ڰ� �빮�ڶ� ���� ������ �ҹ��� �϶�
            doNotEnqueue = true; //itemCreator���� enqueue���� ����

            /*for (int i = 0; i < itemCreator.objQueue.Count; i++)
            {
                var tempitem = itemCreator.objQueue.Dequeue();
                if (tempitem.name[0] == itemCreator.correntString.ToLower()[0])
                {
                    Debug.Log("klklkl");
                    Destroy(other); //�ϳ��� ���������. Refrigerator
                    break;
                }

                else
                    itemCreator.objQueue.Enqueue(tempitem);
            }*/


        }

        other.gameObject.SetActive(false);
        itemCreator.CompareString(other.name[0]);
        //StartCoroutine(itemCreator.CompareString(other.name[0]));
                
    }

}
