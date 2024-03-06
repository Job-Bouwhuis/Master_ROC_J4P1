// Creator: job

using ShadowUprising.Items;
using ShadowUprising.UI.Loading;
using ShadowUprising.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using WinterRose;
using static ShadowUprising.Inventory.InventoryInteractionResult;

#nullable enable

namespace ShadowUprising.Inventory
{
    [DontDestroyOnLoad]
    public class InventoryManager : Singleton<InventoryManager>
    {
        public readonly struct InventoryInteractResult
        {
            public InventoryInteractionResult Status { get; }
            public string Message { get; }
            public Item? Item { get; }

            public InventoryInteractResult(InventoryInteractionResult success, string message, Item? item)
            {
                Status = success;
                Message = message;
                Item = item;
            }
        }

        [Header("Settings"), Tooltip("The amount of unique itens that are allowed in the inventory. this also dictates how many slots the inventory will show")]
        public int maxUniqueItems = 6;
        [Tooltip("The amount of units between each pixel. change if clipping occures")]
        public int slotSpacing = 55;
        [Tooltip("The prefab for the slot in the inventory")]
        public GameObject slotPrefab;
        [Tooltip("The UI canvas that the inventory is on")]
        public GameObject uiCanvas;
        [Tooltip("The parent of the slots in the inventory")]
        public Transform slotParent;
        [Tooltip("The speed at which the slots will animate in and out when this is needed")]
        public float slotAnimationSpeed = 1.2f;

        /// <summary>
        /// All items that can be found in the game<br></br><br></br>
        /// 
        /// <b>This list automatically updates when an item is added to the inventory. <br></br>
        /// when a given item is not yet encountered using the inventory system before, the item will be added and the item index of the item will be set automaitcally</b>
        /// </summary>
        [Header("DEBUG. DO NOT CHANGE"), Tooltip(
            "All items that can be found in the game. Does not need to be filled in editor time, " +
            "whenever an item is added to the inventory through any means of " +
            "pickup it will be added to this list if it doesnt already exist")]
        public List<Item> allItems = new();
        [Tooltip("All the items that are currently in the players inventory")]
        public List<Item> playerInventory = new();
        [Tooltip("All the slots in the inventory")]
        public List<Slot> invSlots = new();

        public UnityEvent<InventoryInteractResult> OnInventoryInteract { get; private set; } = new UnityEvent<InventoryInteractResult>();

        /// <summary>
        /// The amount of unique items in the inventory
        /// </summary>
        public int Count => playerInventory.Count;

        /// <summary>
        /// The item that is currently selected in the inventory.
        /// May be null if no item is selected
        /// </summary>
        public Item? SelectedItem => selectedItem;
        [SerializeField] private Item? selectedItem;

        [SerializeField] private float slotsStartY = 70;
        private Vector3 inventoryNormalPos;
        private Vector3 inventoryHiddenPos;

        protected override void Awake()
        {
            SetupReferenceChecks();

            base.Awake();

            inventoryNormalPos = new Vector3(slotParent.position.x, slotsStartY, slotParent.position.z);
            inventoryHiddenPos = new Vector3(slotParent.position.x, -slotsStartY, slotParent.position.z);

            SceneManager.sceneLoaded += OnNewSceneLoad;

            if (LoadingScreen.Instance != null)
            {
                LoadingScreen.Instance.OnLoadingComplete.AddListener(() =>
                {
                    StartCoroutine(AnimateInventoryIn());
                });

                LoadingScreen.Instance.OnStartLoading.Subscribe(() =>
                {
                    StartCoroutine(AnimateInventoryOut());
                    return 0.3f;
                });
            }
            else
            {
                // set inventory pos to visible
                slotParent.position = inventoryNormalPos;
                slotParent.gameObject.SetActive(true);
            }

            // call the TypeWorker.FindType method to make sure all required assemblies for it are loaded
            // this helps to prevent a big lag spike when any item is interacted with for the first time.
            TypeWorker.FindType(nameof(Vector3));

            foreach (int i in maxUniqueItems)
            {
                GameObject slot = Instantiate(slotPrefab, slotParent);

                // move slot right by 55 pixels for each slot
                slot.transform.transform.position = new Vector3(slot.transform.position.x + (slotSpacing * i), slot.transform.position.y, slot.transform.position.z);

                Slot s = slot.GetComponent<Slot>();
                s.Init(i, this);
                invSlots.Add(s);
            }

            invSlots[0].IsSelected = true;

            Log.Push("Inventory initialized.");
        }
        
        private void SetupReferenceChecks()
        {
            if (slotPrefab == null)
            {
                Windows.MessageBox("Slot prefab is not set in the inventory manager", "Error", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Error);
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }
            if (uiCanvas == null)
            {
                Windows.MessageBox("UI canvas is not set in the inventory manager", "Error", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Error);
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }
            if (slotParent == null)
            {
                Windows.MessageBox("Slot parent is not set in the inventory manager", "Error", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Error);
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }
        }

