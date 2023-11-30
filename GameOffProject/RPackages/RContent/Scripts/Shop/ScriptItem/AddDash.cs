// Copyrighted by team Rézoskour
// Created by alexandre buzon on 26

using Rezoskour.Content.Shop;
using UnityEngine;

namespace Rezoskour.Content
{
    [CreateAssetMenu(fileName = "AddDash", menuName = "Rezoskour/Shop Item/Add Dash")]
    public class AddDash : ShopItem
    {
        [SerializeField] private DashData dash;

        public override void Buy()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<DashSystem>().AddDash(dash.DashName);
        }
    }
}