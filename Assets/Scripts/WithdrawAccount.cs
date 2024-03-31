using BackEnd;

public class WithdrawAccount : Login_Base
{
    /// <summary>
    /// 탈퇴 진행 함수
    /// </summary>
    public void OnClickedWithdrawAccount()
    {
        SendQueue.Enqueue(Backend.BMember.WithdrawAccount, callback =>                                      //서버에 탈퇴 요청(즉시 탈퇴)
        {
            if (callback.IsSuccess())                                                                       // 탈퇴 성공
            {
                SetMassage($"처음 화면으로 이동합니다.");

                Invoke("Back_Loadding", 0.5f);
            }
            else                                                                                            // 실패 시 메시지 전달
            {
                callback.GetMessage();
            }
        });
    }
    /// <summary>
    /// 처음 화면으로 이동
    /// </summary>
    private void Back_Loadding()
    {
        soundManager.Instance.now_Set_possible = false;                                             // Slider(오디오 믹서 조절) 제어 변수 값 변경
        Utils.Instance.LoadScene(SceneNames.Loadding);
    }
}
