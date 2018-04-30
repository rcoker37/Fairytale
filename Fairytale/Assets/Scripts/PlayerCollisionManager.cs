using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisionManager : MonoBehaviour {

    public bool AtCheckpoint;
    public bool AtGiantFallZone;

	private readonly ContactPoint2D[] contactPointBuffer = new ContactPoint2D[1024];

	private Collider2D col;

	private Dictionary<Collider2D, Vector2> otherCols = new Dictionary<Collider2D, Vector2>();

	private bool collidingClimbableLeft;
	private bool collidingClimbableRight;
	private List<GameObject> grounds;

	private void Start()
	{
		col = GetComponent<Collider2D>();
		grounds = new List<GameObject>();
	}

	public bool IsColliding(Vector2 direction, bool requirePushable, bool requireClimbable)
	{
		return GetColliding(direction, requirePushable, requireClimbable) != null;
	}

	public Collider2D GetColliding(Vector2 direction, bool requirePushable, bool requireClimbable)
	{
		foreach (Collider2D otherCol in otherCols.Keys)
		{

			if (FacingDirection(otherCol, direction))
			{
				Interactable interactable = otherCol.gameObject.GetComponent<Interactable>();
				bool pushable = interactable != null && interactable.Pushable;
				bool climbable = interactable != null && interactable.Climbable;
				if ((!requirePushable || pushable) &&
					(!requireClimbable || climbable))
				{
					return otherCol;
				}
			}
		}

		return null;
	}

	private bool FacingDirection(Collider2D otherCol, Vector2 direction)
	{
		Vector2 playerPosition = otherCols[otherCol];
		return Vector2.Dot(playerPosition, direction) < -Mathf.Abs(Vector2.Dot(otherCol.bounds.extents, direction));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//otherCols.Add(collision, transform.position - collision.bounds.center);
		otherCols[collision] = transform.position - collision.bounds.center;

		bool grounded = gameObject.GetComponent<PlayerControllerManager>().activeState == PlayerControllerManager.State.GROUNDED;
		if (collision.gameObject.CompareTag("HidingSpot") && grounded)
		{
			collision.gameObject.GetComponent<HidingSpot>().canvas.SetActive(true);
		}

		if (collision.gameObject.CompareTag("Tutorial"))
		{
			collision.gameObject.GetComponent<TutorialText>().canvas.SetActive(true);
		}

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            AtCheckpoint = true;
        }

        if (collision.gameObject.CompareTag("GiantFallZone"))
        {
            AtGiantFallZone = true;
        }

		if (collision.gameObject.CompareTag("NextLevel"))
		{
			//TODO set loading screen active
			int sceneIndex = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(sceneIndex + 1);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		otherCols.Remove(collision);

		if (collision.gameObject.CompareTag("HidingSpot"))
		{
			collision.gameObject.GetComponent<HidingSpot>().canvas.SetActive(false);
		}

        if (collision.gameObject.CompareTag("Checkpoint")){
            AtCheckpoint = false; 
        }

        if (collision.gameObject.CompareTag("GiantFallZone"))
        {
            AtGiantFallZone = false;
        }

		if (collision.gameObject.CompareTag("Tutorial"))
		{
			collision.gameObject.GetComponent<TutorialText>().canvas.SetActive(false);
		}
	}

	public void ShowHiding(bool show)
	{
		foreach (Collider2D col in otherCols.Keys)
		{
			if (col.gameObject.CompareTag("HidingSpot"))
			{
				col.gameObject.GetComponent<HidingSpot>().canvas.SetActive(show);
			}
		}
	}
	
	private float NormalDot(ContactPoint2D contact)
	{
		Vector2 normal = contact.normal;
		return Vector2.Dot(normal, Vector2.down);
	}

	private float[] NormalDotList(Collision2D collision)
	{
		float[] dots = new float[collision.contacts.Length];
		for (int i = 0; i < collision.contacts.Length; i++)
		{
			dots[i] = NormalDot(collision.contacts[i]);
		}

		return dots;
	}
	
	private bool IsGround(Collision2D collision)
	{
		if (collision == null) return false;
        float[] dots = NormalDotList(collision);
        foreach (float dot in dots)
        {
            if (dot < 0)
            {
                return true;
            }
        }
        return false;
		//return FacingDirection(collision.collider, Vector2.down); //TODO: fix
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (IsGround(collision))
		{
			grounds.Add(collision.gameObject);
		}

		//print("HIT " + IsGrounded() + " with " + collision.gameObject.name);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		grounds.Remove(collision.gameObject);

		//print("UNHIT " + IsGrounded() + " with " + collision.gameObject.name);
	}

	public bool CanHide()
	{
		foreach(Collider2D col in otherCols.Keys)
		{
			if (col.gameObject.CompareTag("HidingSpot"))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsGrounded()
	{
		return grounds.Count > 0;
	}
}
