using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] RunTimeData _data;

    // Start is called before the first frame update
    void Awake()
    {
        _data.playerPos = new Vector3(0, 0, 0);
    }
}
