using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project
{
    internal interface IUnusableItem : IItem
    {

    }

    //unusable items:
    //    rock
    //    sand
    //    leaf

    internal class Rock : IUnusableItem
    {
        public string Name { get; set; }
     

        public Rock(string _name = "Rock")
        {
            Name = _name;

        }
        public int Drink(Player player, Room room) { return 0; }
        //public void AddtoHand(Player player, char hand)
        //{
        //    if (hand == 'L')
        //    {
        //        player.LeftHand = player.Inventory[player.SelectedItem];
        //        player.Inventory.RemoveAt(player.SelectedItem);
        //        player.SelectedItem = 0;

        //    }
        //    else
        //    {
        //        //hand == 'R'
        //        player.RightHand = player.Inventory[player.SelectedItem];
        //        player.Inventory.RemoveAt(player.SelectedItem);
        //        player.SelectedItem = 0;
        //    }
        //}

        //public void RemovefromHand(Player player, char hand,Room room)
        //{
        //    if (hand == 'L' && player.LeftHand != null)
        //    {
        //        player.Inventory.Add(player.LeftHand);
        //        player.LeftHand = null;

        //    }
        //    else
        //    {
        //        if (player.RightHand != null)
        //        {
        //            player.Inventory.Add(player.RightHand);
        //            player.RightHand = null;
        //        }

        //    }
        //}

        public void Collect(Player player)
        {
            player.Inventory.Add(this);

          
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    internal class Sand : IUnusableItem
    {
        public string Name { get; set; }
      

        public Sand(string _name = "Sand")
        {
            Name = _name;

        }
        public int Drink(Player player, Room room) { return 0; }
        //public void AddtoHand(Player player, char hand)
        //{
        //    if (hand == 'L')
        //    {
        //        player.LeftHand = player.Inventory[player.SelectedItem];
        //        player.Inventory.RemoveAt(player.SelectedItem);
        //        player.SelectedItem = 0;

        //    }
        //    else
        //    {
        //        //hand == 'R'
        //        player.RightHand = player.Inventory[player.SelectedItem];
        //        player.Inventory.RemoveAt(player.SelectedItem);
        //        player.SelectedItem = 0;
        //    }
        //}

        //public void RemovefromHand(Player player, char hand, Room room)
        //{
        //    if (hand == 'L' && player.LeftHand != null)
        //    {
        //        player.Inventory.Add(player.LeftHand);
        //        player.LeftHand = null;

        //    }
        //    else
        //    {
        //        if (player.RightHand != null)
        //        {
        //            player.Inventory.Add(player.RightHand);
        //            player.RightHand = null;
        //        }

        //    }
        //}
        public void Collect(Player player)
        {
            player.Inventory.Add(this);

        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    internal class Leaf : IUnusableItem
    {
        public string Name { get; set; }
       

        public Leaf(string _name = "Leaf")
        {
            Name = _name;

        }
        public int Drink(Player player, Room room) { return 0; }
        //public void AddtoHand(Player player, char hand)
        //{
        //    if (hand == 'L')
        //    {
        //        player.LeftHand = player.Inventory[player.SelectedItem];
        //        player.Inventory.RemoveAt(player.SelectedItem);
        //        player.SelectedItem = 0;

        //    }
        //    else
        //    {
        //        //hand == 'R'
        //        player.RightHand = player.Inventory[player.SelectedItem];
        //        player.Inventory.RemoveAt(player.SelectedItem);
        //        player.SelectedItem = 0;
        //    }
        //}

        //public void RemovefromHand(Player player, char hand, Room room)
        //{
        //    if (hand == 'L' && player.LeftHand != null)
        //    {
        //        player.Inventory.Add(player.LeftHand);
        //        player.LeftHand = null;

        //    }
        //    else
        //    {
        //        if (player.RightHand != null)
        //        {
        //            player.Inventory.Add(player.RightHand);
        //            player.RightHand = null;
        //        }

        //    }
        //}
        public void Collect(Player player)
        {
            player.Inventory.Add(this);

           
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }  
    
    //you cant pick up glass!! it'll cut your hand!
    internal class Glass : IUnusableItem
    {
        public string Name { get; set; }
       

        public Glass(string _name = "Glass")
        {
            Name = _name;

        }
        public int Drink(Player player, Room room) { return 0; }
        //public void AddtoHand(Player player, char hand)
        //{
        //    //you cant pick up glass!! it'll cut your hand!
        //}

        //public void RemovefromHand(Player player, char hand, Room room)
        //{
        //    //you cant pick up glass!! it'll cut your hand!
        //}
        public void Collect(Player player)
        {
            player.Inventory.Add(this); 
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }

}
