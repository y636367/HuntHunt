using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// �� �ε� �� ȣ��� �Լ�
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.isLoaded)
            StartCoroutine(nameof(DataLoadCheck));                                      // �� �ε� �� ȣ��� �Լ�
    }
    /// <summary>
    /// ��������Ʈ ü�� ����
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;                                      // �޸� ���� ������ ���� ü�� ����
    }
    /// <summary>
    /// ��������Ʈ ü�� �߰�
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;                                      //�� �ε� �� �̺�Ʈ �ڵ鸵 ����
    }
    IEnumerator DataLoadCheck()
    {
        while (!Backend_GameData.Instance.On_userdata                                   // ��� ������ �ε尡 �Ϸᰡ �Ǹ�
            || !Backend_GameData.Instance.On_statusdata
            || !Backend_GameData.Instance.On_statusLvdata
            || !Backend_GameData.Instance.On_weapondata
            || !Backend_GameData.Instance.On_cleardata
            || !Backend_GameData.Instance.On_lifedate)
        {
            yield return null;
        }
        try
        {
            Main_UIManager.Instance.Setting();                                          // ���� ���� �Լ� ȣ��
        }
            catch (MissingReferenceException) { }
    }
}
