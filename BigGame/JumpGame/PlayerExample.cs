using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExample : MonoBehaviour
{
    public Pool pool;
    private float timeSpend;
    private float spawnTime;
    GameObject pooledObject;
    public int eatenCount = 0;
    private IEnumerator Start()
    {
        timeSpend = 0;
        spawnTime = 3.0f;
        yield return new WaitForSeconds(1f);
        //Debug.Log(pool.objectPool.Count);
        while(pool.objectPool.Count >0)
        {
            DoubleSpawner();
            yield return new WaitForSeconds(1.5f);
            if (pool.objectPool.Count <1)
                yield return new WaitForSeconds(3f);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        timeSpend += Time.deltaTime;        
        if (spawnTime < timeSpend)
        {
            pooledObject = pool.Dequeue();
            pooledObject.transform.position = new Vector3(Random.Range(-18f,18f),25,0);
            
            //Debug.Log(pooledObject.GetComponent<Rigidbody>().velocity);
            timeSpend = 0;
        }
        /*if(pooledObject)
            pooledObject.transform.Rotate(new Vector3
                (Time.deltaTime * Random.Range(30,60), Time.deltaTime * Random.Range(30, 60), Time.deltaTime * Random.Range(-30,-60)));
    }*/
    private void DoubleSpawner()
    {
        pooledObject = pool.Dequeue();
        pooledObject.transform.position = new Vector3(Random.Range(-18f, 18f), 25, 0);
        if (pooledObject)
            pooledObject.transform.Rotate(new Vector3
                (Time.deltaTime * Random.Range(30, 60), Time.deltaTime * Random.Range(30, 60), Time.deltaTime * Random.Range(-30, -60)));
    }

    public void EatenCounter()
    {
        eatenCount++;
        //return eatenCount;
    }
}