using UnityEngine;
using static WeaponData;

public class WeaponData_Controller : MonoBehaviour
{
    [SerializeField]
    public WeaponData data;                                                         // ������ Weapon Data
    [SerializeField]
    public Weapon_UI ui;
    public int Level;

    [SerializeField]
    public int Upgrade_Cost;
    [SerializeField]
    public int Plus_Cost;

    [SerializeField]
    public bool Max_Level;

    /// <summary>
    /// �� �ʱ�ȭ
    /// </summary>
    public void Reset_value()
    {
        Level = 1;
        Upgrade_Cost = data.Upgrade_Cost;

        Setting_Data();
        Set_UI();
    }
    private void Set_UI()
    {
        if (data.Numbering == Backend_GameData.Instance.Userdatas.WeaponNumber)    // UserData�� ��ϵ� Weapon Number�� ��ġ �� ���� �� ����
        {
            Main_UIManager.Instance.Get_NowWeapon(this, ui);
        }
    }
    /// <summary>
    /// �ʱ�ȭ �� �������� ������ ������ �缳��
    /// </summary>
    private void Setting_Data()
    {
        switch (data.W_type)
        {
            case WeponType.Pistol:
                Level = Backend_GameData.Instance.Userweaponlevel.Pistol;
                break;
            case WeponType.Shotgun:
                Level = Backend_GameData.Instance.Userweaponlevel.Shotgun;
                break;
            case WeponType.Sniper:
                Level = Backend_GameData.Instance.Userweaponlevel.Sniper;
                break;
            case WeponType.Submachin:
                Level = Backend_GameData.Instance.Userweaponlevel.Rampage;
                break;
            case WeponType.Flare:
                Level = Backend_GameData.Instance.Userweaponlevel.Flare_gun;
                break;
            case WeponType.Mine:
                Level = Backend_GameData.Instance.Userweaponlevel.Mine;
                break;
            case WeponType.FireThrower:
                Level = Backend_GameData.Instance.Userweaponlevel.FlareThrower;
                break;
            case WeponType.GasShiled:
                Level = Backend_GameData.Instance.Userweaponlevel.GasShield;
                break;
            case WeponType.Rocket:
                Level = Backend_GameData.Instance.Userweaponlevel.RocketLauncer;
                break;
            case WeponType.Knife:
                Level = Backend_GameData.Instance.Userweaponlevel.Knife;
                break;
        }

        for (int index = 1; index < Level; index++)
        {
            Upgrade_Cost += Plus_Cost;
        }

        if (Level >= 6)
            Max_Level = true;
    }
}
