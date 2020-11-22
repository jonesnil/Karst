using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour
{
    [SerializeField] RunTimeData _data;
    [SerializeField] float _moveSpeed;
    public int damage;
    Rigidbody2D body;
    SpriteRenderer sprite;
    bool seen;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        seen = false;
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
    }

    private void OnBecameVisible()
    {
        seen = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            Destroy(collision.collider.gameObject);
            Destroy(this.gameObject);
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
}
