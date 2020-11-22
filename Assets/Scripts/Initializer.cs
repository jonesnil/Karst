using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] RunTimeData _data;
    [SerializeField] float followDistance;
    [SerializeField] float followSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        _data.playerPos = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        Vector3 mouseWorldPos = new Vector3(this.transform.position.x, this.transform.position.y, 0);

        

        if ((_data.playerPos - mouseWorldPos).magnitude > followDistance) 
        {
            Vector3 dummyPos = Vector3.Lerp(mouseWorldPos, _data.playerPos, followSpeed * .01f);
            this.transform.position = new Vector3(dummyPos.x, dummyPos.y, this.transform.position.z);
        }
    }
}
