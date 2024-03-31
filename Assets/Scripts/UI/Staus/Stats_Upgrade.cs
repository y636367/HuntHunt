using UnityEngine;
using UnityEngine.Events;

public class Stats_Upgrade : MonoBehaviour
{
    [System.Serializable]
    public class UI_Update : UnityEvent { };                                                              // 후 이벤트 진행을 위한 Evnet 클래스 인스턴스 생성
    public UI_Update ui_update = new UI_Update();

    [SerializeField]
    public Player_Status_Controller data;                                                                                       // data 제어할 컨트롤러

    private Upgrade_s upgrade_S;                                                                                                // 업그레이드 판단 및 표출 UI 관리
    private void Awake()
    {
        data = GetComponent<Upgrade_s>().data;
        upgrade_S=GetComponent<Upgrade_s>();
    }
    /// <summary>
    /// 실제 업데이트 진행 함수
    /// </summary>
    public void Upgrade_Status()
    {
        if (Backend_GameData.Instance.Userdatas.Coin >= data.Upgrade_Cost)                                                      // 혹 모를 방지 위해 한번 더 Cost 검사
        {
            Backend_GameData.Instance.Userdatas.Coin -= data.Upgrade_Cost;                                                      // Level 및 Cost 데이터 갱신
            data.Upgrade_Cost += data.Plus_Cost + 1;

            data.Level += 1;

            switch (data.data.S_type)                                                                                           // Type 에 맞게 진행 Status Upgrade
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
            Backend_Reflections();                                                                                              // 서버 반영

            upgrade_S.Cost_Check(data);                                                                                         // 다음 강화를 위한 Cost 체크
            upgrade_S.Setting_UI();                                                                                             // UI 갱신
            ui_update?.Invoke();
        }
    }
    /// <summary>
    /// 서버에 데이터 반영
    /// </summary>
    private void Backend_Reflections()
    {
        Backend_GameData.Instance.UpdateUserDatas_();
        Backend_GameData.Instance.UpdateStatusDatas_();
        Backend_GameData.Instance.UpdateStatusLVDatas_();
    }
}
