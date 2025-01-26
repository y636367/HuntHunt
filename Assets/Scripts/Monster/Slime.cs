using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Slime : MonoBehaviour
{
    #region Variable
    [SerializeField]
    private Slime_Face faces;                                                   // Idle, Hit, Dead �� ǥ�� ��Ƽ����

    Animator animator;
    Rigidbody rb;
    WaitForFixedUpdate wait;

    public NavMeshAgent agent;
    private Material faceMaterial;

    enum State
    {
        Idle,
        Run,
        Death
    }

    private State state;

    [SerializeField]
    private GameObject SlimeBody;

    [Header("�������ͽ�")]
    [SerializeField]
    private float Physical_strength;
    [SerializeField]
    private float Attack_power;
    [SerializeField]
    private float Defensive_power;

    public float MaxHealth;

    [SerializeField]
    public float speed;

    public bool run = false;

    [SerializeField]
    private bool isLive;

    [SerializeField]
    private Transform Player;

    private float Slower_Timer = 0f;
    private float Shield_Timer = 0f;
    private float s_speed;
    public float default_speed;

    private bool Slow;
    private bool Stun;

    public bool Boss_Skill;

    public int monster_num;

    [SerializeField]
    private SkinnedMeshRenderer[] Skin;
    [SerializeField]
    private Color[] Existing;
    #endregion
    #region ����
    [Header("Damage_Sounds")]
    [SerializeField]
    private string Damage_s;

    [Header("Spawn_Sounds")]
    [SerializeField]
    private string Spawn_s;

    [Header("Dead_Sounds")]
    [SerializeField]
    private string Dead_s;
    #endregion
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        faceMaterial = SlimeBody.GetComponent<Renderer>().materials[1];
        wait = new WaitForFixedUpdate();

        Existing = new Color[Skin.Length];

        for (int index = 0; index < Skin.Length; index++)                                                   // �ǰ� �� ���� ���·� ���ư��� ���� ���� SkinRenderer ����
        {
            Existing[index] = Skin[index].material.color;
        }

        state = State.Idle;
    }
    private void Update()
    {
        if (!GameManager.Instance.Start_ || GameManager.Instance.Player_Dead)                                   // ���� ����, �Ͻ�����, ���
        {
            Stop_Animation();
            return;
        }
        else
            Resume_Animation();

        FreezeRotation();
        Move();
    }
    /// <summary>
    /// Player ���Է� �̵�
    /// </summary>
    private void Move()
    {
        if (run)
        {
            agent.speed = speed;                                                                    // �ӷ� ����
            agent.SetDestination(Player.position);                                                  // ������ ����
        }
    }
    /// <summary>
    /// Spawn �� ���� ��ȯ(�ִϸ��̼� �̺�Ʈ �Լ�)
    /// </summary>
    private void Birth()
    {
        run = true;
        state = State.Run;
        animator.SetTrigger("Move");
    }
    /// <summary>
    /// Ȥ�ó� ������Ʈ�� Ƣ�� ���� �����Ͽ� �ʱ�ȭ
    /// </summary>
    private void FreezeRotation()
    {
        if (run)
        {
            rb.velocity = Vector3.zero;                                                         // �ӷ� �� ���� �ʱ�ȭ
            rb.angularVelocity = Vector3.zero;
        }
    }
    /// <summary>
    /// Spawn�� ����Ʈ ���� �Լ�(�ִϸ��̼� �̺�Ʈ ó��)
    /// </summary>
    private void Spawn_Effect()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(0);
        soundManager.Instance.PlaySoundEffect(Spawn_s);
        effect.transform.position = this.transform.position;
    }
    /// <summary>
    /// Dead �� ����Ʈ ���� �Լ�(�ִϸ��̼� �̺�Ʈ ó��)
    /// </summary>
    private void Death_Effect()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(3);
        soundManager.Instance.PlaySoundEffect(Dead_s);
        effect.transform.position = this.transform.position;
    }
    /// <summary>
    /// ������Ʈ Ǯ���� ���� ActvieFalse(�ִϸ��̼� �̺�Ʈ ó��)
    /// </summary>
    private void Death()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Face Matirial ����
    /// </summary>
    /// <param name="tex"></param>
    private void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }
    /// <summary>
    /// Ȱ��ȭ�� ���� ���� ���¿� ���� Status �� ���� �ʱ�ȭ
    /// </summary>
    private void OnEnable()
    {
        Player = GameManager.Instance.player.GetComponent<Transform>();
        agent.enabled = true;
        SetFace(faces.IdleFace_1);
        isLive = true;
        Physical_strength = MaxHealth;
        default_speed = this.speed;
        this.GetComponent<Collider>().enabled = true;
        for (int index = 0; index < Skin.Length; index++)
        {
            Skin[index].material.color = Existing[index];
        }
    }
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    /// <param name="t_data"></param>
    public void Init(SpawnData t_data)
    {
        MaxHealth = t_data.Health;
        Physical_strength = t_data.Health;
        Defensive_power = t_data.Defensive;
        Attack_power = t_data.Attack;

        MaxHealth = GameManager.Instance.spawnData.Stage_Multiple(MaxHealth);                                                   // Stage�� ���� �ɷ�ġ ��
        Defensive_power = GameManager.Instance.spawnData.Stage_Multiple(Defensive_power);
        Attack_power = GameManager.Instance.spawnData.Stage_Multiple(Attack_power);

        if (GameManager.Instance.Upgrade_Count != 0)
        {
            for (int index = 0; index < GameManager.Instance.Upgrade_Count; index++)
            {
                MaxHealth = GameManager.Instance.spawnData.Time_Upgrade(MaxHealth);
                Defensive_power = GameManager.Instance.spawnData.Time_Upgrade(Defensive_power);
            }
        }
        Physical_strength = MaxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))                                                                                             // Shield�� �������
        {
            s_speed = speed - other.GetComponent<Bullet>().additional;

            this.Slow = true;                                                                                                       // Slow �����
            speed = s_speed;
        }
        else if (other.CompareTag("Bullet"))                                                                                        // Bullet�� �������
        {
            try
            {
                Physical_strength -= other.GetComponent<Bullet>().Damage * (1 / (1 + Defensive_power));                             // Bullet Ÿ�� �ǰ�
                Hit_Damage();
            }
            catch (NullReferenceException) { }
            try
            {
                Physical_strength -= other.GetComponent<Bomb>().Damage * (1 / (1 + Defensive_power));                               // Bomb Ÿ�� �ǰ�
                StartCoroutine(OnDamage());
            }
            catch (NullReferenceException) { }
        }

        if (Physical_strength <= 0)
            Death_state();                                                                                                          // ü���� 0 ���Ϸ� �������� ��� ó��
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slower"))                                                                                 // Slower�� ����ؼ� ��� �ִ� ���
        {
            Slower_Timer += Time.deltaTime;

            s_speed = speed - other.GetComponent<Bomb>().Damage;                                                        // �ӵ� ����

            this.Slow = true;
            speed = s_speed;
            Invoke("Debuff_Off", 1.5f);                                                                                 // ����� ������ ������ Exit �ǰ� �� ��
            if (Slower_Timer > 0.5f)                                                                                    // �� �����Ӹ��� ���� ����
            {
                if (Physical_strength > 0)
                {
                    Physical_strength -= other.GetComponent<Bomb>().Damage * (1 / (1 + Defensive_power));               // ������ ����� ü�� ����
                    Slower_Timer = 0f;
                }
                else
                {
                    Death_state();
                }
            }
        }
        else if (other.CompareTag("Stun"))                                                                              // Stun�� ����ؼ� ��� �ִ� ���
        {
            s_speed = 0f;                                                                                               // �ӵ� �ʱ�ȭ

            this.Stun = true;
            speed = s_speed;

            SetFace(faces.DeathFace);
            animator.speed = 0f;

            Invoke("Debuff_Off", 1.5f);                                                                                 // ����� ������ ������ Exit �ǰ� �� ��
        }
        else if (other.CompareTag("Shield"))                                                                            // Shield�� ����ؼ� ��� �ִ� ���
        {
            Shield_Timer += Time.deltaTime;

            if (Shield_Timer > 0.5f)                                                                                    // �� ������ ���� ���� ����
            {
                if (Physical_strength > 0)
                {
                    Physical_strength -= other.GetComponent<Bullet>().Damage * (1 / (1 + Defensive_power));
                    StartCoroutine(OnDamage());
                    Shield_Timer = 0f;
                }
                else
                {
                    Death_state();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shield"))
            Debuff_Off();
    }
    /// <summary>
    /// ȭ�� ����, ������ ����Ʈ, ��ȣź �� ������
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Bullet"))
            return;

        try
        {
            Physical_strength -= other.GetComponent<Bullet>().Damage * (1 / (1 + Defensive_power));
            StartCoroutine(OnDamage());
        }
        catch (NullReferenceException) { }
        try
        {
            Physical_strength -= other.GetComponent<Bomb>().Damage * (1 / (1 + Defensive_power));
            StartCoroutine(OnDamage());
        }
        catch (NullReferenceException) { }


        if (Physical_strength <= 0)
            Death_state();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.player.Hit)
            {
                return;
            }
            else
                Hit_Player();
        }
    }
    /// <summary>
    /// Player�� �����ϰ� �ɽ� Player �ǰ�
    /// </summary>
    private void Hit_Player()
    {
        GameManager.Instance.player.On_Damage(MaxHealth);
        UIManager.instance.HealthBar.Update_HUD();
    }
    /// <summary>
    /// ����� ����
    /// </summary>
    private void Debuff_Off()
    {
        if (this.Slow)
        {
            speed = default_speed;
            this.Slow = false;
        }
        else if (this.Stun)
        {
            speed = default_speed;
            this.Stun = false;
            SetFace(faces.IdleFace_1);
            animator.speed = 1f;
        }
    }
    /// <summary>
    /// ��� ���� ��ȯ �� �ɷ�ġ ����
    /// </summary>
    public void Death_state()
    {
        Debuff_Off();

        StopCoroutine(OnDamage());
        StopCoroutine(Knockback());
        this.GetComponent<Collider>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        Physical_strength = -1f;
        run = false;
        state = State.Death;
        isLive = false;
        agent.speed = 0;
        animator.SetTrigger("Death");

        for (int index = 0; index < Skin.Length; index++)
        {
            Skin[index].material.color = Existing[index];
        }

        if (!Boss_Skill)                                                                                                    // Boss ��ų�� ������ ���Ͱ� �ƴ϶��
        {
            GameManager.Instance.pool.EnemyCount--;
            GameManager.Instance.current_Kill += 1;
            UIManager.instance.Kill.Update_HUD();                                                                           // Kill HUD Update
            GameManager.Instance.drop.Category(0, -1, this.transform);                                                      // ���� ����
            GameManager.Instance.drop.Category(1, monster_num, this.transform);                                             // ����ġ ����
            GameManager.Instance.drop.Category(4, -1, this.transform);                                                      // ������ ����
        }
        SetFace(faces.DeathFace);
    }
    /// <summary>
    /// �ǰ� ó��
    /// </summary>
    private void Hit_Damage()
    {
        StartCoroutine(OnDamage());
        StartCoroutine(Knockback());
    }
    /// <summary>
    /// �ǰ� ȿ�� ����� ���� Skin ���� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnDamage()
    {
        soundManager.Instance.PlaySoundEffect(Damage_s);

        for (int index = 0; index < Skin.Length; index++)
        {
            Skin[index].material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);

        if (Physical_strength > 0)
        {
            for (int index = 0; index < Skin.Length; index++)
            {
                Skin[index].material.color = Existing[index];
            }
        }
    }
    /// <summary>
    /// �ǰ� �� �з��� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Knockback()
    {
        agent.enabled = false;
        yield return wait;                                                                                  // ���� �ϳ��� ���� ������ ������
                                                                                                            //yield return new WaitForSeconds(2f) //����ȭ�� ���� �ʱ⿡ �ǵ��� ���� ���� �����ؼ� ����� ��

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        rb.AddForce(dirVec.normalized * 10, ForceMode.Impulse);                                             //�������� ���̱⿡ Impulse
        agent.enabled = true;
    }
    /// <summary>
    /// �ִϸ��̼� ���� �� ���ڸ����� ����
    /// </summary>
    private void Stop_Animation()
    {
        agent.velocity = Vector3.zero;
        animator.speed = 0f;
        agent.speed = 0;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }
    /// <summary>
    /// �ִϸ��̼� ��� �� ���ڸ� ���� ����
    /// </summary>
    public void Resume_Animation()
    {
        animator.speed = 1f;
        agent.speed = 1f;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }
}