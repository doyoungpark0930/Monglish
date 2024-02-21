using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionManagerMonkeyGarden : MonoBehaviour
{
    [SerializeField] private GameObject Direction; //화살표
    [SerializeField] private GameObject Player; //플레이어 오브젝트

    public GameObject GetDirection()
    {
        return Direction;
    }


    Vector3 offset; //Player와 Direction의 거리
    Vector3 GoalTarget; //산위 가운데 지점

    void Awake()
    {
        offset = Direction.transform.position - Player.transform.position;
        GoalTarget = new Vector3(-2.5f, 5.11f, 67.89f); //산위 가운데 지점의 위치

    }
   
    void FixedUpdate()
    {
        Direction.transform.position = Player.transform.position + offset;  //화살표의 위치를 항상 플레이어 옆에
        Direction.transform.LookAt(GoalTarget); //Direction이 GoalTarget을 바라보게한다.
    }
  

    public IEnumerator SetOnDirection()
    {
        Direction.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        Direction.SetActive(false);

        while (true)
        {
            yield return null;
            yield return new WaitForSecondsRealtime(4f); //4초 간격으로 화살표 띄우기
            Direction.SetActive(true);
            yield return new WaitForSecondsRealtime(2f); //화살표 지속시간 2초
            Direction.SetActive(false);
        }

    }
}
