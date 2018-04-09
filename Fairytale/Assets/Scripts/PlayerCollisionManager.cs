using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour {

    private readonly ContactPoint2D[] contactPointBuffer = new ContactPoint2D[1024];

    private Collider2D col;

    private Dictionary<Collider2D, Vector2> otherCols = new Dictionary<Collider2D, Vector2>();

    private bool collidingClimbableLeft;
    private bool collidingClimbableRight;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider2D>();		
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
        otherCols.Add(collision, transform.position - collision.bounds.center);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        otherCols.Remove(collision);
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
}
