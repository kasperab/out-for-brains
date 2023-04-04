using UnityEngine;
using Unity.VisualScripting;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(StateMachine))]
public class Interaction : MonoBehaviour
{
	public static bool Interacting { get; private set; } = false;
	private StateMachine stateMachine;
	private static StateMachine currentStateMachine;

	private void Start()
	{
		stateMachine = GetComponent<StateMachine>();
	}

	public void Interact()
	{
		Interacting = true;
		stateMachine.enabled = true;
		currentStateMachine = stateMachine;
	}

	public static void End()
	{
		Interacting = false;
		currentStateMachine.enabled = false;
	}
}
