using UnityEngine;

public class HealthSystem : MonoBehaviour {

	[Header("Health Settings")] 
	public int MaxHp = 20; // Max health
	public int CurrentHp = 20; // Current health
	public bool invulnerable; // Is the player invulnerable?

	[Header("Healthbar Settings")] 
	public Sprite HUDPortrait; // Player portrait
	public string PlayerName; // Player name

	public delegate void OnHealthChange(float percentage, GameObject GO); // Health change event
	public static event OnHealthChange onHealthChange; // Health change event

	void Start(){ // Start
		SendHealthUpdateEvent(); // Send health update event
	}

	//substract health
	public void SubstractHealth(int damage){ // Substract health
		if(!invulnerable){ // If not invulnerable

			//reduce hp
			CurrentHp = Mathf.Clamp(CurrentHp -= damage, 0, MaxHp); // Reduce health

			//Health reaches 0
			if (isDead()) gameObject.SendMessage("Death", SendMessageOptions.DontRequireReceiver); // If dead, send death message
		}

		SendHealthUpdateEvent(); // Send health update event
	}

	//add health
	public void AddHealth(int amount){ // Add health
		CurrentHp = Mathf.Clamp(CurrentHp += amount, 0, MaxHp); // Add health
		SendHealthUpdateEvent(); // Send health update event
	}

	//health update event
	void SendHealthUpdateEvent(){ // Send health update event
		float CurrentHealthPercentage = 1f/MaxHp * CurrentHp; // Calculate health percentage
		if(onHealthChange != null) onHealthChange(CurrentHealthPercentage, gameObject); // Send health update event
	}

	//death
	bool isDead(){ // Is dead?
		return CurrentHp == 0; // Return true if health is 0, otherwise return false
	}
}
