using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public Image itemImage;
	public Image infoImage;
	public Text infoText;

	private Item item = null;
	private static Item draggedItem = null;

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
		if (itemImage.enabled && !draggedItem)
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
		if (itemImage.enabled)
		{
			draggedItem = item;
			itemImage.GetComponent<Canvas>().overrideSorting = true;
			PointerExit();
		}
	}

	public void Drag()
	{
		if (itemImage.enabled)
		{
			itemImage.transform.position = Input.mousePosition;
		}
	}

	public void EndDrag()
	{
		itemImage.transform.localPosition = Vector3.zero;
		itemImage.GetComponent<Canvas>().overrideSorting = false;
	}

	public void Drop()
	{
		if (!itemImage.enabled || !draggedItem)
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
