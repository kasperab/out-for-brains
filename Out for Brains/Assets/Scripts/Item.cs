using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
	public string description;
	public Sprite image;
	public Item combineWith;
	public Item combineResult;
}
