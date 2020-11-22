using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float _moveSpeed;
    PolygonCollider2D collider;
    public Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<PolygonCollider2D>();

        Vector3 mouseDummy = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseWorldPos = new Vector3(mouseDummy.x, mouseDummy.y, 0);
        transform.right = mouseWorldPos - transform.position;
    }

    private void Update()
    {
        Vector3 move = (direction - this.transform.position).normalized * _moveSpeed;
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

        if (this.transform.position != direction)
        {
            this.transform.position += move;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, _data.playerPos, (.01f *_moveSpeed));
        }

        if (hitWall) 
        {
            Destroy(this.gameObject);
        }
    }


}
