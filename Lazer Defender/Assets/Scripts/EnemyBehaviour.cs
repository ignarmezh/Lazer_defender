using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject projectile;

    float health = 100f;

    public float shootPerSeconds = 0.5f;

    public AudioClip fireSound;
    public AudioClip deathSound;

    private int scoreValue = 100;
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        float probability = Time.deltaTime * shootPerSeconds;
        if (Random.value<probability)   
        {
            Fire();
        }
    }

    void Fire()
    {
        //Vector3 startPosition = transform.position + new Vector3(0,-1f,0);
        GameObject missile = Instantiate(projectile,transform.position,Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5f,0f);
        AudioSource.PlayClipAtPoint(fireSound,transform.position);
        Destroy(missile,2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound,transform.position);
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
    }
}
