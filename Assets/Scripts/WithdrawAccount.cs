using BackEnd;

public class WithdrawAccount : Login_Base
{
    /// <summary>
    /// Ż�� ���� �Լ�
    /// </summary>
    public void OnClickedWithdrawAccount()
    {
        SendQueue.Enqueue(Backend.BMember.WithdrawAccount, callback =>                                      //������ Ż�� ��û(��� Ż��)
        {
            if (callback.IsSuccess())                                                                       // Ż�� ����
            {
                SetMassage($"ó�� ȭ������ �̵��մϴ�.");

                Invoke("Back_Loadding", 0.5f);
            }
            else                                                                                            // ���� �� �޽��� ����
            {
                callback.GetMessage();
            }
        });
    }
    /// <summary>
    /// ó�� ȭ������ �̵�
    /// </summary>
    private void Back_Loadding()
    {
        soundManager.Instance.now_Set_possible = false;                                             // Slider(����� �ͼ� ����) ���� ���� �� ����
        Utils.Instance.LoadScene(SceneNames.Loadding);
    }
}
