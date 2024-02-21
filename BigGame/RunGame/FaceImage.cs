using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceImage : MonoBehaviour
{
    RectTransform rect;
    float deltatime;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        deltatime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        deltatime += Time.deltaTime;
        if (Time.timeScale != 0f)
        {
            if (deltatime < 0.305f)
            {
                //Debug.Log("ddd");
                rect.localScale *= 1.0003f;
            }
            else if (deltatime < 0.6f)
            {
                //Debug.Log("mm");
                //deltatime = 0f;
                rect.localScale *= 0.9997f;
            }
            else
                deltatime = 0f;
        }

    }
}
