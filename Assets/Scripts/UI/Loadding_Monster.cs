using UnityEngine;
using UnityEngine.AI;

public class Loadding_Monster : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Monsters;                                                  // 로딩 화면에 표출된 몬스터

    private void Awake()
    {
        Monster_Init(Random_Monster());                                             // Monsters 몬스터들 중 랜덤하게 하나 설정
    }
    /// <summary>
    /// 랜덤 몬스터 선정
    /// </summary>
    /// <returns></returns>
    private int Random_Monster()
    {
        int num = Random.Range(0, Monsters.Length);

        return num;
    }
    /// <summary>
    /// 선언된 몬스터의 인스턴스 소환 및 해당 몬스터의 클론 지정으로 혹여 생기게 될 오류들 방지
    /// </summary>
    /// <param name="monster_num"></param>
    private void Monster_Init(int monster_num)
    {
        GameObject Loadding_Monster = Instantiate(Monsters[monster_num]);
        Loadding_Monster.GetComponent<Monster>().Clone = true;
        Loadding_Monster.GetComponent<Monster>().enabled = false;
        Loadding_Monster.GetComponent<NavMeshAgent>().enabled = false;
        Loadding_Monster.GetComponent<Rigidbody>().useGravity = false;

        Loadding_Monster.transform.parent = transform;
        Loadding_Monster.transform.position= Vector3.zero;
        Loadding_Monster.transform.rotation = Quaternion.Euler(0, 140, 0);
    }
}
