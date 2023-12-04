using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextImage : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] Sprite[] correctimg = new Sprite[3];
    [SerializeField] Sprite falseimg ;
    [SerializeField] Sprite clearimg ;
    [SerializeField] private Image loadingImg;
    [SerializeField] private AudioController audioController;

    [SerializeField] ExitManagerQuizGame ExitManagerQuizGame;

    private Image correntimg;
    private AudioSource audioSource;
    

    void Awake()
    {
        correntimg = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        audioSource = audioController.GetComponent<AudioSource>();
    }
    public IEnumerator CallResWord(bool b)
    {
        gameObject.SetActive(true);
        if (b == true)
        {
            int temp = Random.Range(0, 3);
            correntimg.sprite = correctimg[temp];
            audioController.playSuccess(temp);
        }
            
        else
        {
            correntimg.sprite = falseimg;
            audioController.playFail();
        }


        yield return new WaitUntil(() => !audioSource.isPlaying);
        //yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
    public IEnumerator Clear()
    {
        //audioController.playClear();
        rect.anchoredPosition = new Vector2(0, 0);
        gameObject.SetActive(true);
        correntimg.sprite = clearimg;
        while(gameObject.transform.localScale.y <= 0.24)
        {
            gameObject.transform.localScale *= 1.05f;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);

        Color temp;
        temp = loadingImg.color;
        
        while(loadingImg.color.a <1)
        {
            temp.a += 0.01f;
            loadingImg.color = temp;
            yield return new WaitForFixedUpdate(); 
        }
        /*Debug.Log("Go to MonkeyGarden");*/
        ExitManagerQuizGame.MonkeyGardenScene();    
    }
}
