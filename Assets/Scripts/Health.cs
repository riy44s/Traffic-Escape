using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;

    [Header("UI")]
    public RectTransform healthBar;
    private float originalHealthBarSize;

    public static Health instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalHealthBarSize = healthBar.sizeDelta.x;
    }
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        Debug.Log("Health : " + health);
        healthBar.sizeDelta = new Vector2(originalHealthBarSize * health / 100f, healthBar.sizeDelta.y);

        if (health <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

}
