using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndScreen : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 dummyPosition = transform.position;
        transform.position = new Vector3(dummyPosition.x + 3 * Time.deltaTime, dummyPosition.y, dummyPosition.z);
    }
}
