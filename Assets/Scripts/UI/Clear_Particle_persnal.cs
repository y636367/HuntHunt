using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Particle_persnal : MonoBehaviour
{
    [SerializeField]
    public GameObject Particle;

    public float Sec;
    private void Awake()
    {
        Particle.SetActive(false);
    }
    private void Update()
    {
        if (Sec >= 0)       
            Sec -= Time.deltaTime;
        else
            Particle.SetActive(true);
    }
}
