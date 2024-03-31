using UnityEngine;

public class Get_Wall : MonoBehaviour
{
    #region WallPoints
    [SerializeField]
    public GameObject[] Walls_1;
    [SerializeField]
    public GameObject[] Walls_2;
    [SerializeField]
    public GameObject[] Walls_3;
    [SerializeField]
    public GameObject[] Walls_4;
    #endregion
    int Wall_num;
    int preivew_Wall_num = -1;

    /// <summary>
    /// Turtle �� ��ų���� Point ���� �� ���� point ��� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject Next_Point()
    {
        while (true)
        {
            Wall_num = Random.Range(0, 3);                                          // �� �� �� �� �� �ϳ� Random

            if (preivew_Wall_num != Wall_num)                                       // ���� Point �� ���� ������ �ƴ϶��
            {
                preivew_Wall_num = Wall_num;                                        // ���� ����
                break;
            }            
        }
        int point_num = Random.Range(0, Walls_1.Length);                            // ���� �� �ϳ� Random

        GameObject Return_obj = null;

        switch(Wall_num)
        {
            case 0:
                Return_obj = Walls_1[point_num];
                break;
            case 1:
                Return_obj = Walls_2[point_num];
                break;
            case 2:
                Return_obj = Walls_3[point_num];
                break;
            case 3:
                Return_obj = Walls_4[point_num];
                break;
        }

        return Return_obj;
    }
}
