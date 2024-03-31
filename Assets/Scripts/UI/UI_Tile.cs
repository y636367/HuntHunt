using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tile : MonoBehaviour
{
    [SerializeField]
    private Image Tile;                                     // ������ �� ���

    [SerializeField]
    private float Pre_x;                                    // ��� ��ǥ -x
    [SerializeField]
    private float Pre_y;                                    // ��� ��ǥ -y
    [SerializeField]
    private float Nex_x;                                    // ���� ��ǥ -x
    [SerializeField]
    private float Nex_y;                                    // ���� ��ǥ -y

    [SerializeField]
    private float speed;                                    // ��� �̵� �ӵ�

    float moveX;                                            // ���� ��ǥ -x
    float moveY;                                            // ���� ��ǥ -y
    private void Awake()
    {
        Tile= GetComponent<Image>();                        
    }
    private void Start()
    {
        Tile.rectTransform.anchoredPosition = new Vector2(Pre_x, Pre_y);            // ��� ��� ��ǥ ����
        StartCoroutine(nameof(Tile_BackPanel));                                     // �ڷ�ƾ���� �̵��ϴ� ��� ȿ�� ����
    }
    IEnumerator Tile_BackPanel()
    {
        while (Tile.rectTransform.anchoredPosition.x > Nex_x)
        {
            moveX = (Nex_x - Pre_x) % speed;                                        // �ӵ� ����ؼ� õõ�� �̵�
            moveY = (Nex_y - Pre_y) % speed;

            Tile.rectTransform.anchoredPosition = new Vector2(Tile.rectTransform.anchoredPosition.x + moveX, Tile.rectTransform.anchoredPosition.y + moveY);
            yield return null;
        }

        Return_Position();
    }
    /// <summary>
    /// ��� �ٽ� ��� ��ǥ�� ����ġ
    /// </summary>
    private void Return_Position()
    {
        Tile.rectTransform.anchoredPosition = new Vector2(Pre_x, Pre_y);
        StartCoroutine(nameof(Tile_BackPanel));
    }
}