        private void Update()
        {
            OemKeybinds();
            InteractKeybind();
            Scrolling();
            ApplyItemsToSlots();
        }

        private void ApplyItemsToSlots()
        {
            // loop over all items in the inventory, and let the slot know about its item
            for (int i = 0; i < invSlots.Count; i++)
            {
                if (i < playerInventory.Count)
                {
                    invSlots[i].SetITem(playerInventory[i]);
                }
                else
                {
                    invSlots[i].item = null;
                    invSlots[i].Clear();
                }
            }
        }

        private void Scrolling()
        {
            // scroll logic for switching selected slots using function SelectIndex
            if (Input.mouseScrollDelta.y < 0)
            {
                for (int i = 0; i < invSlots.Count; i++)
                {
                    Slot slot = invSlots[i];
                    if (slot.IsSelected)
                    {
                        if (i == invSlots.Count - 1)
                        {
                            SelectIndex(0);
                            slot.IsSelected = false;
                        }
                        else
                        {
                            SelectIndex(i + 1);
                            slot.IsSelected = false;
                        }
                        break;
                    }
                }
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                for (int i = 0; i < invSlots.Count; i++)
                {
                    Slot slot = invSlots[i];
                    if (slot.IsSelected)
                    {
                        if (i == 0)
                        {
                            SelectIndex(invSlots.Count - 1);
                            slot.IsSelected = false;
                        }
                        else
                        {
                            SelectIndex(i - 1);
                            slot.IsSelected = false;
                        }
                        break;
                    }
                }
            }
        }

        private void InteractKeybind()
        {
            if (SelectedItem is null)
                return;

            bool mayInteract = SelectedItem switch
            {
                { interactButton: Item.ItemInteractButton.LeftClick } => Input.GetMouseButtonDown(0),
                { interactButton: Item.ItemInteractButton.RightClick } => Input.GetMouseButtonDown(1),
                { interactButton: Item.ItemInteractButton.MiddleClick } => Input.GetMouseButtonDown(2),
                { interactButton: Item.ItemInteractButton.E } => Input.GetKeyDown(KeyCode.E),
                _ => false
            };

            if (mayInteract && SelectedItem.HasFunction)
                SelectedItem.ItemFunction!.UseItem();
        }

