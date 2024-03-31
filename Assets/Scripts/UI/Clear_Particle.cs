using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Particle : MonoBehaviour
{
    [SerializeField]
    private GameObject Particle_Prefab;

    public void Create_Particle()
    {
        int Count = Random.Range(4, 8);

        while (Count >=0)
        {
            GameObject Particle_ = Instantiate(Particle_Prefab, this.transform);
            Set_Position(Particle_);
            Count--;
        }
    }
    private void Set_Position(GameObject t_par)
    {
        float Active_sec = Random.Range(0, 3.0f);

        t_par.GetComponent<Clear_Particle_persnal>().Sec = Active_sec;

        int Postion_x = Random.Range(-300, 300);
        int Postion_y = Random.Range(-650, 650);

        GameObject par = t_par.GetComponent<Clear_Particle_persnal>().Particle;

        par.GetComponent<RectTransform>().localPosition = new Vector3(Postion_x, Postion_y, 0);
    }
}
