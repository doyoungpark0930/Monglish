using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionManagerMonkeyGarden : MonoBehaviour
{
    [SerializeField] private GameObject Direction; //ȭ��ǥ
    [SerializeField] private GameObject Player; //�÷��̾� ������Ʈ

    public GameObject GetDirection()
    {
        return Direction;
    }


    Vector3 offset; //Player�� Direction�� �Ÿ�
    Vector3 GoalTarget; //���� ��� ����

    void Awake()
    {
        offset = Direction.transform.position - Player.transform.position;
        GoalTarget = new Vector3(-2.5f, 5.11f, 67.89f); //���� ��� ������ ��ġ

    }
   
    void FixedUpdate()
    {
        Direction.transform.position = Player.transform.position + offset;  //ȭ��ǥ�� ��ġ�� �׻� �÷��̾� ����
        Direction.transform.LookAt(GoalTarget); //Direction�� GoalTarget�� �ٶ󺸰��Ѵ�.
    }
  

    public IEnumerator SetOnDirection()
    {
        Direction.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        Direction.SetActive(false);

        while (true)
        {
            yield return null;
            yield return new WaitForSecondsRealtime(4f); //4�� �������� ȭ��ǥ ����
            Direction.SetActive(true);
            yield return new WaitForSecondsRealtime(2f); //ȭ��ǥ ���ӽð� 2��
            Direction.SetActive(false);
        }

    }
}
