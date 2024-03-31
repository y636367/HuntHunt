using UnityEngine;
using UnityEngine.Events;

public class Stats_Upgrade : MonoBehaviour
{
    [System.Serializable]
    public class UI_Update : UnityEvent { };                                                              // �� �̺�Ʈ ������ ���� Evnet Ŭ���� �ν��Ͻ� ����
    public UI_Update ui_update = new UI_Update();

    [SerializeField]
    public Player_Status_Controller data;                                                                                       // data ������ ��Ʈ�ѷ�

    private Upgrade_s upgrade_S;                                                                                                // ���׷��̵� �Ǵ� �� ǥ�� UI ����
    private void Awake()
    {
        data = GetComponent<Upgrade_s>().data;
        upgrade_S=GetComponent<Upgrade_s>();
    }
    /// <summary>
    /// ���� ������Ʈ ���� �Լ�
    /// </summary>
    public void Upgrade_Status()
    {
        if (Backend_GameData.Instance.Userdatas.Coin >= data.Upgrade_Cost)                                                      // Ȥ �� ���� ���� �ѹ� �� Cost �˻�
        {
            Backend_GameData.Instance.Userdatas.Coin -= data.Upgrade_Cost;                                                      // Level �� Cost ������ ����
            data.Upgrade_Cost += data.Plus_Cost + 1;

            data.Level += 1;

            switch (data.data.S_type)                                                                                           // Type �� �°� ���� Status Upgrade
            {
                case Player_Status.StatusType.A_Power:
                    Backend_GameData.Instance.Userstatusdatas.Attack_Power += data.data.Upgrade_status;
                    Backend_GameData.Instance.Userstatuslevel.AP += 1;
                    break;
                case Player_Status.StatusType.A_speed:
                    Backend_GameData.Instance.Userstatusdatas.AttackSpeed += data.data.Upgrade_status;
                    Backend_GameData.Instance.Userstatuslevel.AS += 1;
                    break;
                case Player_Status.StatusType.P_Strength:
                    Backend_GameData.Instance.Userstatusdatas.Max_HP += data.data.Upgrade_status;
                    Backend_GameData.Instance.Userstatuslevel.PH += 1;
                    break;
                case Player_Status.StatusType.D_Power:
                    Backend_GameData.Instance.Userstatusdatas.Deffensive_Power += data.data.Upgrade_status;
                    Backend_GameData.Instance.Userstatuslevel.DP += 1;
                    break;
                case Player_Status.StatusType.M_Speed:
                    Backend_GameData.Instance.Userstatusdatas.MoveSpeed += data.data.Upgrade_status;
                    Backend_GameData.Instance.Userstatuslevel.MS += 1;
                    break;
            }
            Backend_Reflections();                                                                                              // ���� �ݿ�

            upgrade_S.Cost_Check(data);                                                                                         // ���� ��ȭ�� ���� Cost üũ
            upgrade_S.Setting_UI();                                                                                             // UI ����
            ui_update?.Invoke();
        }
    }
    /// <summary>
    /// ������ ������ �ݿ�
    /// </summary>
    private void Backend_Reflections()
    {
        Backend_GameData.Instance.UpdateUserDatas_();
        Backend_GameData.Instance.UpdateStatusDatas_();
        Backend_GameData.Instance.UpdateStatusLVDatas_();
    }
}
