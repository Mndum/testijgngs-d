using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Group", menuName = "Music/MusicGroup", order = 1)]
public class MusicGroup : ScriptableObject
{
	public List<MusicWithInfo> music;
}
