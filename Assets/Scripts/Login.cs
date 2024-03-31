using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
public class Login : Login_Base
{
    #region Variable
    [Header("ID")]                                              // 아이디 관련 변수
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private Text inputFieldID;

    [Header("PW")]                                              // 비밀번호 관련 변수
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private InputField inputFieldPW;                            // 비밀번호 항목으로 선택시 **** 문자가 텍스트 대신 삽입되기에 InputField에 입력된 Text 값 사용 위함

    [Header("BTN")]                                             // 각 버튼 변수
    [SerializeField]
    private Button LoginBtn;
    [SerializeField]
    private Button JoinBtn;
    [SerializeField]
    private Button FindIDBtn;
    [SerializeField]
    private Button FindPWBtn;

    [Header("AuotoLogin")]
    [SerializeField]
    private Toggle autologin;                                                   // Autologin 체크
    private string autologinKey = "AutoLogin";                                  // PlayerPrefs 의 선언 및 생성을 위한 string 변수
    #endregion

    /// <summary>
    /// 로그인 버튼 이벤트 발생 시
    /// </summary>
    public void OnClickeLogin()
    {
        ResetUI(imageID, imagePW);                                              // 매개변수로 입력한 InputField UI의 색상과 Message 내용 초기화

        if (IsFieldEmpty(imageID, inputFieldID.text, "ID"))                     // ID InputField 공백 체크
            return;

        if (IsFieldEmpty(imagePW, inputFieldPW.text, "PW"))                     // PW InputFIeld 공백 체크
            return;

        LoginBtn.interactable = false;                                          // 각 버튼 연타 방지를 위한 비활성화
        JoinBtn.interactable = false;
        FindIDBtn.interactable = false;
        FindPWBtn.interactable = false;

        StartCoroutine(nameof(LoginProcess));                                   // 서버 로그인 요청 하는 동안 내용 업데이트 필요
        Quit_Game.Instance.Limit = true;                                        // Quit 스크립트 실행 제어를 위한 변수 값 변경

        ResponseToLogin(inputFieldID.text, inputFieldPW.text);                  // 서버에 로그인 시도
    }
    /// <summary>
    /// ID, PW 값을 가지고 서버에 로그인 시도
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="PW"></param>
    private void ResponseToLogin(string ID, string PW)
    {
        string Error_message = string.Empty;                                    // 에러 메시지값을 받기 위한 string 변수 선언

        SendQueue.Enqueue(Backend.BMember.CustomLogin, ID, PW, callback =>      // SendQueue(비동기 큐) 구현(코루틴 계속 진행)
        {
            StopCoroutine(nameof(LoginProcess));                                // 콜백이 왔을 시 계속해서 업데이트 되는 코루틴 정지
            Sounds.instance.Complite_Sound();                                   // 소리 재생

            if (callback.IsSuccess())                                           // 로그인 성공 시
            {
                SetMassage($"{inputFieldID.text}님 환영합니다.");               // 문구 출력

                if (autologin.isOn == true)                                     // autologin 이 체크되어 있다면
                {
                    PlayerPrefs.SetInt(autologinKey, 1);                        // PlayerPrefs에 1(참) 삽입
                }
                else                                                            // 체크되어 있지 않다면
                {
                    PlayerPrefs.SetInt(autologinKey, 0);                        // PlayerPrefs에 0(참) 삽입
                }                                                               // 다음 로그인시 참고

                Error_message = Backend_GameData.Instance.GetDatas();

                if (Error_message != "")                 // User의 데이터 서버에서 불러와 연동, 데이터를 불러오기 위해 실행시킨 함수 값들 중 문제가 있었다면
                {
                    SetMassage(Error_message);                                  // 해당 에러 문구 전달

                    Invoke(nameof(Return_Loadding), 0.2f);                      // 서버 이상 발생이기에 초기 화면으로 이동
                    return;
                }
                else
                {
                    Invoke("Login_", 0.3f);                                     // 0.2초 후 Login_ 함수 실행
                }
            }
            else                                                                // 로그인 실패 시
            {
                LoginBtn.interactable = true;                                   // 각 버튼 활성화
                JoinBtn.interactable = true;
                FindIDBtn.interactable = true;
                FindPWBtn.interactable = true;

                string message = string.Empty;                                  // 에러 확인을 위한 string 변수 선언

                switch (int.Parse(callback.GetStatusCode()))                    // 오류 코드를 정수로 변환해서 해당 에러 내용 message에 저장해 출력
                {
                    case 401:                                                   // 존재하지 않는 아이디, 또는 잘못된 비밀번호 확인
                        message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디입니다." : "잘못된 비밀번호 입니다.";
                        break;
                    case 403:                                                   // 유저 또는 기기 차단 확인
                        message = callback.GetMessage().Contains("user") ? "차단당한 유저입니다." : "차단당한 디바이스입니다.";
                        break;
                    case 410:                                                   // 탈퇴 진행중 확인
                        message = "탈퇴가 진행중인 유저입니다.";
                        break;
                    default:                                                    // 그외
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("비밀번호"))                               // 401 : 잘못된 비밀번호 일시
                {
                    GuideForCorrectlyEnteredData(imagePW, message);             // 해당 칸(InputField) 색상 변경으로 시각적 표현
                }
                else                                                            // 아이디가 잘못되었을 시
                {
                    GuideForCorrectlyEnteredData(imageID, message);
                }

                Invoke(nameof(Reset_Text), 0.7f);                               // 0.7초 후 Reset_Text 함수 실행
            }
        });
    }
    /// <summary>
    /// 서버에 callback 받아 올때까지 진행될 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoginProcess()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            SetMassage($"로그인 중입니다...{time:F1}");                        // 지연 시간 출력

            yield return null;
        }
    }
    /// <summary>
    /// 로그인 성공 시 실행될 함수
    /// </summary>
    private void Login_()                               
    {
        Quit_Game.Instance.Limit = false;                                       // Quit 스크립트 실행 제어를 위한 변수 값 변경
        Utils.Instance.LoadScene(SceneNames.Main);                              // Main 씬 활성화 작업 진행
    }
    /// <summary>
    /// 서버 오류로 인한 초기 화면 이동 함수
    /// </summary>
    private void Return_Loadding()
    {
        Utils.Instance.LoadScene(SceneNames.Loadding);                          // 초기화면 활성화 작업 진행
    }
}

// nameof : 맴버, 변수, 형식에 대한 문자열 반환 ex) nameof(List<list>) 결과 List 반환
// Parse : 형변환
// Contains : 문자열 포함확인
