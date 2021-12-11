using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {
    Theunravelling controls;

    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private GameObject inGameMenu;

    [SerializeField]
    private GameObject HUD;

    [SerializeField]
    private Interactable _chest;

    // Global objects
    private Mouse mouse;
    private Camera currentCamera;

    private PlayerInput playerInput;

    private void Awake() {
        controls = new Theunravelling();

        playerInput = GetComponent<PlayerInput>();
 
        // Grab global objects
        mouse = Mouse.current;
        currentCamera = Camera.main;

        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");  
    }

    private void OnEnable() {
        playerInput.actions["Player/Inventory"].performed += OnOpenInventory;
        playerInput.actions["Player/Place"].performed += OnActionPlace;
        playerInput.actions["Player/Cancel"].performed += OnActionCancel;
        playerInput.actions["Player/Destroy"].performed += OnActionDamage;
        playerInput.actions["Player/Interact"].performed += OnActionInteract;

        playerInput.actions["UI/Cancel"].performed += OnCloseInventory;
    }

    private void OnDisable() {
        playerInput.actions["Player/Inventory"].performed -= OnOpenInventory;
        playerInput.actions["Player/Place"].performed -= OnActionPlace;
        playerInput.actions["Player/Cancel"].performed -= OnActionCancel;
        playerInput.actions["Player/Destroy"].performed -= OnActionDamage;
        playerInput.actions["Player/Interact"].performed -= OnActionInteract;

        playerInput.actions["UI/Cancel"].performed -= OnCloseInventory;
    }

    /// <summary>
    /// Function that can be called outside this class to activate inventory
    /// </summary>
    public void publicOpenInventory() {
        playerInput.SwitchCurrentActionMap("UI");
        playerInventory.ActivateInventory();
    }
    
    /// <summary>
    /// Function to open the inventory subscribed to input action
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnOpenInventory(InputAction.CallbackContext ctx) {
        publicOpenInventory();        
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    public void publicCloseInventory() {
        playerInput.SwitchCurrentActionMap("Player");
        playerInventory.DeactivateInventory();
        _chest?.CloseChestInventory();
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnCloseInventory(InputAction.CallbackContext ctx) {
        publicCloseInventory();
        inGameMenu.SetActive(false);
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionInteract(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Interact action!");
        RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(),Vector2.zero);
		foreach (RaycastHit2D hit in hits)
		if (hit.collider != null) {
            playerInput.SwitchCurrentActionMap("UI");
            _chest = hit.collider.GetComponent<Interactable>();
            _chest?.OpenChestInventory();
        }
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionPlace(InputAction.CallbackContext ctx) {
        playerInventory.PlaceObject();
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionCancel(InputAction.CallbackContext ctx) {
        if (playerInventory.previewCraft.activeSelf) {
            playerInput.SwitchCurrentActionMap("Player");
            playerInventory.CancelInventoryAction();
        } else {
            inGameMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    /// <param name="ctx">Input action callback for registering action</param>
    private void OnActionDamage(InputAction.CallbackContext ctx) {
		RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(),Vector2.zero);
		foreach (RaycastHit2D hit in hits)
		if (hit.collider != null) {
            hit.collider.GetComponent<IClickable>()?.OnDamage(playerInventory.player.entityDamage);
        }
	}

    /// <summary>
    /// Function for button to save and exit the game
    /// </summary>
    public void SaveGameAndExitButtonClick() {
        inGameMenu.SetActive(false);
        HUD.SetActive(false);
        GameData.Get.SaveWorld();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Function for button to resume the game
    /// </summary>   
    public void ResumeButtonClick() {
        inGameMenu.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
    }

    /// <summary>
    /// Function to get mouse position
    /// </summary>
    public Vector3 GetMousePosition() {
        // Grab the position of the mouse in screen space
        Vector3 mousePos = mouse.position.ReadValue();
        mousePos.z = 1.0f;

        // Convert to world space coordinates
        return currentCamera.ScreenToWorldPoint(mousePos);
    }
}
