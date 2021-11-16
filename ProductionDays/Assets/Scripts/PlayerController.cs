using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _blockMoveSpeed;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _healthPoint;

    private bool _moveLeft = false;
    private bool _moveRight = false;
    private bool _walkable = true;
    private bool _blocking = false;
    private bool _attacking = false;
    private bool _lookRight = true;

    private BoxCollider _attackColliderFist;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _attackColliderFist = transform.GetChild(0).GetComponent<BoxCollider>();
        _attackColliderFist.enabled = false;
    }


    void FixedUpdate()
    {
        if (_walkable)
        {
            Vector3 move = Vector3.zero;
            if (_moveRight)
            {
                if (_lookRight)
                {
                    move += Vector3.right;
                }
                else
                {
                    move += Vector3.left;
                }
            }
            if (_moveLeft)
            {
                if (_lookRight)
                {
                    move += Vector3.left;
                }
                else
                {
                    move += Vector3.right;
                }
            }

            if (_blocking)
            {
                move = move * _blockMoveSpeed * Time.deltaTime;
            }
            else
            {
                move = move * _moveSpeed * Time.deltaTime;
            }

            transform.Translate(move);
        }
    }

    void OnLeftButtonDown()
    {
        _moveLeft = true;
        if (_lookRight && !_attacking) 
        { 
            transform.Rotate(new Vector3(0, 180, 0)); 
            _lookRight = false;
        }
    }
    void OnRightButtonDown()
    {
        _moveRight = true; 
        if (!_lookRight && !_attacking)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            _lookRight = true;
        }
    }

    void OnLeftButtonUp()
    {
        _moveLeft = false;
    }
    void OnRightButtonUp()
    {
        _moveRight = false;
    }

    void OnAttackButton()
    {
        if (!_blocking)
        {
            _attacking = true;
            _walkable = false;
            _attackColliderFist.enabled = true;
            _animator.SetBool("Attacking", true);
        }
    }

    void OnBlockButtonDown()
    {
        if (!_attacking)
        {
            _blocking = true;
            _animator.SetBool("Blocking", true);
        }
    }

    void OnBlockButtonUp()
    {
        _animator.SetBool("Blocking", false);
        _blocking = false;
    }

    void AttackAnimationEnd()
    {
        _animator.SetBool("Attacking", false);
        _attackColliderFist.enabled = false;
        _walkable = true;
        _attacking = false;
        Debug.Log(transform.eulerAngles);
        if (_moveLeft && (int)transform.eulerAngles.y == (int)0)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            _lookRight = false;
        }

        if (_moveRight && (int)transform.eulerAngles.y == (int)180)
        {

            transform.Rotate(new Vector3(0, 180, 0));
            _lookRight = true;
        }
    }

    public void PlayerHit(float damage)
    {
        if (!_blocking)
        {
            _healthPoint -= damage;
        }
    }

    public void EnemyHit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyHPScript>().Updatehealt(_attackDamage);
        }
    }
    
}
