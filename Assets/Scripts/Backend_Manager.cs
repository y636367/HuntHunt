using UnityEngine;
using UnityEngine.UI;
using BackEnd; // 뒤끝 SDK
// 뒤끝 모든 기능 BackEnd 클래스 내부에 싱글톤 인스턴스로 선언되어있음

public class Backend_Manager : MonoBehaviour
{
    [SerializeField]
    private InputField Hashkey;
    private void Awake()
    {
        // Update 메소드의 Backend.AsyncPoll(); 호출을 위해 파괴 x
        DontDestroyOnLoad(gameObject);

        BackendSetUP(); // 서버 초기화
    }
    private void Update()
    {
        // 서버의 비동기 메소드 호출(콜백 함수 풀링)을 위해 작성
        if (Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }
    private void BackendSetUP()
    {
        // 뒤끝 초기화
        var init = Backend.Initialize(true);

        // 초기화에 대한 응답값
        if (init.IsSuccess())
        {
            // 성공 시 statusCode 204 Success
            Debug.Log($"초기화 성공 : {init}");
            GetGoogleHash();
        }
        else
            Debug.LogError($"초기화 실패 : {init}");
    }
    // 오직 안드로이드에서만 필요한 작업
    public void GetGoogleHash()
    {
        // 구글 해시키 획득
        string googleHashKey=Backend.Utils.GetGoogleHash();
        // 존재할 경우
        if(!string.IsNullOrEmpty(googleHashKey) )
        {
            Debug.Log(googleHashKey);
            Hashkey.text = googleHashKey;
        }
    }
}
