using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MonigoteController : MonoBehaviour
{
    // Constantes para identificar los parameters del animator
    private const string MOVE_HANDS = "Move Hands";
    private const string MOVE_X = "Move_X";
    private const string MOVE_Y = "Move_Y";
    private const string IS_MOVING = "Is Moving";

    private Animator _animator;

    private bool isMovingHands = false, isMoving = false;
    private float moveX = 0f, moveY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        // Asegurar que esté alineado el valor inicial con el valor que seteo en Unity animator
        _animator.SetBool(MOVE_HANDS, isMovingHands);
        _animator.SetFloat(MOVE_X, moveX);
        _animator.SetFloat(MOVE_Y, moveY);
        _animator.SetBool(IS_MOVING, isMoving);
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if(Mathf.Sqrt(moveX * moveX + moveY * moveY) > 0.01)
        {
            _animator.SetBool(IS_MOVING, true);
            _animator.SetFloat(MOVE_X, moveX);
            _animator.SetFloat(MOVE_Y, moveY);
        }
        else
        {
            _animator.SetBool(IS_MOVING, false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMovingHands = !isMovingHands;
            _animator.SetBool(MOVE_HANDS, isMovingHands);
        }
    }
}
