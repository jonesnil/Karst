using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour
{
    [SerializeField] RunTimeData _data;
    [SerializeField] float _moveSpeed;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != _data.playerPos) 
        {
            this.body.velocity = (_data.playerPos - this.transform.position).normalized * _moveSpeed;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, _data.playerPos, (.01f *_moveSpeed));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            Destroy(collision.collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
