using UnityEngine;
using UnityEngine.UI;

public class Main_Hud : MonoBehaviour
{
    public enum InfoType                                                                        // UI 타입 지정을 위한 열거형
    {
        Level, Exp_Bar, Coin, Life,
        Now_Weapon
    }
    public InfoType type;

    Text my_Text;

    private void Awake()
    {
        my_Text = GetComponent<Text>();
    }
    public void Update_Data()                                                                   // UI 갱신
    {
        switch (type)
        {
            case InfoType.Level:
                my_Text.text = $"{Backend_GameData.Instance.Userdatas.Level}";
                break;
            case InfoType.Life:
                my_Text.text = $"{Backend_GameData.Instance.Userdatas.Life}";
                break;
            case InfoType.Coin:
                my_Text.text = $"{Backend_GameData.Instance.Userdatas.Coin}";
                break;
            case InfoType.Now_Weapon:
                    my_Text.text = string.Format("{0} : LV.{1}", Backend_GameData.Instance.Userdatas.Now_Weapon.data.Name,
                        Backend_GameData.Instance.Userdatas.Now_Weapon.Max_Level ? "MAX" : Backend_GameData.Instance.Userdatas.Now_Weapon.Level);
                break;
        }
    }
}
