using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Animator _animator;

    private int _moveDirection = 0;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = Vector3.zero;
        bool isMoving = false;
        _animator.SetBool("IsRunning", false);
        if (Input.GetKey(KeyCode.D))
        {
            targetDirection.x = 1;
            _animator.SetBool("IsRunning", true);
            isMoving = true;
        } else if (Input.GetKey(KeyCode.A))
        {
            targetDirection.x = -1;
            _animator.SetBool("IsRunning", true);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            targetDirection.z = 1;
            _animator.SetBool("IsRunning", true);
            isMoving = true;
        } else if (Input.GetKey(KeyCode.S))
        {
            targetDirection.z = -1;
            _animator.SetBool("IsRunning", true);
            isMoving = true;
        }

        Vector3 finalDirection = Vector3.RotateTowards(transform.forward, targetDirection,
            Time.deltaTime * 20.0f, 0.0f);
        transform.rotation = Quaternion.LookRotation(finalDirection);
        if (isMoving)
            transform.Translate(targetDirection * (Time.deltaTime * 5.0f), Space.World);
    }
}
