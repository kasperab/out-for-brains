using UnityEngine;
using Unity.VisualScripting;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(StateMachine))]
public class Clickable : MonoBehaviour
{
	private StateMachine stateMachine;

	private void Start()
	{
		stateMachine = GetComponent<StateMachine>();
	}

	public void Click()
	{
		stateMachine.enabled = true;
	}
}
