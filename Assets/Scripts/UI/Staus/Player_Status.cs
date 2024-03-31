using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StatusData", menuName = "Scriptable object/StatusData")]
public class Player_Status : ScriptableObject
{
    public enum StatusType                                          // 특성 구붓 짓기 위한 열거형
    {
        A_Power,A_speed,D_Power, P_Strength,M_Speed
    }

    [Header("# Info")]
    [SerializeField]
    public Sprite Image;                                            // 고유 이미지
    [SerializeField]
    public string Name;                                             // 고유 이름
    [SerializeField]
    public StatusType S_type;                                       // 고유 특성

    [SerializeField]
    public Item_Data data;                                          // 설정된 무기 스크립터블 오브젝트

    [SerializeField]
    public string Desc;                                             // 고유 설명

    [SerializeField]
    public float Upgrade_status;                                    // Upgrade 할시 올라가는 수치

    [SerializeField]
    public int Upgrade_Cost;                                        // Upgrade 비용
    [SerializeField]
    public int Plus_Cost;                                           // 다음 Upgrade에 추가될 비용
}
