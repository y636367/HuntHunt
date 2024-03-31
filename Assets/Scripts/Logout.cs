using BackEnd;

public class Logout : Login_Base
{
    /// <summary>
    /// �α׾ƿ� ���� �Լ�
    /// </summary>
    public void OnClickedLogout()
    {
        SendQueue.Enqueue(Backend.BMember.Logout, callback =>                               // ������ �α׾ƿ� ��û(�α׾ƿ� �� Date ����)
        {
            if (callback.IsSuccess())                                                       // �α׾ƿ� ����
            {
                SetMassage($"ó�� ȭ������ �̵��մϴ�.");

                Invoke("Back_Loadding", 0.5f);
            }
            else                                                                            // ���� �� ���� �޽��� ����
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
