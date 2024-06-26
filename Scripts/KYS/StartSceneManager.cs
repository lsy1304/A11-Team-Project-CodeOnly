using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartScreenManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoPlayer ������Ʈ ����
    public RawImage rawImage; // RawImage ������Ʈ ����
    public Button startButton;

    void Start()
    {
        // VideoPlayer ����
        if (videoPlayer != null)
        {
            videoPlayer.isLooping = true; // ���� ���� ����
            videoPlayer.Play(); // ���� ���
        }

        // ���� ȭ�� BGM ���
        BattleSoundManager.Instance.PlayStartScreenBGM();
    }

    public void StartGame()
    {
        BattleSoundManager.Instance.StopStartScreenBGM(); // ���� ȭ�� BGM ����
        BattleSoundManager.Instance.PlayMainBGM(); // ���� BGM ���
        SceneManager.LoadScene("NewMap");
    }
}
