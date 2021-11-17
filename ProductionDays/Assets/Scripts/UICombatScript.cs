using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UICombatScript : MonoBehaviour
{
    private PlayerController _player;
    [SerializeField] private Image _hp;
    [SerializeField] private Image _block;
    [SerializeField] private float _maxHP, _maxBlock;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _player.UpdateUI += UpdateHP;
        _player.StaminaUI += UpdateStamina;
    }

    public void UpdateHP()
    {
        float currentP = _player.MaxHealthPoints / _player.HealthPoint;
        float currentValue = _maxHP / currentP;
        _hp.rectTransform.sizeDelta = new Vector2(currentValue, 50f);
    }
    public void UpdateStamina()
    {
        float currentP = _player.MaxBlockPoints / _player._blockStaminaCurrent;
        float currentValue = _maxBlock / currentP;
        _block.rectTransform.sizeDelta = new Vector2(currentValue, 50f);
    }
}
