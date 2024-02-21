using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

public class CacheDownloaded : MonoBehaviour
{
    [SerializeField]
    AssetLabelReference assetLabel;
    [SerializeField]
    Text sizeText;

    [SerializeField]
    GameObject ExitButton;

    [SerializeField]
    GameObject NotInternet;
    [SerializeField]
    GameObject DataInternet;
    [SerializeField]
    GameObject DataDownloading;

    [SerializeField]
    Slider DataDownloadingSlider;

    bool DataInternet_YesButton=false;
    bool ExitMenu_On = false;
    public void YesButton()
    {
        DataInternet_YesButton = true;
    }
  

    void Awake()
    {
        NotInternet.SetActive(false);
        DataInternet.SetActive(false);
        DataDownloading.SetActive(false);
    }
    void Start()
    {
        //CacheDelete();
        CheckDataSize();


    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Downloaded") == 1) //ĳ�ð� �ٿ��� �ƴٸ�
        {
            NotInternet.SetActive(false);
            DataInternet.SetActive(false);
            DataDownloading.SetActive(false);

            ExitButton.SetActive(true);
        }
        else    //ĳ�ð� �ٿ��� �� �ƴٸ�
        {
            ExitButton.SetActive(false);    //�����ư ��Ȱ��ȭ
            
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    NotInternet.SetActive(true);
                    DataInternet.SetActive(false);
                    DataDownloading.SetActive(false);
                }
                else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork && !DataInternet_YesButton) //�����ͳ�Ʈ��ũ && Yes��ư�� �̹� �������� �ȶ���
                {
                    CheckDataSize();

                    NotInternet.SetActive(false);
                    DataInternet.SetActive(true);
                    DataDownloading.SetActive(false);
                }
                //Wifi�����̰ų� Data���� yes��ư�� ������ ��
                else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork || (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork && DataInternet_YesButton))
                {
                    CheckDataSize();

                    NotInternet.SetActive(false);
                    DataInternet.SetActive(false);
                    DataDownloading.SetActive(true);

                    StartCoroutine(Bundle_Down());
                }
            
            
        }
    }

    void CacheDelete()
    {
        Addressables.ClearDependencyCacheAsync(assetLabel.labelString);
        PlayerPrefs.SetInt("Downloaded", 0);
        Debug.Log("CacheDeleted");
    }
    void CheckDataSize()
    {
        Addressables.GetDownloadSizeAsync(assetLabel.labelString).Completed +=
            (SizeHandle) =>
            {
                Debug.Log(assetLabel.labelString);
                int DownloadSize = (int)(SizeHandle.Result * 0.000000953d);     //Byte�� Mb�� ��ȯ
                Debug.Log("�ٿ�ε� ũ�� : " + DownloadSize.ToString() + "MB");
                sizeText.text = "�ٿ�ε� ũ�� : "+ DownloadSize.ToString()+"MB";
            };
    }
    IEnumerator Bundle_Down()
    {
        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(assetLabel.labelString); //�̷��Ը��ص� �ٿ��� ���۵Ǵµ�
        
        while(handle.Status==AsyncOperationStatus.None)
        {
            DataDownloadingSlider.value = handle.GetDownloadStatus().Percent;
            Debug.Log(handle.GetDownloadStatus().Percent);
            yield return null;
        }
        if(handle.Status==AsyncOperationStatus.Succeeded)
        {
            DataDownloadingSlider.value = handle.GetDownloadStatus().Percent;
            PlayerPrefs.SetInt("Downloaded", 1);    //�ٿ��� �Ϸ�Ǹ� ���õ����Ͱ��� 1�� �����Ѵ�.    //���� 1�̸� Downloaded�� 1. �ƹ��͵� ������ �� Downloaded�� 0�̴�
        }
        else
        {
            Debug.LogError("FailedToDownload");
        }

        yield break;
    }
    

    


  


}

