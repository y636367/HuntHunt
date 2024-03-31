using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable object/WeaponData")]
public class WeaponData : ScriptableObject
{
    public enum WeponType                                    // ���� Ư�� ���� ���� ������
    {
        Pistol,Shotgun,Sniper,Submachin,Mine,Rocket,GasShiled,Flare,FireThrower,Knife
    }

    [Header(" # Data")]
    [SerializeField]
    public Sprite image;                                     // ���� �̹���                 
    [SerializeField]
    public string Name;                                      // ���� �̸�
    [SerializeField]
    public WeponType W_type;                                 // ���� ���� Ÿ��

    [SerializeField]
    public Item_Data data;                                   // ������ ���� ��ũ���ͺ� ������Ʈ

    [SerializeField]
    public string Desc;                                      // ���� ����

    [SerializeField]
    public int Upgrade_Cost;                                 // ��ȭ ���
    [SerializeField]
    public int Plus_Cost;                                    // ���� ��ȭ �� �߰��� ���

    [SerializeField]
    public int Numbering;                                    // ��ũ���ͺ� ������Ʈ�� ��ϵ� ���� ��ȣ
}
