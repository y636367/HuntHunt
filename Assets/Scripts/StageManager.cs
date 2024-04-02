using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int Life_Delete;
    public int Stage_num;
    public int Difficult;                                                                   // 0 Difficult 1 Difficult 2 Difficult 3 Infinity
    
    /// <summary>
    /// 0 -> 10M / 1 -> 15M / 2 -> 30M / 3 -> Infinity
    /// </summary>
    public void Get_Time()
    {
        switch (Difficult)
        {
            case 0:
                GameManager.Instance.MaxGameTime = 10f * 60f;
                break;
            case 1:
                GameManager.Instance.MaxGameTime = 16f * 60f;
                break;
            case 2:
                GameManager.Instance.MaxGameTime = 32f * 60f;
                break;
            default:
                GameManager.Instance.MaxGameTime = -1;
                GameManager.Instance.Infinity_Check = true;
                break;
        }
    }
    /// <summary>
    /// ���ھ� ����
    /// Result = Game ���� �ð�(���� �ð�) * n(���̵��� ���� ��)
    /// Result += ���� ���� ��  * k(���̵��� ���� ��)
    /// Result += ������������ ���̵��� ���� ����ġ
    /// </summary>
    /// <returns></returns>
    public float Result_Score()
    {
        var Result = 0f;

        switch (Difficult)
        {
            case 0:
                Result = GameManager.Instance.GameTime * 1;
                Result += GameManager.Instance.current_Kill * 0.1f;
                Result += Stage_num * 5;
                break;
            case 1:
                Result = (GameManager.Instance.GameTime * 1) + (GameManager.Instance.GameTime * 0.5f);
                Result += GameManager.Instance.current_Kill * 0.3f;
                Result += Stage_num * 8;
                break;
            case 2:
                Result = GameManager.Instance.GameTime * 2;
                Result += GameManager.Instance.current_Kill * 0.5f;
                Result += Stage_num * 12;
                break;
            default:
                Result = (GameManager.Instance.GameTime * 2) + (GameManager.Instance.GameTime * 0.5f);          // ���� �ð�
                Result += GameManager.Instance.current_Kill * 0.8f;                                             // óġ ��
                Result += Stage_num * 20;                                                                       // ��������
                break;
        }

        return Result;
    }
    /// <summary>
    /// ���̵� �� �Ҹ�Ǵ� Life
    /// </summary>
    public void Consumed_Life()
    {
        switch (Difficult)
        {
            case 0:
                Life_Delete = 5;
                break;
            case 1:
                Life_Delete = 10;
                break;
            case 2:
                Life_Delete = 15;
                break;
            case 3:
                Life_Delete = 20;
                break;
        }
    }
}
