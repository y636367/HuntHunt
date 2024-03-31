using BackEnd;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Password_Check : Login_Base
{
    [System.Serializable]
    public class PasswordCheckEvent : UnityEngine.Events.UnityEvent { }                             // 비밀번호 일치 확인시 호출할 메소드 등록
    public PasswordCheckEvent onCheckEvent=new PasswordCheckEvent();                                // 클래스 인스턴스 선언

    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private InputField inputFieldPW;                                                                // 비밀번호 항목으로 선택시 **** 문자가 텍스트 대신 삽입되기에 InputField의 Text 가져오기

    [SerializeField]
    private Button CheckBtn;

    /// <summary>
    /// 비밀번호 확인 함수
    /// </summary>
    public void OnClicekdPassword_Check()
    {
        ResetUI(imagePW);                                                                           // 내용 초기화

        if (IsFieldEmpty(imagePW, inputFieldPW.text, "PW"))                                         // 공백 체크
            return;

        CheckBtn.interactable = false;                                                              // 연타 못하게 비활성화

        StartCoroutine(nameof(P_CheckProcess));                                                     // 확인 요청 하는 동안 내용 업데이트
        Quit_Game.Instance.Limit = true;

        ResponseToPasswordCheck(inputFieldPW.text);
    }
    /// <summary>
    /// 서버에 확인 요청 함수
    /// </summary>
    /// <param name="PW"></param>
    private void ResponseToPasswordCheck(string PW)
    {
        SendQueue.Enqueue(Backend.BMember.ConfirmCustomPassword, PW, callback =>                    // 서버에 비밀번호 확인 시도
        {
            StopCoroutine(nameof(P_CheckProcess));                                                  // callback 올시 코루틴 중지

            if (callback.IsSuccess())                                                               // 비밀번호 일치 할시
            {
                SetMassage("비밀번호가 일치합니다.");

                Quit_Game.Instance.Limit = false;                                                   // Quit 함수 제어 변수 값 변경
                Quit_Game.Instance.Panel_Out();                                                     // 비밀번호 확인 패널 닫음과 동시에 List 제거 함수 실행

                onCheckEvent.Invoke();                                                              // 비밀번호 일치 시 onCheckEvent에 등록된 이벤트 메소드 호출
            }
            else                                                                                    // 비밀번호 불일치
            {
                CheckBtn.interactable = true;                                                       // 버튼 활성화

                string message = "잘못된 비밀번호 입니다.";

                GuideForCorrectlyEnteredData(imagePW, message);                                     // InputField 색상 변경 및 Text Message 전달

                Invoke(nameof(Reset_Text), 0.7f);
            }
        });
    }
    /// <summary>
    /// 확인하는 동안 업데이트 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator P_CheckProcess()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            SetMassage($"확인 중입니다...{time:F1}");                                              // 지연시간 출력

            yield return null;
        }
    }
    private void OnEnable()
    {
        Reset_Text();
        CheckBtn.interactable = true;
    }
}
