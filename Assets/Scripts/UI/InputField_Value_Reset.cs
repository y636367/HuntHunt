using UnityEngine;
using UnityEngine.UI;

public class InputField_Value_Reset : MonoBehaviour
{
    [SerializeField]
    private InputField[] fields;                                            // 내용 초기화 하고픈 InputField 변수들

    /// <summary>
    /// InputField 변수들 내용 초기화
    /// </summary>
    private void Reset()
    {
        for(int index=0; index<fields.Length; index++)
        {
            fields[index].text = string.Empty;
        }
    }
    private void OnEnable()
    {
        Reset();
    }
}
