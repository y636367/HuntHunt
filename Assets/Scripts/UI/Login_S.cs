using UnityEngine;

public class Login_S : MonoBehaviour
{
    [SerializeField]
    private string now_BGM;                                             // ���� ������ ����� BGM �̸�

    public bool Awake_;                                                 // Awake ���� Ȯ��
    #region ���� ��� �Ǵ� ȿ����
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
    /// ����� ���
    /// </summary>
    public void BGM_S()
    {
        soundManager.Instance.PlaySoundBGM(now_BGM);
    }
    /// <summary>
    /// ����� ����
    /// </summary>
    public void StopBGM()
    {
        soundManager.Instance.StopAllSoundBGM();
    }
    /// <summary>
    /// ����� Fade_Out
    /// </summary>
    public void FadeOut_BGM()
    {
        soundManager.Instance.Sounds_BGM_Fade_Out();
    }
    /// <summary>
    /// �ž� ���۵ɶ����� ����
    /// </summary>
    private void Awake()
    {
        if (Awake_)
        {
            soundManager.Instance.Reset_BGM_Fade();                         // Fade_Out �Ǿ��� BGM ����� �ҽ� ũ�� �ʱ�ȭ
            StopBGM();                                                      // ������̴�(���� ������ ����Ǵ�) BGM ����

            if (!string.IsNullOrEmpty(now_BGM))                             // ���� Ȱ��ȭ�� ������ ����Ǿ�� �� BGM�� �ִٸ�
            {
                BGM_S();                                                    // ����� ���
            }
        }
    }
    public void Instance_Setting()
    {
        soundManager.Instance.Reset_BGM_Fade();                         // Fade_Out �Ǿ��� BGM ����� �ҽ� ũ�� �ʱ�ȭ
        StopBGM();                                                      // ������̴�(���� ������ ����Ǵ�) BGM ����
        
        if (!string.IsNullOrEmpty(now_BGM))                             // ���� Ȱ��ȭ�� ������ ����Ǿ�� �� BGM�� �ִٸ�
        {
            BGM_S();                                                    // ����� ���
        }
    }
}
