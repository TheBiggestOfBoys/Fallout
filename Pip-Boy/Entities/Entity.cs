using Pip_Boy.Items;
using Pip_Boy.Objects;
using System;
using System.Collections.Generic;

namespace Pip_Boy.Entities
{
    [Serializable]
    public abstract class Entity
    {
        #region Arrays
        [NonSerialized]
        public Inventory Inventory;

        public Dictionary<string, byte> SPECIAL = new()
        {
            {"Strength", 5},
            {"Perception", 5},
            {"Endurance", 5},
            {"Charisma", 5},
            {"Intelligence", 5},
            {"Agility", 5},
            {"Luck", 5}
        };

        public Dictionary<string, byte> Skills = new(){
            {"Barter", 10},
            {"Energy Weapons", 10},
            {"Explosives", 10},
            {"Gun", 10},
            {"Lockpick", 10},
            {"Medicine", 10},
            {"Melee Weapons", 10},
            {"Repair", 10},
            {"Science", 10},
            {"Sneak", 10},
            {"Speech", 10},
            {"Survival", 10},
            {"Unarmed", 10}
        };

        public List<Effect> Effects = [];
        #endregion

        #region Entity Info
        public string Name { get; set; }
        public byte Level { get; set; } = 1;
        public static ushort MaxHealth { get; private set; } = 100;
        public int CurrentHealth { get; private set; } = 100;

        public static byte DamageRessistance { get; private set; } = 0;
        public static float RadiationRessistance { get; set; } = 0f;
        #endregion

        #region Constructor
        public Entity()
        {
            Inventory = new();
            Name = string.Empty;
        }
        #endregion

        #region EquippedItems
        public HeadPiece? headPiece;
        public TorsoPiece? torsoPiece;
        public Weapon? weapon;
        public Ammo? ammo;
        #endregion

        #region Items
        public void Equip(Equippable item)
        {
            if (item is HeadPiece headPieceItem)
            {
                headPiece = headPieceItem;
            }
            else if (item is TorsoPiece torsoPieceItem)
            {
                torsoPiece = torsoPieceItem;
            }
            else if (item is Weapon weaponItem)
            {
                weapon = weaponItem;
            }
            else if (item is Ammo ammoItem)
            {
                ammo = ammoItem;
            }
            item.Equip(this);
        }

        public void Unequip(Equippable item)
        {
            if (item is not null)
            {
                item.Unequip(this);
                if (item is HeadPiece)
                {
                    headPiece = null;
                }
                else if (item is TorsoPiece)
                {
                    torsoPiece = null;
                }
                else if (item is Weapon)
                {
                    weapon = null;
                }
                else if (item is Ammo)
                {
                    ammo = null;
                }
            }
        }
        #endregion

        #region Effects
        public void ApplyEffects()
        {
            ResetEffects();
            foreach (Effect effect in Effects)
            {
                foreach (string attribute in SPECIAL.Keys)
                {
                    if (effect.ToTitleCase() == attribute)
                    {
                        if (SPECIAL[attribute] + effect.Value >= 1)
                        {
                            SPECIAL[attribute] = (byte)(SPECIAL[attribute] + effect.Value);
                        }
                        break;
                    }
                }
                foreach (string attribute in Skills.Keys)
                {
                    if (effect.ToTitleCase() == attribute)
                    {
                        if (Skills[attribute] + effect.Value >= 1)
                        {
                            Skills[attribute] = (byte)(Skills[attribute] + effect.Value);
                        }
                        break;
                    }
                }
            }
        }

        public void ResetEffects()
        {
            Effects.Clear();
        }
        #endregion
    }
}
