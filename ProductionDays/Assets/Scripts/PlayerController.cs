using System;
using System.Collections;
using System.Data;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Action UpdateUI;
    public Action StaminaUI;
    public float _moveSpeed;
    [SerializeField] private float _blockMoveSpeed;
    [SerializeField] private float _attackDamage;
    public float MaxHealthPoints { get; private set; }
    public float HealthPoint;
    public float MaxBlockPoints { get; private set; }
    public float BlockStamina;
    [SerializeField] private float _blockStaminaRegen;

    private bool _moveLeft = false;
    private bool _moveRight = false;
    private bool _moveUp = false;
    private bool _moveDown = false;
    private bool _walkable = true;
    private bool _blocking = false;
    private bool _attacking = false;
    private bool _lookRight = true;
    private bool _blockBrocken = false;
    private bool _decisionBlocked = false;

    public float _blockStaminaCurrent { get; private set; }

    private BoxCollider _attackColliderFist;
    private Animator _animator;

    [Header("LevelSize")] 
    public float _lvlWidth;
    public float _lvlDeapth;
    public float _lvlOffsetX;
    public float _lvlOffsetZ;
    public float _lvlCenterX;
    public float _lvlCenterZ;
    private float _lastLvlSizeXwall;

    private ParticleEffects ParticleEffect { get; set; }

    void Awake()
    {

        if (InstanceRepository.Instance.Get<PlayerController>() == null)
        {
            InstanceRepository.Instance.AddOnce(this);
        }

        ParticleEffect = InstanceRepository.Instance.Get<ParticleEffects>();
        
        _animator = GetComponent<Animator>();
        _attackColliderFist = transform.GetChild(0).GetComponent<BoxCollider>();
        _attackColliderFist.enabled = false;
        _blockStaminaCurrent = BlockStamina;
        MaxHealthPoints = HealthPoint;
        MaxBlockPoints = BlockStamina;
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

            if (_moveUp)
            {
                if (_lookRight)
                {
                    move += Vector3.forward;
                }
                else
                {
                    move += Vector3.back;
                }
            }
            if (_moveDown)
            {
                if (_lookRight)
                {
                    move += Vector3.back;
                }
                else
                {
                    move += Vector3.forward;
                }
            }
            move = CheckIfOnScreen(move);
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

        if (StaminaUI != null) StaminaUI();
    }

    #region MoveInput
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

    void OnUpButtonDown()
    {
        _moveUp = true;
    }

    void OnUpButtonUp()
    {

        _moveUp = false;
    }
    void OnDownButtonDown()
    {
        _moveDown = true;
    }

    void OnDownButtonUp()
    {
        _moveDown = false;
    }

#endregion
    #region Attack / Block
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

    #endregion
    #region PlayerHit Cal
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
            ParticleEffect.SpawnEffect(Effect.Damage, transform.position);
            AudioManager.Instance.PlayOneShot(AudioEvent.Combat.GruntFemale, transform.position);
            if (UpdateUI != null) UpdateUI();
        }
        else if(_blocking && dirBlock)
        {
            _blockStaminaCurrent -= damage;
            ParticleEffect.SpawnEffect(Effect.Miss, transform.position);
            AudioManager.Instance.PlayOneShot(AudioEvent.Combat.BlockImpact, transform.position);
            if (_blockStaminaCurrent <= 0)
            {
                _blockBrocken = true;
                OnBlockButtonUp();
                StartCoroutine(BlockBrockenTimer());
            }
        }

        if (HealthPoint <= 0)
        {
            AudioManager.Instance.PlayOneShot(AudioEvent.Combat.PlayerDie);
            //Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void EnemyHit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlayOneShot(AudioEvent.Combat.Impact);
            ParticleEffect.SpawnEffect(Effect.Hit, col.transform.position);
            col.gameObject.GetComponent<EnemyHPScript>().Updatehealt(_attackDamage);
        }
    }
#endregion
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

    #region moveConstrainCheck

    public void SetBlockActive()
    {
        _decisionBlocked = true;
        _lastLvlSizeXwall = transform.position.x;
    }

    public void SetBlockDisable()
    {
        _decisionBlocked = false;
    }

    private Vector3 CheckIfOnScreen(Vector3 move)
    {
        Vector3 placeToBe = transform.position + move * _moveSpeed * Time.deltaTime;
        bool answer = true;
        if (placeToBe.x < (_lvlCenterX - _lvlWidth / 2) + _lvlOffsetX)
        {
            if (_lookRight)
            {
                move -= Vector3.left;
            }
            else
            {
                move -= Vector3.right;
            }
        }

        if (_decisionBlocked)
        {
            if (placeToBe.x > _lastLvlSizeXwall - _lvlOffsetX)
            {
                if (_lookRight)
                {
                    move -= Vector3.right;
                }
                else
                {
                    move -= Vector3.left;
                }
            }
        }
        else
        {
            if (placeToBe.x > (_lvlCenterX + _lvlWidth / 2) - _lvlOffsetX)
            {
                if (_lookRight)
                {
                    move -= Vector3.right;
                }
                else
                {
                    move -= Vector3.left;
                }
            }
        }
        if (placeToBe.z > (_lvlCenterZ - _lvlDeapth / 2)+_lvlOffsetZ)
        {
            if (_lookRight)
            {
                move -= Vector3.forward;
            }
            else
            {
                move -= Vector3.back;
            }
        }
        if (placeToBe.z < (_lvlCenterZ + _lvlDeapth / 2)-_lvlOffsetZ)
        {
            if (_lookRight)
            {
                move -= Vector3.back;
            }
            else
            {
                move -= Vector3.forward;
            }
        }
        return move;
    }

    #endregion

    public Vector3 GetFloorPosition()
    {
        Renderer renderer = GetComponent<Renderer>();
        Vector3 position = transform.position;
        position.y -= renderer.bounds.extents.y;

        return position;
    }
    
    void OnDestroy()
    {
        InstanceRepository.Instance.Remove(this);
    }

}
