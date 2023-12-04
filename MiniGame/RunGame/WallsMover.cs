using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WallsMover : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initpos;
    // Update is called once per frame
    private RectTransform rect;
    private Vector3 deltarect;

    private Image img;
    private Color tempcol;
    void Start()
    {
        
        rect = GetComponent<RectTransform>();
        if (rect.anchoredPosition3D.x < 0)
            initpos = new Vector3(-15, -188, -360);
        else
            initpos = new Vector3(15, -186, -310);
        deltarect = new Vector3(0, 0, Time.deltaTime *5);
        //deltarect = new Vector3(0, 0, Time.deltaTime *10);

        img = GetComponent<Image>();
        tempcol = img.color;
        
        
    }
    void Update()
    {
        //Debug.Log(rect.anchoredPosition3D);
        if (rect.anchoredPosition3D.z < -660)
        {
            rect.anchoredPosition3D = initpos;

        }
        if (Time.timeScale > 0.5f)
            rect.anchoredPosition3D -= new Vector3(0, 0, Time.deltaTime * 5);


        else if (Time.timeScale > 0.3f)
        {
            rect.anchoredPosition3D -= new Vector3(0, 0, Time.deltaTime * 5) * 0.8f;
        }
        else if (Time.timeScale > 0.1f)
        {
            rect.anchoredPosition3D -= new Vector3(0, 0, Time.deltaTime * 5) * 0.2f;


        }
    }
}
