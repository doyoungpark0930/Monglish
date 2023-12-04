using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMonkey : MonoBehaviour
{
    Animator anim;
    float bananaHeight;
    int quiznum; // ¹®Á¦¼ö
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        bananaHeight = 1.6f;
        quiznum = 4;



    }

    public IEnumerator MoveMonkey()
    {
        anim.SetInteger("animation", 3);
        anim.StopPlayback();
        Vector3 deltaH = Vector3.zero;
        while (true)
        {
            deltaH += 0.1f * Vector3.up * Time.deltaTime;
            transform.position += 0.1f * Vector3.up * Time.deltaTime;
            yield return new WaitForFixedUpdate();
            //Debug.Log(deltaH);
            //Debug.Log(bananaHeight / quiznum);
            if (deltaH.y > bananaHeight / quiznum)
            {
                anim.StartPlayback();
                yield break;
            }
                
        }

    }
}
