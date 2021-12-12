using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInventoryDisplay : MonoBehaviour {
    public GameObject chestInventoryCanvas;
    public Transform _playerPanel;
    public Transform _chestPanel;
    private ItemSlot[] itemSlots;
    private StorageSlot[] storageSlots;

    void Awake() {
        itemSlots = _playerPanel.GetComponentsInChildren<ItemSlot>();
		storageSlots = _chestPanel.GetComponentsInChildren<StorageSlot>();
    }

    public void ActivateStorageInventory(InventoryWithStorage storage) {
        AddItems(storage);
        chestInventoryCanvas.SetActive(true);
    }

    public void DeactivateStorageInventory() {
        chestInventoryCanvas.SetActive(false);
    }

    private void AddItems(InventoryWithStorage storage) {
        storage.removeEmpty();

        for(int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].ClearData();
            if(i < storage.items.Count) {
                itemSlots[i].AddItem(storage.items[i]);
            }
        }
    }

    /// <summary>
    /// Development function to check the inventory content
    /// </summary>
    /// <param name="storage">The storage to check content</param>
    public void InventoryContent(InventoryWithStorage storage) {
        for (int i = 0; i < storage.items.Count; i++) {
            if(storage.items[i] == null) return;
            Debug.Log("Item count : " + i + " is -> " + storage.items[i].item.itemName + 
                                            " count -> " + storage.items[i].amount);
        }
    }
}
