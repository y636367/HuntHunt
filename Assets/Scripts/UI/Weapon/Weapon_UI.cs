using UnityEngine;
using UnityEngine.UI;

public class Weapon_UI : MonoBehaviour
{
    [SerializeField]
    public WeaponData_Controller data;                                                      // 현재 WeaponData

    #region 현재 표출할 값
    [SerializeField]
    public Image sprite;
    [SerializeField]
    public Text Name;
    [SerializeField]
    public Text Lv;
    [SerializeField]
    public Text Desc;
    #endregion

    /// <summary>
    /// 데이터 표출
    /// </summary>
    public void Data_Up()
    {
        sprite.sprite = data.data.image;

        Name.text = string.Format("{0}", data.data.Name);
        Lv.text = string.Format("Lv.{0}", data.Max_Level ? "MAX" : data.Level == 0 ? "미해방" : data.Level);
        Desc.text = string.Format("{0}", data.data.Desc);
    }
}
