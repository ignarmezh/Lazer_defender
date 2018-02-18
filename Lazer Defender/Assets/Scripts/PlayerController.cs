using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 15f;
    float padding = 0.5f;
    float health = 100f;

    public AudioClip fireSound;

    public GameObject Projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;

    float xposminmax;
    float yposminmax;

    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
        Vector3 toptmost = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distance));
        xposminmax = leftmost.x - padding;
        yposminmax = toptmost.y- padding;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire",0.00001f,firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        float _xPos = Input.GetAxis("Horizontal");
        float _yPos = Input.GetAxis("Vertical");
        transform.position += new Vector3(_xPos * speed * Time.deltaTime,_yPos * speed * Time.deltaTime,0f);
        float newX = Mathf.Clamp(transform.position.x,-xposminmax,xposminmax);
        float newY = Mathf.Clamp(transform.position.y,-yposminmax,yposminmax);
        transform.position = new Vector3(newX,newY,0f);
    }

    void Fire()
    {
        //Vector3 offset = new Vector3(0,0.7f,0);
        GameObject beam = Instantiate(Projectile,transform.position,Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,projectileSpeed,0);
        AudioSource.PlayClipAtPoint(fireSound,transform.position);
        Destroy(beam,2f);
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
        LevelManager lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        lm.LoadLevel("Win Screen");
        Destroy(gameObject);

    }
}
