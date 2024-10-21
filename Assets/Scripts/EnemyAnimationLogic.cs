using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationLogic : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" || other.name == "Player (1)" || other.name == "Player (2)")
        {
            Animator ani = GetComponent<Animator>();
            ani.SetTrigger("ATTACK");
        }
    }
}
