using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal interface IPotion : IItem
    {
      
    }

    internal class Potion : IPotion
    {
        public string Name { get; set; }

        public Potion(string _name = "Potion")
        {
            Name = _name;
        }

        public void Collect(Player player)
        {
            player.Inventory.Add(this);
        }
        public override string ToString()
        {
            return $"{Name}";
        }

     

        public int Drink(Player player, Room room)
        {
            player.Inventory.RemoveAt(player.SelectedItem);
            player.SelectedItem = 0;
            Glass glass = new Glass();
            room.Grid[player.Position[0], player.Position[1]].AddItem(glass);

            return 1;

        }
    }


    internal class PotionDecorator : IPotion
    {
        protected IPotion original;

        public virtual string Name
        {
            get { return original.Name; }
            set { original.Name = value; }
        }

        public PotionDecorator(IPotion potion)
        {
            original = potion;
        }

        public virtual void Collect(Player player)
        {
            player.Inventory.Add(this);

        }

        public virtual int Drink(Player player, Room room)
        {
            player.Inventory.RemoveAt(player.SelectedItem);
            player.SelectedItem = 0;
            Glass glass = new Glass();
            room.Grid[player.Position[0], player.Position[1]].AddItem(glass);

            return 1;

        }
 

        public override string ToString()
        {
            return original.ToString();
        }

    }


    internal class HealthPotion : PotionDecorator
    {
        public override void Collect(Player player)
        {
            player.Inventory.Add(this);

        }

        public HealthPotion(IPotion potion) : base(potion)
        {
            original.Name = "Health " + original.Name;
        }

     
        public override int Drink(Player player, Room room)
        {
            player.ActiveEffects.Add(new HealthEffectObserver(player));

            original.Drink(player,room);

            return 1;

        }

    }

    internal class WisdomPotion : PotionDecorator
    {
        public override void Collect(Player player)
        {
            player.Inventory.Add(this);

        }

        public WisdomPotion(IPotion potion) : base(potion)
        {
            original.Name = "Wisdom " + original.Name;
        }

  

        public override int Drink(Player player, Room room)
        {
            player.ActiveEffects.Add(new WisdomEffectObserver(player,4));

            original.Drink(player, room);
            return 1;

        }
    }


    internal class StrengthPotion: PotionDecorator
    {
        public override void Collect(Player player)
        {
            player.Inventory.Add(this);

        }

        public StrengthPotion(IPotion potion) : base(potion)
        {
            original.Name = "Strength " + original.Name;
        }


        public override int Drink(Player player, Room room)
        {
            player.ActiveEffects.Add(new StrengthEffectObserver(player));

            original.Drink(player, room);
            return 1;

        }
    }

    internal class AntidotePotion: PotionDecorator
    {
        public override void Collect(Player player)
        {
            player.Inventory.Add(this);

        }

        public AntidotePotion(IPotion potion) : base(potion)
        {
            original.Name = "Antidote " + original.Name;
        }

     

        public override int Drink(Player player, Room room)
        {
            player.ActiveEffects.Insert(0,new AntidoteEffectObserver(player));

            original.Drink(player, room);
            return 1;

        }
    }

 
}
