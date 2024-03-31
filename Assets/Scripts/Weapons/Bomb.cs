using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    public float Damage;

    [SerializeField]
    public int Count;

    [SerializeField]
    private float Life_Time;

    [SerializeField]
    private bool Slower;

    [SerializeField]
    private bool Stun;

    [SerializeField]
    private bool Only;
    private void ColliderOff()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
    }
    private void OnEnable()
    {
        this.gameObject.GetComponent<Collider>().enabled = true;
        Invoke("ColliderOff", Life_Time);
    }
}
