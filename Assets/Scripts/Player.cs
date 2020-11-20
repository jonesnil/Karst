using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _movementSpeed;
    [SerializeField] RunTimeData _data;
    [SerializeField] GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mouseDummy = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseWorldPos = new Vector3(mouseDummy.x, mouseDummy.y, 0);
            Vector3 directionToMouse = (this.transform.position - mouseWorldPos).normalized;
            
            Bullet newBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(directionToMouse)).GetComponent<Bullet>();
        }
    }

    void Move() 
    {
        Vector3 HorizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 VerticalMovement = new Vector3(0, Input.GetAxis("Vertical"), 0);

        this.transform.position += (HorizontalMovement + VerticalMovement) * Time.deltaTime * _movementSpeed;

        _data.playerPos = this.transform.position;
    }
}
