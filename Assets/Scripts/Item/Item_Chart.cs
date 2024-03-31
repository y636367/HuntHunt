using UnityEngine;
using UnityEngine.UI;

public class Item_Chart : MonoBehaviour
{
    [SerializeField]
    public Image[] images;

    [SerializeField]
    private Button[] buttons;

    [SerializeField]
    private Sprite Default_image;
    public int num = 0;

    [SerializeField]
    private bool char_;
    [SerializeField]
    private bool weapon_;

    [SerializeField]
    private bool Desc;
    private void Awake()
    {
        for(int index=0; index<images.Length; index++)
        {
            images[index].sprite = Default_image;
            if (Desc)
                buttons[index].enabled = false;
        }
    }
    /// <summary>
    /// ���â���� ȹ���� Ư�� Ȯ���� ���� �̹��� Update
    /// </summary>
    public void GetSprite()
    {
        if (char_)
        {
            for (int index = 0; index < images.Length; index++)
            {
                images[index].sprite = GameManager.Instance.c_chart.images[index].sprite;
            }
        }
        else if (weapon_)
        {
            for (int index = 0; index < images.Length; index++)
            {
                images[index].sprite = GameManager.Instance.w_chart.images[index].sprite;
            }
        }
    }
    /// <summary>
    /// ���â���� ������ �̹��� ��������
    /// </summary>
    /// <param name="t_image"></param>
    /// <param name="t_data"></param>
    public void Get_Image(Sprite t_image, Item_Control t_data)
    {
        images[num].sprite=t_image;
        num += 1;
    }
    /// <summary>
    /// ��ϵ� �����Ͱ� �ִٸ� Ȱ��ȭ
    /// </summary>
    private void OnEnable()
    {
        for (int index = 0; index < buttons.Length; index++)
        {
            if (buttons[index].GetComponent<Desc_Item>().data != null)
                buttons[index].enabled = true;
        }
    }
}
