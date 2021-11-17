using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Action UpdateUI;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _blockMoveSpeed;
    [SerializeField] private float _attackDamage;
    public float HealthPoint;
    public float BlockStamina;
    [SerializeField] private float _blockStaminaRegen;

    private bool _moveLeft = false;
    private bool _moveRight = false;
    private bool _walkable = true;
    private bool _blocking = false;
    private bool _attacking = false;
    private bool _lookRight = true;
    private bool _blockBrocken = false;

    private float _blockStaminaCurrent;

    private BoxCollider _attackColliderFist;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _attackColliderFist = transform.GetChild(0).GetComponent<BoxCollider>();
        _attackColliderFist.enabled = false;
        _blockStaminaCurrent = BlockStamina;
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

        if (!_blocking && !_blockBrocken && _blockStaminaCurrent < BlockStamina)
        {
            _blockStaminaCurrent += _blockStaminaRegen * Time.deltaTime;
            if (_blockStaminaCurrent > BlockStamina) _blockStaminaCurrent = BlockStamina;
        }
    }

    void OnLeftButtonDown()
    {
        _moveLeft = true;
        if (_lookRight && !_attacking && !_blocking) 
        { 
            transform.Rotate(new Vector3(0, 180, 0)); 
            _lookRight = false;
        }
    }
    void OnRightButtonDown()
    {
        _moveRight = true; 
        if (!_lookRight && !_attacking && !_blocking)
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
        if (!_attacking && !_blockBrocken)
        {
            _blocking = true;
            _animator.SetBool("Blocking", true);
        }
    }

    void OnBlockButtonUp()
    {
        _animator.SetBool("Blocking", false);
        _blocking = false; 
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

    public void PlayerHit(float damage, Transform trans)
    {
        bool dirBlock = false;
        if (_blocking)
        {
            if (transform.position.x < trans.position.x && _lookRight) dirBlock = true;
            if (transform.position.x > trans.position.x && !_lookRight) dirBlock = true;
        }
        if (!_blocking || !dirBlock)
        {
            HealthPoint -= damage;

            if (UpdateUI != null) UpdateUI();
        }
        else if(_blocking && dirBlock)
        {
            _blockStaminaCurrent -= damage;
            if (_blockStaminaCurrent <= 0)
            {
                _blockBrocken = true;
                OnBlockButtonUp();
                StartCoroutine(BlockBrockenTimer());
            }
        }

        if (HealthPoint <= 0)
        {
            //Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void EnemyHit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyHPScript>().Updatehealt(_attackDamage);
        }
    }

    IEnumerator BlockBrockenTimer()
    {
        while (_blockStaminaCurrent < BlockStamina)
        {
            _blockStaminaCurrent += _blockStaminaRegen * Time.deltaTime;
            if (_blockStaminaCurrent > BlockStamina) _blockStaminaCurrent = BlockStamina;
            yield return null;
        }
        _blockBrocken = false;
    }
    
}
