[System.Serializable]
public class User_Datas
{
    /// <summary>
    /// Player_Data
    /// </summary>
    public int Level;
    public int Coin;
    public int Life;
    public float NowExp;
    public WeaponData_Controller Now_Weapon;
    public int WeaponNumber;

    /// <summary>
    /// EXP
    /// </summary>
    public float Next_Exp_value;
    public float Next_Exp_UP;
    public float Exp_Multiplication;
    public float t_exp;

    /// <summary>
    /// UserData 값들 초기화
    /// </summary>
    public void ResetData()
    {
        Level = 1;
        Coin = 500;
        Life = 5;
        NowExp = 0;

        Next_Exp_UP = 2.5f;
        Next_Exp_value = 5f;
        Exp_Multiplication = 0.1f;

        WeaponNumber = 1;                                           // Pistol
    }
    /// <summary>
    /// Player의 EXP에 따른 LevelUP 함수
    /// </summary>
    public void Next_Level()
    {
        NowExp -= Next_Exp_value;
        Level += 1;

        t_exp = Next_Exp_UP * Exp_Multiplication;
        Exp_Multiplication += Exp_Multiplication;
        Next_Exp_value += (Next_Exp_UP + t_exp);
    }
}
public class Clear_datas
{
    /// <summary>
    /// Progress
    /// </summary>
    public bool S1_Difficult_1;
    public bool S1_Difficult_2;
    public bool S1_Difficult_3;

    public bool S2_Difficult_1;
    public bool S2_Difficult_2;
    public bool S2_Difficult_3;

    public bool S3_Difficult_1;
    public bool S3_Difficult_2;
    public bool S3_Difficult_3;

    public bool S4_Difficult_1;
    public bool S4_Difficult_2;
    public bool S4_Difficult_3;

    public bool S5_Difficult_1;
    public bool S5_Difficult_2;
    public bool S5_Difficult_3;

    public int High_Stage;
    /// <summary>
    /// 클리어 정보 초기화
    /// </summary>
    public void ResetProgress()
    {
        High_Stage = 0;

        S1_Difficult_1 = false;
        S1_Difficult_2 = false;
        S1_Difficult_3 = false;

        S2_Difficult_1 = false;
        S2_Difficult_2 = false;
        S2_Difficult_3 = false;

        S3_Difficult_1 = false;
        S3_Difficult_2 = false;
        S3_Difficult_3 = false;

        S4_Difficult_1 = false;
        S4_Difficult_2 = false;
        S4_Difficult_3 = false;

        S5_Difficult_1 = false;
        S5_Difficult_2 = false;
        S5_Difficult_3 = false;
    }
}
public class User_statusLevel
{
    /// <summary>
    /// Upgrade_Status Level
    /// </summary>
    public int PH;
    public int AP;
    public int DP;
    public int MS;
    public int AS;
    public void ResetStatusLevel()
    {
        PH = 1;
        AP = 1;
        DP = 1;
        AS = 1;
        MS = 1;
    }
}
public class User_status
{
    /// <summary>
    /// Status_
    /// </summary>
    public float Max_HP;
    public float Attack_Power;
    public float Deffensive_Power;
    public float MoveSpeed;
    public float AttackSpeed;

    /// <summary>
    /// User의 Status 초기화
    /// </summary>
    public void ResetStatus()
    {
        Max_HP = 100;
        Attack_Power = 1;
        Deffensive_Power = 0.1f;
        MoveSpeed = 3;
        AttackSpeed = 0.1f;
    }
}
public class User_WeaponLevel
{
    /// <summary>
    /// Weapon_Status
    /// </summary>
    public int Pistol;
    public int Mine;
    public int Shotgun;
    public int Sniper;
    public int Flare_gun;
    public int GasShield;
    public int FlareThrower;
    public int Knife;
    public int RocketLauncer;
    public int Rampage;

    /// <summary>
    /// User가 보유한 무기의 Level 값 초기화
    /// </summary>
    public void ResetWeapons()
    {
        Pistol = 1;                                         // Pistol 빼고 나머지 0으로 초기화 할 필요 있음
        Mine = 0;
        Shotgun = 0;
        Sniper = 0;
        Flare_gun = 0;
        GasShield = 0;
        FlareThrower = 0;
        Knife = 0;
        RocketLauncer = 0;
        Rampage = 0;
    }
}
public class Life_Date
{
    public System.DateTime LifeDate;                // 새로운 Life가 생성된 시점의 시간
    public System.DateTime UTCDate;                 // 세계 시간 기준 시간
}
