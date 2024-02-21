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
        if (PlayerPrefs.GetInt("Downloaded") == 1) //캐시가 다운이 됐다면
        {
            NotInternet.SetActive(false);
            DataInternet.SetActive(false);
            DataDownloading.SetActive(false);

            ExitButton.SetActive(true);
        }
        else    //캐시가 다운이 안 됐다면
        {
            ExitButton.SetActive(false);    //종료버튼 비활성화
            
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    NotInternet.SetActive(true);
                    DataInternet.SetActive(false);
                    DataDownloading.SetActive(false);
                }
                else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork && !DataInternet_YesButton) //데이터네트워크 && Yes버튼을 이미 눌렀으면 안띄운다
                {
                    CheckDataSize();

                    NotInternet.SetActive(false);
                    DataInternet.SetActive(true);
                    DataDownloading.SetActive(false);
                }
                //Wifi연결이거나 Data사용시 yes버튼을 눌렀을 때
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
                int DownloadSize = (int)(SizeHandle.Result * 0.000000953d);     //Byte를 Mb로 변환
                Debug.Log("다운로드 크기 : " + DownloadSize.ToString() + "MB");
                sizeText.text = "다운로드 크기 : "+ DownloadSize.ToString()+"MB";
            };
    }
    IEnumerator Bundle_Down()
    {
        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(assetLabel.labelString); //이렇게만해도 다운이 시작되는듯
        
        while(handle.Status==AsyncOperationStatus.None)
        {
            DataDownloadingSlider.value = handle.GetDownloadStatus().Percent;
            Debug.Log(handle.GetDownloadStatus().Percent);
            yield return null;
        }
        if(handle.Status==AsyncOperationStatus.Succeeded)
        {
            DataDownloadingSlider.value = handle.GetDownloadStatus().Percent;
            PlayerPrefs.SetInt("Downloaded", 1);    //다운이 완료되면 로컬데이터값에 1을 저장한다.    //숫자 1이면 Downloaded가 1. 아무것도 안했을 땐 Downloaded가 0이다
        }
        else
        {
            Debug.LogError("FailedToDownload");
        }

        yield break;
    }
    

    


  


}

