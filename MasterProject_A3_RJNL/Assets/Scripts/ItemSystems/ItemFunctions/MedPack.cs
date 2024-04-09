using ShadowUprising.Inventory;
using ShadowUprising.Player;
using static UnityEngine.Object;

namespace ShadowUprising.Items.ItemFunctions
{
    public class MedPack : IItemFunction
    {
        public void UseItem()
        {
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            playerStats.AddHealth(30);
            InventoryManager.Instance.ConsumeItem();
        }
    }
}