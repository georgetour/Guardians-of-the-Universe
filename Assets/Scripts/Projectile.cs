using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float damage = 100f;
    public AudioClip fireSound;
    public AudioClip ongoingSound;
    public Vector2 speed;
    public int size;

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
       
        Destroy(gameObject);
    }

    public void LaunchSound()
    {
        AudioSource.PlayClipAtPoint(fireSound,transform.position);
    }



}
