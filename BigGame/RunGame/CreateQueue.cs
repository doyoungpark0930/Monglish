using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

public class CreateQueue : MonoBehaviour
{
    

    // Start is called before the first frame update
    /*void Start()
    {
        AlphaQueueMaker("Cooker");
    }*/

    // Update is called once per frame

/*    public Queue<char> AlphaQueueMaker(string inp)
    {
        int arrSize = inp.Length * 2; // ������ ������ �� ����
        char[] chars = new char[arrSize];
        int k = 0;
        foreach(char c in inp)
        {
            chars[k++] = c;
        }
        for (; k < arrSize; k++)
        {// ó�� �빮�ڰ� �ҹ��ڶ� �ߺ��ȵǰ� ����
            int temp = Random.Range('a', 'z');
            chars[k] = (char)temp;
            *//*Debug.Log((char)(chars[0] -('A' - 'a')));*//*
            while (temp == (int)(chars[0] - ('A' - 'a')))
            {
                temp = Random.Range('a', 'z');
                chars[k] = (char)temp;
            }
        }
        
        for(int i =0; i<chars.Length; i++)
        {// ���� ���� ���� �÷��̾�� ���� ���ڿ� ����
            *//*Debug.Log(arr.Length);*//*
            int temp = Random.Range(0,arrSize);
            char arrtemp = chars[i];
            chars[i] = chars[temp];
            chars[temp] = arrtemp;
           
        }
        *//*string Builtsting = inp;
        for (int i =arrSize/2; i< arrSize; i++)
        {
            int temp = Random.Range('a', 'z');
            Builtsting += ((char)temp);
        }

        char [] arr = Builtsting.ToCharArray();
        */
        /*for(int i =0; i<Builtsting.Length; i++)
        {
            *//*Debug.Log(arr.Length);*//*
            int temp = Random.Range(0,arrSize);
            char arrtemp = arr[i];
            arr[i] = arr[temp];
            arr[temp] = arrtemp;
           
        }*//*

        Debug.Log(string.Join("", chars));



        Queue<char> alphaQueue = new Queue<char>(); //������ �ܾ�� ���� ������ deque
    
        foreach (char i in chars)
        {//ť ����
            alphaQueue.Enqueue(i);
            *//*Debug.Log(i);*//*      
        }
        
        //ť ���
        int Queuesize = alphaQueue.Count;

        for (int i = 0; i < Queuesize; i++)
        {//���� ť���� ���ĺ� ������
            var temp = alphaQueue.Dequeue();//��������
            alphaQueue.Enqueue(temp); //ó�� ���·� �����
        }

        return alphaQueue;

    }*/
}
