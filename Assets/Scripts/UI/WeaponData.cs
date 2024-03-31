using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable object/WeaponData")]
public class WeaponData : ScriptableObject
{
    public enum WeponType                                    // 무기 특정 짓기 위한 열거형
    {
        Pistol,Shotgun,Sniper,Submachin,Mine,Rocket,GasShiled,Flare,FireThrower,Knife
    }

    [Header(" # Data")]
    [SerializeField]
    public Sprite image;                                     // 고유 이미지                 
    [SerializeField]
    public string Name;                                      // 고유 이름
    [SerializeField]
    public WeponType W_type;                                 // 고유 무기 타입

    [SerializeField]
    public Item_Data data;                                   // 설정된 무기 스크립터블 오브젝트

    [SerializeField]
    public string Desc;                                      // 고유 설명

    [SerializeField]
    public int Upgrade_Cost;                                 // 강화 비용
    [SerializeField]
    public int Plus_Cost;                                    // 다음 강화 때 추가될 비용

    [SerializeField]
    public int Numbering;                                    // 스크립터블 오브젝트에 등록된 고유 번호
}
