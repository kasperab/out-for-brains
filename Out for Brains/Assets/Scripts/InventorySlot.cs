using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public Image itemImage;
	public Image infoImage;
	public Text infoText;

	public void SetItem(Item item)
	{
		itemImage.sprite = item.image;
		itemImage.enabled = true;
		infoText.text = item.name + " - " + item.description;
	}

	public void RemoveItem()
	{
		itemImage.enabled = false;
		infoImage.enabled = false;
		infoText.enabled = false;
	}

	public void PointerEnter()
	{
		if (itemImage.enabled)
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
}
