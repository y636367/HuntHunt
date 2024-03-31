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
    /// Player ���� ȸ��
    /// </summary>
    private void Awake()
    {
        offset = transform.position - target.position;                                                          // ���� �Ÿ� �̻� ������ ���� Offset ����
    }
    private void Update()
    {
        if (!GameManager.Instance.Start_)                                                                           // ���� ���� ��Ȳ(����, ���, �Ͻ�����)
        {   
            return;
        }

        if (axe)                                                                                                    // ȸ��ü�� ������ ���
        {
            transform.RotateAround(target.position, Vector3.up, Speed * Time.deltaTime);                            // taregt(Player)�� ������ ȸ��
        }
        else if (Flame && transform.childCount > 0)                                                                 // ȸ��ü�� ��ȣź, ���� ������ ���
        {
            gameObject.transform.GetChild(0).rotation = GameManager.Instance.player.transform.rotation;             // �ڽ�ü Player �ֺ����� ȸ��

            t_time -= Time.deltaTime;

            if (t_time < 0)
            {
                t_time = time;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);                                        // ���� Ȱ��ȭ
            }
        }
        transform.position = target.position + offset;                                                              // ��ġ ����
        offset = transform.position - target.position;
    }
    /// <summary>
    /// ��ȣź �߻� ��Ÿ�� �ޱ� ���� �Լ�
    /// </summary>
    /// <param name="t_timer"></param>
    public void Get_Flame(float t_timer)
    {
        t_time = time = t_timer;
    }
}
