using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))] //requires a slider component
public class HealthHUD : MonoBehaviour 
{
    public Text nameField; //name of the player
	public Image playerPortrait; //portrait of the player
	public Slider HpSlider; //slider for the health
	public bool isPlayer; //is this the player?

	void OnEnable() { //when enabled
		HealthSystem.onHealthChange += UpdateHealth; //subscribe to the onHealthChange event
	}

	void OnDisable() { //when disabled
		HealthSystem.onHealthChange -= UpdateHealth; //unsubscribe to the onHealthChange event
	} 

	void Start(){ //when the game starts
		if(isPlayer) SetPlayerPortraitAndName(); //if this is the player, set the portrait and name
	}

	void UpdateHealth(float percentage, GameObject go){ //when the health changes
		if(isPlayer && go.CompareTag("Player")){ //if this is the player and the gameobject is the player
			HpSlider.value = percentage; //set the health slider to the percentage
		} 	

		if(!isPlayer && go.CompareTag("Enemy")){ //if this is not the player and the gameobject is an enemy
			HpSlider.gameObject.SetActive(true); //set the health slider to active
			HpSlider.value = percentage; //set the health slider to the percentage
			nameField.text = "Blitz"; //set the name to the enemy name
			if(percentage == 0) Invoke("HideOnDestroy", 2); //if the percentage is 0, hide the health bar
		}
	}

	void HideOnDestroy(){ //when the enemy is destroyed
		HpSlider.gameObject.SetActive(false); //set the health slider to inactive
		nameField.text = ""; //set the name to nothing
	}

	//loads the HUD icon of the player from the player prefab (Healthsystem)
	void SetPlayerPortraitAndName(){ //when the player is set
		if(playerPortrait != null){ //if the player portrait is not null
			GameObject player = GameObject.FindGameObjectWithTag("Player"); //find the player
			HealthSystem healthSystem = player.GetComponent<HealthSystem>(); //get the health system of the player

			if(player && healthSystem != null){ //if the player and health system are not null

				//set portrait
				Sprite HUDPortrait = healthSystem.HUDPortrait; //set the HUD portrait to the health system portrait
				playerPortrait.overrideSprite = HUDPortrait; //override the player portrait with the HUD portrait

				//set name
				nameField.text = healthSystem.PlayerName; //set the name to the health system name
			}
		}
	}
}
