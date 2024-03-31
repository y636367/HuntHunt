using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class RegisterAccount : Login_Base
{
    #region Variable
    [Header("ID")]
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private Text inputFieldID;

    [Header("PW")]
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private InputField inputFieldPW;

    [Header("PW_Confirm")]
    [SerializeField]
    private Image imagePW_C;
    [SerializeField]
    private InputField inputFieldPW_C;

    [Header("E-Mail")]
    [SerializeField]
    private Image imageMail;
    [SerializeField]
    private Text inputFieldMail;

    [SerializeField]
    private Button RegisterBtn;
    [SerializeField]
    private Button Back;
    #endregion

    /// <summary>
    /// ���� ���� �̺�Ʈ �߻� ��
    /// </summary>
    public void OnClickedAccount()
    {
        ResetUI(imageID, imagePW, imagePW_C, imageMail);                                // �� InputField ���� �ʱ�ȭ

        #region �� �׸� ���� üũ
        if (IsFieldEmpty(imageID, inputFieldID.text, "ID"))
            return;

        if (IsFieldEmpty(imagePW, inputFieldPW.text, "PW"))
            return;

        if (IsFieldEmpty(imagePW_C, inputFieldPW_C.text, "PW Ȯ��"))
            return;

        if (IsFieldEmpty(imageMail, inputFieldMail.text, "E-Mail"))
            return;
        #endregion

        if (!inputFieldPW.text.Equals(inputFieldPW_C.text))                             // ��й�ȣ�� ��й�ȣ Ȯ���� �ٸ� ��
        {
            GuideForCorrectlyEnteredData(imagePW_C, "��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }
        
        if (!inputFieldMail.text.Contains("@"))                                        // ���� ���� �˻�
        {
            GuideForCorrectlyEnteredData(imageMail, "���� ������ �߸� �Ǿ����ϴ�. (ex. address@xxxx.xxx)");
            return;
        }

        RegisterBtn.interactable = false;                                              // ��Ÿ ���ϰ� ��Ȱ��ȭ
        Back.interactable = false;

        SetMassage("���� �������Դϴ�.");

        CustomSIgnUp();                                                                // ������ ���� ���� �õ�
    }
    /// <summary>
    /// ���� ���� �õ��� �����κ��� ���� message ������� ó��
    /// </summary>
    private void CustomSIgnUp()
    {
        SendQueue.Enqueue(Backend.BMember.CustomSignUp,inputFieldID.text, inputFieldPW.text, callback =>
        {
            if (callback.IsSuccess())                                                                                // ���� ���� ��
            {
                SendQueue.Enqueue(Backend.BMember.UpdateCustomEmail, inputFieldMail.text, callback =>                // E-Mail ���� ������Ʈ �õ�
                {
                    if (callback.IsSuccess())                                                                        // E-Mail ���� ������Ʈ ���� ��
                    {
                        SetMassage($"���� ���� �Ϸ�. {inputFieldID.text}�� ȯ���մϴ�.");                            // ���� ���� ���

                        Quit_Game.Instance.Limit = true;                                                             // Quit �Լ� ��� ���� ���� �� ����
                        Backend_GameData.Instance.GameDataInsert();                                                  // ������ ���� �Ǵ� �� ���̺� �� ����

                        Invoke("ActiveFalse", 0.5f);                                                                 // 0.5�� �� ActiveFalse �Լ� ����
                    }
                });
            }
            else                                                                                                    // ���� ���� ��
            {
                RegisterBtn.interactable = true;                                                                    // ���� ���� ��ư Ȱ��ȭ
                Back.interactable = true;

                string message = string.Empty;

                switch(int.Parse(callback.GetStatusCode()))                                                         // �����ڵ� ������ ��ȯ, message�� ���� ���� �� ���
                {
                    case 409:
                        message = "�̹� �����ϴ� ���̵� �Դϴ�.";
                        break;
                    case 403:                                                                                       // ���� ����̽�
                    case 401:                                                                                       // ������Ʈ ���� ����
                    case 400:                                                                                       // ����̽� ������ null �� ���
                    default:                                                                                        // �� ��
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("���̵�"))                                                                    // �ߺ� ���̵� ���� ���
                {
                    GuideForCorrectlyEnteredData(imageID, message);
                }
                else                                                                                                // ���� ��� �޽��� ���
                {
                    SetMassage(message);
                }

                Invoke(nameof(Reset_Text), 0.7f);
            }
        });
    }
    /// <summary>
    /// ȸ�� ���� �г� �ʱ�ȭ �� �ݱ�
    /// </summary>
    private void ActiveFalse()
    {
        Reset_Text();                                                                                              // InputField �ʱ�ȭ
        Quit_Game.Instance.Panel_Out();                                                                            // ȸ�� ���� �г� List���� ����
        Quit_Game.Instance.Limit = false;
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        ResetUI(imageID, imagePW, imagePW_C, imageMail);                                                            // Ȱ��ȭ �ɶ� ���� �� InputField �ʱ�ȭ
    }
}
