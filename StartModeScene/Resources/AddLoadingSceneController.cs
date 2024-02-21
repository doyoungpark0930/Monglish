using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public class AddLoadingSceneController : MonoBehaviour //Add�� Addressable������ ��巹����ε�����Ʈ�ѷ�
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
                    instance = Create(); //AddLoadingUI�������� �ν��Ͻ�ȭ
                }
            }
            return instance;
        }
    }

    private static AddLoadingSceneController Create() //LoadingUI�������� �ν��Ͻ�ȭ
    {
        return Instantiate(Resources.Load<AddLoadingSceneController>("AddLoadingUI"));
    }

    private void Awake()
    {
        if (Instance != this) //�ڱ��ڽ��� �ƴ϶�� �ı�
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //�ڱ��ڽ��̶�� �ı������ʴ´�
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
        yield return StartCoroutine(Fade(true)); //ȣ���� �ڷ�ƾ�� ���������� ��ٸ�

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(loadSceneName, LoadSceneMode.Single, false);



        //�׸��� ���� 90�ۼ�Ʈ������ �ε���       //true���ϸ� �� �ε��� �ʹ������� ����!�ϰ� ��������������
        float timer = 0f;
        while (true)
        {
            yield return null;
            if (handle.PercentComplete < 0.9f) // 90�ۼ�Ʈ���� �ε��ϹǷ�
            {
                Debug.Log(handle.PercentComplete);
                progressBar.fillAmount = handle.PercentComplete;

            }
            else //���൵�� 90�ۼ�Ʈ���� Ŀ���� ����ũ�ε�, ������ 10�ۼ�Ʈ�� 2�ʰ� ä��� �����ҷ���
            {

                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer); //0.9f�� 1f���̿� timer�� 0.05���  0.95��ä����

                if (progressBar.fillAmount >= 1f && handle.Status == AsyncOperationStatus.Succeeded) //���࿡ ������ �Ǵ��� �������� 1�ʾȿ� handle.PercentComplete�� �Ϸᰡ �ȵȴٸ� ������ ��
                {
                    Debug.Log(handle.Status);
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {

                        yield return handle.Result.ActivateAsync(); //�̰� op.allowSceneActivation = true; �� ������. �� �ε� �Ϸ�� �� �ҷ����°�,
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
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer); //������ true�϶� fade in , �������� false�϶� fade out
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }

}
