using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	
	public HealthBar healthBar;
	
	
	void StartHealth()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}
	
	private void Update()
	{
			UpdateHealth();
	}
	
	void UpdateHealth()
	{
	if (Input.GetKeyDown(KeyCode.F5))
		{
			TakeDamage(20);
		}
		
	}
	
	void TakeDamage(int damage)
	{
		currentHealth -= damage;
		
		healthBar.SetHealth(currentHealth);
	}
	
}