using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _healthPoint;

    private bool _moveLeft = false;
    private bool _moveRight = false;
    private bool _walkable = true;

    void Update()
    {
        if (_walkable)
        {
            Vector3 move = Vector3.zero;
            if (_moveRight)
            {
                move += Vector3.right;
            }

            if (_moveLeft)
            {
                move += Vector3.left;
            }

            move = move * _moveSpeed * Time.deltaTime;
            transform.Translate(move);
        }
    }

    void OnLeftButton()
    {
        _moveLeft = !_moveLeft;
    }
    void OnRightButton()
    {
        _moveRight = !_moveRight;
    }

    void OnAttackButton()
    {
        _walkable = false;
        //wait for Animation then _walkable = true;
        Debug.Log("Attack");
    }

    void OnBlockButton()
    {
        //walk slower look into direction you started the block
    }
}
