using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    private Animator anim;
    public float speed;
    public LayerMask isGround;
    public LayerMask isPlayer;
    public Transform wallHitBox;
    public Transform playerHitBox;
    private bool wallHit;
    private bool playerHit;
    public float wallHitHeight;
    public float wallHitWidth;
    public float playerHitHeight;
    public float playerHitWidth;

    private AudioSource source;
    public AudioClip deathClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
	}

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void FixedUpdate ()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        playerHit = Physics2D.OverlapBox(playerHitBox.position, new Vector2(playerHitWidth, playerHitHeight), 0, isPlayer);
        if (wallHit == true)
        {
            speed = speed * -1;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && playerHit == true)
        {
            anim.SetBool("isDead", true);
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(deathClip);
            Debug.Log("Goomba dead");
            Destroy(gameObject, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color =Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(playerHitBox.position, new Vector3(playerHitWidth, playerHitHeight, 1));

    }
}