        private void OemKeybinds()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SelectIndex(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SelectIndex(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SelectIndex(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                SelectIndex(3);
            if (Input.GetKeyDown(KeyCode.Alpha5))
                SelectIndex(4);
            if (Input.GetKeyDown(KeyCode.Alpha6))
                SelectIndex(5);
            if (Input.GetKeyDown(KeyCode.Alpha7))
                SelectIndex(6);
            if (Input.GetKeyDown(KeyCode.Alpha8))
                SelectIndex(7);
            if (Input.GetKeyDown(KeyCode.Alpha9))
                SelectIndex(8);
            if (Input.GetKeyDown(KeyCode.Alpha0))
                SelectIndex(9);
        }

        /// <summary>
        /// Attempts to add an item to the inventory
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns></returns>
        public InventoryInteractResult AddItem(Item item)
        {
            InventoryInteractResult result;
            item = ValidateItem(item);

            foreach (Item i in playerInventory)
            {
                if (i.id == item.id)
                {
                    if (i.CurrentStackSize < i.MaxStackSize)
                    {
                        i.SetStack(i.CurrentStackSize + 1);
                        return InvokeInteractEvent(new InventoryInteractResult(Success | ItemAddedToStack, "Item added to stack", item));
                    }
                    else
                    {
                        return InvokeInteractEvent(new InventoryInteractResult(Failure | StackFull, "Item stack is full", item));
                    }
                }
            }

            if (playerInventory.Count >= maxUniqueItems)
            {
                return InvokeInteractEvent(new InventoryInteractResult(Failure | InventoryFull, "Inventory is full", item));
            }

            playerInventory.Add(item);

            // make sure the slot in which the item is placed knows about the item
            invSlots[playerInventory.Count - 1].SetITem(item);

            // if the slot in which the item is placed is selected, select the item
            if (invSlots[playerInventory.Count - 1].IsSelected)
            {
                SelectIndex(playerInventory.IndexOf(item));
            }

            return InvokeInteractEvent(new InventoryInteractResult(Success | ItemAdded, "Item added to inventory", item));
        }
        /// <summary>
        /// Removes the given item from the inventory regardless of the stack size
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public InventoryInteractResult RemoveItem(Item item)
        {
            ValidateItem(item);

            if (playerInventory.Contains(item))
            {
                playerInventory.Remove(item);
                return InvokeInteractEvent(new InventoryInteractResult(Success | ItemRemoved, "Item removed from inventory", item));
            }
            else
            {
                return InvokeInteractEvent(new InventoryInteractResult(Failure | ItemNotInInventory, "Item not in inventory", item));
            }
        }
        /// <summary>
        /// Removes a given amount of the item from the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public InventoryInteractResult RemoveItem(Item item, int count)
        {
            InventoryInteractResult result;
            ValidateItem(item);

            if (playerInventory.Contains(item))
            {
                if (item.CurrentStackSize > count)
                {
                    item.SetStack(item.CurrentStackSize - count);
                    return InvokeInteractEvent(new InventoryInteractResult(Success | ItemRemovedFromStack, "Item removed from stack", item));
                }
                else
                {
                    playerInventory.Remove(item);
                    return InvokeInteractEvent(new InventoryInteractResult(Success | ItemRemoved, "Item removed from inventory", item));
                }
            }
            else
            {
                return InvokeInteractEvent(new InventoryInteractResult(Failure | ItemNotInInventory, "Item not in inventory", item));
            }
        }
        /// <summary>
        /// Selects the item at the given index in the inventory
        /// </summary>
        /// <param name="index">The index of the item that should be equipped</param>
        public InventoryInteractResult SelectIndex(int index)
        {
            if (index < 0 || index >= invSlots.Count)
            {
                return InvokeInteractEvent(new InventoryInteractResult(Failure | OutOfInventoryRange, "Index out of bounds", null));
            }

            invSlots.Foreach(x => x.IsSelected = false);

            Slot slot = invSlots[index];
            selectedItem = slot.item;
            slot.IsSelected = true;

            if (slot.item != null)
                Log.Push(slot.item.name + " selected");

            return InvokeInteractEvent(new InventoryInteractResult(Success | ItemEquipped, "Item selected", slot.item));
        }
        /// <summary>
        /// Interacts with the current selected item, given there is an item selected and the item has a function 
        /// <br></br>
        /// <b>Possible Results:</b><br></br>
        /// <b><see cref="Success"/> | <see cref="ItemUsed"/></b><br></br>
        /// <b><see cref="Failure"/> | <see cref="NoItemSelected"/></b><br></br>
        /// <b><see cref="Failure"/> | <see cref="ItemNotInteractable"/></b>
        /// </summary>
        /// <returns>One of the results</returns>
        public InventoryInteractResult Interact()
        {
            if (SelectedItem == null)
                return InvokeInteractEvent(new InventoryInteractResult(Failure | NoItemSelected, "No item selected", null));
            if (SelectedItem.ItemFunction == null)
                return InvokeInteractEvent(new InventoryInteractResult(Failure | ItemNotInteractable, "Item is not interactable", SelectedItem));

            SelectedItem.ItemFunction.UseItem();
            Log.Push($"Interaction with item '{SelectedItem.itemName}' complete");
            return InvokeInteractEvent(new InventoryInteractResult(Success | ItemUsed, "Item interacted with", SelectedItem));
        }

        /// <summary>
        /// Invokes the <see cref="OnInventoryInteract"/> event and returns <paramref name="result"/>
        /// <br></br><br></br>
        /// Used in all methods that interact with the inventory to invoke the event and return the result at the same time to reduce code duplication and increase readability
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private InventoryInteractResult InvokeInteractEvent(InventoryInteractResult result)
        {
            Log.Push("Inventory interaction event invoked");
            OnInventoryInteract?.Invoke(result);
            return result;
        }
        private void OnNewSceneLoad(Scene scene, LoadSceneMode mode)
        {
            Log.Push("New scene loaded. clearing interaction event subscribers");
            OnInventoryInteract.RemoveAllListeners();
            if (scene.name == "MainMenu")
            {
                Log.Push("The new scene is the Main Menu. Destroying inventory");
                Destroy(gameObject);
                SceneManager.sceneLoaded -= OnNewSceneLoad;
            }
        }
        /// <summary>
        /// Validates the given item and adds it the the <see cref="allItems"/> list if it is not already in there. and inits the item
        /// </summary>
        /// <param name="item"></param>
        private Item ValidateItem(Item item)
        {
            if (item.id == -1)
            {
                Log.Push("Item has no id. Generating associated ID");
                // check if there is already an item with the same name
                Item existing = allItems.Where(x => x.itemName == item.itemName).FirstOrDefault();
                if (existing != null)
                {
                    item.id = allItems.Find(x => x.itemName == item.itemName).id;
                }
                else
                {
                    item.id = allItems.Count;
                    allItems.Add(item);
                    existing = item.Copy();
                }
                return existing;
            }

            return item.Copy();

        }
        private IEnumerator AnimateInventoryIn()
        {
            slotParent.gameObject.SetActive(true);
            // smoothly lerp the slot parent position down 800 units

            while (slotParent.position.y < slotsStartY - 0.01f)
            {
                slotParent.position = Vector3.Lerp(slotParent.position, inventoryNormalPos, slotAnimationSpeed * Time.deltaTime);
                yield return null;
            }

            slotParent.position = inventoryNormalPos;
        }

        private IEnumerator AnimateInventoryOut()
        {
            // smoothly lerp the slot parent position up 800 units
            while (slotParent.position.y > -slotsStartY + 0.01f)
            {
                slotParent.position = Vector3.Lerp(slotParent.position, inventoryHiddenPos, slotAnimationSpeed * Time.deltaTime);
                yield return null;
            }

            slotParent.gameObject.SetActive(false);
        }
    }
}