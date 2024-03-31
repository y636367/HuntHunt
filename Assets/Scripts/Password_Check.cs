using BackEnd;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Password_Check : Login_Base
{
    [System.Serializable]
    public class PasswordCheckEvent : UnityEngine.Events.UnityEvent { }                             // ��й�ȣ ��ġ Ȯ�ν� ȣ���� �޼ҵ� ���
    public PasswordCheckEvent onCheckEvent=new PasswordCheckEvent();                                // Ŭ���� �ν��Ͻ� ����

    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private InputField inputFieldPW;                                                                // ��й�ȣ �׸����� ���ý� **** ���ڰ� �ؽ�Ʈ ��� ���ԵǱ⿡ InputField�� Text ��������

    [SerializeField]
    private Button CheckBtn;

    /// <summary>
    /// ��й�ȣ Ȯ�� �Լ�
    /// </summary>
    public void OnClicekdPassword_Check()
    {
        ResetUI(imagePW);                                                                           // ���� �ʱ�ȭ

        if (IsFieldEmpty(imagePW, inputFieldPW.text, "PW"))                                         // ���� üũ
            return;

        CheckBtn.interactable = false;                                                              // ��Ÿ ���ϰ� ��Ȱ��ȭ

        StartCoroutine(nameof(P_CheckProcess));                                                     // Ȯ�� ��û �ϴ� ���� ���� ������Ʈ
        Quit_Game.Instance.Limit = true;

        ResponseToPasswordCheck(inputFieldPW.text);
    }
    /// <summary>
    /// ������ Ȯ�� ��û �Լ�
    /// </summary>
    /// <param name="PW"></param>
    private void ResponseToPasswordCheck(string PW)
    {
        SendQueue.Enqueue(Backend.BMember.ConfirmCustomPassword, PW, callback =>                    // ������ ��й�ȣ Ȯ�� �õ�
        {
            StopCoroutine(nameof(P_CheckProcess));                                                  // callback �ý� �ڷ�ƾ ����

            if (callback.IsSuccess())                                                               // ��й�ȣ ��ġ �ҽ�
            {
                SetMassage("��й�ȣ�� ��ġ�մϴ�.");

                Quit_Game.Instance.Limit = false;                                                   // Quit �Լ� ���� ���� �� ����
                Quit_Game.Instance.Panel_Out();                                                     // ��й�ȣ Ȯ�� �г� ������ ���ÿ� List ���� �Լ� ����

                onCheckEvent.Invoke();                                                              // ��й�ȣ ��ġ �� onCheckEvent�� ��ϵ� �̺�Ʈ �޼ҵ� ȣ��
            }
            else                                                                                    // ��й�ȣ ����ġ
            {
                CheckBtn.interactable = true;                                                       // ��ư Ȱ��ȭ

                string message = "�߸��� ��й�ȣ �Դϴ�.";

                GuideForCorrectlyEnteredData(imagePW, message);                                     // InputField ���� ���� �� Text Message ����

                Invoke(nameof(Reset_Text), 0.7f);
            }
        });
    }
    /// <summary>
    /// Ȯ���ϴ� ���� ������Ʈ �Լ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator P_CheckProcess()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            SetMassage($"Ȯ�� ���Դϴ�...{time:F1}");                                              // �����ð� ���

            yield return null;
        }
    }
    private void OnEnable()
    {
        Reset_Text();
        CheckBtn.interactable = true;
    }
}
