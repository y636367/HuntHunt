using UnityEngine;
using UnityEngine.UI;
using BackEnd; // �ڳ� SDK
// �ڳ� ��� ��� BackEnd Ŭ���� ���ο� �̱��� �ν��Ͻ��� ����Ǿ�����

public class Backend_Manager : MonoBehaviour
{
    [SerializeField]
    private InputField Hashkey;
    private void Awake()
    {
        // Update �޼ҵ��� Backend.AsyncPoll(); ȣ���� ���� �ı� x
        DontDestroyOnLoad(gameObject);

        BackendSetUP(); // ���� �ʱ�ȭ
    }
    private void Update()
    {
        // ������ �񵿱� �޼ҵ� ȣ��(�ݹ� �Լ� Ǯ��)�� ���� �ۼ�
        if (Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }
    private void BackendSetUP()
    {
        // �ڳ� �ʱ�ȭ
        var init = Backend.Initialize(true);

        // �ʱ�ȭ�� ���� ���䰪
        if (init.IsSuccess())
        {
            // ���� �� statusCode 204 Success
            Debug.Log($"�ʱ�ȭ ���� : {init}");
            GetGoogleHash();
        }
        else
            Debug.LogError($"�ʱ�ȭ ���� : {init}");
    }
    // ���� �ȵ���̵忡���� �ʿ��� �۾�
    public void GetGoogleHash()
    {
        // ���� �ؽ�Ű ȹ��
        string googleHashKey=Backend.Utils.GetGoogleHash();
        // ������ ���
        if(!string.IsNullOrEmpty(googleHashKey) )
        {
            Debug.Log(googleHashKey);
            Hashkey.text = googleHashKey;
        }
    }
}
