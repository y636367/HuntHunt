using UnityEngine;

public class Small_Particle_Control : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] particles;

    private bool done = false;

    private void Update()
    {
        if (!GameManager.Instance.Start_)                                               // ���� ����, ���, �Ͻ�����
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
    /// ��ƼŬ �ʱ�ȭ
    /// </summary>
    public void Particle_Clear()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
    /// <summary>
    /// ��ƼŬ ����
    /// </summary>
    public void Particle_Stop()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
    }
    /// <summary>
    /// ��ƼŬ �Ͻ�����
    /// </summary>
    public void Particle_Pause()
    {
        foreach(ParticleSystem particle in particles)
        {
            particle.Pause();
        }
    }
    /// <summary>
    /// ��ƼŬ �ٽ� ���
    /// </summary>
    public void Particle_Resume()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
