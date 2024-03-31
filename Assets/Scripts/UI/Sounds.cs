using UnityEngine;

public class Sounds : MonoBehaviour
{
    #region 싱글톤
    public static Sounds instance;                                  // 자주 사용될 사운드를 편하게 불러오기 위해 싱글톤 선언

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

    #region 자주 사용되는 UI 사운드 모음
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
    /// Button 소리
    /// </summary>
    public void Button_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Button);
    }
    /// <summary>
    ///  완료 소리
    /// </summary>
    public void Complite_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Complite);
    }
    /// <summary>
    /// 패널 열림 소리
    /// </summary>
    public void WindowOpen_Sound()
    {
        soundManager.Instance.PlaySoundEffect(WindowOpen);
    }
    /// <summary>
    ///  선택 소리
    /// </summary>
    public void Choice_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Choice);
    }
    /// <summary>
    /// 강화 성공 소리
    /// </summary>
    public void Upgrade_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Upgrade);
    }
    /// <summary>
    /// 시작 소리
    /// </summary>
    public void Start_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Start);
    }
    /// <summary>
    /// 준비 소리
    /// </summary>
    public void Ready_Sound()
    {
        soundManager.Instance.PlaySoundEffect(Ready);
    }
}
