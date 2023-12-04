using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    [SerializeField] private Pool pool;
    float activeTime;
    float activeTimeRate = 3.0f;

    void Start()
    {
        activeTime = activeTimeRate;
    }
    // Update is called once per frame
    void Update()
    {
        activeTime -= Time.deltaTime;
        if (activeTime < 0)
        {
            activeTime = activeTimeRate;
            
            //Debug.Log(this.name);
            pool.Enqueue(gameObject); //오브젝트 작동 끝나면 속도 0으로 바꿔줘야 됨
            /*transform.position = new Vector3(0, 20, 0);*/
            /*transform.position = new Vector3(0, 20, 0);//new Vector3(0, 0, 0.1f);*/
            //transform.GetComponent<Rigidbody>().AddForce(0, 0, 0);
            //Debug.Log("active");

        }
/*        else
        {
            
        }*/
    }
    public void Init(Pool _pool)
    {// pool의 인스턴스를 받는 함수
        pool = _pool;
    }

    public void CleanUp()
    {// 오브젝트 상태 초기화
        activeTime = activeTimeRate;
    }
}
