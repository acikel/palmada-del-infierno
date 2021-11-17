using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UICombatScript : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private Image _hp;
    [SerializeField] private Image _block;
    [SerializeField] private float _maxHP, _maxBlock;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<PlayerController>().UpdateUI += UpdateHP;
    }

    public void UpdateHP()
    {
        Debug.Log(("test"));
    }
}
