using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WordLayoutMaker : MonoBehaviour
{
    [SerializeField] private GameObject [] WordPregfabs = new GameObject[2]; // ���� ǥ���� ������ �޴� �迭
    public GameObject []wordLayout;
    [SerializeField] private ItemCreator itemcreater;

    public bool shown = false;
    public IEnumerator MakeLayout(string str, int wordPrefabNum)
    {//�Է� ���� ���ڿ��� ������ ���̾ƿ� �����
        //Debug.Log(str);
        wordLayout = new GameObject[str.Length];
        shown = false;
        for (int i = 0; i < str.Length; i++)
        {
            wordLayout[i] = (Instantiate(WordPregfabs[wordPrefabNum]));
            wordLayout[i].gameObject.GetComponentInChildren<Text>().text = str[i].ToString();
            wordLayout[i].transform.SetParent(transform, false);
        }
        
        
        yield return StartCoroutine(ShowLayout()); //ShowLayout() �����ϰ� ���� �� ���� ���
        

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
            yield return new WaitForSecondsRealtime(1f); //2�ʵ��� �����ֱ�
            //yield break;

            for (int i = 0; i < wordLayout.Length; i++)
                wordLayout[i].gameObject.SetActive(false);
            shown = true;
        }
        else
        {
            for (int i = 0; i < wordLayout.Length; i++)
                wordLayout[i].gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(2f); //2�ʵ��� �����ֱ�
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
        //�θ��� �̹��� ���� ����
        Color color = wordLayout[i].transform.GetComponent<Image>().color;
        color.a = val;
        wordLayout[i].GetComponent<Image>().color = color;

        //�ڽ��� ���� ���� ����
        color = wordLayout[i].gameObject.GetComponentInChildren<Text>().color;
        color.a = val;
        /*Debug.Log(color.a);*/
        wordLayout[i].gameObject.GetComponentInChildren<Text>().color = color;
    }
    
}