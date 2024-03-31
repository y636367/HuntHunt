using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOut_Login : MonoBehaviour
{
    /// <summary>
    ///  Panel 닫을 시 Quit List에서 해당 오브젝트 뺴기
    /// </summary>
    public void Panel_List_Out_()
    {
        Quit_Game.Instance.Panel_Out();
    }
}
