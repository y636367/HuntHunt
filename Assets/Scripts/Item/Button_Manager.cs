using UnityEngine;

public class Button_Manager : MonoBehaviour
{
    /// <summary>
    /// 게임 일시 정지(효과음 일시정지)
    /// </summary>
    public void Pause_B()
    {
        GameManager.Instance.Stop();
        soundManager.Instance.Pause_Sfx();
    }
    /// <summary>
    /// 게임 재시작(효과음 다시 재생)
    /// </summary>
    public void Resume_B()
    {
        GameManager.Instance.Resume();
        soundManager.Instance.UnPause_Sfx();
    }
}
