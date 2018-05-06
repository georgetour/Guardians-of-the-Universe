using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {


    public float health = 250;
    public static int enemiesKilled ;


    public float projectileSpeed = 10f;
    public float firerate= 0.5f;
    //private float probability;

    private ScoreKeeper scoreKeeper;

    public List<Projectile> weapons = new List<Projectile>();
    public int currentWeapon = 1;

    public AudioClip deathSound;


    private void Start()
    {
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        
    }

    private void Update()
    {
        float probability = Time.deltaTime * firerate;
        if (Random.value < probability)
        {
            Fire();
        }

    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        //Check if the collider was a projectile
        if (weapons[currentWeapon])
        {
            health -= weapons[currentWeapon].GetDamage();
            scoreKeeper.ScoreToGive(50);
        }
        Destroy(collider.gameObject);

        if (health <= 0)
        {
            Die();
        }
        if (enemiesKilled >5)
        {
            Destroy(this.gameObject);
        }
    }


    //What the enemy will do when they die
    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(this.gameObject);
        scoreKeeper.ScoreToGive(1000);
        enemiesKilled++;
        Debug.Log(enemiesKilled);
    }

    //Make the enemy shoot
    void Fire()
    {
        weapons[currentWeapon].LaunchSound();
        GameObject beam = Instantiate(weapons[currentWeapon].gameObject, new Vector3(transform.position.x, transform.position.y - 1f, 0), Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
        
    }




}
