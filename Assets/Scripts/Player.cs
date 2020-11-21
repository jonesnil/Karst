using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _movementSpeed;
    [SerializeField] RunTimeData _data;
    [SerializeField] GameObject bulletPrefab;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mouseDummy = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseWorldPos = new Vector3(mouseDummy.x, mouseDummy.y, 0);
            Vector3 directionToMouse = (mouseWorldPos - this.transform.position) * 10000;

            Bullet newBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity).GetComponent<Bullet>();
            newBullet.direction = directionToMouse;
        }

    }

    void Move() 
    {
        Vector3 HorizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 VerticalMovement = new Vector3(0, Input.GetAxis("Vertical"), 0);
        Vector3 move = (HorizontalMovement + VerticalMovement) * Time.deltaTime * _movementSpeed;
        this.transform.position += move;

        animator.SetFloat("moveSpeed", move.magnitude);

        _data.playerPos = this.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Baddie") 
        {
            animator.SetBool("hit", true);
        }
    }
}
