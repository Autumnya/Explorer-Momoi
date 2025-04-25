using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : SingletonMono<LoadManager>
{
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private Image _loadingBg;
    [SerializeField] private Canvas _loadingMaskCanvas;

    [SerializeField] private float _fadeSpeed;

    private Scene _rootScene;
    private Scene? _oldScene;
    public string CurLoadingSceneName { get; private set; }

    protected override void OnAwake()
    {
        _rootScene = SceneManager.GetActiveScene();
        if (_rootScene == null)
        {
            Debug.LogError("Can't find loading scene.");
        }
        CurLoadingSceneName = "";

        Init();
        SwitchScene("MainScene");
    }

    private void Update()
    {

    }

    private void Init()
    {

    }

    private IEnumerator Fade()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha = Mathf.Clamp(alpha + Time.deltaTime * _fadeSpeed, 0f, 1f);
            _loadingBg.color = Color.Lerp(Color.clear, Color.black, alpha);
            yield return null;
        }
        if (SceneManager.GetActiveScene() != _rootScene)
            SceneManager.SetActiveScene(_rootScene);
        _loadingBar.gameObject.SetActive(true);
        StartCoroutine(OnLoadingScene());
    }

    private IEnumerator OnLoadingScene()
    {
        AsyncOperation asyncOperation;
        if (_oldScene != null)
        {
            asyncOperation = SceneManager.UnloadSceneAsync((Scene)_oldScene);
            if (asyncOperation != null)
            {
                while (!asyncOperation.isDone)
                {
                    _loadingBar.value = asyncOperation.progress / 0.9f;
                    yield return null;
                }
            }
        }
        asyncOperation = SceneManager.LoadSceneAsync(CurLoadingSceneName, LoadSceneMode.Additive);
        while (!asyncOperation.isDone)
        {
            _loadingBar.value = asyncOperation.progress / 0.9f;
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(CurLoadingSceneName));
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        _loadingBar.gameObject.SetActive(false);
        float alpha = _loadingBg.color.a;
        while (alpha > 0f)
        {
            alpha = Mathf.Clamp(alpha - Time.deltaTime * _fadeSpeed, 0f, 1f);
            _loadingBg.color = Color.Lerp(Color.clear, Color.black, alpha);
            yield return null;
        }
        _loadingBg.gameObject.SetActive(false);
        _loadingMaskCanvas.gameObject.SetActive(false);
        yield return null;
    }

    public void SwitchScene(string sceneName)
    {
        Scene? curScene = SceneManager.GetActiveScene();
        if (curScene != _rootScene)
            _oldScene = curScene;
        else
            _oldScene = null;

        CurLoadingSceneName = sceneName;
        _loadingBar.value = 0f;
        _loadingMaskCanvas.gameObject.SetActive(true);
        _loadingBg.gameObject.SetActive(true);
        //ÇÐ»»Loading³¡¾°
        StartCoroutine(Fade());
    }
}
