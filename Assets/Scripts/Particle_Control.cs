using UnityEngine;

public class Particle_Control : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] particles;

    /// <summary>
    /// ��ϵ� ��ƼŬ �Ͻ�����
    /// </summary>
    public void Effect_Pause()
    {
        Get_Particles();
        Particles_Pause();
    }
    /// <summary>
    /// ������ ��ƼŬ �ý��� �ޱ�
    /// </summary>
    private void Get_Particles()
    {
        particles = new ParticleSystem[this.gameObject.transform.childCount];

        for (int index=0; index<this.gameObject.transform.childCount; index++)
        {
            particles[index] = this.gameObject.transform.GetChild(index).GetComponent<ParticleSystem>();
        }
    }
    /// <summary>
    /// ��ϵ� ��ƼŬ �Ͻ�����
    /// </summary>
    private void Particles_Pause()
    {
        for(int index=0; index<particles.Length; index++)
        {
            if (particles[index].gameObject.activeSelf)
            {
                particles[index].Pause();
            }
        }
    }
    /// <summary>
    ///  ��ϵ� ��ƼŬ ���
    /// </summary>
    public void Particles_Resume()
    {
        for (int index = 0; index < particles.Length; index++)
        {
            if (particles[index].gameObject.activeSelf)
            {
                particles[index].Play();
            }
        }
    }
}
