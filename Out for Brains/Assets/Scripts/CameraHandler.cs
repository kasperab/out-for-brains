using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CameraHandler : MonoBehaviour
{
	public static Camera currentCamera = null;
	public new Camera camera;
	public bool startCamera = false;

	private void Start()
	{
		if (startCamera && !currentCamera)
		{
			camera.enabled = true;
			currentCamera = camera;
		}
		else
		{
			camera.enabled = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<NavMeshAgent>())
		{
			currentCamera.enabled = false;
			camera.enabled = true;
			currentCamera = camera;
		}
	}
}
