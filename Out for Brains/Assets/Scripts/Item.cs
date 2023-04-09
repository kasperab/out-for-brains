using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
	public new string name;
	public string description;
	public Sprite image;
	public Item combineWith;
	public Item combineResult;
}
