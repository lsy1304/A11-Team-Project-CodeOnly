using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartScreenManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트 연결
    public RawImage rawImage; // RawImage 컴포넌트 연결
    public Button startButton;

    void Start()
    {
        // VideoPlayer 설정
        if (videoPlayer != null)
        {
            videoPlayer.isLooping = true; // 비디오 루프 설정
            videoPlayer.Play(); // 비디오 재생
        }

        // 시작 화면 BGM 재생
        BattleSoundManager.Instance.PlayStartScreenBGM();
    }

    public void StartGame()
    {
        BattleSoundManager.Instance.StopStartScreenBGM(); // 시작 화면 BGM 정지
        BattleSoundManager.Instance.PlayMainBGM(); // 메인 BGM 재생
        SceneManager.LoadScene("NewMap");
    }
}
