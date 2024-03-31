using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StatusData", menuName = "Scriptable object/StatusData")]
public class Player_Status : ScriptableObject
{
    public enum StatusType                                          // Ư�� ���� ���� ���� ������
    {
        A_Power,A_speed,D_Power, P_Strength,M_Speed
    }

    [Header("# Info")]
    [SerializeField]
    public Sprite Image;                                            // ���� �̹���
    [SerializeField]
    public string Name;                                             // ���� �̸�
    [SerializeField]
    public StatusType S_type;                                       // ���� Ư��

    [SerializeField]
    public Item_Data data;                                          // ������ ���� ��ũ���ͺ� ������Ʈ

    [SerializeField]
    public string Desc;                                             // ���� ����

    [SerializeField]
    public float Upgrade_status;                                    // Upgrade �ҽ� �ö󰡴� ��ġ

    [SerializeField]
    public int Upgrade_Cost;                                        // Upgrade ���
    [SerializeField]
    public int Plus_Cost;                                           // ���� Upgrade�� �߰��� ���
}
