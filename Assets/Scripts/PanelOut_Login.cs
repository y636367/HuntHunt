using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOut_Login : MonoBehaviour
{
    /// <summary>
    ///  Panel ���� �� Quit List���� �ش� ������Ʈ ����
    /// </summary>
    public void Panel_List_Out_()
    {
        Quit_Game.Instance.Panel_Out();
    }
}
