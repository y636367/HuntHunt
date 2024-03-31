using UnityEngine;

public class Desc_Item : MonoBehaviour
{
    [SerializeField]
    public Item_Data data;
    private int data_level;

    [SerializeField]
    private int data_num;

    [SerializeField]
    private Desc desc;
    /// <summary>
    /// 획득한 특성 후에 Desc 창에 보여지기 위한 data 임시 저장
    /// </summary>
    /// <param name="level"></param>
    /// <param name="t_data"></param>
    public void Get_Desc(int level, Item_Data t_data)
    {
        this.data_level = level;
        this.data = t_data;
    }
    /// <summary>
    /// Desc 창에 현재 선택된 특성의 data, level 설정
    /// </summary>
    public void Send_Desc()
    {
        desc.data = this.data;
        desc.level = this.data_level;
    }
}
