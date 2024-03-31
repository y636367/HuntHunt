using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class FindID : Login_Base
{
    #region Variable
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private Text inputFieldEmail;

    [SerializeField]
    private Button Find_id_btn;
    [SerializeField]
    private Button Back;
    #endregion
    public void OnClickFindID()
    {
        ResetUI(imageEmail);                                                                                    // ���� �� InputField ���� �ʱ�ȭ

        if (IsFieldEmpty(imageEmail, inputFieldEmail.text, "���� �ּ�"))                                        // ���� üũ
            return;

        if (!inputFieldEmail.text.Contains("@"))                                                                // ���� üũ
        {
            GuideForCorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.(ex. address@xxx.xxx)");
            return;
        }

        Find_id_btn.interactable = false;                                                                       // ��ư ��Ȱ��ȭ
        Back.interactable = false;

        SetMassage("���� �߼� ��...");

        FindCustomID();                                                                                         // �������� ID ã�� �õ�
    }
    private void FindCustomID()
    {
        SendQueue.Enqueue(Backend.BMember.FindCustomID, inputFieldEmail.text, callback =>                            // ID ���� ����� �̸��Ϸ� �߼�
        {
            if (callback.IsSuccess())                                                                                // ���� �߼� ������
            {
                SetMassage($"{inputFieldEmail.text} �ּҷ� ������ �߼��Ͽ����ϴ�.");

                Quit_Game.Instance.Limit = true;
                Invoke("ActiveFalse", 0.5f);
            }
            else                                                                                                     // �߼� ���� ��
            {
                Find_id_btn.interactable = true;                                                                     // ��ư Ȱ��ȭ
                Back.interactable = true;

                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404:                                                                                       // �ش� �̸����� ����ڰ� ���� ���
                        message = "�ش� �̸����� ����ϴ� ����ڰ� �����ϴ�.";
                        break;
                    case 429:                                                                                       // 24�ð� �̳� 5ȸ �̻� ���� �̸��� ������ ���̵�/��й�ȣ ã�� �õ��� ���
                        message = "24�ð� �̳� 5ȸ �̻� ���̵�/��й�ȣ ã�⸦ �õ��Ͽ����ϴ�.";
                        break;
                    default:                                                                                        // statusCode : 400  ������Ʈ�� Ư�����ڰ� �߰��� ���(�ȳ� ���� �̹߼� �� ���� �߻�)
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("�̸���"))                                                                     //�̸��� �߸� �Էµ� ��� �̱⿡ ��� ǥ��
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
        ResetUI(imageEmail);
    }
}
