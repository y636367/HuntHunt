using UnityEngine;
public class Axe : MonoBehaviour
{
    #region Variable
    public Transform target;
    public float Speed;
    Vector3 offset;

    [SerializeField]
    private bool axe;

    [SerializeField]
    private bool Flame;
    [SerializeField]
    private float time;

    private float t_time;
    #endregion
    /// <summary>
    /// Player 주위 회전
    /// </summary>
    private void Awake()
    {
        offset = transform.position - target.position;                                                          // 일정 거리 이상 벌리기 위한 Offset 설정
    }
    private void Update()
    {
        if (!GameManager.Instance.Start_)                                                                           // 게임 정지 상황(종료, 사망, 일시정지)
        {   
            return;
        }

        if (axe)                                                                                                    // 회전체가 도끼일 경우
        {
            transform.RotateAround(target.position, Vector3.up, Speed * Time.deltaTime);                            // taregt(Player)주 변으로 회전
        }
        else if (Flame && transform.childCount > 0)                                                                 // 회전체가 신호탄, 가스 쉴드일 경우
        {
            gameObject.transform.GetChild(0).rotation = GameManager.Instance.player.transform.rotation;             // 자식체 Player 주변으로 회전

            t_time -= Time.deltaTime;

            if (t_time < 0)
            {
                t_time = time;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);                                        // 쉴드 활성화
            }
        }
        transform.position = target.position + offset;                                                              // 위치 설정
        offset = transform.position - target.position;
    }
    /// <summary>
    /// 신호탄 발사 쿨타임 받기 위한 함수
    /// </summary>
    /// <param name="t_timer"></param>
    public void Get_Flame(float t_timer)
    {
        t_time = time = t_timer;
    }
}
