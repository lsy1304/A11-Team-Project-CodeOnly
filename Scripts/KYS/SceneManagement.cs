using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
    private bool isSceneLoading = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        if (!isSceneLoading)
        {
            isSceneLoading = true;
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += (AsyncOperation op) =>
            {
                isSceneLoading = false;
            };
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (!isSceneLoading)
        {
            isSceneLoading = true;
            SceneManager.UnloadSceneAsync(sceneName).completed += (AsyncOperation op) =>
            {
                isSceneLoading = false;
            };
        }
    }

    public bool IsSceneLoading()
    {
        return isSceneLoading;
    }

    public void ResetSceneLoading()
    {
        isSceneLoading = false;
    }
}
