using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroMovement : MonoBehaviour
{
	public VirtualJoystick joystick;
	public float moveSpeed = 7.5f;
	public Vector3 MoveVector{set;get;}
	public float drag = 0.5f;
	public float terminalRotationSpeed = 25.0f;
	public float rotationAngle = 0f;
	public float dirAngle = 0f;
	private bool moving = true;
	private float m_Radius = 3f;
	[SerializeField]
	private Rigidbody heroRB;
	
	// For animations
	[SerializeField]
	private GameObject playerModel;

	// Use this for initialization
	private void Start ()
	{
		//thisRigidbody = gameObject.AddComponent<Rigidbody>();
		//thisRigidbody.maxAngularVelocity = terminalRotationSpeed;
		//thisRigidbody.drag = drag;
	}
	
	// Update is called once per frame
	private	void FixedUpdate ()
	{
		Collider[] collisionColliders = Physics.OverlapSphere(transform.position,m_Radius);
		MoveVector = PoolInput();
		
		Move();
	}
	private void Move()
	{
		if(moving)
		{
			// float horizontal = Input.GetAxis("Vertical");
			transform.LookAt(transform.position + MoveVector);
			// Vector3 movement = new Vector3(horizontal + moveSpeed * Time.deltaTime, 0, moveSpeed * Time.deltaTime);
			if (MoveVector.z != 0f || MoveVector.x != 0f)
			{
				// transform.position += transform.forward * Time.deltaTime * moveSpeed;
				// heroRB.MovePosition(transform.position + movement);
				playerModel.GetComponent<Animator>().Play("Walk");
				heroRB.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed);
				// heroRB.MovePosition(Vector3.forward * Time.deltaTime * 6f);
				// transform.Translate( Vector3.forward * Time.deltaTime * 6f);
			}
			else
			{
				playerModel.GetComponent<Animator>().Play("Idle");
			}
		}
		
		//thisRigidbody.MovePosition(MoveVector);
	}
	private Vector3 PoolInput()
	{
		Vector3 dir = Vector3.zero;
		
		dir.x = joystick.Horizontal();
		dir.z = joystick.Vertical();

		
		if(dir.magnitude > 1)
			dir.Normalize();
		
		
		return dir;
	}
	public void heroMovementOnOff()
	{
		moving = !moving;
	}
}
