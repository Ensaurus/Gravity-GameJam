using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Animator _animator;

    private int _animState = 0;
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
        if (Input.GetKey(KeyCode.D))
        {
            targetDirection = Vector3.right;
            _animator.SetBool("IsRunning", true);
            isMoving = true;
        } else if (Input.GetKey(KeyCode.A))
        {
            targetDirection = Vector3.left;
            _animator.SetBool("IsRunning", true);
            isMoving = true;
        } else if (Input.GetKey(KeyCode.W))
        {
            targetDirection = Vector3.forward;
            _animator.SetBool("IsRunning", true);
            isMoving = true;
        } else if (Input.GetKey(KeyCode.S))
        {
            targetDirection = Vector3.back;
            _animator.SetBool("IsRunning", true);
            isMoving = true;
        }
        else
        {
            _animator.SetBool("IsRunning", false);
            _animState = 0;
            isMoving = false;
        }

        Vector3 finalDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * 5.0f, 0.0f);
        transform.rotation = Quaternion.LookRotation(finalDirection);
        if (isMoving)
            transform.Translate(transform.forward * Time.deltaTime);
    }
}
