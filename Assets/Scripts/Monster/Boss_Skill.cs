using UnityEngine;

public class Boss_Skill : MonoBehaviour
{
    #region
    [SerializeField]
    private int Boss_num;                                                                                   // ���� ���� ������ int ����

    [SerializeField]
    private GameObject t_obj;                                                                               // Ÿ�� ������ �ʿ��� ������ ���� GameObject ����

    Monster monster;

    [SerializeField]
    private float Value;
    float Value_2;

    [SerializeField]
    private string[] Turtle_s;                                                                              // Turtle �ε��� ����Ʈ

    bool t_F = false;
    #endregion
    private void Awake()
    {
        monster=GetComponent<Monster>();
    }
    /// <summary>
    ///  GameObject ���� �ʱ�ȭ
    /// </summary>
    private void OnEnable()
    {
        t_obj = GetComponent<GameObject>();
        t_obj = null;
    }
    private void Update()
    {
        switch (Boss_num)
        {
            case 0:
                if (t_F)                                                                                                                                // (��ų - ����)�����°�?
                {
                    Value -= Time.deltaTime;
                    if (Value <= 0f)
                    {
                        Value = Value_2;
                        Goast();
                    }
                }
                break;
            case 1:
                if (t_F)                                                                                                                                // ����(��ų - ǳ����)���ΰ�?
                {
                    transform.position = Vector3.MoveTowards(transform.position, t_obj.transform.position, Value);                                      // Navagent�� ���� �̹� ������ Player�� ���� �ְ�, ���� �� ������ ���ڱ� �ٲ�°� �����ϱ� ���� ������Ʈ�� ���� ���� �̵�
                }
                break;
            case 4:
                if (t_F)                                                                                                                                // ȸ��(��ų - �ź���)���ΰ�?
                {
                    Value_2 -= Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, t_obj.transform.position, Value);
  
                    if (Value_2 < 0)
                    {
                        Spin_Over();
                    }
                }
                break;
        }
    }
 /// <summary>
 /// 0 - ����, 1 - ǳ����, 2 - ���ٱ�, 3 - ����, 4 - �ź���, 5 - ���� ������
 /// </summary>
    public void Skills()
    {
        switch (Boss_num)
        {
            case 0: // ����
                Invoke("Goast", 0.8f);
                break;
            case 1: // ǳ����
                Invoke("Beetle", 0.5f);
                break;
            case 2: // ���ٱ�
                Invoke("Skull_King", 0.8f);
                break;
            case 3: // ����
                Invoke("MushRoom", 0.13f);
                break;
            case 4: // �ź���
                Invoke("Turtle", 0.45f);
                break;
            case 5: // ���� ������
                Invoke("Red_Slime", 0.5f);
                break;
        }
    }
 /// <summary>
 /// ���� ��ų �Լ� (������ٰ� Player�� �ֺ����� ����)
 /// </summary>
    private void Goast()
    {
        GameObject Render_Body = transform.GetChild(0).gameObject;

        if (!t_F)
        {
            GameObject effect = GameManager.Instance.pool.Effect_Get(2);                                                            // ����Ʈ ����
            effect.transform.position = this.transform.position;

            Render_Body.SetActive(false);                                                                                           // �ƹ�Ÿ�� Render Body�� enable=false

            monster.animator.speed = 0f;

            this.tag = "Untagged";

            if (GameManager.Instance.player.scanner.nearestTarget == this.gameObject)                                               // Player�� ���� �� �� ������(���� ������� ���� ���� ��ġ Ư�� ����) null
                GameManager.Instance.player.scanner.nearestTarget = null;
            t_F = true;                                                                                                             // ��ų ���

            monster.Now_Skill = true;                                                                                               // �ִϸ��̼� ������ ���� ���� ���� �� ����

            Value = Random.Range(3f, 5f);
            Value_2 = Value;
        }
        else
        {
            t_F = false;                                                                                                            // ��ų ��� ����(��� �巯����)

            int ran = Random.Range(0, 360);

            Vector3 pos = GameManager.Instance.player.transform.position + (Random.insideUnitSphere * 4.7f);                        // Player �ֺ����� ���� ������ ��
            pos.y = transform.position.y;

            this.transform.position = pos;                                                                                          // ���� ������ ��ġ�� ����

            monster.animator.speed = 1f;
            monster.Now_Skill_Time();

            Render_Body.SetActive(true);

            this.tag = "Enemy";
        }
    }
 /// <summary>
 /// ǳ���� ��ų �Լ�(Player ���� ����)
 /// </summary>
    private void Beetle()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(4);                                                                // ����Ʈ ����
        effect.transform.position = this.transform.position;
        monster.agent.isStopped = true;                                                                                             // agent ����

        if (t_obj == null)
        {
            t_obj = new GameObject();
            t_obj.transform.parent = this.transform;                                                                                // ǳ���� ���ʿ� ������Ʈ�� �ξ� �� ������Ʈ�� ���ؼ� ����ؼ� �̵��� �� �ֵ��� ����
        }

        t_obj.transform.position = this.transform.forward * 5;
        t_F = true;
        Invoke("Stop_Beetle", 1.3f);                                                                                                // ���� ����
    }
 /// <summary>
 /// ǳ���� ��ų �Լ�(���� ����)
 /// </summary>
    private void Stop_Beetle()
    {
        t_F = false;
        monster.Now_Skill_Time();
    }
 /// <summary>
 /// ���̷��� ŷ ��ų �Լ�(ü�� ȸ��)
 /// </summary>
    private void Skull_King()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(4);                                                                // ����Ʈ ����
        effect.transform.position = this.transform.position;

        monster.Physical_strength += monster.MaxHealth / 3;                                                                         // ���� ���̷��� ŷ�� �ִ�ü���� 3���� 1��ŭ ȸ��
        monster.Now_Skill_Time();
    }
  /// <summary>
  /// �Ŵ���� ��ų �Լ�(�Ϲ� ���� ����)
  /// </summary>
    private void MushRoom()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(4);                                                                // ����Ʈ ����
        effect.transform.position = this.transform.position;

        GameManager.Instance.spawner.Spawn_Boss_Skill();                                                                            // �Ϲ� ���� ����
    }
  /// <summary>
  /// �ź��� ��ų �Լ�(Ư�� ����Ʈ�� ���� ȸ���ϸ� ����)
  /// </summary>
    private void Turtle()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        LayerMask excludeLayer = LayerMask.GetMask("Default");                                                                      // Default ���̾� ���� ó��

        rb.excludeLayers = excludeLayer;                                                                                            // �ε��� ���� ����(Default ������Ʈ - ���, �� �� �ε��� x �׳� ���)

        Value_2 = Random.Range(4.5f, 7.5f);
        monster.animator.SetTrigger("Spin");
        Target_Wall();
    }
    /// <summary>
    /// ���� ������ ��ų �Լ�(���� ü�� ��� �н� ����)
    /// </summary>
    private void Red_Slime()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(4);                                                               // ����Ʈ ����
        effect.transform.position = this.transform.position;

        GameManager.Instance.spawner.red_Slime_Skill(monster.Physical_strength, monster.Defensive_power, monster.Attack_power);    // ���� ü�� ��� �н� ����
    }
   /// <summary>
   /// �ź��� ��ų - ���ο� Ÿ�� �˻�
   /// </summary>
    private void Target_Wall()
    {
        t_obj = GameManager.Instance.walls.Next_Point();                                                                           // �ź����� ���ο� ���� Ÿ��

        t_F = true;
    }
    private void Turtle_S_Rand()                                                                                                  // �ε��� �Ҹ��� �� ���� �ϰ� �ϳ� ���
    {
        int t = Random.Range(0, Turtle_s.Length);

        soundManager.Instance.PlaySoundEffect(Turtle_s[t]);
    }
    /// <summary>
    /// �ź��� ��ų �Լ�(ȸ�� ����)
    /// </summary>
    private void Spin_Over()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        LayerMask excludeLayer = LayerMask.GetMask("Noting");                                                                       // ���̾� ����ó�� ����

        rb.excludeLayers = excludeLayer;                                                                                            // �ƹ��͵� ����ó�� ���� �������� ����

        t_F = false;
        t_obj = null;
        monster.Now_Skill_Time();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (this.GetComponent<Monster>().Category == Monster.Name.Turtle)                                                        // ���� �ź��̰�
            if (t_F && Value_2 > 0f)                                                                                             // ����Ʈ�� �浹 �ߴٸ�
            {
                if (collision.collider.transform.gameObject.CompareTag("Wall"))
                {
                    t_F = false;
                    Turtle_S_Rand();                                                                                             // ���ο� ����Ʈ ���
                    Target_Wall();
                }
            }
    }
}
