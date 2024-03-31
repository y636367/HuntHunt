using UnityEngine;

public class Flare_Ball : MonoBehaviour
{
    #region Variable
    [SerializeField]
    public float Damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float bullet_speed;
    [SerializeField]
    private float life_time;
    [SerializeField]
    private int id;
    [SerializeField]
    private GameObject base_object;
    [SerializeField]
    private int per;
    [SerializeField]
    private bool alive;

    [SerializeField]
    private float additional;

    private float t_speed;

    [SerializeField]
    private GameObject target;
    #endregion

    [Header("Shot_Sounds")]
    [SerializeField]
    private string Shot_s;
    private void Update()
    {
        if (!GameManager.Instance.Start_)                                                                                           // ���� ����, ���, �Ͻ�����
            return;

        if (t_speed > 0)
            t_speed -= Time.deltaTime;
        else
        {
            Shot();
            t_speed = speed;
        }
    }
    /// <summary>
    /// ��ȣź �߻� �Լ�
    /// </summary>
    private void Shot()
    {
        Vector3 dir = target.transform.position - transform.position;

        Transform bullet = GameManager.Instance.pool.Bullet_Get(id).transform;
        soundManager.Instance.PlaySoundEffect(Shot_s);

        bullet.parent = base_object.transform;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.rotation = Quaternion.LookRotation(dir);                                                                             // ȸ���� ����(Ÿ���� ���ϵ���)
        bullet.position = transform.position;
        bullet.GetComponent<Bullet>().Init(this.Damage, this.per+1, dir, this.alive, this.bullet_speed, this.life_time);            // -1 ���� ������
    }
    /// <summary>
    /// ��ȣź �ʱ�ȭ(������, �ӵ�, Life_Time ��)
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="speed"></param>
    /// <param name="t_speed"></param>
    /// <param name="life_time"></param>
    /// <param name="base_"></param>
    /// <param name="alive"></param>
    /// <param name="per"></param>
    /// <param name="additional"></param>
    public void Init_(float damage, float speed, float t_speed, float life_time, GameObject base_, bool alive, int per, float additional)
    {
        this.Damage = damage;
        this.speed= speed;
        this.bullet_speed = t_speed;
        this.life_time = life_time;
        this.base_object = base_;
        base_object = base_;
        this.id = (int)additional;
        this.alive = alive;
        this.per= per;
    }
}
