using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int MaxHealth = 100;

    public int CurrentHealth { get; private set; }

    public Stat Damage;
    public Stat Armor;

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int _damage)
    {
        _damage -= Armor.GetValue();
        _damage = Mathf.Clamp(_damage, 0, int.MaxValue);

        CurrentHealth -= _damage;
        Debug.Log(transform.name + " takes " + _damage + " damage.");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }

}
