using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
 {
	 
    private GameObject target;//set target from inspector instead of looking in Update
	private GameObject portal;
    private float speed = 3f;
	private float range_Enemy;
	private float currentSteed = 3f;
	private float stantardSpeed = 3f;
	private float m_Position = 0f;
	private float m_Radius = 3f;
	private bool moving = true;
	[SerializeField]
	private Rigidbody enemyRB;
	
	private IEnumerator waitTime;
	
	private GameObject hitEffect;
	
	
 
 
    void Start ()
	{
		target = GameObject.FindWithTag("Player");
		portal = GameObject.FindWithTag("portal");
		range_Enemy = gameObject.GetComponent<Enemy>().getRangeEnemy();
    }
 
    void FixedUpdate()
	{
	if(moving)
	{
		Collider[] collisionColliders = Physics.OverlapSphere(transform.position,m_Radius);
			if (Vector3.Distance(transform.position, target.transform.position) < range_Enemy)
				{
					// if speed 0, means target is next to Player, Makes Autoattack call and hits enemy
					// enemyRB.MovePosition(0f);
					// speed = 0f;
					transform.LookAt(target.transform.position);
					gameObject.GetComponent<Enemy>().AttackEnemy(target.transform);
				}
			
			else
			{
				if (Vector3.Distance(transform.position, target.transform.position) > 20f)
					{
						if(Vector3.Distance(transform.position, target.transform.position) > 60f)
						{
							transform.LookAt(portal.transform.position);
							// transform.Translate(Vector3.forward * Time.deltaTime * 0f);
							enemyRB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
						
						}
						else
						{	// if Player gets too far away, starts focusing on Portal
							transform.LookAt(portal.transform.position);
							enemyRB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
							// transform.Translate(Vector3.forward * Time.deltaTime * speed);
							if (Vector3.Distance(transform.position, portal.transform.position) < 4f)
							{
								gameObject.GetComponent<Enemy>().enemySurvive();
							}
						}
					}
				else
					{
						transform.LookAt(target.transform.position);
						enemyRB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);

					}
			}
	}
    }
	public void slowEnemy(float slowRate, float slowTime)
	{
		gameObject.transform.GetComponent<dotManager>().activateFirstInLine(slowTime);
		speed = slowRate * speed;
		waitTime = slowTimeNumerator(slowTime);
		StartCoroutine(waitTime);
	}
	private IEnumerator slowTimeNumerator(float slowTime)
	{
		yield return new WaitForSeconds(slowTime);
		speed = stantardSpeed;
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		//Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
		Gizmos.DrawWireSphere (transform.position, m_Radius);
	}
	public void enemyMovementOnOff()
	{
		moving = !moving;
	}
 
 }