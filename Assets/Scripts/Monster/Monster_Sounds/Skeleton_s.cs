using UnityEngine;

public class Skeleton_s : MonoBehaviour
{
    /// <summary>
    /// ½ºÄÌ·¹Åæ ¼Ò¸® ¸ðÀ½
    /// </summary>
    [SerializeField]
    private string[] Skeleton_S;

    public void Skeleton_1()
    {
        soundManager.Instance.PlaySoundEffect(Skeleton_S[0]);
    }
    public void Skeleton_2()
    {
        soundManager.Instance.PlaySoundEffect(Skeleton_S[1]);
    }
    public void Skeleton_3()
    {
        soundManager.Instance.PlaySoundEffect(Skeleton_S[2]);
    }
    public void Skeleton_4()
    {
        soundManager.Instance.PlaySoundEffect(Skeleton_S[3]);
    }
    public void Skeleton_5()
    {
        soundManager.Instance.PlaySoundEffect(Skeleton_S[4]);
    }
}
