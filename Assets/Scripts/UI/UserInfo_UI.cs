using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UserInfo_UI : MonoBehaviour
{
    [SerializeField]
    private Text Id;

    [SerializeField]
    private Text Nickname;
    [SerializeField]
    private Text Password;
    [SerializeField]
    private Text Email;

    public bool isBrif;

    /// <summary>
    /// ID, NickName UI 표출
    /// </summary>
    public void UpdateBrif()
    {
        Id.text = User_Info.Data.UserID;
        Nickname.text = User_Info.Data.NickName == null ? "No Name" : User_Info.Data.NickName;
    }
    /// <summary>
    /// NickName, Email UI 표출
    /// </summary>
    public void UpdateDetail()
    {
        Nickname.text = User_Info.Data.NickName == null ? "No Name" : User_Info.Data.NickName;
        Email.text = User_Info.Data.emailForFindPassword;
    }
    /// <summary>
    /// PasswordCheck에서 비밀번호 받아오기
    /// </summary>
    /// <param name="pw"></param>
    public void Get_Password(InputField pw)
    {
        Password.text = pw.text;
    }
    /// <summary>
    /// 비활성화시 scrollview 활성화
    /// </summary>
    private void OnDisable()
    {
        if(isBrif)
            Main_UIManager.Instance.Scrollview_On();
    }
}
