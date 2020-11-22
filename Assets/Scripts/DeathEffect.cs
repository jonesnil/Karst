using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    Animator deathAnimator;

    // Start is called before the first frame update
    void Start()
    {
        deathAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathAnimator.GetCurrentAnimatorStateInfo(0).IsName("deathDummyEffect")) 
        {
            Destroy(this.gameObject);
        }
    }
}
