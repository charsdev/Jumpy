using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WayPoint : MonoBehaviour
{
    public bool AutomaticMovement = false;

    public WayPoint Up ;
    public WayPoint Down;
    public WayPoint Left;
    public WayPoint Right;

	public virtual bool CanGoUp()
	{
		if (Up != null) { return true; } else { return false; }
	}

	public virtual bool CanGoRight()
	{
		if (Right != null) { return true; } else { return false; }
	}

	public virtual bool CanGoDown()
	{
		if (Down != null) { return true; } else { return false; }
	}
	
	public virtual bool CanGoLeft()
	{
		if (Left != null) { return true; } else { return false; }
	}

	public virtual void OnTriggerStay2D(Collider2D collider)
	{
		LevelMapCharacter mapCharacter = collider.GetComponent<LevelMapCharacter>();
		if (mapCharacter == null)
			return;

		// we tell our character it's now colliding with a path element
		mapCharacter.CollidingWithAPathElement = true;
		mapCharacter.SetCurrentPathElement(this);

		// if our path element is on automatic, we'll direct our character to its next target
		if (AutomaticMovement)
		{
			if (mapCharacter.LastVisitedPathElement != Up && Up != null) { mapCharacter.SetDestination(Up); }
			if (mapCharacter.LastVisitedPathElement != Right && Right != null) { mapCharacter.SetDestination(Right); }
			if (mapCharacter.LastVisitedPathElement != Down && Down != null) { mapCharacter.SetDestination(Down); }
			if (mapCharacter.LastVisitedPathElement != Left && Left != null) { mapCharacter.SetDestination(Left); }
		}
	}


	public virtual void OnTriggerExit2D(Collider2D collider)
	{
		//LevelMapCharacter mapCharacter = collider.GetComponent<LevelMapCharacter>();
		//if (mapCharacter == null)
		//	return;

		//GUIManager.Instance.GetComponent<LevelSelectorGUI>().TurnOffLevelName();

		//// we tell our character it's now not colliding with any path element
		//mapCharacter.CollidingWithAPathElement = false;
		//mapCharacter.SetCurrentPathElement(null);
		//mapCharacter.LastVisitedPathElement = this;
	}


	public virtual void OnTriggerEnter2D(Collider2D collider)
	{
		//LevelMapCharacter mapCharacter = collider.GetComponent<LevelMapCharacter>();
		//if (mapCharacter == null)
		//	return;

		//if (GetComponent<LevelSelector>() != null)
		//{
		//	if (GUIManager.Instance.GetComponent<LevelSelectorGUI>() != null)
		//	{
		//		GUIManager.Instance.GetComponent<LevelSelectorGUI>().SetLevelName(GetComponent<LevelSelector>().LevelName.ToUpper());
		//	}
		//}
	}

	protected virtual void OnDrawGizmos()
	{
		if (Up != null)
		{
			if (Up.Down == this) { Gizmos.color = Color.blue; } else { Gizmos.color = Color.red; }
			Gizmos.DrawLine(transform.position, Up.transform.position);
		}
		if (Right != null)
		{
			if (Right.Left == this) { Gizmos.color = Color.blue; } else { Gizmos.color = Color.red; }
			Gizmos.DrawLine(transform.position, Right.transform.position);
		}
		if (Down != null)
		{
			if (Down.Up == this) { Gizmos.color = Color.blue; } else { Gizmos.color = Color.red; }
			Gizmos.DrawLine(transform.position, Down.transform.position);
		}
		if (Left != null)
		{
			if (Left.Right == this) { Gizmos.color = Color.blue; } else { Gizmos.color = Color.red; }
			Gizmos.DrawLine(transform.position, Left.transform.position);
		}
	}
}
