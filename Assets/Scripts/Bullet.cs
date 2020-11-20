using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float _moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        this.transform.position += this.transform.forward * _moveSpeed * Time.deltaTime;
    }


}
