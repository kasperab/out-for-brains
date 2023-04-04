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
	private Interaction interaction = null;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Interaction.Interacting)
		{
			return;
		}
		if (interaction && !agent.hasPath)
		{
			interaction.Interact();
			interaction = null;
			animator.SetBool("moving", false);
			SetCursor();
		}
		else
		{
			Ray ray = CameraHandler.currentCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
			{
				if (hit.collider.GetComponent<Interaction>())
				{
					SetCursor(true);
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						interaction = hit.collider.GetComponent<Interaction>();
					}
				}
				else
				{
					SetCursor();
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						interaction = null;
					}
				}
			}
			else
			{
				SetCursor();
			}
			animator.SetBool("moving", agent.hasPath);
		}
	}

	private void SetCursor(bool hand = false)
	{
		if (hand)
		{
			Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.Auto);
		}
		else
		{
			Cursor.SetCursor(cursorPointer, Vector2.zero, CursorMode.Auto);
		}
	}
}
