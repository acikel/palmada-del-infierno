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
    [SerializeField] private GameObject Heart;
    [SerializeField] private List<GameObject> Hearts;
    [SerializeField] private Vector2 StartPos;
    [SerializeField] private float Offset;
    [SerializeField] private Sprite _hpFull;
    [SerializeField] private Sprite _hpEmpty;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _player.UpdateUI += UpdateHP;
        _player.StaminaUI += UpdateStamina;
    }

    public void UpdateHP()
    {
        Hearts[(int) _player.HealthPoint].GetComponent<UnityEngine.UI.Image>().sprite = _hpEmpty;
        /*
        float currentP = _player.MaxHealthPoints / _player.HealthPoint;
        float currentValue = _maxHP / currentP;
        _hp.rectTransform.sizeDelta = new Vector2(currentValue, 50f);
        */
    }
    public void UpdateStamina()
    {
        float currentP = _player.MaxBlockPoints / _player._blockStaminaCurrent;
        float currentValue = _maxBlock / currentP;
        _block.rectTransform.sizeDelta = new Vector2(currentValue, 50f);
    }
}
