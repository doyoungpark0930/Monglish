using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject [] poolableObject; //과일 프리팹
    private int objectpoolCount = 10;
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    public GameObject [] objectDisplay;

    public GameObject[] instDisp = new GameObject[10] ;


    // Start is called before the first frame update
    void Start()
    {
        CreatePooledObject();
       
        objectDisplay = objectPool.ToArray();
        
        instDisp = objectDisplay; //왜 이렇게해야 생성이 되는가?

        /*Instantiate(objectDisplay[0]);*/
        /*objectDisplay[0].GetComponent<Poolable>().Init(this);
        objectDisplay[0].SetActive(true);*/

        /*foreach (GameObject obj in objectDisplay)
        {
            Instantiate(obj);
            obj.transform.position = new Vector3(22, 20, 0);
        }*/
        Color tempcor;
        for (int i = 0; i < objectpoolCount; i++)
        {
            instDisp[i].gameObject.transform.localScale *= 0.8f;
            instDisp[i] = Instantiate(objectDisplay[i]);
            instDisp[i].SetActive(true);
            instDisp[i].GetComponent<Poolable>().enabled = false;
            instDisp[i].GetComponent<SphereCollider>().enabled = false;
            if (i < 5)
                instDisp[i].transform.position = new Vector3(-26f, 5f * (5f - i), 0);
            else
                instDisp[i].transform.position = new Vector3(24f, 25f -5f * (i - 5f), 0);
            instDisp[i].GetComponent<Rigidbody>().isKinematic = true;
            tempcor = instDisp[i].GetComponent<MeshRenderer>().material.color;
            tempcor.a = 0.5f;
            instDisp[i].GetComponent<MeshRenderer>().material.color = tempcor;
        }

    }



    void CreatePooledObject()
    {// 중복 안되게 생성
        int[] tempnums = new int[objectpoolCount];

        for (int i = 0; i < objectpoolCount; i++)
        {
            tempnums[i] = Random.Range(0, poolableObject.Length);

            for (int k = 0; k < i; k++)
            {
                if (i > 0 && tempnums[i].Equals(tempnums[k]))
                {
                    i--;
                    break;
                }
            }
        }
        //var arr = objectPool.ToArray();

        for(int i =0; i<tempnums.Length; i++)
        {
            GameObject temp = poolableObject[tempnums[i]];
            temp = Instantiate(poolableObject[tempnums[i]]);
            temp.GetComponent<Poolable>().Init(this);
            temp.SetActive(false);
            objectPool.Enqueue(temp);   
        }


    }/*    void CreatePooledObject()
    {

        for(int i = 0; i < objectpoolCount; i++)
        {
            
            var arr = objectPool.ToArray();
            //Debug.Log(arr.Length);
            int tempnum = Random.Range(0, poolableObject.Length);
            GameObject temp = poolableObject[tempnum];
            if (arr.Length != 0 && arr[arr.Length - 1].name == temp.name+"(Clone)")
                do//바로 전 오브젝트랑 같지 않게 만듦
                {
                    //Debug.Log("recreate");
                    tempnum = Random.Range(0, poolableObject.Length);
                    temp = poolableObject[tempnum];
                } while (arr[arr.Length - 1].name == temp.name);
            temp = Instantiate(poolableObject[tempnum]);
            temp.GetComponent<Poolable>().Init(this);
            temp.SetActive(false);
            objectPool.Enqueue(temp);
        }
    }*/

    // Get Object
    public GameObject Dequeue()
    {
/*        if (objectPool.Count < 0)
        {
            CreatePooledObject();
        }*/     
        GameObject dequeueObject = objectPool.Dequeue();
        //dequeueObject.GetComponent<Poolable>().CleanUp();
        dequeueObject.SetActive(true);
        return dequeueObject;
    }

    // Back to pool
    public void Enqueue(GameObject _enqueueObject)
    {
        _enqueueObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _enqueueObject.SetActive(false);
        objectPool.Enqueue(_enqueueObject);
    }
    public void Opaque(string str)
    {// 불투명화
        Color tempcol;
        for(int i =0; i<instDisp.Length; i++)
        {
             var tempinst = instDisp[i].GetComponent<MeshRenderer>();
            if (tempinst.material.color.a != 1f && instDisp[i].name.Substring(0, instDisp[i].name.Length - 14) 
                == str.Substring(0, str.Length - 7))
            {
                /*Debug.Log("str " + str.Substring(0, str.Length - 7));
                Debug.Log(instDisp[i].name.Substring(0, instDisp[i].name.Length - 14));*/
                tempcol = tempinst.material.color;
                tempcol.a = 1f;
                tempinst.material.color = tempcol;
                break;
            }
        }

    }
}