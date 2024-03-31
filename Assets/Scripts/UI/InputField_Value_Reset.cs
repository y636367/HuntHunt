using UnityEngine;
using UnityEngine.UI;

public class InputField_Value_Reset : MonoBehaviour
{
    [SerializeField]
    private InputField[] fields;                                            // ���� �ʱ�ȭ �ϰ��� InputField ������

    /// <summary>
    /// InputField ������ ���� �ʱ�ȭ
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
