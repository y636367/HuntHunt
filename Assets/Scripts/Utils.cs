using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneNames {Loadding=0,Login, Main,}                                // �Ϲ� ��
public enum StageNames { Stage_1, Stage_2, Stage_3, Stage_4, Stage_5, }         // ���� �÷��� ��
//SceneManager ��� ����
public class Utils : MonoBehaviour
{
    #region �̱���
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
    /// ���� �� �̸� ��ȯ
    /// </summary>
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;                              // ���� ���� ���� �� �̸� ��ȯ
    }
    /// <summary>
    /// ���� Load�� �� �̸� �� ���ڿ��� ����
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName = "")
    {
        if (sceneName == "")                                                    // ���ڿ��� �޾��� �� �Ű����� ���������
        {
            //StartCoroutine(AsyncSceneChange(GetActiveScene()));                 // �񵿱� ���� �� �ٽ� �ε�
            SceneManager.LoadScene(GetActiveScene());
        }
        else                                                                    // ������� �ʱ⿡ �ۼ��� 
        {
            //StartCoroutine(AsyncSceneChange(sceneName));                        // �񵿱� ���ڿ� �̸��� �� �ε�
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
    /// <summary>
    /// �������� �Ϲݾ� �ε� �ڷ�ƾ ����
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(SceneNames sceneName)
    {
        //StartCoroutine(AsyncSceneChange(sceneName.ToString()));
        SceneManager.LoadScene(sceneName.ToString());
    }
    /// <summary>
    /// �������� ���������� �ε� �ڷ�ƾ ����
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(StageNames sceneName)
    {
        //StartCoroutine(AsyncSceneChange(sceneName.ToString()));
        SceneManager.LoadScene(sceneName.ToString());
    }
    /// <summary>
    /// �񵿱� �� �ε� �� ���� �� ��Ȱ��ȭ
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    //IEnumerator AsyncSceneChange(string sceneName)
    //{
    //    soundManager.Instance.now_Set_possible = false;                                             // Slider(����� �ͼ� ����) ���� ���� �� ����

    //    string currentSceneName = GetActiveScene();                                                 // ���� �� �޾ƿ� ���ο� ������ �̸� ����

    //    AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);      // �񵿱� sceneName�� �� �ε�

    //    while (!async.isDone)                                                                       // �� �ε尡 �Ϸ� �ɶ����� return
    //    {
    //        yield return null;
    //    }
                                                                                                    
    //    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));                        // ���ο� �� �ε� �Ϸ�� �ε�� ���� Ȱ��ȭ
    //    SceneManager.UnloadSceneAsync(currentSceneName);                                            // ���� ��� �ǰ� �ִ� �� ��Ȱ��ȭ

    //    Quit_Game.Instance.QCR.Setting_Cammera();                                                   // Quit_Canvas�� world ī�޶� ����
    //}
    /// <summary>
    /// ������ ������ �Լ�
    /// </summary>
    public void Delay_Frame(int n = 1)                                                              // n �� �Է½� �ڵ� 1 �Է����� 1������ �ѱ涧���� ���
    {
        StartCoroutine(OneFrame(n));
    }
    /// <summary>
    /// 1 ������ ������
    /// </summary>
    /// <returns></returns>
    private IEnumerator OneFrame(int n)
    {
        for (int index = 0; index < n; index++)
        {
            yield return null;
        }
    }

    // �� �ε带 �񵿱�� �����ϴ� ���� �������� �Լ� ������ null error �߻�, Delay�� ���� �ϴ� ����ֱ�
}
