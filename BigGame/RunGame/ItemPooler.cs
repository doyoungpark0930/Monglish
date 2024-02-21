using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPooler : MonoBehaviour
{
    public ItemPooler Instance; //인스턴스 선언

    public GameObject[] AlphaBetArr = new GameObject[52];
    [SerializeField]

    private GameObject itemPrefab; // 풀링할 아이템
    private Queue<Item> itemPoolerQueue = new Queue<Item>(); // 생성할 객체 담는 곳

    private void Awake()
    {

        Instance = this; // 스크립트가 붙어 있는 객체를 담음     
        Initialize(3); // 객체를 만들어 큐에 집어 넣음

    }

    private Item CreateNewObject()
    {// 미리만든 오브젝트 다 사용하면 새로 생성하는 함수
     //외부에서 마음대로 생성하지 못하도록 private 사용   
        var newObj = Instantiate(itemPrefab, transform).GetComponent<Item>(); //ItemPooler의 자식으로 새로운 객체 생성
        newObj.gameObject.SetActive(false); //비활성화
        Instance.itemPoolerQueue.Enqueue(newObj);
        return newObj;
    }
    private void Initialize(int num)
    {
        for (int i = 0; i < num; i++)
        {
            CreateNewObject(); //큐에 만든 객체 담기
        }
    }

    public Item GetPooledItem()
    {
        if (Instance.itemPoolerQueue.Count > 0)
        {
            var obj = Instance.itemPoolerQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {

            Instance.CreateNewObject();
            var newObj = Instance.itemPoolerQueue.Dequeue();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            /*newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(false);*/

            return newObj;
        }
    }

    public void ReturnItem(Item item)
    {

        item.gameObject.SetActive(false);
        item.transform.SetParent(Instance.transform); //Pooler의 위치로 위치 지정해서
        Instance.itemPoolerQueue.Enqueue(item); // 큐에 다시 집어 넣음
    }

}
