using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tile : MonoBehaviour
{
    [SerializeField]
    private Image Tile;                                     // 움직이 뒷 배경

    [SerializeField]
    private float Pre_x;                                    // 출발 좌표 -x
    [SerializeField]
    private float Pre_y;                                    // 출발 좌표 -y
    [SerializeField]
    private float Nex_x;                                    // 도착 좌표 -x
    [SerializeField]
    private float Nex_y;                                    // 도착 좌표 -y

    [SerializeField]
    private float speed;                                    // 배경 이동 속도

    float moveX;                                            // 현재 좌표 -x
    float moveY;                                            // 현재 좌표 -y
    private void Awake()
    {
        Tile= GetComponent<Image>();                        
    }
    private void Start()
    {
        Tile.rectTransform.anchoredPosition = new Vector2(Pre_x, Pre_y);            // 배경 출발 좌표 설정
        StartCoroutine(nameof(Tile_BackPanel));                                     // 코루틴으로 이동하는 배경 효과 설정
    }
    IEnumerator Tile_BackPanel()
    {
        while (Tile.rectTransform.anchoredPosition.x > Nex_x)
        {
            moveX = (Nex_x - Pre_x) % speed;                                        // 속도 비례해서 천천히 이동
            moveY = (Nex_y - Pre_y) % speed;

            Tile.rectTransform.anchoredPosition = new Vector2(Tile.rectTransform.anchoredPosition.x + moveX, Tile.rectTransform.anchoredPosition.y + moveY);
            yield return null;
        }

        Return_Position();
    }
    /// <summary>
    /// 배경 다시 출발 좌표로 원위치
    /// </summary>
    private void Return_Position()
    {
        Tile.rectTransform.anchoredPosition = new Vector2(Pre_x, Pre_y);
        StartCoroutine(nameof(Tile_BackPanel));
    }
}
