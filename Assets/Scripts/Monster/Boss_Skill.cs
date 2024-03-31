using UnityEngine;

public class Boss_Skill : MonoBehaviour
{
    #region
    [SerializeField]
    private int Boss_num;                                                                                   // 보스 종류 구분할 int 변수

    [SerializeField]
    private GameObject t_obj;                                                                               // 타겟 설정이 필요한 보스를 위한 GameObject 변수

    Monster monster;

    [SerializeField]
    private float Value;
    float Value_2;

    [SerializeField]
    private string[] Turtle_s;                                                                              // Turtle 부딪힐 포인트

    bool t_F = false;
    #endregion
    private void Awake()
    {
        monster=GetComponent<Monster>();
    }
    /// <summary>
    ///  GameObject 변수 초기화
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
                if (t_F)                                                                                                                                // (스킬 - 유령)숨었는가?
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
                if (t_F)                                                                                                                                // 돌진(스킬 - 풍뎅이)중인가?
                {
                    transform.position = Vector3.MoveTowards(transform.position, t_obj.transform.position, Value);                                      // Navagent로 인해 이미 방향은 Player를 항해 있고, 돌진 시 방향이 갑자기 바뀌는걸 방지하기 위해 오브젝트를 향해 점차 이동
                }
                break;
            case 4:
                if (t_F)                                                                                                                                // 회전(스킬 - 거북이)중인가?
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
 /// 0 - 유령, 1 - 풍뎅이, 2 - 뼈다귀, 3 - 버섯, 4 - 거북이, 5 - 레드 슬라임
 /// </summary>
    public void Skills()
    {
        switch (Boss_num)
        {
            case 0: // 유령
                Invoke("Goast", 0.8f);
                break;
            case 1: // 풍뎅이
                Invoke("Beetle", 0.5f);
                break;
            case 2: // 뼈다귀
                Invoke("Skull_King", 0.8f);
                break;
            case 3: // 버섯
                Invoke("MushRoom", 0.13f);
                break;
            case 4: // 거북이
                Invoke("Turtle", 0.45f);
                break;
            case 5: // 레드 슬라임
                Invoke("Red_Slime", 0.5f);
                break;
        }
    }
 /// <summary>
 /// 유령 스킬 함수 (사라졌다가 Player의 주변에서 등장)
 /// </summary>
    private void Goast()
    {
        GameObject Render_Body = transform.GetChild(0).gameObject;

        if (!t_F)
        {
            GameObject effect = GameManager.Instance.pool.Effect_Get(2);                                                            // 이펙트 생성
            effect.transform.position = this.transform.position;

            Render_Body.SetActive(false);                                                                                           // 아바타의 Render Body만 enable=false

            monster.animator.speed = 0f;

            this.tag = "Untagged";

            if (GameManager.Instance.player.scanner.nearestTarget == this.gameObject)                                               // Player가 감지 할 수 없도록(오토 사격으로 인한 몬스터 위치 특정 예방) null
                GameManager.Instance.player.scanner.nearestTarget = null;
            t_F = true;                                                                                                             // 스킬 사용

            monster.Now_Skill = true;                                                                                               // 애니메이션 변경을 위한 상태 변수 값 변경

            Value = Random.Range(3f, 5f);
            Value_2 = Value;
        }
        else
        {
            t_F = false;                                                                                                            // 스킬 사용 중지(모습 드러내기)

            int ran = Random.Range(0, 360);

            Vector3 pos = GameManager.Instance.player.transform.position + (Random.insideUnitSphere * 4.7f);                        // Player 주변에서 랜덤 포지션 값
            pos.y = transform.position.y;

            this.transform.position = pos;                                                                                          // 랜덤 포지션 위치로 등장

            monster.animator.speed = 1f;
            monster.Now_Skill_Time();

            Render_Body.SetActive(true);

            this.tag = "Enemy";
        }
    }
 /// <summary>
 /// 풍뎅이 스킬 함수(Player 향해 돌진)
 /// </summary>
    private void Beetle()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(4);                                                                // 이펙트 생성
        effect.transform.position = this.transform.position;
        monster.agent.isStopped = true;                                                                                             // agent 정지

