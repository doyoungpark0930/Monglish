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
    public bool firstCharChecking = false; // 첫글자와 같은 글자 있는지 확인
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
            //ScreenToWorldPoint를 쓸 때 z값이 없는 값을 매개변수로 주면 카메라 위치를 반환한다.
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
        {// 대문자랑 같은 순서의 소문자 있으면서 대문자 먹지 않은 상태이고 먹은 글자가 대문자랑 같은 순서의 소문자 일때
            doNotEnqueue = true; //itemCreator에서 enqueue하지 않음

            /*for (int i = 0; i < itemCreator.objQueue.Count; i++)
            {
                var tempitem = itemCreator.objQueue.Dequeue();
                if (tempitem.name[0] == itemCreator.correntString.ToLower()[0])
                {
                    Debug.Log("klklkl");
                    Destroy(other); //하나만 없애줘야함. Refrigerator
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
