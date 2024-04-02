using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Slime : MonoBehaviour
{
    #region Variable
    [SerializeField]
    private Slime_Face faces;                                                   // Idle, Hit, Dead 얼굴 표현 머티리얼

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

    [Header("스테이터스")]
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
    #region 사운드
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
        faceMaterial= SlimeBody.GetComponent<Renderer>().materials[1];
        wait=new WaitForFixedUpdate();

        Existing = new Color[Skin.Length];

        for (int index = 0; index < Skin.Length; index++)                                                   // 피격 후 원래 상태로 돌아가기 위한 원래 SkinRenderer 저장
        {
            Existing[index] = Skin[index].material.color;
        }

        state = State.Idle;
    }
    private void Update()
    {
        if (!GameManager.Instance.Start_ || GameManager.Instance.Player_Dead)                                   // 게임 종료, 일시정지, 사망
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
    /// Player 에게로 이동
    /// </summary>
    private void Move()
    {
        if (run)
        {
            agent.speed = speed;                                                                    // 속력 설정
            agent.SetDestination(Player.position);                                                  // 목적지 설정
        }
    }
    /// <summary>
    /// Spawn 후 상태 변환(애니메이션 이벤트 함수)
    /// </summary>
    private void Birth()
    {
        run = true;
        state = State.Run;
        animator.SetTrigger("Move");
    }
    /// <summary>
    /// 혹시나 오브젝트가 튀는 것을 방지하여 초기화
    /// </summary>
    private void FreezeRotation()
    {
        if (run)
        {
            rb.velocity = Vector3.zero;                                                         // 속력 및 각도 초기화
            rb.angularVelocity = Vector3.zero;
        }
    }
    /// <summary>
    /// Spawn시 이펙트 생성 함수(애니메이션 이벤트 처리)
    /// </summary>
    private void Spawn_Effect()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(0);
        soundManager.Instance.PlaySoundEffect(Spawn_s);
        effect.transform.position = this.transform.position;
    }
    /// <summary>
    /// Dead 시 이펙트 생성 함수(애니메이션 이벤트 처리)
    /// </summary>
    private void Death_Effect()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(3);
        soundManager.Instance.PlaySoundEffect(Dead_s);
        effect.transform.position = this.transform.position;
    }
    /// <summary>
    /// 오브젝트 풀링을 위해 ActvieFalse(애니메이션 이벤트 처리)
    /// </summary>
    private void Death()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Face Matirial 설정
    /// </summary>
    /// <param name="tex"></param>
    private void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }
    /// <summary>
    /// 활성화시 현재 게임 상태에 따른 Status 및 상태 초기화
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
    /// 초기화
    /// </summary>
    /// <param name="t_data"></param>
    public void Init(SpawnData t_data)
    {
        MaxHealth = t_data.Health;
        Physical_strength = t_data.Health;
        Defensive_power = t_data.Defensive;
        Attack_power = t_data.Attack;

        MaxHealth = GameManager.Instance.spawnData.Stage_Multiple(MaxHealth);                                                   // Stage에 따른 능력치 곱
        Attack_power = GameManager.Instance.spawnData.Stage_Multiple(Attack_power);

        if (GameManager.Instance.Upgrade_Count != 0)
        {
            for (int index = 0; index < GameManager.Instance.Upgrade_Count; index++)
            {
                MaxHealth = GameManager.Instance.spawnData.Time_Upgrade(MaxHealth);
            }
        }
        Physical_strength = MaxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))                                                                                             // Shield에 닿았을시
        {
            s_speed = speed - other.GetComponent<Bullet>().additional;

            this.Slow = true;                                                                                                       // Slow 디버프
            speed = s_speed;
        }
        else if (other.CompareTag("Bullet"))                                                                                        // Bullet에 닿았을시
        {
            try
            {
                Physical_strength -= other.GetComponent<Bullet>().Damage * (1 - Defensive_power);                                   // Bullet 타입 피격
                Hit_Damage();
            }
            catch (NullReferenceException) { }
            try
            {
                Physical_strength -= other.GetComponent<Bomb>().Damage * (1 - Defensive_power);                                     // Bomb 타입 피격
                StartCoroutine(OnDamage());
            }
            catch (NullReferenceException) { }
        }

        if (Physical_strength > 0)
        {

        }
        else
        {
            Death_state();                                                                                                          // 체력이 0 이하로 떨어지면 사망 처리
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slower"))                                                                                 // Slower에 계속해서 닿고 있는 경우
        {
            Slower_Timer += Time.deltaTime;

            s_speed = speed - other.GetComponent<Bomb>().Damage;                                                        // 속도 감소

            this.Slow = true;
            speed = s_speed;    
            Invoke("Debuff_Off", 1.5f);                                                                                 // 디버프 해제는 완전히 Exit 되고 난 후
            if (Slower_Timer > 0.5f)                                                                                    // 매 프레임마다 감소 방지
            {
                if (Physical_strength > 0)
                {
                    Physical_strength -= other.GetComponent<Bomb>().Damage * (1 - Defensive_power);                     // 데미지 존재시 체력 감소
                    Slower_Timer = 0f;
                }
                else
                {
                    Death_state();
                }
            }
        }
        else if (other.CompareTag("Stun"))                                                                              // Stun에 계속해서 닿고 있는 경우
        {
            s_speed = 0f;                                                                                               // 속도 초기화

            this.Stun = true;
            speed = s_speed;

            SetFace(faces.DeathFace);
            animator.speed = 0f;

            Invoke("Debuff_Off", 1.5f);                                                                                 // 디버프 해제는 완전히 Exit 되고 난 후
        }
        else if (other.CompareTag("Shield"))                                                                            // Shield에 계속해서 닿고 있는 경우
        {
            Shield_Timer += Time.deltaTime;

            if (Shield_Timer > 0.5f)                                                                                    // 매 프레임 마다 감소 방지
            {
                if (Physical_strength > 0)
                {
                    Physical_strength -= other.GetComponent<Bullet>().Damage * (1 - Defensive_power);
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
    /// 화염 방사기, 폭발형 이펙트, 신호탄 등 데미지
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Bullet"))
            return;

        try
        {
            Physical_strength -= other.GetComponent<Bullet>().Damage * (1 - Defensive_power);
            StartCoroutine(OnDamage());
        }
        catch (NullReferenceException) { }
        try
        {
            Physical_strength -= other.GetComponent<Bomb>().Damage * (1 - Defensive_power);
            StartCoroutine(OnDamage());
        }
        catch (NullReferenceException) { }


        if (Physical_strength > 0)
        {

        }
        else
        {
            Death_state();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.player.Hit)
            {
                return;
            }else
                Hit_Player();
        }
    }
    /// <summary>
    /// Player에 접촉하게 될시 Player 피격
    /// </summary>
    private void Hit_Player()
    {
        GameManager.Instance.player.On_Damage(MaxHealth);
        UIManager.instance.HealthBar.Update_HUD();
    }
    /// <summary>
    /// 디버프 해제
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
    /// 사망 상태 전환 및 능력치 수정
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

        if (!Boss_Skill)                                                                                                    // Boss 스킬로 생성된 몬스터가 아니라면
        {
            GameManager.Instance.pool.EnemyCount--;
            GameManager.Instance.current_Kill += 1;
            UIManager.instance.Kill.Update_HUD();                                                                           // Kill HUD Update
            GameManager.Instance.drop.Category(0, -1, this.transform);                                                      // 코인 생성
            GameManager.Instance.drop.Category(1, monster_num, this.transform);                                             // 경험치 생성
            GameManager.Instance.drop.Category(4, -1, this.transform);                                                      // 아이템 생성
        }
        SetFace(faces.DeathFace);
    }
    /// <summary>
    /// 피격 처리
    /// </summary>
    private void Hit_Damage()
    {
        StartCoroutine(OnDamage());
        StartCoroutine(Knockback());
    }
    /// <summary>
    /// 피격 효과 재생을 위한 Skin 색상 변경
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
    /// 피격 시 밀려남 실행
    /// </summary>
    /// <returns></returns>
    private IEnumerator Knockback()
    {
        agent.enabled= false;
        yield return wait;                                                                                  // 다음 하나의 물리 프레임 딜레이
                                                                                                            //yield return new WaitForSeconds(2f) //최적화에 좋지 않기에 되도록 따로 변수 생성해서 사용할 것

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        rb.AddForce(dirVec.normalized * 10, ForceMode.Impulse);                                             //순간적인 힘이기에 Impulse
        agent.enabled = true;
    }
    /// <summary>
    /// 애니메이션 정지 및 제자리에서 정지
    /// </summary>
    private void Stop_Animation()
    {
        agent.velocity = Vector3.zero;
        animator.speed = 0f;
        agent.speed = 0;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }
    /// <summary>
    /// 애니메이션 재게 및 제자리 정지 해제
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
