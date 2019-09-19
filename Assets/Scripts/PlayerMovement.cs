using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float maxSpeed;
	[SerializeField] float jumpHeight;
	[SerializeField] float friction;
	Vector3 pos;
	Vector2 velocity;
	public float gravity;
	RaycastHit2D findFloor;
	bool grounded;


    // Start is called before the first frame update
    void Start()
    {
		pos = transform.position;
		velocity = new Vector2(0,0);
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
		pos = transform.position;
		float direction = Input.GetAxis("Horizontal") * speed;

		//Slows down and then stops the player when they stop moving.
		if (direction == 0) {
			velocity.x /= friction;
		}

		if (velocity.x < 0.001 && velocity.x > -0.001) {
			velocity.x = 0;
		}

		//Apply direction to the velocity
		velocity.x += direction * Time.deltaTime;
		velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

		//Flip the player according to the last direction pressed.
		//Fix this to not change when no movement is done.
		GetComponent<SpriteRenderer>().flipX = (direction > 0);

		//Find if grounded
		findFloor = Physics2D.Raycast(transform.position, -Vector2.up, 7.5f);
		grounded = (findFloor.collider != null);

		velocity.y = (grounded) ? 0 : velocity.y - gravity * Time.deltaTime;

		//Jumping
		if (Input.GetButtonDown("Jump") && grounded) {
			velocity.y = jumpHeight;
		}

		//Apply velocity changes to position.
		pos.x += velocity.x;
		pos.y += velocity.y;
		transform.position = pos;

    }
		
}
