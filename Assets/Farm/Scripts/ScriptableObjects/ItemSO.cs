using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryCont.Model
{
    //stores the data about the item into a SO
    [CreateAssetMenu]
    public class ItemSO : ScriptableObject
    {
        //enables stacking of an item
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [field: SerializeField]
        public int Cost { get; set; }
    }
}

