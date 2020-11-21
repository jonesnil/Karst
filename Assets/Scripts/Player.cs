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

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        health = startingHealth;
        GameEvents.PlayerHit += OnPlayerHit;
        _data.startingHealth = startingHealth;
        _data.health = health;
        sprite = this.GetComponent<SpriteRenderer>();
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

    }

    void Move() 
    {
        Vector3 HorizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 VerticalMovement = new Vector3(0, Input.GetAxis("Vertical"), 0);
        Vector3 move = (HorizontalMovement + VerticalMovement) * Time.deltaTime * _movementSpeed;
        this.transform.position += move;

        if (Input.GetAxis("Horizontal") < 0) 
        {
            animator.SetBool("faceRight", false);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("faceRight", true);
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
        this.health -= args.baddiePayload.damage;
        _data.health = health;
        animator.SetBool("hit", true);
        Invoke("FlashColor", .2f);
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

    void RevertColor() 
    {
        sprite.color = spriteColor;
    }
}
