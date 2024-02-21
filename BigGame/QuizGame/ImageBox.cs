using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject pic; //�ٲ� �̹����� ��� ������Ʈ
    [SerializeField] private Sprite[] images;
    
    private Image correntImage;
    
    void Awake()
    {// ��ü�� ���� �����ϱ� ���� Awake ��� alpOwl������ setActive(false)
     // setActive(true)�϶� getcomponent����
        correntImage = pic.GetComponent<Image>();
    }
    public void ChangeImage(int i)
    {
        
        //Debug.Log((images[i]));
        //Debug.Log(correntImage);
        correntImage.sprite = images[i];
    }
    public void OffImage()
    {
        Color alpha = correntImage.color;
        alpha.a = 0;
        correntImage.color = alpha;
        /*gameObject.SetActive(false);*/
    }
    public void OnImage()
    {
        Color alpha = correntImage.color;
        alpha.a = 1;
        correntImage.color = alpha;
        /*gameObject.SetActive(true);*/
    }
}
