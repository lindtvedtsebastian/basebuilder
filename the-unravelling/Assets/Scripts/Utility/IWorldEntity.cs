using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "IWorldEntity", menuName = "Entity/IWorldEntity")]
public class IWorldEntity : ComponentEntity {
	public Drop[] drops;
}

[Serializable]
public class Drop {
	public GameObject dropObject;
	public int amount;
}