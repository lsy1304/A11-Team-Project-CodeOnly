using System.Collections;
using UnityEngine;

public class BattleSoundManager : Singleton<BattleSoundManager>
{
    public AudioClip startScreenBGM;   // 시작 화면 브금
    public AudioClip mainBGM;          // 메인 브금
    public AudioClip battleBGM;        // 배틀 브금
    public AudioClip playerAttackSound;
    public AudioClip playerSkillSound;
    public AudioClip enemyAttackSound;
    public AudioClip enemySkillSound;

    public AudioSource startScreenBGMSource;
    public AudioSource mainBGMSource;
    public AudioSource battleBGMSource;
    public AudioSource effectSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        startScreenBGMSource = gameObject.AddComponent<AudioSource>();
        mainBGMSource = gameObject.AddComponent<AudioSource>();
        battleBGMSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();

        startScreenBGMSource.loop = true;
        mainBGMSource.loop = true;
        battleBGMSource.loop = true;

        startScreenBGMSource.volume = 0.5f; // 초기 볼륨을 50%로 설정
        mainBGMSource.volume = 0.1f;        // 초기 볼륨을 10%로 설정
        battleBGMSource.volume = 0.5f;      // 초기 볼륨을 50%로 설정
    }

    public void PlayStartScreenBGM()
    {
        if (startScreenBGM != null)
        {
            startScreenBGMSource.clip = startScreenBGM;
            StartCoroutine(FadeIn(startScreenBGMSource, 0.5f));
        }
        else
        {
            Debug.LogWarning("Start Screen BGM is not set.");
        }
    }

    public void PlayMainBGM()
    {
        if (mainBGM != null)
        {
            mainBGMSource.clip = mainBGM;
            StartCoroutine(FadeIn(mainBGMSource, 0.5f)); 
        }
        else
        {
            Debug.LogWarning("Main BGM is not set.");
        }
    }

    public void PlayBattleBGM()
    {
        if (battleBGM != null)
        {
            battleBGMSource.clip = battleBGM;
            StartCoroutine(FadeIn(battleBGMSource, 0.5f));
        }
        else
        {
            Debug.LogWarning("Battle BGM is not set.");
        }
    }

    public void StopStartScreenBGM()
    {
        StartCoroutine(FadeOut(startScreenBGMSource, 0.5f));
    }

    public void StopMainBGM()
    {
        StartCoroutine(FadeOut(mainBGMSource, 0.5f));
    }

    public void StopBattleBGM()
    {
        StartCoroutine(FadeOut(battleBGMSource, 0.5f));
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.volume = 0;
        audioSource.Play();
        float targetVolume = 0.1f; 

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void TransitionToBattleBGM()
    {
        StopMainBGM();
        StartCoroutine(WaitAndPlayBattleBGM(1f)); // 메인 BGM이 페이드 아웃된 후 배틀 BGM 페이드 인
    }

    private IEnumerator WaitAndPlayBattleBGM(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayBattleBGM();
    }

    public void TransitionToMainBGM()
    {
        StopBattleBGM();
        StartCoroutine(WaitAndPlayMainBGM(1f)); // 배틀 BGM이 페이드 아웃된 후 메인 BGM 페이드 인
    }

    private IEnumerator WaitAndPlayMainBGM(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayMainBGM();
    }
}
