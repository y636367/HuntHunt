using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Tile_Manager : MonoBehaviour
{
    NavMeshSurface nav_s;

    [SerializeField]
    private List<Ground_Tile> Tiles = new List<Ground_Tile>();

    [SerializeField]
    public GameObject Center;
    public GameObject Preview_Center;

    public int direction_number = 0;
    public int Change_count = 0;

    public float Max_Distance = 0f;
    public float Middle_Distance;

    public bool Change = false;

    /// <summary>
    /// Stage4, Stage5�� ��� ���� �� ������ ���� Tile�̵� �Ŵ���
    /// </summary>
    private void Awake()
    {
        Tiles = new List<Ground_Tile>(GetComponentsInChildren<Ground_Tile>(true));
        nav_s = GetComponent<NavMeshSurface>();

        Middle_Distance = Vector3.Distance(Tiles[0].transform.position, Tiles[1].transform.position);

        for (int i = 0; i < Tiles.Count; i++)                                                                                               // CenterŸ���� ���������ؼ� ���� �հŸ����ִ� Tile���� �ִ� �Ÿ� ����
        {
            if (Max_Distance < Vector3.Distance(Tiles[i].transform.position, Center.transform.position))
                Max_Distance = Vector3.Distance(Tiles[i].transform.position, Center.transform.position);
        }

        Preview_Center = Center;                                                                                                            // ���� Ÿ�ϰ� ���� Ÿ�� ��ġȭ
    }
    public void Tile_Surface_Resetting()
    {
        if (PD_Control.Instance.StageManager_.Stage_num == 3)
        {
            if (Change_count >= 3)
            {
                Change_count = 0;
                ReSet_Nav();
                Change = false;
            }
        }
        else if (PD_Control.Instance.StageManager_.Stage_num == 4)
        {
            if (Change_count >= 1)
            {
                Change_count = 0;
                ReSet_Nav();
                Change = false;
            }
        }
    }
    /// <summary>
    /// Tile �̵� �� NavSurface�� �Բ� �̵����� �ʱ⿡ ���� �� �ٽ� �����ϴ� �Լ�
    /// </summary>
    private void ReSet_Nav()
    {  
        nav_s.RemoveData();                                                                                                 // ������ Bake �� NavSurface ����
        nav_s.BuildNavMesh();                                                                                               // ���Ӱ� NavSurface ����

        foreach(Ground_Tile tile in Tiles)
        {
            if (tile.inMonster)            
                tile.Turn_On_agent();                                                                                       // Tile ���� ��� ���͵��� NavAgent On
        }
    }
    /// <summary>
    /// ���� Player�� ��� �ִ� Ÿ��, ���� Player�� ��� �ִ� Ÿ�Ϸ� Player�� �̵��� ���� ����
    /// </summary>
    public void ReSetting_Tile()
    {
        if (Preview_Center.transform.position.x > Center.transform.position.x)                                                      // �÷��̾ �������� �̵�
        {
            direction_number = 1;
        }
        else if (Preview_Center.transform.position.x < Center.transform.position.x)                                                 // �÷��̾ ���������� �̵�
        {
            direction_number = 2;
        }
        else if (Preview_Center.transform.position.z > Center.transform.position.z)                                                 // �÷��̾ �Ʒ����� �̵�
        {
            direction_number = 3;
        }
        else if (Preview_Center.transform.position.z < Center.transform.position.z)                                                 // �÷��̾ �������� �̵�
        {
            direction_number = 4;
        }

        Change = true;                                                                                                              // �̵� ����
    }
}
