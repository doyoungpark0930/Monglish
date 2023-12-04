using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WordLayoutMaker : MonoBehaviour
{
    [SerializeField] private GameObject [] WordPregfabs = new GameObject[2]; // 문자 표시할 프리팹 받는 배열
    public GameObject []wordLayout;
    [SerializeField] private ItemCreator itemcreater;

    public bool shown = false;
    public IEnumerator MakeLayout(string str, int wordPrefabNum)
    {//입력 받은 문자열을 가지고 레이아웃 만들기
        //Debug.Log(str);
        wordLayout = new GameObject[str.Length];
        shown = false;
        for (int i = 0; i < str.Length; i++)
        {
            wordLayout[i] = (Instantiate(WordPregfabs[wordPrefabNum]));
            wordLayout[i].gameObject.GetComponentInChildren<Text>().text = str[i].ToString();
            wordLayout[i].transform.SetParent(transform, false);
        }
        
        
        yield return StartCoroutine(ShowLayout()); //ShowLayout() 시작하고 끝날 때 까지 대기
        

        for (int i =0; i<wordLayout.Length; i++)
            SetPregfabAlphaval(i, 0.7f);
        
        OffLayout();
        
        //yield return StartCoroutine(ShowLayout());
        ; 
    }
    public void DestroyLayout()
    {
        for(int i = 0; i < wordLayout.Length; i++)
            Destroy(wordLayout[i]);
    }
 
    public IEnumerator ShowLayout()
    {
        if(!shown)
        {
            for (int i = 0; i < wordLayout.Length; i++)
                wordLayout[i].gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f); 
            itemcreater.audioSource.PlayOneShot(itemcreater.electronicsPronunci[itemcreater.quizOrder[itemcreater.correntOrder]]);
            yield return new WaitForSecondsRealtime(1f); //2초동안 보여주기
            //yield break;

            for (int i = 0; i < wordLayout.Length; i++)
                wordLayout[i].gameObject.SetActive(false);
            shown = true;
        }
        else
        {
            for (int i = 0; i < wordLayout.Length; i++)
                wordLayout[i].gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(2f); //2초동안 보여주기
            //yield break;

            for (int i = 0; i < wordLayout.Length; i++)
                wordLayout[i].gameObject.SetActive(false);
        }

    }

    public void OffLayout()
    {
        for (int i = 0; i < wordLayout.Length; i++)
            wordLayout[i].gameObject.SetActive(false);
    }
    public void SetPregfabAlphaval(int i, float val)
    {
        //부모인 이미지 투명도 변경
        Color color = wordLayout[i].transform.GetComponent<Image>().color;
        color.a = val;
        wordLayout[i].GetComponent<Image>().color = color;

        //자식인 문자 투명도 변경
        color = wordLayout[i].gameObject.GetComponentInChildren<Text>().color;
        color.a = val;
        /*Debug.Log(color.a);*/
        wordLayout[i].gameObject.GetComponentInChildren<Text>().color = color;
    }
    
}