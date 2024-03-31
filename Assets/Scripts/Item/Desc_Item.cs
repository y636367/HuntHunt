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
    /// ȹ���� Ư�� �Ŀ� Desc â�� �������� ���� data �ӽ� ����
    /// </summary>
    /// <param name="level"></param>
    /// <param name="t_data"></param>
    public void Get_Desc(int level, Item_Data t_data)
    {
        this.data_level = level;
        this.data = t_data;
    }
    /// <summary>
    /// Desc â�� ���� ���õ� Ư���� data, level ����
    /// </summary>
    public void Send_Desc()
    {
        desc.data = this.data;
        desc.level = this.data_level;
    }
}
