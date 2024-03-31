using UnityEngine;
using UnityEngine.UI;

public class Upgrade_s : MonoBehaviour
{
    #region Variable
    [SerializeField]
    public Player_Status_Controller data;

    [SerializeField]
    public Image sprite;
    [SerializeField]
    public Text Name;
    [SerializeField]
    public Text Lv;
    [SerializeField]
    public Text Desc;
    [SerializeField]
    public Text status;

    [SerializeField]
    private Text Next_status;

    [SerializeField]
    private Button upgrade_Button;

    [SerializeField]
    private Text Button_Text;

    [SerializeField]
    private Stats_Upgrade stupgrade;
    #endregion
    private void Awake()
    {
        stupgrade = GetComponent<Stats_Upgrade>();
    }
    private void OnEnable()
    {
        Setting_UI();
    }
    /// <summary>
    /// ���� ���õ� Status Data ��������
    /// </summary>
    /// <param name="t_data"></param>
    public void Get_Data(Player_Status_Controller t_data)
    {
        data = t_data;
        stupgrade.data = data;

        Cost_Check(data);
    }
    /// <summary>
    /// Status ��ġ(Ŭ��) �� Upgrade�� ���� Cost�� ������� �˻�
    /// </summary>
    /// <param name="data"></param>
    public void Cost_Check(Player_Status_Controller data)
    {
        if (Backend_GameData.Instance.Userdatas.Coin < data.Upgrade_Cost)
        {
            upgrade_Button.gameObject.SetActive(false);
        }
        else
        {
            upgrade_Button.gameObject.SetActive(true);
            upgrade_Button.interactable = true;
            Button_Text.text = data.Upgrade_Cost.ToString();
        }
    }
    /// <summary>
    /// UI ǥ��
    /// </summary>
    public void Setting_UI()
    {
        sprite.sprite = data.data.data.ItemIcon;

        Name.text = data.data.Name;
        Lv.text = "Lv." + (data.Level);
        Desc.text = data.data.Desc;

        switch (data.data.S_type)
        {
            case Player_Status.StatusType.A_Power:
                status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.Attack_Power}";
                Next_status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.Attack_Power + data.data.Upgrade_status}";
                break;
            case Player_Status.StatusType.A_speed:
                status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.AttackSpeed}";
                Next_status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.AttackSpeed + data.data.Upgrade_status}";
                break;
            case Player_Status.StatusType.P_Strength:
                status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.Max_HP}";
                Next_status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.Max_HP + data.data.Upgrade_status}";
                break;
            case Player_Status.StatusType.D_Power:
                status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.Deffensive_Power}";
                Next_status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.Deffensive_Power + data.data.Upgrade_status}";
                break;
            case Player_Status.StatusType.M_Speed:
                status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.MoveSpeed}";
                Next_status.text = $"����\n{Backend_GameData.Instance.Userstatusdatas.MoveSpeed + data.data.Upgrade_status}";
                break;
        }
    }
}
