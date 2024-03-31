using UnityEngine;

public class Sounds : MonoBehaviour
{
    #region �̱���
    public static Sounds instance;                                  // ���� ���� ���带 ���ϰ� �ҷ����� ���� �̱��� ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region ���� ���Ǵ� UI ���� ����
    [Header("Button_Sounds")]
    [SerializeField]
    private string Button;

    [Header("Complite_Sounds")]
    [SerializeField]
    private string Complite;

    [Header("WindowOpen_Sounds")]
    [SerializeField]
    private string WindowOpen;

    [Header("Choice_Sounds")]
    [SerializeField]
    private string Choice;

    [Header("Upgrade_Sounds")]
    [SerializeField]
    private string Upgrade;

    [Header("Ready_Sounds")]
    [SerializeField]
    private string Ready;

    [Header("Start_Sounds")]
    [SerializeField]
    private string Start;
    #endregion

    /// <summary>
    /// Button �Ҹ�
    /// </summary>
    public void Button_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Button);
    }
    /// <summary>
    ///  �Ϸ� �Ҹ�
    /// </summary>
    public void Complite_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Complite);
    }
    /// <summary>
    /// �г� ���� �Ҹ�
    /// </summary>
    public void WindowOpen_Sound()
    {
        soundManager.Instance.PlaySoundEffect(WindowOpen);
    }
    /// <summary>
    ///  ���� �Ҹ�
    /// </summary>
    public void Choice_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Choice);
    }
    /// <summary>
    /// ��ȭ ���� �Ҹ�
    /// </summary>
    public void Upgrade_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Upgrade);
    }
    /// <summary>
    /// ���� �Ҹ�
    /// </summary>
    public void Start_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Start);
    }
    /// <summary>
    /// �غ� �Ҹ�
    /// </summary>
    public void Ready_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Ready);
    }
}
