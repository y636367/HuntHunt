 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Main_UIManager : MonoBehaviour
{
    [System.Serializable]
    public class UI_Update : UnityEvent { };
    public UI_Update up_update = new UI_Update();

    public static Main_UIManager Instance;                                          // �ܺο��� ���� ���� �� �� �ֵ��� static instance ����

    [SerializeField]
    public Slider Exp_Bar;                                                          // ����ġ ��

    [SerializeField]
    private Player_Status_Controller[] Status;                              
    [SerializeField]
    private WeaponData_Controller[] Weapons;

    public WeaponData_Controller NowWeapons;                                        // ���� ���õ� ����
    public Weapon_UI Weapon_UI;

    private Life_Calculation life_calc;

    [SerializeField]
    private User_Info user;

                                   
    [SerializeField]                                                                // inputfield �Է� ���� ������ ���� �������� ����
    private GameObject Scrollview;                                                  // scrollview contents

    #region ����
    [Header("SoundsSlider")]
    [SerializeField]
    private Slider BGM;
    [SerializeField]
    private Slider SFX;
    #endregion

    [SerializeField]
    private GameObject Fake_Panel;                                                  // ��ġ ���� �г�

    private void Awake()
    {
        Instance = this;

        Quit_Game.Instance.Limit = false;
        Fake_Panel.SetActive(false);
    }
    /// <summary>
    ///  �� �񵿱� �ε�� ���� UserData�� ���� �������� ���� �ڵ� �������� UI Data ���ŵ��� ���� ������ ���� ���� ����
    /// </summary>
    public void Setting()
    {
        life_calc = GetComponent<Life_Calculation>();
        PD_Control.Instance.Set_Controller(Weapons, Status);

        Update_Info();
        Setting_Exp();

        soundManager.Instance.GetSliders(BGM, SFX);
        
        life_calc.SettingLife();

        up_update?.Invoke();
    }
    public void Get_NowWeapon(WeaponData_Controller t_now_weapon, Weapon_UI t_weapon_ui)
    {
        Backend_GameData.Instance.Userdatas.Now_Weapon = NowWeapons = t_now_weapon;
        Weapon_UI = t_weapon_ui;
    }
    public void Main_Upgarde(Upgrade main_panel)
    {
        main_panel.UI = Weapon_UI;
    }
    public void Scrollview_On()
    {
        Scrollview.SetActive(true);
    }
    public void Scrollview_Off()
    {
        Scrollview.SetActive(false);
    }
    /// <summary>
    /// User�� ����ġ ���� ǥ��
    /// </summary>
    public void Setting_Exp()
    {
        Exp_Bar.value = Backend_GameData.Instance.Userdatas.NowExp / Backend_GameData.Instance.Userdatas.Next_Exp_value;
    }
    /// <summary>
    /// ����� ���� ����
    /// </summary>
    public void Update_Info()
    {
        user.GetUserInfoFromBackend();
    }
    /// <summary>
    /// �г� ������ ���ÿ� List ���� ����
    /// </summary>
    public void Panel_List_Out()
    {
        Quit_Game.Instance.Panel_Out();
    }
    /// <summary>
    ///  ��ġ ���� �г� on
    /// </summary>
    public void Fake_Panel_On()
    {
        Fake_Panel.SetActive(true);
    }
}
