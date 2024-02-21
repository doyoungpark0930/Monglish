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
        int arrSize = inp.Length * 2; // 보내줄 문자의 총 길이
        char[] chars = new char[arrSize];
        int k = 0;
        foreach(char c in inp)
        {
            chars[k++] = c;
        }
        for (; k < arrSize; k++)
        {// 처음 대문자가 소문자랑 중복안되게 만듦
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
        {// 글자 순서 섞기 플레이어에게 보낼 문자열 생성
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



        Queue<char> alphaQueue = new Queue<char>(); //보내줄 단어들 생성 먹으면 deque
    
        foreach (char i in chars)
        {//큐 생성
            alphaQueue.Enqueue(i);
            *//*Debug.Log(i);*//*      
        }
        
        //큐 사용
        int Queuesize = alphaQueue.Count;

        for (int i = 0; i < Queuesize; i++)
        {//만든 큐에서 알파벳 보내기
            var temp = alphaQueue.Dequeue();//내보내기
            alphaQueue.Enqueue(temp); //처음 상태로 만들기
        }

        return alphaQueue;

    }*/
}
