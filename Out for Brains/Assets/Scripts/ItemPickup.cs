using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
	public Item item;

	public Item PickUp()
	{
		gameObject.SetActive(false);
		return item;
	}
}
