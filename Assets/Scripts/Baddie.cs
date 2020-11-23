using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour
{
    [SerializeField] RunTimeData _data;
    [SerializeField] float _moveSpeed;
    [SerializeField] int _health;
    [SerializeField] GameObject deathEffectPrefab;

    int redFrames;
    int redFrame;
    bool red;
    Color spriteColor;
    public float damage;
    Rigidbody2D body;
    SpriteRenderer sprite;
    bool seen;
    AudioSource shotSound;
    PolygonCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        seen = false;
        spriteColor = sprite.color;
        red = false;
        redFrame = 0;
        redFrames = 15;
        shotSound = this.GetComponent<AudioSource>();
        collider = this.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (seen) 
        {
            FollowPlayer();
        }

        Vector3 dontRotate = new Vector3(0f, 0f, 0f);
        this.transform.rotation = Quaternion.Euler(dontRotate);

        if (this.red) 
        {
            this.redFrame += 1;

            if (this.redFrame == redFrames)
                RevertColor();
        }
    }

    private void OnBecameVisible()
    {
        seen = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            this._health -= _data.bulletDamage;

            TurnRed();

            Destroy(collision.collider.gameObject);

            shotSound.Play();

            if (this._health <= 0)
                Die();

        }

        if (collision.collider.gameObject.tag == "Player")
        {
            GameEvents.InvokePlayerHit(this);
        }
    }

    void FollowPlayer() 
    {
        if (this.transform.position != _data.playerPos)
        {
            this.body.velocity = (_data.playerPos - this.transform.position).normalized * _moveSpeed;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, _data.playerPos, (.01f *_moveSpeed));

            if ((_data.playerPos.x > this.transform.position.x) && sprite.flipX == false)
            {
                sprite.flipX = true;
            }

            if ((_data.playerPos.x < this.transform.position.x) && sprite.flipX == true)
            {
                sprite.flipX = false;
            }
        }
    }

    void TurnRed() 
    {
        this.red = true;
        this.redFrame = 0;
        
        sprite.color = Color.red;
    }

    void RevertColor() 
    {
        this.red = false;
        this.redFrame = 0;

        sprite.color = spriteColor;
    }

    public void Die() 
    {
        Instantiate(deathEffectPrefab, this.transform.position, Quaternion.identity);

        body.simulated = false;
        collider.enabled = false;
        this.sprite.enabled = false;
    }
}
