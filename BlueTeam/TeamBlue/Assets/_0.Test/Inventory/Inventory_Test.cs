using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Inventory
{
    struct ItemAbillity
    {
        private int attack;
        public int Attack { get { return attack; } private set { } }
        private int defence;
        public int Defence { get { return defence; } private set { } }
        private int hp;
        public int HP { get { return hp; } private set { } }

        public ItemAbillity(int attack, int defence, int hp)
        {
            this.attack = attack;
            this.defence = defence;
            this.hp = hp;
        }
    }
    struct NecessaryResources
    {
        private int wood;

        private int iron;
        public int Iron { get { return iron; } private set { } }
        private int sheep;
        public int Sheep { get { return sheep; } private set { } }
        private int brick;
        public int Brick { get { return brick; } private set { } }

        public NecessaryResources(int wood, int iron, int sheep, int brick)
        {
            this.wood = wood;
            this.iron = iron;
            this.sheep = sheep;
            this.brick = brick;
        }
    }
    class Recipe
    {
        private NecessaryResources necessaryResources;
        public NecessaryResources NecessaryResources
        {
            get { return necessaryResources; }
            private set { }
        }
    }
    class Item
    {
        private ItemAbillity itemAbillity;
        public ItemAbillity ItemAbillity
        {
            get { return itemAbillity; }
            private set { }
        }
    }
    public class Inventory_Test : MonoBehaviour
    {

    }
}