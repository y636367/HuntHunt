using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class RefreshBtn : MonoBehaviour
{
    [System.Serializable]
    public class Hud : UnityEvent { };                                              // 이벤트 등록을 위한 인스턴스 클래스 생성
    public Hud hud = new Hud();

    [SerializeField]
    private Button Refresh_Btn;

    /// <summary>
    /// Data 갱신
    /// </summary>
    public void Refresh_data()
    {
        string message = string.Empty;

        btn_Off();

        message = Backend_GameData.Instance.GetDatas();

        if (message != "")
        {     
            StopCoroutine(nameof(Button_Hold));
            btn_On();
        }
        Main_UIManager.Instance.Update_Info();
        PD_Control.Instance.ResetDatas();
        hud?.Invoke();
    }
    /// <summary>
    /// Data 갱신 버튼 활성화
    /// </summary>
    public void btn_On()
    {
        Refresh_Btn.interactable = true;
        Refresh_Btn.image.color = new Color(Refresh_Btn.image.color.r, Refresh_Btn.image.color.g, Refresh_Btn.image.color.b, 1);
    }
    /// <summary>
    /// Data 갱신 버튼 비활성화
    /// </summary>
    private void btn_Off()
    {
        Refresh_Btn.interactable = false;
        Refresh_Btn.image.color = new Color(Refresh_Btn.image.color.r, Refresh_Btn.image.color.g, Refresh_Btn.image.color.b, 0);

        StartCoroutine(nameof(Button_Hold));
    }
    /// <summary>
    /// 버튼 재활성화 까지 제한
    /// </summary>
    /// <returns></returns>
    IEnumerator Button_Hold()
    {
        float time = 0;

        while (time < 5 * 60f)
        {
            time += Time.deltaTime;

            yield return null;
        }

        btn_On();
    }
}
