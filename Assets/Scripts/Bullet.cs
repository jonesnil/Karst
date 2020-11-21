using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float _moveSpeed;
    public Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (this.transform.position != direction)
        {
            this.transform.position += (direction - this.transform.position).normalized * _moveSpeed;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, _data.playerPos, (.01f *_moveSpeed));
        }
    }


}
