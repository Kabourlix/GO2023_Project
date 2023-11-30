// Copyrighted by team Rézoskour
// Created by alexandre buzon on 24

using Rezoskour.Content.Shop;
using SDKabu.KCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rezoskour.Content
{
    public class ShopItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private TextMeshProUGUI itemPrice;
        [SerializeField] private Image priceIcon;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Button buyButton;
        private ShopItem item;

        public void SetItem(ShopItem _item)
        {
            itemName.text = _item.ObjectName;
            itemDescription.text = _item.ObjectDescription;
            itemIcon.sprite = _item.ObjectIconPath != null ? Resources.Load<Sprite>(_item.ObjectIconPath) : null;
            if (_item.Price == 0)
            {
                itemPrice.gameObject.SetActive(false);
                priceIcon.gameObject.SetActive(false);
            }
            else
            {
                itemPrice.text = _item.Price.ToString();
            }

            item = _item;
            buyButton.onClick.AddListener(BuyItem);
        }

        private void OnDestroy()
        {
            buyButton.onClick.RemoveAllListeners();
        }

        private void BuyItem()
        {
            //KServiceInjection.Get<IGameManager>().PlayerTransform;
            /*if (KServiceInjection.Get<ShopSystem>().TryBuy(, item))
            {
                item.Buy();
            }*/
        }
    }
}