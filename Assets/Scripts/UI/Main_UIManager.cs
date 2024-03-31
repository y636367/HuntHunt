 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Main_UIManager : MonoBehaviour
{
    [System.Serializable]
    public class UI_Update : UnityEvent { };
    public UI_Update up_update = new UI_Update();

    public static Main_UIManager Instance;                                          // 외부에서 쉽게 선언 할 수 있도록 static instance 선언

    [SerializeField]
    public Slider Exp_Bar;                                                          // 경험치 바

    [SerializeField]
    private Player_Status_Controller[] Status;                              
    [SerializeField]
    private WeaponData_Controller[] Weapons;

    public WeaponData_Controller NowWeapons;                                        // 현재 선택된 무기
    public Weapon_UI Weapon_UI;

    private Life_Calculation life_calc;

    [SerializeField]
    private User_Info user;

                                   
    [SerializeField]                                                                // inputfield 입력 방해 방지를 위한 스와이프 제어
    private GameObject Scrollview;                                                  // scrollview contents

    #region 사운드
    [Header("SoundsSlider")]
    [SerializeField]
    private Slider BGM;
    [SerializeField]
    private Slider SFX;
    #endregion

    [SerializeField]
    private GameObject Fake_Panel;                                                  // 터치 방지 패널

    private void Awake()
    {
        Instance = this;

        Quit_Game.Instance.Limit = false;
        Fake_Panel.SetActive(false);
    }
    /// <summary>
    ///  씬 비동기 로드로 인한 UserData의 순차 갱신으로 인해 코드 꼬임으로 UI Data 갱신등의 오류 방지를 위한 순차 관리
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
    /// User의 경험치 상태 표출
    /// </summary>
    public void Setting_Exp()
    {
        Exp_Bar.value = Backend_GameData.Instance.Userdatas.NowExp / Backend_GameData.Instance.Userdatas.Next_Exp_value;
    }
    /// <summary>
    /// 사용자 정보 갱신
    /// </summary>
    public void Update_Info()
    {
        user.GetUserInfoFromBackend();
    }
    /// <summary>
    /// 패널 닫음과 동시에 List 에서 빼기
    /// </summary>
    public void Panel_List_Out()
    {
        Quit_Game.Instance.Panel_Out();
    }
    /// <summary>
    ///  터치 방지 패널 on
    /// </summary>
    public void Fake_Panel_On()
    {
        Fake_Panel.SetActive(true);
    }
}
