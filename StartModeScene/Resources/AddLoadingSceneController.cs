using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public class AddLoadingSceneController : MonoBehaviour //Add는 Addressable을말함 어드레서블로딩씬컨트롤러
{
    private static AddLoadingSceneController instance;
    public static AddLoadingSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<AddLoadingSceneController>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create(); //AddLoadingUI프리팹을 인스턴스화
                }
            }
            return instance;
        }
    }

    private static AddLoadingSceneController Create() //LoadingUI프리팹을 인스턴스화
    {
        return Instantiate(Resources.Load<AddLoadingSceneController>("AddLoadingUI"));
    }

    private void Awake()
    {
        if (Instance != this) //자기자신이 아니라면 파괴
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //자기자신이라면 파괴하지않는다
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image progressBar;

    private string loadSceneName;


    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());

    }


    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true)); //호출한 코루틴이 끝날때까지 기다림

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(loadSceneName, LoadSceneMode.Single, false);



        //그리고 씬을 90퍼센트까지만 로딩함       //true로하면 좀 로딩이 너무빠를시 깜빡!하고 지나갈수도잇음
        float timer = 0f;
        while (true)
        {
            yield return null;
            if (handle.PercentComplete < 0.9f) // 90퍼센트까지 로딩하므로
            {
                Debug.Log(handle.PercentComplete);
                progressBar.fillAmount = handle.PercentComplete;

            }
            else //진행도가 90퍼센트보다 커지면 페이크로딩, 나머지 10퍼센트를 2초간 채우고 씬을불러옴
            {

                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer); //0.9f와 1f사이에 timer가 0.05라면  0.95로채워짐

                if (progressBar.fillAmount >= 1f && handle.Status == AsyncOperationStatus.Succeeded) //만약에 후자의 판단이 없엇으면 1초안에 handle.PercentComplete가 완료가 안된다면 오류가 뜸
                {
                    Debug.Log(handle.Status);
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {

                        yield return handle.Result.ActivateAsync(); //이게 op.allowSceneActivation = true; 이 역할임. 딱 로딩 완료된 씬 불러오는거,
                        StartCoroutine(Fade(false));
                    }
                    else
                    {
                        Debug.LogError($"AsyncHandle Status :{handle.Status}");
                    }
                    yield break;
                }
            }

        }
    }



    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer); //왼쪽은 true일때 fade in , 오른쪽은 false일때 fade out
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }

}
