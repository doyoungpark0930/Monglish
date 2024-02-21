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
            pool.Enqueue(gameObject); //������Ʈ �۵� ������ �ӵ� 0���� �ٲ���� ��
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
    {// pool�� �ν��Ͻ��� �޴� �Լ�
        pool = _pool;
    }

    public void CleanUp()
    {// ������Ʈ ���� �ʱ�ȭ
        activeTime = activeTimeRate;
    }
}
