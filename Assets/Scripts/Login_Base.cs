using UnityEngine;
using UnityEngine.UI;

public class Login_Base : MonoBehaviour
{
    [SerializeField]
    private Text text_inputFIeld;

    // params : ���� ���� �Ű�����
    /// <summary>
    /// �޽��� ���� �ʱ�ȭ, InputField ���� �ʱ�ȭ
    /// </summary>
    protected void ResetUI(params Image[] images)
    {
        text_inputFIeld.text = string.Empty;

        for(int i=0; i<images.Length; i++)
        {
            images[i].color = Color.white;
        }
    }
    /// <summary>
    ///  �Ű������� �ִ� ���� ���
    /// </summary>
    protected void SetMassage(string msg)
    {
        text_inputFIeld.text = msg;
    }
    /// <summary>
    /// InPutField �Է� ���� �߻� ��, ���� ���� �� ���� �޽��� ������� ���
    /// </summary>
    protected void GuideForCorrectlyEnteredData(Image image, string msg)
    {
        text_inputFIeld.text = msg;
        image.color = Color.red;
    }
    /// <summary>
    /// �Է� �ʵ� ���� ��, �ش��ϴ� �ʵ� ���� ���� �� ��� �޽��� ���
    /// (image : �ʵ�, field : ����, result : ��µ� ����)
    /// </summary>
    /// <returns></returns>
    protected bool IsFieldEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals("")) // Trim: ���ڿ� ��, �� ���� ����
        {
            GuideForCorrectlyEnteredData(image, $"\"{result}\"�� �Է� ���� �ʾҽ��ϴ�.");

            return true;
        }

        return false;
    }
    /// <summary>
    /// �޽��� ���� �ʱ�ȭ
    /// </summary>
    protected void Reset_Text()
    {
        text_inputFIeld.text = string.Empty;
    }
}
