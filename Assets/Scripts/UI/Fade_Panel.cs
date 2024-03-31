using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Panel : MonoBehaviour
{
    [SerializeField]
    private Image Panel;

    public float speed;                                                     // Fade 속도
    private void Awake()
    {
        Panel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 씬 변경됬음을 알리는 Fade In 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        while (Panel.color.a > 0)
        {
            Panel.color = new Color(Panel.color.r, Panel.color.g, Panel.color.b, Panel.color.a - (Time.deltaTime / speed));
            yield return null;
        }
        Panel.gameObject.SetActive(false);
    }
    /// <summary>
    /// UI 구성 후 FadeIn
    /// </summary>
    public void Fade_Start()
    {
        StartCoroutine("FadeIn");
    }
}
