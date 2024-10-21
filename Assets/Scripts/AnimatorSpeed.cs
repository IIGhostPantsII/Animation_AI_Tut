using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorSpeed : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator ani;
    public float baseAgentSpeed = 1.5f;
    private string speedParameterName = "SPEED";

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        float currentSpeed = agent.velocity.magnitude;

        float speedFactor = currentSpeed / baseAgentSpeed;

        ani.SetFloat(speedParameterName, speedFactor);
    }
}
