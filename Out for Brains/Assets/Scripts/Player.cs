using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
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
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						clicked = hit.collider.GetComponent<Clickable>();
					}
				}
				else
				{
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						clicked = null;
					}
				}
			}
		}
		animator.SetBool("moving", agent.hasPath);
	}
}
