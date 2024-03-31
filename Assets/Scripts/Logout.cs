using BackEnd;

public class Logout : Login_Base
{
    /// <summary>
    /// 로그아웃 진행 함수
    /// </summary>
    public void OnClickedLogout()
    {
        SendQueue.Enqueue(Backend.BMember.Logout, callback =>                               // 서버에 로그아웃 요청(로그아웃 시 Date 남음)
        {
            if (callback.IsSuccess())                                                       // 로그아웃 성공
            {
                SetMassage($"처음 화면으로 이동합니다.");

                Invoke("Back_Loadding", 0.5f);
            }
            else                                                                            // 실패 시 오류 메시지 전달
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
