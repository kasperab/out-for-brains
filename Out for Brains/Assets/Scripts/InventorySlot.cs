using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public Image itemImage;
	public Image infoImage;
	public Text infoText;
	public Player player;

	private Item item = null;
	private static Item draggedItem = null;
	private static bool isDragging = false;

	public void SetItem(Item newItem)
	{
		item = newItem;
		itemImage.sprite = item.image;
		itemImage.enabled = true;
		infoText.text = item.name + " - " + item.description;
	}

	public void RemoveItem()
	{
		item = null;
		itemImage.enabled = false;
		infoImage.enabled = false;
		infoText.enabled = false;
	}

	public bool HasItem()
	{
		return item;
	}

	public bool HasItem(Item compare)
	{
		return item == compare;
	}

	public void PointerEnter()
	{
		if (item && !isDragging)
		{
			infoImage.enabled = true;
			infoText.enabled = true;
		}
	}

	public void PointerExit()
	{
		infoImage.enabled = false;
		infoText.enabled = false;
	}

	public void BeginDrag()
	{
		if (item)
		{
			draggedItem = item;
			itemImage.GetComponent<Canvas>().overrideSorting = true;
			PointerExit();
			isDragging = true;
		}
		else
		{
			draggedItem = null;
			isDragging = false;
		}
	}

	public void Drag()
	{
		if (item)
		{
			itemImage.transform.position = Input.mousePosition;
		}
	}

	public void EndDrag()
	{
		itemImage.transform.localPosition = Vector3.zero;
		itemImage.GetComponent<Canvas>().overrideSorting = false;
		isDragging = false;
		player.DropItem(draggedItem);
	}

	public void Drop()
	{
		if (!item || !draggedItem)
		{
			draggedItem = null;
			return;
		}
		if (item.combineWith == draggedItem || draggedItem.combineWith == item)
		{
			Item result;
			if (item.combineResult)
			{
				result = item.combineResult;
			}
			else
			{
				result = draggedItem.combineResult;
			}
			Player.RemoveItem(item);
			Player.RemoveItem(draggedItem);
			Player.AddItem(result);
		}
		draggedItem = null;
	}
}
