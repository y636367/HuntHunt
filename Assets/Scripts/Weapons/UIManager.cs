using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // 이벤트 적용을 위한 인스턴스 클래스 생성
    public After_ after_ = new After_();
    public After_ after_Status = new After_();

    public static UIManager instance;

    #region ScorePanel
    [SerializeField]
    private GameObject Score_Panel;

    [Header("Title")]
    [SerializeField]
    private Image t_back;
    [SerializeField]
    private Text t_text;
    [SerializeField]
    private Text C_text;

    [Header("Infomation_1")]
    [SerializeField]
    private Text stage;
    [SerializeField]
    private Text difficult;

    [Header("Infomation_2")]
    [SerializeField]
    private Text coin;
    [SerializeField]
    private Text alive;

    [Header("Infomation_3")]
    [SerializeField]
    private Text exp;
    [SerializeField]
    private Slider Exp_Bar;
    [SerializeField]
    private Image Bar_Back;

    [Header("Main_Button")]
    [SerializeField]
    private Text t_main;
    [SerializeField]
    private Image t_main_;

    [Header("Go_Main")]
    [SerializeField]
    private Button Sure;

    [SerializeField]
    private float speed;
    #endregion
    #region 기타
    [SerializeField]
    private GameObject Option_A;
    [SerializeField]
    private GameObject Option_B;
    [SerializeField]
    private Clear_Particle clear_particle;

    [Header("HUDs")]
    [SerializeField]
    public HUD HealthBar;
    [SerializeField]
    public HUD CurrentCoin;
    [SerializeField]
    public HUD Kill;

    Animator animator;

    Vector2 basePos;                                                                                                // 처음 누른 위치
    Vector2 currentPos;                                                                                             // 드래그 된 위치(현재 위치)
    public Vector2 dir;

    bool Click_Check = false;
    #endregion
    #region 스틱
    [Header("Joystick")]
    [SerializeField]
    private RectTransform joystickBG;
    [SerializeField]
    private RectTransform stick;

    [SerializeField]
    private float Stick_Range;
    #endregion
    #region 사운드
    [Header("SoundSliders")]
    [SerializeField]
    private Slider BGM;
    [SerializeField]
    private Slider SFX;
    #endregion

    private void Awake()
    {
        instance= this;
        animator = GetComponent<Animator>();

        Score_Text_Panel_Off();

        Option_A.SetActive(false);
        Option_B.SetActive(false);

        joystick_Off();
    }
    /// <summary>
    /// 후 동작 이벤트
    /// </summary>
    public void Setting()
    {
        soundManager.Instance.GetSliders(BGM, SFX);

        after_?.Invoke();
    }
    /// <summary>
    /// Status HUD
    /// </summary>
    public void Status_Setting()
    {
        after_Status?.Invoke();
    }
    /// <summary>
    /// Score HUD
    /// </summary>
    private void FixedUpdate()
    {
        if (GameManager.Instance.Start_On)                                                      // 일시 정지 및 게임 시작 조절 변수
            return;

        if (!GameManager.Instance.Start_)
            return;
        else
        {
            JoySticOn();
        }
    }
    /// <summary>
    /// 조이스틱은 화면 터치(클릭)시에만 on
    /// </summary>
    private void joystick_Off()
    {
        joystickBG.gameObject.SetActive(false);
        stick.gameObject.SetActive(false);

        dir = Vector3.zero;
    }
    /// <summary>
    /// 현재 터치된 화면의 Position 값 구하는 함수
    /// </summary>
    private void OnStickDown()
    {
#if UNITY_EDITOR
        basePos = Input.mousePosition;                                                          // 터치된 지점 기준
#endif
#if UNITY_ANDROID || UNITY_IOS
        basePos = Input.GetTouch(0).position;
#endif
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.transform as RectTransform, basePos, this.GetComponent<Canvas>().worldCamera, out localPoint);        // 입력된 좌표 값을 screen 좌표계로 전환해서 재 저장(시행 하지 않을 시 좌표가 엇나가 엉뚱한 곳에 생성)

        basePos = localPoint;

        joystickBG.anchoredPosition = basePos;                                                  // 조이스틱 이미지 위치 지정
        stick.anchoredPosition = basePos;

        joystickBG.gameObject.SetActive(true);
        stick.gameObject.SetActive(true);

        dir = Vector2.zero;
    }
    /// <summary>
    /// 조이스틱 드래그 시
    /// </summary>
    public void OnStickDrag()
    {
#if UNITY_EDITOR
        currentPos = Input.mousePosition;                                                       // 현재 위치를 저장
#endif
#if UNITY_ANDROID || UNITY_IOS
        currentPos =Input.GetTouch(0).position;
#endif
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.transform as RectTransform, currentPos, this.GetComponent<Canvas>().worldCamera, out localPoint);                  // 입력된 좌표 값을 screen 좌표계로 전환해서 재 저장(시행 하지 않을 시 좌표가 엇나가 엉뚱한 곳에 생성)

        currentPos = localPoint;

        Vector2 v = Vector2.ClampMagnitude(currentPos - basePos, Stick_Range);                  // 기준점으로부터의 거리를 제한한다 (스틱의 이동 거리)

        stick.anchoredPosition = basePos + v;                                                   // 스틱의 위치를 기준점에서 v를 더한 위치로 지정한다

        dir = v.normalized;                                                                     // 방향 갱신
    }
    /// <summary>
    /// 조이스틱 실시간 입력값 확인 함수
    /// </summary>
    private void JoySticOn()
    {
#if UNITY_EDITOR
        if (!Input.GetMouseButton(0))                                                          // 클릭 종료 및 없을 시
        {
            Click_Check = false;
            joystick_Off();
        }

        if (Input.GetMouseButtonDown(0) && !Click_Check)                                       // 클릭
        {
            Click_Check = true;
            OnStickDown();
        }
        else if (Input.GetMouseButton(0) && Click_Check)                                       // 클릭 후 드래그 시
        {
            OnStickDrag();
        }
#endif

#if UNITY_ANDROID || UNITY_IOS                                                                          // IOS 대응 확인 후 수정 필요
        if (Input.touchCount == 0)                                                                      // 터치 종료 및 없을 시
        {
            Click_Check = false;
            joystick_Off();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !Click_Check)        // 터치 시작
        {
            Click_Check = true;
            OnStickDown();
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Click_Check)    // 터치 후 드래그 시
        {
            OnStickDrag();
        }
#endif
    }
    public void UI_On()
    {
        animator.SetTrigger("On");
    }
    private void Over_Animation()
    {
        animator.enabled = false;
    }
    #region ScorePanel
    /// <summary>
    /// 게임 종료, 스코어 집계 계열 시작
    /// </summary>
    public void Score_Panel_on()
    {
        Score_Panel.SetActive(true);
        Quit_Game.Instance.Limit = true;                                                                // Quit_Canvas 제한 변수 값 변경
        StartCoroutine(title_fade_in());
    }
    private void Score_Text_Panel_Off()
    {
        t_back.gameObject.SetActive(false);
        C_text.gameObject.SetActive(false);
        t_text.gameObject.SetActive(false);

        stage.gameObject.SetActive(false);
        difficult.gameObject.SetActive(false);

        coin.gameObject.SetActive(false);
        alive.gameObject.SetActive(false);

        exp.gameObject.SetActive(false);
        Exp_Bar.gameObject.SetActive(false);
        Bar_Back.gameObject.SetActive(false);

        t_main.gameObject.SetActive(false);
        t_main_.GetComponent<Button>().enabled = false;
        t_main_.gameObject.SetActive(false);

        Score_Panel.SetActive(false);
    }
    IEnumerator title_fade_in()                                                                         // 나타나야 하기에 미리 a값 0으로 만들고 해야함
    {
        Text text_d;

        if (GameManager.Instance.Clear)                                                                 // 생존 성공 시
        {
            text_d = C_text;
            clear_particle.Create_Particle();                                                           // Clear Particle 생성
            ClearDataCheck();                                                                           // 난이도 및 Stage 번호에 따라 점수 계산           
        }
        else
            text_d = t_text;

        #region Fade 효과
        t_back.gameObject.SetActive(true);
        t_back.color = new Color(t_back.color.r, t_back.color.g, t_back.color.b, 0f);
        text_d.gameObject.SetActive(true);
        text_d.color = new Color(text_d.color.r, text_d.color.g, text_d.color.b, 0f);

        while (t_back.color.a < 1.0f)
        {
            t_back.color = new Color(t_back.color.r, t_back.color.g, t_back.color.b, t_back.color.a + (Time.deltaTime / speed));
            text_d.color = new Color(text_d.color.r, text_d.color.g, text_d.color.b, text_d.color.a + (Time.deltaTime / speed));
            if (t_back.color.a >= 1.0f)
                StartCoroutine(info_fade_in());
            yield return null;
        }
        #endregion
    }
    IEnumerator info_fade_in()
    {
        #region Fade 효과
        stage.gameObject.SetActive(true);
        stage.color = new Color(stage.color.r, stage.color.g, stage.color.b, 0f);
        difficult.gameObject.SetActive(true);
        difficult.color = new Color(difficult.color.r, difficult.color.g, difficult.color.b, 0f);

        while (stage.color.a < 1.0f)
        {
            stage.color = new Color(stage.color.r, stage.color.g, stage.color.b, stage.color.a + (Time.deltaTime / speed));
            difficult.color = new Color(difficult.color.r, difficult.color.g, difficult.color.b, difficult.color.a + (Time.deltaTime / speed));

            if (stage.color.a >= 1.0f)
                StartCoroutine(info2_fade_in());
            yield return null;
        }
        #endregion
    }
    IEnumerator info2_fade_in()
    {
        #region Fade 효과
        coin.gameObject.SetActive(true);
        coin.color = new Color(coin.color.r, coin.color.g, coin.color.b, 0f);
        alive.gameObject.SetActive(true);
        alive.color = new Color(alive.color.r, alive.color.g, alive.color.b, 0f);

        while (coin.color.a < 1.0f)
        {
            coin.color = new Color(coin.color.r, coin.color.g, coin.color.b, coin.color.a + (Time.deltaTime / speed));
            alive.color = new Color(alive.color.r, alive.color.g, alive.color.b, alive.color.a + (Time.deltaTime / speed));

            if (coin.color.a >= 1.0f)
                StartCoroutine(info3_fade_in());
            yield return null;
        }
        #endregion
    }
    IEnumerator info3_fade_in()
    {
        #region Fade 효과 및 ExpBar 값 초기화
        exp.text = string.Format("Get Exp {0:F3}", GameManager.Instance.Get_Exp_Player);
        Exp_Bar.value = Backend_GameData.Instance.Userdatas.NowExp / Backend_GameData.Instance.Userdatas.Next_Exp_value;

        exp.gameObject.SetActive(true);
        exp.color = new Color(exp.color.r, exp.color.g, exp.color.b, 0f);
        Exp_Bar.gameObject.SetActive(true);
        Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color = new Color(Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color.r, Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color.g, Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color.b, 0);
        Bar_Back.gameObject.SetActive(true);
        Bar_Back.color = new Color(Bar_Back.color.r, Bar_Back.color.g, Bar_Back.color.b, 0f);

        while (exp.color.a < 1.0f)
        {
            exp.color = new Color(exp.color.r, exp.color.g, exp.color.b, exp.color.a + (Time.deltaTime / speed));
            Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color = new Color(Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color.r, Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color.g, Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color.b, Exp_Bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color.a + (Time.deltaTime / speed));
            Bar_Back.color = new Color(Bar_Back.color.r, Bar_Back.color.g, Bar_Back.color.b, Bar_Back.color.a + (Time.deltaTime / speed));
            yield return null;
        }

        StartCoroutine(Get_Exp_Effect());
        #endregion
    }
    IEnumerator Get_Exp_Effect()
    {
        #region Exp Bar 애니메이션 효과
        yield return new WaitForSeconds(1f);                                                            // 1초 대기

        float duration = 2.0f;
        float offset = (0 - GameManager.Instance.Get_Exp_Player) / duration;

        while (GameManager.Instance.Get_Exp_Player > 0)
        {
            GameManager.Instance.Get_Exp_Player += offset * Time.deltaTime;
            Backend_GameData.Instance.Userdatas.NowExp += (-1) * (offset * Time.deltaTime);
            exp.text = string.Format("Get Exp {0:F3}", GameManager.Instance.Get_Exp_Player);

            if (Backend_GameData.Instance.Userdatas.Next_Exp_value <= Backend_GameData.Instance.Userdatas.NowExp)
            {
                Backend_GameData.Instance.Userdatas.Next_Level();
                PD_Control.Instance.LevelUP_LifeMax = true;                                                                         // User LevelUp 시 Life MaxLife 만큼 충전
            }

            Exp_Bar.value = Backend_GameData.Instance.Userdatas.NowExp / Backend_GameData.Instance.Userdatas.Next_Exp_value;

            if (GameManager.Instance.Get_Exp_Player < 0)
            {
                GameManager.Instance.Get_Exp_Player = 0;
                exp.text = string.Format("Get Exp {0:F3}", GameManager.Instance.Get_Exp_Player);
            }

            yield return null;
        }
        #endregion
        Backend_GameData.Instance.UpdateUserDatas_();                                                   // 서버에 데이터 반영
        Backend_GameData.Instance.UpdateClearDatas_();

        StartCoroutine(button_fade_in());
    }
    IEnumerator button_fade_in()
    {
        #region Fade 효과
        t_main.gameObject.SetActive(true);
        t_main.color = new Color(t_main.color.r, t_main.color.g, t_main.color.b, 0f);
        t_main_.gameObject.SetActive(true);
        t_main_.color = new Color(t_main_.color.r, t_main_.color.g, t_main_.color.b, 0f);

        while (t_main.color.a < 1.0f)
        {
            t_main.color = new Color(t_main.color.r, t_main.color.g, t_main.color.b, t_main.color.a + (Time.deltaTime / speed));
            t_main_.color = new Color(t_main_.color.r, t_main_.color.g, t_main_.color.b, t_main_.color.a + (Time.deltaTime / speed));

            if(t_main.color.a>=1.0f)
                t_main_.GetComponent<Button>().enabled = true;

            yield return null;
        }
        #endregion
    }
    #endregion
    public void SceneChange_Main()
    {
        Time.timeScale = 1;                                                                             // Time.timeScale의 변경된 값이 씬이 변경되고서 유지될 수 있기에 초기화

        Quit_Game.Instance.Limit = false;

        soundManager.Instance.Save_prview_SliderVale();
        Utils.Instance.LoadScene(SceneNames.Main);
    }
    /// <summary>
    /// 현재 Difficult 및 Stagenum에 따른 클리어 성공시 데이터 반영
    /// </summary>
    private void ClearDataCheck()
    {
        switch(PD_Control.Instance.StageManager_.Stage_num)
        {
            case 0:
                switch (PD_Control.Instance.StageManager_.Difficult)
                {
                    case 0:
                        Backend_GameData.Instance.Cleardatas.S1_Difficult_1 = true;
                        break;
                    case 1:
                        Backend_GameData.Instance.Cleardatas.S1_Difficult_2 = true;
                        break;
                    case 2:
                        Backend_GameData.Instance.Cleardatas.S1_Difficult_3 = true;
                        break;
                }
                break;
            case 1:
                switch (PD_Control.Instance.StageManager_.Difficult)
                {
                    case 0:
                        Backend_GameData.Instance.Cleardatas.S2_Difficult_1 = true;
                        break;
                    case 1:
                        Backend_GameData.Instance.Cleardatas.S2_Difficult_2 = true;
                        break;
                    case 2:
                        Backend_GameData.Instance.Cleardatas.S2_Difficult_3 = true;
                        break;
                }
                break;
            case 2:
                switch (PD_Control.Instance.StageManager_.Difficult)
                {
                    case 0:
                        Backend_GameData.Instance.Cleardatas.S3_Difficult_1 = true;
                        break;
                    case 1:
                        Backend_GameData.Instance.Cleardatas.S3_Difficult_2 = true;
                        break;
                    case 2:
                        Backend_GameData.Instance.Cleardatas.S3_Difficult_3 = true;
                        break;
                }
                break;
            case 3:
                switch (PD_Control.Instance.StageManager_.Difficult)
                {
                    case 0:
                        Backend_GameData.Instance.Cleardatas.S4_Difficult_1 = true;
                        break;
                    case 1:
                        Backend_GameData.Instance.Cleardatas.S4_Difficult_2 = true;
                        break;
                    case 2:
                        Backend_GameData.Instance.Cleardatas.S4_Difficult_3 = true;
                        break;
                }
                break;
            case 4:
                switch (PD_Control.Instance.StageManager_.Difficult)
                {
                    case 0:
                        Backend_GameData.Instance.Cleardatas.S5_Difficult_1 = true;
                        break;
                    case 1:
                        Backend_GameData.Instance.Cleardatas.S5_Difficult_2 = true;
                        break;
                    case 2:
                        Backend_GameData.Instance.Cleardatas.S5_Difficult_3 = true;
                        break;
                }
                break;
        }
    }
    public void Quit_ListOut()
    {
        Quit_Game.Instance.Panel_Out();
    }
    public void Quit_ListClear()
    {
        Quit_Game.Instance.All_Panel_Out();
    }
    public void Go_Main_Button_deActive()
    {
        Sure.interactable = false;
    }
}
