using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject pic; //바꿀 이미지가 담길 오브젝트
    [SerializeField] private Sprite[] images;
    
    private Image correntImage;
    
    void Awake()
    {// 객체를 먼저 저장하기 위해 Awake 사용 alpOwl에서는 setActive(false)
     // setActive(true)일때 getcomponent가능
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
