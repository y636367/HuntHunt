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
    /// 씬 로드 후 호출될 함수
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.isLoaded)
            StartCoroutine(nameof(DataLoadCheck));                                      // 씬 로드 후 호출될 함수
    }
    /// <summary>
    /// 델리게이트 체인 해제
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;                                      // 메모리 누수 방지를 위해 체인 해제
    }
    /// <summary>
    /// 델리게이트 체인 추가
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;                                      //씬 로드 후 이벤트 핸들링 설정
    }
    IEnumerator DataLoadCheck()
    {
        while (!Backend_GameData.Instance.On_userdata                                   // 모든 데이터 로드가 완료가 되면
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
            Main_UIManager.Instance.Setting();                                          // 다음 실행 함수 호출
        }
            catch (MissingReferenceException) { }
    }
}
