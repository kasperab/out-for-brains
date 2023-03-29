using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
	public Texture2D cursorPointer;
	public Texture2D cursorHand;

	private NavMeshAgent agent;
	private Animator animator;
	private Clickable clicked = null;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (clicked && !agent.hasPath)
		{
			clicked.Click();
			clicked = null;
		}
		else
		{
			Ray ray = CameraHandler.currentCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
			{
				if (hit.collider.GetComponent<Clickable>())
				{
					Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.Auto);
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						clicked = hit.collider.GetComponent<Clickable>();
					}
				}
				else
				{
					Cursor.SetCursor(cursorPointer, Vector2.zero, CursorMode.Auto);
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						clicked = null;
					}
				}
			}
			else
			{
				Cursor.SetCursor(cursorPointer, Vector2.zero, CursorMode.Auto);
			}
		}
		animator.SetBool("moving", agent.hasPath);
	}
}
