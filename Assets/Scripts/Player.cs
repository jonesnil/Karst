using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _movementSpeed;
    [SerializeField] RunTimeData _data;
    [SerializeField] GameObject bulletPrefab;
    Animator animator;
    float health;
    [SerializeField] float startingHealth;
    Color spriteColor;
    SpriteRenderer sprite;
    Rigidbody2D body;
    Collider2D collider;
    bool knockedBack;
    bool cantMove;
    Vector3 knockDirection;
    int knockBackFrame;
    [SerializeField] int knockBackFrames;
    [SerializeField] int noMoveFrames;
    [SerializeField] int iFrames;
    [SerializeField] float knockBackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        health = startingHealth;
        GameEvents.PlayerHit += OnPlayerHit;
        _data.startingHealth = startingHealth;
        _data.health = health;
        sprite = this.GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<Collider2D>();
        knockedBack = false;
        cantMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Shoot();

        if (health <= 0) 
        {
            Destroy(this.gameObject);
        }

        if(knockedBack)
            KnockBack();

    }

    void Move() 
    {
        Vector3 HorizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 VerticalMovement = new Vector3(0, Input.GetAxis("Vertical"), 0);
        Vector3 move = (HorizontalMovement + VerticalMovement) * Time.deltaTime * _movementSpeed;

        RaycastHit2D[] results = new RaycastHit2D[10];

        bool hitWall = false;
        int hit = collider.Raycast(move, results, move.magnitude);

        foreach (RaycastHit2D result in results)
        {
            if (result.collider != null && result.collider.tag == "Wall")
            {
                hitWall = true;
            }
        }
        

        if(hit == 0 && !cantMove)
            this.transform.position += move;

        if (!cantMove && knockedBack && !hitWall)
            this.transform.position += move;

        if (Input.GetAxis("Horizontal") < 0 && !sprite.flipX) 
        {
            sprite.flipX = true;
        }

        if (Input.GetAxis("Horizontal") > 0 && sprite.flipX)
        {
            sprite.flipX = false;
        }

        animator.SetFloat("moveSpeed", move.magnitude);

        _data.playerPos = this.transform.position;

    }

    void Shoot() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseDummy = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseWorldPos = new Vector3(mouseDummy.x, mouseDummy.y, 0);
            Vector3 directionToMouse = (mouseWorldPos - this.transform.position) * 10000;

            Bullet newBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity).GetComponent<Bullet>();
            newBullet.direction = directionToMouse;
        }
    }


    void OnPlayerHit(object sender, BaddieEventArgs args) 
    {
        if (!knockedBack)
        {
            Baddie baddie = args.baddiePayload;
            this.health -= baddie.damage;
            _data.health = health;
            animator.SetBool("hit", true);
            Invoke("FlashColor", .2f);
            knockedBack = true;
            cantMove = true;
            knockBackFrame = 0;
            knockDirection = (this.transform.position - baddie.gameObject.transform.position).normalized * knockBackSpeed * Time.deltaTime;
            Destroy(baddie.gameObject);
        }
    }


    void FlashColor() 
    {
        spriteColor = sprite.color;

        if ((health / startingHealth) > (2f / 3f))
        {
            sprite.color = Color.green;
        }

        else if ((health / startingHealth) > (1f / 3f))
        {
            sprite.color = Color.yellow;
        }

        else
        {
            sprite.color = Color.red;
        }

        Invoke("RevertColor", .1f);
    }

    void KnockBack() 
    {
        
        RaycastHit2D[] results = new RaycastHit2D[10];
        if (collider.Raycast(knockDirection, results, knockDirection.magnitude) == 0 && (knockBackFrame <= knockBackFrames))
            this.transform.position += knockDirection;
        knockBackFrame += 1;

        if (knockBackFrame >= noMoveFrames && cantMove) 
        {
            cantMove = false;
        }

        if (knockBackFrame >= iFrames) 
        {
            knockedBack = false;
            knockBackFrame = 0;
        }
    }

    void RevertColor() 
    {
        sprite.color = spriteColor;
    }
}
