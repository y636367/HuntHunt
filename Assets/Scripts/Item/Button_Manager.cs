using UnityEngine;

public class Button_Manager : MonoBehaviour
{
    /// <summary>
    /// ���� �Ͻ� ����(ȿ���� �Ͻ�����)
    /// </summary>
    public void Pause_B()
    {
        GameManager.Instance.Stop();
        soundManager.Instance.Pause_Sfx();
    }
    /// <summary>
    /// ���� �����(ȿ���� �ٽ� ���)
    /// </summary>
    public void Resume_B()
    {
        GameManager.Instance.Resume();
        soundManager.Instance.UnPause_Sfx();
    }
}
