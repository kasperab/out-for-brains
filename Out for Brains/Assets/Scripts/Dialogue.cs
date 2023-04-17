using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
	private Image background;
	public Image box;
	public Text text;
	public Image nameBox;
	public Text nameText;
	private static Dialogue dialogue;

	private void Start()
	{
		background = GetComponent<Image>();
		background.enabled = false;
		box.enabled = false;
		text.enabled = false;
		nameBox.enabled = false;
		nameText.enabled = false;
		dialogue = this;
	}

	public static void Say(string text, string name)
	{
		dialogue.background.enabled = true;
		dialogue.box.enabled = true;
		dialogue.text.enabled = true;
		dialogue.text.text = text.Replace("<br>", "\n");
		if (!string.IsNullOrEmpty(name))
		{
			dialogue.nameBox.enabled = true;
			dialogue.nameText.enabled = true;
			dialogue.nameText.text = name;
		}
		else
		{
			dialogue.nameBox.enabled = false;
			dialogue.nameText.enabled = false;
		}
	}

	public static void Hide()
	{
		dialogue.background.enabled = false;
		dialogue.box.enabled = false;
		dialogue.text.enabled = false;
		dialogue.nameBox.enabled = false;
		dialogue.nameText.enabled = false;
	}
}