        if (t_obj == null)
        {
            t_obj = new GameObject();
            t_obj.transform.parent = this.transform;                                                                                // 풍뎅이 앞쪽에 오브젝트를 두어 이 오브젝트를 향해서 계속해서 이동할 수 있도록 설정
        }

        t_obj.transform.position = this.transform.forward * 5;
        t_F = true;
        Invoke("Stop_Beetle", 1.3f);                                                                                                // 돌진 정지
    }
 /// <summary>
 /// 풍뎅이 스킬 함수(돌진 중지)
 /// </summary>
    private void Stop_Beetle()
    {
        t_F = false;
        monster.Now_Skill_Time();
    }
 /// <summary>
 /// 스켈레톤 킹 스킬 함수(체력 회복)
 /// </summary>
    private void Skull_King()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(4);                                                                // 이펙트 생성
        effect.transform.position = this.transform.position;

        monster.Physical_strength += monster.MaxHealth / 3;                                                                         // 현재 스켈레톤 킹의 최대체력의 3분의 1만큼 회복
        monster.Now_Skill_Time();
    }
  /// <summary>
  /// 거대버섯 스킬 함수(일반 몬스터 생성)
  /// </summary>
    private void MushRoom()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(4);                                                                // 이펙트 생성
        effect.transform.position = this.transform.position;

        GameManager.Instance.spawner.Spawn_Boss_Skill();                                                                            // 일반 몬스터 생성
    }
  /// <summary>
  /// 거북이 스킬 함수(특정 포인트를 향해 회전하며 돌진)
  /// </summary>
    private void Turtle()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        LayerMask excludeLayer = LayerMask.GetMask("Default");                                                                      // Default 레이어 예외 처리

        rb.excludeLayers = excludeLayer;                                                                                            // 부딪힘 방지 위함(Default 오브젝트 - 기둥, 벽 등 부딪힘 x 그냥 통과)

        Value_2 = Random.Range(4.5f, 7.5f);
        monster.animator.SetTrigger("Spin");
        Target_Wall();
    }
    /// <summary>
    /// 레드 슬라임 스킬 함수(현재 체력 기반 분신 생성)
    /// </summary>
    private void Red_Slime()
    {
        GameObject effect = GameManager.Instance.pool.Effect_Get(4);                                                               // 이펙트 생성
        effect.transform.position = this.transform.position;

        GameManager.Instance.spawner.red_Slime_Skill(monster.Physical_strength, monster.Defensive_power, monster.Attack_power);    // 현재 체력 기반 분신 생성
    }
   /// <summary>
   /// 거북이 스킬 - 새로운 타겟 검색
   /// </summary>
    private void Target_Wall()
    {
        t_obj = GameManager.Instance.walls.Next_Point();                                                                           // 거북이의 새로운 돌진 타겟

        t_F = true;
    }
    private void Turtle_S_Rand()                                                                                                  // 부딪힘 소리들 중 랜덤 하게 하나 재생
    {
        int t = Random.Range(0, Turtle_s.Length);

        soundManager.Instance.PlaySoundEffect(Turtle_s[t]);
    }
    /// <summary>
    /// 거북이 스킬 함수(회전 중지)
    /// </summary>
    private void Spin_Over()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        LayerMask excludeLayer = LayerMask.GetMask("Noting");                                                                       // 레이어 예외처리 해제

        rb.excludeLayers = excludeLayer;                                                                                            // 아무것도 예외처리 하지 않음으로 설정

        t_F = false;
        t_obj = null;
        monster.Now_Skill_Time();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (this.GetComponent<Monster>().Category == Monster.Name.Turtle)                                                        // 현재 거북이가
            if (t_F && Value_2 > 0f)                                                                                             // 포인트에 충돌 했다면
            {
                if (collision.collider.transform.gameObject.CompareTag("Wall"))
                {
                    t_F = false;
                    Turtle_S_Rand();                                                                                             // 새로운 포인트 계산
                    Target_Wall();
                }
            }
    }
}
