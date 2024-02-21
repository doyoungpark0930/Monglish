using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController instance;
    public static LoadingSceneController Instance
    {//������Ƽ
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
                    instance = Create(); //LoadingUI�������� �ν��Ͻ�ȭ
                }
            }
            return instance;
        }
    }

    private static LoadingSceneController Create() //LoadingUI�������� �ν��Ͻ�ȭ
    {
        return Instantiate(Resources.Load<LoadingSceneController>("LoadingUI"));
    }

    private void Awake()
    {
        if(Instance != this) //�ڱ��ڽ��� �ƴ϶�� �ı�
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
        SceneManager.sceneLoaded += OnSceneLoaded; //���ε��̳����¼��� ,���������ؾȰ�
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());

    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true)); //ȣ���� �ڷ�ƾ�� ���������� ��ٸ�

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName); //LoadSceneAsync�� �񵿱���
        op.allowSceneActivation = false; //���� �񵿱�� �ҷ��ö�, ���� �ε��� ������ �ڵ����� �ҷ��� ������ �̵��Ұ����� ����
        //�׸��� ���� 90�ۼ�Ʈ������ �ε���       //true���ϸ� �� �ε��� �ʹ������� ����!�ϰ� ��������������
        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f) // 90�ۼ�Ʈ���� �ε��ϹǷ�
            {
                progressBar.fillAmount = op.progress;
            }
            else //���൵�� 90�ۼ�Ʈ���� Ŀ���� ����ũ�ε�, ������ 10�ۼ�Ʈ�� 1�ʰ� ä��� �����ҷ���
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer); //0.9f�� 1f���̿� timer�� 0.05���  0.95��ä����
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    { //���� �ε��� ������ ����Ƽ������ SceneManager.sceneLoaded�� ����ص� OnSceneLoaded�Լ��� �ڵ� ȣ��
        if (arg0.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded; //�ݹ��� �������� ������ ���ε� �����Ҷ� ����� �ݹ��� ��ø��

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
