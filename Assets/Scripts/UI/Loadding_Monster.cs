using UnityEngine;
using UnityEngine.AI;

public class Loadding_Monster : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Monsters;                                                  // �ε� ȭ�鿡 ǥ��� ����

    private void Awake()
    {
        Monster_Init(Random_Monster());                                             // Monsters ���͵� �� �����ϰ� �ϳ� ����
    }
    /// <summary>
    /// ���� ���� ����
    /// </summary>
    /// <returns></returns>
    private int Random_Monster()
    {
        int num = Random.Range(0, Monsters.Length);

        return num;
    }
    /// <summary>
    /// ����� ������ �ν��Ͻ� ��ȯ �� �ش� ������ Ŭ�� �������� Ȥ�� ����� �� ������ ����
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
