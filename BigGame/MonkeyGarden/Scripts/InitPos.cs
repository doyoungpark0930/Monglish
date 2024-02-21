using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPos : MonoBehaviour
{
    public static Vector3 SetInitPos = new Vector3(-77.8f, 3.0f, -19.8f); 

                                                                            
    [SerializeField] GameObject Player;
    void Start()
    {
        Player.transform.position = SetInitPos;
    }


    void Update()
    {
        
    }
}
