using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

	private Animator anim;
	private CharacterController controller;

	public float speed = 600.0f;
	public float turnSpeed = 400.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;
	public float jumpSpeed = 20f;
	public Quaternion rotationLeft;
	public Quaternion rotationRight;
	public Quaternion rotationForward;
	public Quaternion rotationBack;
	void Start () 
	{
		controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update ()
	{
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) 
		{
			anim.SetInteger ("AnimationPar", 1);
		}  
		else 
		{
			anim.SetInteger ("AnimationPar", 0);
		}

		//if(Input.GetKeyDown(KeyCode.W))
  //      {
		//	rotationForward = transform.rotation;
		//	return;
  //      }
		//if (Input.GetKeyDown(KeyCode.S))
		//{
		//	rotationBack = transform.rotation;
		//	return;
		//}
		//if (Input.GetKeyDown(KeyCode.A))
		//{
		//	rotationLeft = transform.rotation;
		//	return;
		//}
		//if (Input.GetKeyDown(KeyCode.D))
		//{
		//	rotationRight = transform.rotation;
		//	return;
		//}

		Quaternion turn = transform.rotation;
		moveDirection = Vector3.zero;
		print(transform.rotation.eulerAngles);

		if (Input.GetAxis("Vertical") > 0)
        {
			
				moveDirection = Vector3.Lerp(Vector3.forward * Input.GetAxis("Vertical") * speed, moveDirection, 0.5f);
				turn = Quaternion.Lerp(turn, rotationForward, 0.05f);
			
        }
		if (Input.GetAxis("Vertical") < 0)
		{
			
				moveDirection = Vector3.Lerp(Vector3.forward * Input.GetAxis("Vertical") * speed, moveDirection, 0.5f);
				turn = Quaternion.Lerp(turn, rotationBack, 0.05f);
			
		}
		if (Input.GetAxis("Horizontal") > 0)
		{
			
				moveDirection = Vector3.Lerp(Vector3.right * Input.GetAxis("Horizontal") * speed, moveDirection, 0.5f);
				turn = Quaternion.Lerp(turn, rotationRight, 0.05f);
			
		}
		if (Input.GetAxis("Horizontal") < 0)
		{
			
				moveDirection = Vector3.Lerp(Vector3.right * Input.GetAxis("Horizontal") * speed, moveDirection, 0.5f);
				turn = Quaternion.Lerp(turn, rotationLeft, 0.05f);
			
		}
		transform.rotation = turn;
		//transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		controller.Move(moveDirection * Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;
	}
}
