using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPooler : MonoBehaviour
{
    public ItemPooler Instance; //�ν��Ͻ� ����

    public GameObject[] AlphaBetArr = new GameObject[52];
    [SerializeField]

    private GameObject itemPrefab; // Ǯ���� ������
    private Queue<Item> itemPoolerQueue = new Queue<Item>(); // ������ ��ü ��� ��

    private void Awake()
    {

        Instance = this; // ��ũ��Ʈ�� �پ� �ִ� ��ü�� ����     
        Initialize(3); // ��ü�� ����� ť�� ���� ����

    }

    private Item CreateNewObject()
    {// �̸����� ������Ʈ �� ����ϸ� ���� �����ϴ� �Լ�
     //�ܺο��� ������� �������� ���ϵ��� private ���   
        var newObj = Instantiate(itemPrefab, transform).GetComponent<Item>(); //ItemPooler�� �ڽ����� ���ο� ��ü ����
        newObj.gameObject.SetActive(false); //��Ȱ��ȭ
        Instance.itemPoolerQueue.Enqueue(newObj);
        return newObj;
    }
    private void Initialize(int num)
    {
        for (int i = 0; i < num; i++)
        {
            CreateNewObject(); //ť�� ���� ��ü ���
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
        item.transform.SetParent(Instance.transform); //Pooler�� ��ġ�� ��ġ �����ؼ�
        Instance.itemPoolerQueue.Enqueue(item); // ť�� �ٽ� ���� ����
    }

}
