using UnityEngine;
using UnityEngine.UI;

public class Login_Base : MonoBehaviour
{
    [SerializeField]
    private Text text_inputFIeld;

    // params : 가변 인자 매개변수
    /// <summary>
    /// 메시지 내용 초기화, InputField 색상 초기화
    /// </summary>
    protected void ResetUI(params Image[] images)
    {
        text_inputFIeld.text = string.Empty;

        for(int i=0; i<images.Length; i++)
        {
            images[i].color = Color.white;
        }
    }
    /// <summary>
    ///  매개변수에 있는 내용 출력
    /// </summary>
    protected void SetMassage(string msg)
    {
        text_inputFIeld.text = msg;
    }
    /// <summary>
    /// InPutField 입력 오류 발생 시, 색상 변경 및 오류 메시지 출력으로 경고
    /// </summary>
    protected void GuideForCorrectlyEnteredData(Image image, string msg)
    {
        text_inputFIeld.text = msg;
        image.color = Color.red;
    }
    /// <summary>
    /// 입력 필드 공백 시, 해당하는 필드 색상 변경 및 경고 메시지 출력
    /// (image : 필드, field : 내용, result : 출력될 내용)
    /// </summary>
    /// <returns></returns>
    protected bool IsFieldEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals("")) // Trim: 문자열 앞, 뒤 공백 제거
        {
            GuideForCorrectlyEnteredData(image, $"\"{result}\"가 입력 되지 않았습니다.");

            return true;
        }

        return false;
    }
    /// <summary>
    /// 메시지 내용 초기화
    /// </summary>
    protected void Reset_Text()
    {
        text_inputFIeld.text = string.Empty;
    }
}
