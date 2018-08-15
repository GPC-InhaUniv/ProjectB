using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Inventory
{
    public class ItemDatabase : MonoBehaviour
    {
        [SerializeField] private List<Item> database = new List<Item>();

        private void Start()
        {
        }
    }
}