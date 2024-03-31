using UnityEngine;
using static Player_Status;

public class Player_Status_Controller : MonoBehaviour
{
    [SerializeField]
    public Player_Status data;                                              // 제어할 Status Data
    public int Level;

    [SerializeField]
    public int Upgrade_Cost;
    [SerializeField]
    public int Plus_Cost;
    /// <summary>
    /// 데이터 초기화
    /// </summary>
    public void Reset_value()
    {
        Level = 1;
        Upgrade_Cost = data.Upgrade_Cost;

        Setting_Data();
    }
    /// <summary>
    /// 초기화 후 서버에서 가져온 값 기반 재 설정
    /// </summary>
    public void Setting_Data()
    {
        switch (data.S_type)
        {
            case StatusType.A_Power:
                Level = Backend_GameData.Instance.Userstatuslevel.AP;
                break;
            case StatusType.A_speed:
                Level = Backend_GameData.Instance.Userstatuslevel.AS;
                break;
            case StatusType.D_Power:
                Level = Backend_GameData.Instance.Userstatuslevel.DP;
                break;
            case StatusType.P_Strength:
                Level = Backend_GameData.Instance.Userstatuslevel.PH;
                break;
            case StatusType.M_Speed:
                Level = Backend_GameData.Instance.Userstatuslevel.MS;
                break;
        }

        for (int index = 1; index < Level; index++)
        {
            Upgrade_Cost += Plus_Cost + 1;
        }
    }
}
