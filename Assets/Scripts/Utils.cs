using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneNames {Loadding=0,Login, Main,}                                // 일반 씬
public enum StageNames { Stage_1, Stage_2, Stage_3, Stage_4, Stage_5, }         // 게임 플레이 씬
//SceneManager 기능 수행
public class Utils : MonoBehaviour
{
    #region 싱글톤
    public static Utils Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    /// <summary>
    /// 현재 씬 이름 반환
    /// </summary>
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;                              // 현재 실행 중인 씬 이름 반환
    }
    /// <summary>
    /// 다음 Load될 씬 이름 을 문자열로 받음
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName = "")
    {
        if (sceneName == "")                                                    // 문자열로 받았을 시 매개변수 비어있으면
        {
            //StartCoroutine(AsyncSceneChange(GetActiveScene()));                 // 비동기 현재 씬 다시 로드
            SceneManager.LoadScene(GetActiveScene());
        }
        else                                                                    // 비어있지 않기에 작성된 
        {
            //StartCoroutine(AsyncSceneChange(sceneName));                        // 비동기 문자열 이름의 씬 로드
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
    /// <summary>
    /// 열거형의 일반씬 로드 코루틴 실행
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(SceneNames sceneName)
    {
        //StartCoroutine(AsyncSceneChange(sceneName.ToString()));
        SceneManager.LoadScene(sceneName.ToString());
    }
    /// <summary>
    /// 열거형의 스테이지씬 로드 코루틴 실행
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(StageNames sceneName)
    {
        //StartCoroutine(AsyncSceneChange(sceneName.ToString()));
        SceneManager.LoadScene(sceneName.ToString());
    }
    /// <summary>
    /// 비동기 씬 로드 및 이전 씬 비활성화
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    //IEnumerator AsyncSceneChange(string sceneName)
    //{
    //    soundManager.Instance.now_Set_possible = false;                                             // Slider(오디오 믹서 조절) 제어 변수 값 변경

    //    string currentSceneName = GetActiveScene();                                                 // 현재 씬 받아와 새로운 변수에 이름 저장

    //    AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);      // 비동기 sceneName의 씬 로드

    //    while (!async.isDone)                                                                       // 씬 로드가 완료 될때까지 return
    //    {
    //        yield return null;
    //    }
                                                                                                    
    //    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));                        // 새로운 씬 로드 완료시 로드된 씬을 활성화
    //    SceneManager.UnloadSceneAsync(currentSceneName);                                            // 현재 재생 되고 있는 씬 비활성화

    //    Quit_Game.Instance.QCR.Setting_Cammera();                                                   // Quit_Canvas의 world 카메라 설정
    //}
    /// <summary>
    /// 프레임 딜레이 함수
    /// </summary>
    public void Delay_Frame(int n = 1)                                                              // n 미 입력시 자동 1 입력으로 1프레임 넘길때까지 대기
    {
        StartCoroutine(OneFrame(n));
    }
    /// <summary>
    /// 1 프레임 딜레이
    /// </summary>
    /// <returns></returns>
    private IEnumerator OneFrame(int n)
    {
        for (int index = 0; index < n; index++)
        {
            yield return null;
        }
    }

    // 씬 로드를 비동기로 진행하다 보니 구문구문 함수 진행중 null error 발생, Delay로 차이 일단 잡아주기
}
