using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Panel : MonoBehaviour
{
    [SerializeField]
    private Image Panel;

    public float speed;                                                     // Fade �ӵ�
    private void Awake()
    {
        Panel.gameObject.SetActive(true);
    }
    /// <summary>
    /// �� ��������� �˸��� Fade In �Լ�
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
    /// UI ���� �� FadeIn
    /// </summary>
    public void Fade_Start()
    {
        StartCoroutine("FadeIn");
    }
}
