using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp, Level, Kill, Time, Health, Coin, AliveTime, Stage, Difficult, Score, currentCoin
    , Player_Attack, Player_Deffensive, Player_Attack_Speed, Player_MoveSpeed, Player_Health
    }
    public InfoType type;

    Text my_Text;
    Slider my_Slider;

    public bool Awake_HUD;
    private void Awake()
    {
        my_Text = GetComponent<Text>();
        my_Slider = GetComponent<Slider>();

        if (Awake_HUD)
            Update_HUD();
    }
    public void Update_HUD()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Instance.current_Exp;
                float maxExp = GameManager.Instance.Require_Exp;
                my_Slider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                my_Text.text = string.Format("Lv.\n{0:F0}",GameManager.Instance.current_Level);                         // 값을 문자열로 포맷, 왼쪽에서부터 차례로 들어갈 인덱스(여기선 level값), F0  : 소수점 안씀
                break;
            case InfoType.currentCoin:
                my_Text.text = string.Format("{0:F0}", GameManager.Instance.current_Coin);
                break;
            case InfoType.Kill:
                my_Text.text = string.Format("{0:F0}", GameManager.Instance.current_Kill);
                break;
            case InfoType.Time:
                if (!GameManager.Instance.Infinity_Check)
                {
                    float remainTime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
                    int min = Mathf.FloorToInt(remainTime / 60);                                                        // FloorToInt 소수점 버림
                    int sec = Mathf.FloorToInt(remainTime % 60);
                    my_Text.text = string.Format("{0:D2}:{1:D2}", min, sec);                                            // D2 : 2 자리로 고정
                }
                else
                {
                    int min = GameManager.Instance.Minute;
                    int sec = 60 - (int)GameManager.Instance.Minute_Check;
                    my_Text.text = string.Format("{0:D2}:{1:D2}", min, sec);                                            // D2 : 2 자리로 고정
                }
                break;
            case InfoType.Health:
                float curHealth = GameManager.Instance.player.current_hp;
                float maxHealth = GameManager.Instance.player.Physical_strength;
                my_Slider.value = curHealth / maxHealth;
                break;
            case InfoType.AliveTime:
                int a_min = (int)(GameManager.Instance.GameTime / 60);
                int a_sec = (int)(GameManager.Instance.GameTime % 60);
                my_Text.text = string.Format("Alvie Time {0:D2}:{1:D2}", a_min, a_sec);                                 // D2 : 2 자리로 고정
                break;
            case InfoType.Coin:
                my_Text.text = string.Format("Get Coin {0:F0}", GameManager.Instance.current_Coin).PadLeft(6);
                break;
            #region 처음 게임 시작 한번만
            case InfoType.Stage:
                if (PD_Control.Instance.StageManager_.Difficult != 3)
                    my_Text.text = string.Format("Stage {0:F0}", PD_Control.Instance.StageManager_.Stage_num + 1);
                else
                    my_Text.text = string.Format("Stage ∞");
                break;
            case InfoType.Difficult:
                if (PD_Control.Instance.StageManager_.Difficult != 3)
                    my_Text.text = string.Format("Difficult {0:F0}", PD_Control.Instance.StageManager_.Difficult + 1);
                else
                    my_Text.text = string.Format("Difficult ∞");
                break;
            #endregion
            case InfoType.Score:
                my_Text.text = string.Format("Get Exp {0:F3}", GameManager.Instance.Get_Exp_Player);
                break;
            #region Status 창 킬때만 호출
            case InfoType.Player_Attack:
                my_Text.text = string.Format("{0:F2}", GameManager.Instance.player.Attack_power);
                break;
            case InfoType.Player_Deffensive:
                my_Text.text = string.Format("{0:F2}", GameManager.Instance.player.Defensive_power);
                break;
            case InfoType.Player_Health:
                my_Text.text = string.Format("{0:F2}", GameManager.Instance.player.Physical_strength);
                break;
            case InfoType.Player_MoveSpeed:
                my_Text.text = string.Format("{0:F2}", GameManager.Instance.player.Move_speed);
                break;
            case InfoType.Player_Attack_Speed:
                my_Text.text = string.Format("{0:F2}", GameManager.Instance.player.Attack_speed);
                break;
                #endregion
        }
    }
}
