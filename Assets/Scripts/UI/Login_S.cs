using UnityEngine;

public class Login_S : MonoBehaviour
{
    [SerializeField]
    private string now_BGM;                                             // 현재 씬에서 재생할 BGM 이름

    public bool Awake_;                                                 // Awake 여부 확인
    #region 자주 사용 되는 효과음
    public void Button_S()
    {
        Sounds.instance.Button_Sound();
    }
    public void Window_S()
    {
        Sounds.instance.WindowOpen_Sound();
    }
    public void Upgrade_S()
    {
        Sounds.instance.Upgrade_Sound();
    }
    public void Choice_S()
    {
        Sounds.instance.Choice_Sound();
    }
    public void Ready_S()
    {
        Sounds.instance.Ready_Sound();
    }
    public void Sstart_S()
    {
        Sounds.instance.Start_Sound();
    }
    #endregion

    /// <summary>
    /// 배경음 재생
    /// </summary>
    public void BGM_S()
    {
        soundManager.Instance.PlaySoundBGM(now_BGM);
    }
    /// <summary>
    /// 배경음 정지
    /// </summary>
    public void StopBGM()
    {
        soundManager.Instance.StopAllSoundBGM();
    }
    /// <summary>
    /// 배경음 Fade_Out
    /// </summary>
    public void FadeOut_BGM()
    {
        soundManager.Instance.Sounds_BGM_Fade_Out();
    }
    /// <summary>
    /// 매씬 시작될때마다 실행
    /// </summary>
    private void Awake()
    {
        if (Awake_)
        {
            soundManager.Instance.Reset_BGM_Fade();                         // Fade_Out 되었던 BGM 오디오 소스 크기 초기화
            StopBGM();                                                      // 재생중이던(이전 씬에서 재생되던) BGM 정지

            if (!string.IsNullOrEmpty(now_BGM))                             // 현재 활성화된 씬에서 재생되어야 할 BGM이 있다면
            {
                BGM_S();                                                    // 배경음 재생
            }
        }
    }
    public void Instance_Setting()
    {
        soundManager.Instance.Reset_BGM_Fade();                         // Fade_Out 되었던 BGM 오디오 소스 크기 초기화
        StopBGM();                                                      // 재생중이던(이전 씬에서 재생되던) BGM 정지
        
        if (!string.IsNullOrEmpty(now_BGM))                             // 현재 활성화된 씬에서 재생되어야 할 BGM이 있다면
        {
            BGM_S();                                                    // 배경음 재생
        }
    }
}
