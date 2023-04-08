using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
	public string name;
	public string description;
	public Texture2D image;
	public Item combineWith;
	public Item combineResult;
}
