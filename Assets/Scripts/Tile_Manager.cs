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
    /// Stage4, Stage5의 경우 무한 맵 구현을 위한 Tile이동 매니저
    /// </summary>
    private void Awake()
    {
        Tiles = new List<Ground_Tile>(GetComponentsInChildren<Ground_Tile>(true));
        nav_s = GetComponent<NavMeshSurface>();

        Middle_Distance = Vector3.Distance(Tiles[0].transform.position, Tiles[1].transform.position);

        for (int i = 0; i < Tiles.Count; i++)                                                                                               // Center타일을 기준으로해서 가장 먼거리에있는 Tile과의 최대 거리 결정
        {
            if (Max_Distance < Vector3.Distance(Tiles[i].transform.position, Center.transform.position))
                Max_Distance = Vector3.Distance(Tiles[i].transform.position, Center.transform.position);
        }

        Preview_Center = Center;                                                                                                            // 현재 타일과 이전 타일 일치화
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
    /// Tile 이동 시 NavSurface는 함께 이동하지 않기에 제거 후 다시 생성하는 함수
    /// </summary>
    private void ReSet_Nav()
    {  
        nav_s.RemoveData();                                                                                                 // 기존에 Bake 된 NavSurface 제거
        nav_s.BuildNavMesh();                                                                                               // 새롭게 NavSurface 생성

        foreach(Ground_Tile tile in Tiles)
        {
            if (tile.inMonster)            
                tile.Turn_On_agent();                                                                                       // Tile 위의 모든 몬스터들의 NavAgent On
        }
    }
    /// <summary>
    /// 이전 Player가 밟고 있는 타일, 현재 Player가 밟고 있는 타일로 Player가 이동한 방향 결정
    /// </summary>
    public void ReSetting_Tile()
    {
        if (Preview_Center.transform.position.x > Center.transform.position.x)                                                      // 플레이어가 왼쪽으로 이동
        {
            direction_number = 1;
        }
        else if (Preview_Center.transform.position.x < Center.transform.position.x)                                                 // 플레이어가 오른쪽으로 이동
        {
            direction_number = 2;
        }
        else if (Preview_Center.transform.position.z > Center.transform.position.z)                                                 // 플레이어가 아래쪽을 이동
        {
            direction_number = 3;
        }
        else if (Preview_Center.transform.position.z < Center.transform.position.z)                                                 // 플레이어가 위쪽으로 이동
        {
            direction_number = 4;
        }

        Change = true;                                                                                                              // 이동 가능
    }
}
