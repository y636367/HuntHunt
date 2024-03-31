using UnityEngine;
using UnityEngine.UI;

public class Desc : MonoBehaviour
{
    [SerializeField]
    public Item_Data data;

    public int level;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text text_level;
    [SerializeField]
    private Text text_Name;
    [SerializeField]
    private Text text_Desc;
    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];

        Text[] texts = GetComponentsInChildren<Text>();
        text_level = texts[0];
        text_Name = texts[1];
    }
    private void OnEnable()
    {
        icon.sprite = data.ItemIcon;

        text_Name.text = data.ItemName;

        if((level+1)>=6)
            text_level.text = "Lv.MAX";
        else
            text_level.text = "Lv." + (level+1);

        switch (data.ItemId)
        {
            case 0: // µµ³¢
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level], Backend_GameData.Instance.Userstatusdatas.Attack_Power, 
                    data.counts[level], data.Cool_time[level]);
                break;
            case 1: // ±ÇÃÑ, ±ÇÃÑ°­È­, ·ÎÄÏ·±Ã³, À¯Åº¹ß»ç±â
            case 2: // Àú°Ý, Àú°ÝÃÑ°­È­
            case 3: // »êÅº, »êÅºÃÑ°­È­, ±â°ü´ÜÃÑ
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level], Backend_GameData.Instance.Userstatusdatas.Attack_Power, 
                    data.counts[level], data.Cool_time[level], Backend_GameData.Instance.Userstatusdatas.AttackSpeed);
                break;
            case 4: // ¿¬¸·Åº
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level], Backend_GameData.Instance.Userstatusdatas.Attack_Power * 0.05f, 
                    data.counts[level], data.Cool_time[level], Backend_GameData.Instance.Userstatusdatas.AttackSpeed);
                break;
            case 5: // ¼¶±¤Åº
                text_Desc.text = string.Format(data.ItemDesc, data.counts[level], data.Cool_time[level], Backend_GameData.Instance.Userstatusdatas.AttackSpeed);
                break;
            case 6: // Áö·Ú
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level], Backend_GameData.Instance.Userstatusdatas.Attack_Power,
                    data.counts[level], data.Cool_time[level], Backend_GameData.Instance.Userstatusdatas.AttackSpeed);
                break;
            case 7: // °¡½º½¯µå, È­¿°½¯µå
                switch (data.Type)
                {
                    case Item_Data.ItemType.nomal_weapon:
                        text_Desc.text = string.Format(data.ItemDesc, data.damages[level], Backend_GameData.Instance.Userstatusdatas.Attack_Power,
                            data.Cool_time[level]);
                        break;
                    case Item_Data.ItemType.reinforced_weapon:
                        text_Desc.text = string.Format(data.ItemDesc, data.damages[level], Backend_GameData.Instance.Userstatusdatas.Attack_Power,
                            data.counts[level], data.Cool_time[level]);
                        break;
                }
                break;
            case 8: // È­¿°¹æ»ç±â, È­¿°¹æ»ç±â°­È­
                text_Desc.text = string.Format(data.ItemDesc, data.counts[level], Backend_GameData.Instance.Userstatusdatas.Attack_Power * 0.05f,
                    data.Cool_time[level], Backend_GameData.Instance.Userstatusdatas.AttackSpeed);
                break;
            case 9: // ½ÅÈ£Åº
            case 10: // ´ë°Å
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level], Backend_GameData.Instance.Userstatusdatas.Attack_Power, 
                    data.counts[level], data.Cool_time[level], Backend_GameData.Instance.Userstatusdatas.AttackSpeed);
                break;
            case 11:
            case 12:
            case 13:
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level]);
                break;
            case 14:
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level], data.counts[level], data.Cool_time[level]);
                break;
            case 15:
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level]);
                break;
            case 16:
                text_Desc.text = string.Format(data.ItemDesc);
                break;
            case 17:
                text_Desc.text = string.Format(data.ItemDesc, data.damages[level]);
                break;
        }
    }
}
