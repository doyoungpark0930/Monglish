using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AlpOwl alpOwl;

    [SerializeField] private AudioClip[] alphaBets = new AudioClip[26]; //���ĺ� ����
    [SerializeField] private AudioClip[] animals = new AudioClip[13]; //���� �ܾ� ����
    [SerializeField] private AudioClip[] successSound = new AudioClip[3]; //���� ���̽�
    [SerializeField] private AudioClip[] treeAudio = new AudioClip[4]; //�����ö󰡴� ���̽�
    [SerializeField] private AudioClip[] guessAudio = new AudioClip[3]; //����� ���̽�
    [SerializeField] private AudioClip treeWait; //���� ��� ���̽�


    [SerializeField] private AudioClip failSound; //���� ���̽�
    [SerializeField] private AudioClip [] clearSound = new AudioClip[2]; //���� Ŭ���� ���̽�
    AudioSource audioSource;

    int guessnum = -1;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void playTreeWait()
    {
        audioSource.PlayOneShot(treeWait);
    }

    public void playGuessAns()
    {
        int temp = Random.Range(0, 3);
        
        while (temp == guessnum || guessnum == -1) //���� ���̽��� ������
        {
            temp = Random.Range(0, 3); //0~2
            guessnum = 4;
            
        }
        guessnum = temp; //�÷����� ���̽� ����
        audioSource.PlayOneShot(guessAudio[guessnum]);

    }

    public void playTreeAudio(int i)
    {
        audioSource.PlayOneShot(treeAudio[i]);
    }

    public void playAlp(int i)
    {
        //audioSource.clip = alphaBets[i];
        audioSource.PlayOneShot(alphaBets[i]);
        //audioSource.Play(); 
    }
    public void playAnimals(int i)
    {
        //audioSource.clip = animals[i];
        audioSource.PlayOneShot(animals[i]);
    }
    public void playSuccess(int i)
    {
        audioSource.PlayOneShot(successSound[i]);
    }
    public void playFail()
    {
        audioSource.PlayOneShot(failSound);
    }
    public void playClear_0()
    {
        audioSource.PlayOneShot(clearSound[0]);

    }
    public void playClear_1()
    {

        audioSource.PlayOneShot(clearSound[1]);
    }
}
