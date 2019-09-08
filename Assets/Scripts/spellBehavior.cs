using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellBehavior : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private Rigidbody playerRB;
	[SerializeField]
	private bool isActivated;
	[SerializeField]
	private Transform player;
	[SerializeField]
	private Transform enemy;
	[SerializeField]
	private Transform spellParent;
	
	[SerializeField]
	private bool size_X;
	[SerializeField]
	private float size_X_speed;
	[SerializeField]
	private bool size_Z;
	[SerializeField]
	private float size_Z_speed;
	[SerializeField]
	private bool moveSpeed;
	[SerializeField]
	private float moveSpeed_ratio;
	[SerializeField]
	private bool explode_onTrigger;
	[SerializeField]
	private bool distance;
	[SerializeField]
	private float distance_ratio;
	[SerializeField]
	private bool movementAbility;
	[SerializeField]
	private float movementAbility_Speed;
	
	private bool spellPosition;
	private bool movementActivate = false;
	
	private Vector3 initPosition;
	private Vector3 initSize;
	
	void Start () {
		
		if(player == null)
		{
			player = enemy;
		}
		initPosition = transform.position;
		initSize = transform.localScale;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	if(isActivated)
		{
		if(distance)
		{
			if(spellPosition)
			{
				transform.localPosition = new Vector3(0, 0f, distance_ratio);
				spellPosition = false;
			}
		}	
		if(movementAbility)
		{
			// Because enemyMovement and playerMovement works differently
			// And same script is used on both Player and Enemy abilities
			// Checking if enemy is null, will decide if we call Enemy or HeroMovement
			if(enemy == null)
			{
				player.GetComponent<HeroMovement>().heroMovementOnOff();
			}
			else
			{
				player.GetComponent<EnemyMovement>().enemyMovementOnOff();
			}
			player.LookAt(transform.position);
			movementActivate = true;
			playerRB.MovePosition(player.transform.position + player.transform.forward * Time.deltaTime * movementAbility_Speed);
		}
		
		transform.parent = null;
		
		if(size_X)
		{
			transform.localScale += new Vector3(size_X_speed , 0 , 0);
		}
		if(size_Z)
		{
			transform.localScale += new Vector3(0 , 0 , size_Z_speed);
		}
		if(moveSpeed)
		{
			transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed_ratio);	
		}

		
	
		}
	}
	public void ActivateObject()
	{
		if(player == null)
		{
			player = enemy;
		}

		spellPosition = true;
		transform.parent = player;
		isActivated = true;
	
		// transform.rotation = Quaternion.identity;
		transform.eulerAngles = player.transform.eulerAngles;
		transform.position = initPosition;
		// transform.localScale = initSize;
		// transform.localScale = new Vector3(2f, 7f, 2f);
	}
}
