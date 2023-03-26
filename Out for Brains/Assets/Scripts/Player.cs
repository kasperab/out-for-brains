using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
	private NavMeshAgent agent;
	private Animator animator;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		Ray ray = CameraHandler.currentCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
		{
			if (Input.GetMouseButtonDown(0))
			{
				agent.destination = hit.point;
			}
		}
		animator.SetBool("moving", agent.hasPath);
	}
}
