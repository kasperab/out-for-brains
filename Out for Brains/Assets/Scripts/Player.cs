using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
	public Texture2D cursorPointer;
	public Texture2D cursorHand;
	public int inventorySize;
	public InventorySlot inventorySlot;

	private NavMeshAgent agent;
	private Animator animator;
	private Interaction interaction = null;
	private ItemPickup itemPickup = null;
	private static InventorySlot[] inventorySlots;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		inventorySlots = new InventorySlot[inventorySize];
		inventorySlots[0] = inventorySlot;
		Transform inventoryTransform = inventorySlot.transform.parent;
		for (int index = 1; index < inventorySlots.Length; index++)
		{
			inventorySlots[index] = Instantiate(inventorySlot, inventoryTransform);
		}
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
		else if (itemPickup && !agent.hasPath)
		{
			AddItem(itemPickup.PickUp());
			itemPickup = null;
			animator.SetBool("moving", false);
		}
		else
		{
			Ray ray = CameraHandler.currentCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && !EventSystem.current.IsPointerOverGameObject())
			{
				if (hit.collider.GetComponent<Interaction>())
				{
					SetCursor(true);
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						interaction = hit.collider.GetComponent<Interaction>();
						itemPickup = null;
					}
				}
				else if (hit.collider.GetComponent<ItemPickup>())
				{
					SetCursor(true);
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						itemPickup = hit.collider.GetComponent<ItemPickup>();
						interaction = null;
					}
				}
				else
				{
					SetCursor();
					if (Input.GetMouseButtonDown(0))
					{
						agent.destination = hit.point;
						interaction = null;
						itemPickup = null;
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

	public static void AddItem(Item item)
	{
		for (int index = 0; index < inventorySlots.Length; index++)
		{
			if (!inventorySlots[index].HasItem())
			{
				inventorySlots[index].SetItem(item);
				return;
			}
		}
		Debug.LogError("Cannot add item " + item.name + " due to inventory being full");
	}

	public static void RemoveItem(Item item)
	{
		for (int index = 0; index < inventorySlots.Length; index++)
		{
			if (inventorySlots[index].HasItem(item))
			{
				inventorySlots[index].RemoveItem();
				break;
			}
		}
	}

	public static bool HasItem(Item item)
	{
		for (int index = 0; index < inventorySlots.Length; index++)
		{
			if (inventorySlots[index].HasItem(item))
			{
				return true;
			}
		}
		return false;
	}
}
