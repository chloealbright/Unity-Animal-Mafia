using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryCont.Model
{
    //stores the data about the item into a SO
    [CreateAssetMenu]
    public class GoldSO : ScriptableObject
    {
        //enables stacking of an item
        [field: SerializeField]
        public int Balance { get; set; }

        [field: SerializeField]
        public bool ExistingUser { get; set; }

        public int ID => GetInstanceID();

    }
}