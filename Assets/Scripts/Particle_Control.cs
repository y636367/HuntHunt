using UnityEngine;

public class Particle_Control : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] particles;

    /// <summary>
    /// 등록된 파티클 일시정지
    /// </summary>
    public void Effect_Pause()
    {
        Get_Particles();
        Particles_Pause();
    }
    /// <summary>
    /// 생성된 파티클 시스템 받기
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
    /// 등록된 파티클 일시정지
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
    ///  등록된 파티클 재생
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
