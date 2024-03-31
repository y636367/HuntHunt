using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
public class Login : Login_Base
{
    #region Variable
    [Header("ID")]                                              // ���̵� ���� ����
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private Text inputFieldID;

    [Header("PW")]                                              // ��й�ȣ ���� ����
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private InputField inputFieldPW;                            // ��й�ȣ �׸����� ���ý� **** ���ڰ� �ؽ�Ʈ ��� ���ԵǱ⿡ InputField�� �Էµ� Text �� ��� ����

    [Header("BTN")]                                             // �� ��ư ����
    [SerializeField]
    private Button LoginBtn;
    [SerializeField]
    private Button JoinBtn;
    [SerializeField]
    private Button FindIDBtn;
    [SerializeField]
    private Button FindPWBtn;

    [Header("AuotoLogin")]
    [SerializeField]
    private Toggle autologin;                                                   // Autologin üũ
    private string autologinKey = "AutoLogin";                                  // PlayerPrefs �� ���� �� ������ ���� string ����
    #endregion

    /// <summary>
    /// �α��� ��ư �̺�Ʈ �߻� ��
    /// </summary>
    public void OnClickeLogin()
    {
        ResetUI(imageID, imagePW);                                              // �Ű������� �Է��� InputField UI�� ����� Message ���� �ʱ�ȭ

        if (IsFieldEmpty(imageID, inputFieldID.text, "ID"))                     // ID InputField ���� üũ
            return;

        if (IsFieldEmpty(imagePW, inputFieldPW.text, "PW"))                     // PW InputFIeld ���� üũ
            return;

        LoginBtn.interactable = false;                                          // �� ��ư ��Ÿ ������ ���� ��Ȱ��ȭ
        JoinBtn.interactable = false;
        FindIDBtn.interactable = false;
        FindPWBtn.interactable = false;

        StartCoroutine(nameof(LoginProcess));                                   // ���� �α��� ��û �ϴ� ���� ���� ������Ʈ �ʿ�
        Quit_Game.Instance.Limit = true;                                        // Quit ��ũ��Ʈ ���� ��� ���� ���� �� ����

        ResponseToLogin(inputFieldID.text, inputFieldPW.text);                  // ������ �α��� �õ�
    }
    /// <summary>
    /// ID, PW ���� ������ ������ �α��� �õ�
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="PW"></param>
    private void ResponseToLogin(string ID, string PW)
    {
        string Error_message = string.Empty;                                    // ���� �޽������� �ޱ� ���� string ���� ����

        SendQueue.Enqueue(Backend.BMember.CustomLogin, ID, PW, callback =>      // SendQueue(�񵿱� ť) ����(�ڷ�ƾ ��� ����)
        {
            StopCoroutine(nameof(LoginProcess));                                // �ݹ��� ���� �� ����ؼ� ������Ʈ �Ǵ� �ڷ�ƾ ����
            Sounds.instance.Complite_Sound();                                   // �Ҹ� ���

            if (callback.IsSuccess())                                           // �α��� ���� ��
            {
                SetMassage($"{inputFieldID.text}�� ȯ���մϴ�.");               // ���� ���

                if (autologin.isOn == true)                                     // autologin �� üũ�Ǿ� �ִٸ�
                {
                    PlayerPrefs.SetInt(autologinKey, 1);                        // PlayerPrefs�� 1(��) ����
                }
                else                                                            // üũ�Ǿ� ���� �ʴٸ�
                {
                    PlayerPrefs.SetInt(autologinKey, 0);                        // PlayerPrefs�� 0(��) ����
                }                                                               // ���� �α��ν� ����

                Error_message = Backend_GameData.Instance.GetDatas();

                if (Error_message != "")                 // User�� ������ �������� �ҷ��� ����, �����͸� �ҷ����� ���� �����Ų �Լ� ���� �� ������ �־��ٸ�
                {
                    SetMassage(Error_message);                                  // �ش� ���� ���� ����

                    Invoke(nameof(Return_Loadding), 0.2f);                      // ���� �̻� �߻��̱⿡ �ʱ� ȭ������ �̵�
                    return;
                }
                else
                {
                    Invoke("Login_", 0.3f);                                     // 0.2�� �� Login_ �Լ� ����
                }
            }
            else                                                                // �α��� ���� ��
            {
                LoginBtn.interactable = true;                                   // �� ��ư Ȱ��ȭ
                JoinBtn.interactable = true;
                FindIDBtn.interactable = true;
                FindPWBtn.interactable = true;

                string message = string.Empty;                                  // ���� Ȯ���� ���� string ���� ����

                switch (int.Parse(callback.GetStatusCode()))                    // ���� �ڵ带 ������ ��ȯ�ؼ� �ش� ���� ���� message�� ������ ���
                {
                    case 401:                                                   // �������� �ʴ� ���̵�, �Ǵ� �߸��� ��й�ȣ Ȯ��
                        message = callback.GetMessage().Contains("customId") ? "�������� �ʴ� ���̵��Դϴ�." : "�߸��� ��й�ȣ �Դϴ�.";
                        break;
                    case 403:                                                   // ���� �Ǵ� ��� ���� Ȯ��
                        message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�." : "���ܴ��� ����̽��Դϴ�.";
                        break;
                    case 410:                                                   // Ż�� ������ Ȯ��
                        message = "Ż�� �������� �����Դϴ�.";
                        break;
                    default:                                                    // �׿�
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("��й�ȣ"))                               // 401 : �߸��� ��й�ȣ �Ͻ�
                {
                    GuideForCorrectlyEnteredData(imagePW, message);             // �ش� ĭ(InputField) ���� �������� �ð��� ǥ��
                }
                else                                                            // ���̵� �߸��Ǿ��� ��
                {
                    GuideForCorrectlyEnteredData(imageID, message);
                }

                Invoke(nameof(Reset_Text), 0.7f);                               // 0.7�� �� Reset_Text �Լ� ����
            }
        });
    }
    /// <summary>
    /// ������ callback �޾� �ö����� ����� �ڷ�ƾ �Լ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoginProcess()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            SetMassage($"�α��� ���Դϴ�...{time:F1}");                        // ���� �ð� ���

            yield return null;
        }
    }
    /// <summary>
    /// �α��� ���� �� ����� �Լ�
    /// </summary>
    private void Login_()                               
    {
        Quit_Game.Instance.Limit = false;                                       // Quit ��ũ��Ʈ ���� ��� ���� ���� �� ����
        Utils.Instance.LoadScene(SceneNames.Main);                              // Main �� Ȱ��ȭ �۾� ����
    }
    /// <summary>
    /// ���� ������ ���� �ʱ� ȭ�� �̵� �Լ�
    /// </summary>
    private void Return_Loadding()
    {
        Utils.Instance.LoadScene(SceneNames.Loadding);                          // �ʱ�ȭ�� Ȱ��ȭ �۾� ����
    }
}

// nameof : �ɹ�, ����, ���Ŀ� ���� ���ڿ� ��ȯ ex) nameof(List<list>) ��� List ��ȯ
// Parse : ����ȯ
// Contains : ���ڿ� ����Ȯ��
