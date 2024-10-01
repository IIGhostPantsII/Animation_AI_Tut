using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] public PathManager pathManager;

    List<Waypoint> thePath;
    Waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;

    private Animator animator;
    bool isWalking;

    public bool second;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
        thePath = pathManager.GetPath();
        if(thePath != null && thePath.Count > 0)
        {
            target = thePath[0];
        }
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
        }

        if(isWalking)
        {
            RotateTowardsTarget();
            moveForward();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("path1"))
        {
            if(!second)
            {
                target = pathManager.GetNextTarget();
            }
        }

        if (other.CompareTag("path2"))
        {
            if(second)
            {
                target = pathManager.GetNextTarget();
            }
        }

        if (other.CompareTag("Enemy"))
        {
            isWalking = false;
            animator.SetBool("isWalking", isWalking);
        }
    }

    void RotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if(distanceToTarget < stepSize)
        {
            return;
        }
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }
}
