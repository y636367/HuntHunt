using UnityEngine;

public class Small_Particle_Control : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] particles;

    private bool done = false;

    private void Update()
    {
        if (!GameManager.Instance.Start_)                                               // 게임 종료, 사망, 일시정지
        {
            if (!done)
            {
                done = true;
                Particle_Pause();
            }
        }
        else
        {
            if (done)
            {
                done = false;
                Particle_Resume();
            }
        }
    }
    /// <summary>
    /// 파티클 초기화
    /// </summary>
    public void Particle_Clear()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
    /// <summary>
    /// 파티클 정지
    /// </summary>
    public void Particle_Stop()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
    }
    /// <summary>
    /// 파티클 일시정지
    /// </summary>
    public void Particle_Pause()
    {
        foreach(ParticleSystem particle in particles)
        {
            particle.Pause();
        }
    }
    /// <summary>
    /// 파티클 다시 재생
    /// </summary>
    public void Particle_Resume()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
