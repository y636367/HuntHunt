using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class RegisterAccount : Login_Base
{
    #region Variable
    [Header("ID")]
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private Text inputFieldID;

    [Header("PW")]
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private InputField inputFieldPW;

    [Header("PW_Confirm")]
    [SerializeField]
    private Image imagePW_C;
    [SerializeField]
    private InputField inputFieldPW_C;

    [Header("E-Mail")]
    [SerializeField]
    private Image imageMail;
    [SerializeField]
    private Text inputFieldMail;

    [SerializeField]
    private Button RegisterBtn;
    [SerializeField]
    private Button Back;
    #endregion

    /// <summary>
    /// 계정 생성 이벤트 발생 시
    /// </summary>
    public void OnClickedAccount()
    {
        ResetUI(imageID, imagePW, imagePW_C, imageMail);                                // 각 InputField 내용 초기화

        #region 각 항목 공백 체크
        if (IsFieldEmpty(imageID, inputFieldID.text, "ID"))
            return;

        if (IsFieldEmpty(imagePW, inputFieldPW.text, "PW"))
            return;

        if (IsFieldEmpty(imagePW_C, inputFieldPW_C.text, "PW 확인"))
            return;

        if (IsFieldEmpty(imageMail, inputFieldMail.text, "E-Mail"))
            return;
        #endregion

        if (!inputFieldPW.text.Equals(inputFieldPW_C.text))                             // 비밀번호와 비밀번호 확인이 다를 시
        {
            GuideForCorrectlyEnteredData(imagePW_C, "비밀번호가 일치하지 않습니다.");
            return;
        }
        
        if (!inputFieldMail.text.Contains("@"))                                        // 메일 형식 검사
        {
            GuideForCorrectlyEnteredData(imageMail, "메일 형식이 잘못 되었습니다. (ex. address@xxxx.xxx)");
            return;
        }

        RegisterBtn.interactable = false;                                              // 연타 못하게 비활성화
        Back.interactable = false;

        SetMassage("계정 생성중입니다.");

        CustomSIgnUp();                                                                // 서버에 계정 생성 시도
    }
    /// <summary>
    /// 계정 생성 시도후 서버로부터 받은 message 기반으로 처리
    /// </summary>
    private void CustomSIgnUp()
    {
        SendQueue.Enqueue(Backend.BMember.CustomSignUp,inputFieldID.text, inputFieldPW.text, callback =>
        {
            if (callback.IsSuccess())                                                                                // 생성 성공 시
            {
                SendQueue.Enqueue(Backend.BMember.UpdateCustomEmail, inputFieldMail.text, callback =>                // E-Mail 정보 업데이트 시도
                {
                    if (callback.IsSuccess())                                                                        // E-Mail 정보 업데이트 성공 시
                    {
                        SetMassage($"계정 생성 완료. {inputFieldID.text}님 환영합니다.");                            // 성공 문구 출력

                        Quit_Game.Instance.Limit = true;                                                             // Quit 함수 제어를 위한 변수 값 변경
                        Backend_GameData.Instance.GameDataInsert();                                                  // 계정에 연동 되는 각 테이블 값 설정

                        Invoke("ActiveFalse", 0.5f);                                                                 // 0.5초 후 ActiveFalse 함수 실행
                    }
                });
            }
            else                                                                                                    // 생성 실패 시
            {
                RegisterBtn.interactable = true;                                                                    // 계정 생성 버튼 활성화
                Back.interactable = true;

                string message = string.Empty;

                switch(int.Parse(callback.GetStatusCode()))                                                         // 오류코드 정수로 변환, message에 내용 저장 후 출력
                {
                    case 409:
                        message = "이미 존재하는 아이디 입니다.";
                        break;
                    case 403:                                                                                       // 차단 디바이스
                    case 401:                                                                                       // 프로젝트 점검 상태
                    case 400:                                                                                       // 디바이스 정보가 null 인 경우
                    default:                                                                                        // 그 외
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("아이디"))                                                                    // 중복 아이디에 대한 경고
                {
                    GuideForCorrectlyEnteredData(imageID, message);
                }
                else                                                                                                // 이하 경고 메시지 출력
                {
                    SetMassage(message);
                }

                Invoke(nameof(Reset_Text), 0.7f);
            }
        });
    }
    /// <summary>
    /// 회원 가입 패널 초기화 및 닫기
    /// </summary>
    private void ActiveFalse()
    {
        Reset_Text();                                                                                              // InputField 초기화
        Quit_Game.Instance.Panel_Out();                                                                            // 회원 가입 패널 List에서 빼기
        Quit_Game.Instance.Limit = false;
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        ResetUI(imageID, imagePW, imagePW_C, imageMail);                                                            // 활성화 될때 마다 각 InputField 초기화
    }
}
