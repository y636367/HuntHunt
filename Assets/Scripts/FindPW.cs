using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class FindPW : Login_Base
{
    #region Variable
    [SerializeField]
    private Image imageid;
    [SerializeField]
    private Text inputFieldid;

    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private Text inputFieldEmail;

    [SerializeField]
    private Button Find_pw_btn;
    [SerializeField]
    private Button Back;
    #endregion
    public void OnClickFindID()
    {
        ResetUI(imageid, imageEmail);                                                                                     // InputField ���� �� Text �ʱ�ȭ

        if (IsFieldEmpty(imageid, inputFieldid.text, "���̵�"))                                                           // ���� üũ
            return;
        if (IsFieldEmpty(imageEmail, inputFieldEmail.text, "���� �ּ�"))
            return;

        if (!inputFieldEmail.text.Contains("@"))                                                                          // ���� ���� üũ
        {
            GuideForCorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.(ex. address@xxx.xxx)");
            return;
        }

        Find_pw_btn.interactable = false;                                                                                 // ��ư ��Ȱ��ȭ
        Back.interactable = false;

        SetMassage("���� �߼� ��...");

        FindCustomPW();                                                                                                 // ������ ��й�ȣ ã�� �õ�
    }
    private void FindCustomPW()
    {
        SendQueue.Enqueue(Backend.BMember.ResetPassword, inputFieldid.text, inputFieldEmail.text , callback =>         // ��й�ȣ�� �ʱ�ȭ�ϰ�, �ʱ�ȭ�� ��й�ȣ ������ �̸��Ϸ� �߼�
        {
            if (callback.IsSuccess())                                                                                  // ���� �߼� ���� ��
            {
                SetMassage($"{inputFieldEmail.text} �ּҷ� ������ �߼��Ͽ����ϴ�.");

                Quit_Game.Instance.Limit = true;

                Invoke("ActiveFalse", 0.5f);
            }
            else                                                                                                      // �߼� ���� ��
            {
                Find_pw_btn.interactable = true;                                                                      // ��ư Ȱ��ȭ
                Back.interactable = true;

                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404:                                                                                         // �ش� �̸����� ����ڰ� ���� ���
                        message = "�ش� �̸����� ����ϴ� ����ڰ� �����ϴ�.";
                        break;
                    case 429:                                                                                         // 24�ð� �̳� 5ȸ �̻� ���� �̸��� ������ ���̵�/��й�ȣ ã�� �õ��� ���
                        message = "24�ð� �̳� 5ȸ �̻� ���̵�/��й�ȣ ã�⸦ �õ��Ͽ����ϴ�.";
                        break;
                    default:                                                                                          // statusCode : 400  ������Ʈ�� Ư�����ڰ� �߰��� ���(�ȳ� ���� �̹߼� �� ���� �߻�)
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("�̸���"))                                                                       //�̸��� �߸� �Էµ� ��� �̱⿡ ��� ǥ��
                {
                    GuideForCorrectlyEnteredData(imageEmail, message);
                }
                else
                {
                    SetMassage(message);
                }

                Invoke(nameof(Reset_Text), 0.7f);
            }
        });
    }
    private void ActiveFalse()
    {
        Reset_Text();
        Quit_Game.Instance.Panel_Out();
        Quit_Game.Instance.Limit = false;
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        ResetUI(imageid, imageEmail);
    }
}
