using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBoxController : MonoBehaviour {

    public int coinCount;
    public Text countText;
    private int count;

    private AudioSource source;
    public AudioClip pickupClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    // Use this for initialization
    void Start ()
    {
        count = 0;
        SetCountText();
    }

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && coinCount != 0 )
        {
            Debug.Log("Player Hit Me");
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(pickupClip);
            count = count + 1;
            coinCount = coinCount - 1;
            SetCountText();
        }
        if (coinCount == 0)
        {
            Destroy(gameObject);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
