using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController instance;
    public static LoadingSceneController Instance
    {//프로퍼티
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<LoadingSceneController>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create(); //LoadingUI프리팹을 인스턴스화
                }
            }
            return instance;
        }
    }

    private static LoadingSceneController Create() //LoadingUI프리팹을 인스턴스화
    {
        return Instantiate(Resources.Load<LoadingSceneController>("LoadingUI"));
    }

    private void Awake()
    {
        if(Instance != this) //자기자신이 아니라면 파괴
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
        SceneManager.sceneLoaded += OnSceneLoaded; //씬로딩이끝나는순간 ,여기잘이해안감
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());

    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true)); //호출한 코루틴이 끝날때까지 기다림

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName); //LoadSceneAsync는 비동기방식
        op.allowSceneActivation = false; //씬을 비동기로 불러올때, 씬의 로딩이 끝나면 자동으로 불러올 씬으로 이동할것인지 설정
        //그리고 씬을 90퍼센트까지만 로딩함       //true로하면 좀 로딩이 너무빠를시 깜빡!하고 지나갈수도잇음
        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f) // 90퍼센트까지 로딩하므로
            {
                progressBar.fillAmount = op.progress;
            }
            else //진행도가 90퍼센트보다 커지면 페이크로딩, 나머지 10퍼센트를 1초간 채우고 씬을불러옴
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer); //0.9f와 1f사이에 timer가 0.05라면  0.95로채워짐
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    { //씬의 로딩이 끝나면 유니티엔진이 SceneManager.sceneLoaded에 등록해둔 OnSceneLoaded함수를 자동 호출
        if (arg0.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded; //콜백을 제거하지 않으면 씬로딩 시작할때 등록한 콜백이 중첩됨

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
