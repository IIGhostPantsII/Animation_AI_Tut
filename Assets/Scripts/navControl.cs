using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navControl : MonoBehaviour
{
    public GameObject Target;
    public GameObject Enemy;
    public string aniName;
    private NavMeshAgent agent;

    bool isWalking = true;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        ani.SetTrigger("WALK");
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalking)
        {
            agent.destination = Target.transform.position;
        }
        else
        {
            agent.destination = transform.position;
            LookAtTarget();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == Target.name)
        {
            isWalking = false;
            ani.SetTrigger(aniName);
        }
    }

    void LookAtTarget()
    {
        Vector3 direction = Enemy.transform.position - transform.position;
        direction.y = 0;

        if(direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
    }
}
