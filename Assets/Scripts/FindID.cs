using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class FindID : Login_Base
{
    #region Variable
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private Text inputFieldEmail;

    [SerializeField]
    private Button Find_id_btn;
    [SerializeField]
    private Button Back;
    #endregion
    public void OnClickFindID()
    {
        ResetUI(imageEmail);                                                                                    // 내용 및 InputField 색상 초기화

        if (IsFieldEmpty(imageEmail, inputFieldEmail.text, "메일 주소"))                                        // 공백 체크
            return;

        if (!inputFieldEmail.text.Contains("@"))                                                                // 형식 체크
        {
            GuideForCorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.(ex. address@xxx.xxx)");
            return;
        }

        Find_id_btn.interactable = false;                                                                       // 버튼 비활성화
        Back.interactable = false;

        SetMassage("메일 발송 중...");

        FindCustomID();                                                                                         // 서버에서 ID 찾기 시도
    }
    private void FindCustomID()
    {
        SendQueue.Enqueue(Backend.BMember.FindCustomID, inputFieldEmail.text, callback =>                            // ID 정보 등록한 이메일로 발송
        {
            if (callback.IsSuccess())                                                                                // 메일 발송 성공시
            {
                SetMassage($"{inputFieldEmail.text} 주소로 메일을 발송하였습니다.");

                Quit_Game.Instance.Limit = true;
                Invoke("ActiveFalse", 0.5f);
            }
            else                                                                                                     // 발송 실패 시
            {
                Find_id_btn.interactable = true;                                                                     // 버튼 활성화
                Back.interactable = true;

                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404:                                                                                       // 해당 이메일의 사용자가 없는 경우
                        message = "해당 이메일을 사용하는 사용자가 없습니다.";
                        break;
                    case 429:                                                                                       // 24시간 이내 5회 이상 같은 이메일 정보로 아이디/비밀번호 찾기 시도한 경우
                        message = "24시간 이내 5회 이상 아이디/비밀번호 찾기를 시도하였습니다.";
                        break;
                    default:                                                                                        // statusCode : 400  프로젝트명에 특수문자가 추가된 경우(안내 메일 미발송 및 에러 발생)
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("이메일"))                                                                     //이메일 잘못 입력된 경우 이기에 경고 표출
                {
                    GuideForCorrectlyEnteredData(imageEmail, message);
                }
                else
                {
                    SetMassage(message);
                }

                Invoke(nameof(Reset_Text), 0.7f);
            }
        });
    }
    private void ActiveFalse()
    {
        Reset_Text();
        Quit_Game.Instance.Panel_Out();
        Quit_Game.Instance.Limit = false;
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        ResetUI(imageEmail);
    }
}
