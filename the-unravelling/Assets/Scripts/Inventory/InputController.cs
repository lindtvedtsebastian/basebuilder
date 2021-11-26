using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private PlayerInput playerInput;

    private void Awake() {
        controls = new Theunravelling();

        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable() {
        controls.Player.Enable();
        controls.Player.Move.performed += OnMove;
        controls.Player.Inventory.performed += OnOpenInventory;
        controls.Player.Place.performed += OnActionPlace;
        controls.Player.Cancel.performed += OnActionCancel;

        controls.UI.Cancel.performed += OnCloseInventory;
    }

    private void OnMove(InputAction.CallbackContext ctx) {
        Vector2 moveInput = ctx.ReadValue<Vector2>();
        Debug.Log($"Move input: {moveInput}");
    }

    public void publicOpenInventory() {
        playerInput.SwitchCurrentActionMap("UI");
        playerInventory.ActivateInventory();
    }
    
    private void OnOpenInventory(InputAction.CallbackContext ctx) {
        Debug.Log("This will open the inventory");
        publicOpenInventory();        
    }

    public void publicCloseInventory() {
        playerInput.SwitchCurrentActionMap("Player");
        playerInventory.DeActivateInventory();
    }

    private void OnCloseInventory(InputAction.CallbackContext ctx) {
        Debug.Log("This will close the inventory");
        publicCloseInventory();
        inGameMenu.SetActive(false);
    }

    private void OnActionPlace(InputAction.CallbackContext ctx) {
        Debug.Log("This will place an object");
        playerInventory.PlaceObject();
    }

    private void OnActionCancel(InputAction.CallbackContext ctx) {
        Debug.Log("This will cancel an action");
        if (playerInventory.previewCraft.activeSelf) {
            playerInput.SwitchCurrentActionMap("Player");
            playerInventory.CancelInventoryAction();
        } else {
            inGameMenu.SetActive(true);
        }
    }

    public void SaveGameAndExitButtonClick() {
        inGameMenu.SetActive(false);
        HUD.SetActive(false);
        GameData.Get.SaveWorld();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    
    public void ResumeButtonClick() {
        inGameMenu.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
    }
}
