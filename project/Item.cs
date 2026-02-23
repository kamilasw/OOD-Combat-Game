    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace project
{

    internal interface IItem
    {
        public string Name { get; set; }
        
        public void Collect(Player player);

        public int  AddtoHand(Player player, char hand)
        {
            return 0;
        }

        public void RemovefromHand(Player player, char hand,Room room)
        {

        }
        public string ToString();

        public void HandtoGround(Room room, Player player,char hand)
        {
            if(hand == 'R')
            {
                if(player.RightHand!=null)
                {
                    room.Grid[player.Position[0], player.Position[1]].AddItem(player.RightHand);
                    player.RightHand = null;
                }
               
            }
            else
            {
                //left

                if (player.LeftHand != null)
                {
                    room.Grid[player.Position[0], player.Position[1]].AddItem(player.LeftHand);
                    player.LeftHand = null;
                }


            }

        }

        int Drink(Player player, Room room); //returns 1 if success 0 if fail (not a potion)
    }


    internal abstract class ItemDecorator : IItem
    {
        protected IItem original;

        public virtual string Name
        {
            get { return original.Name; }
            set { original.Name = value; }
        }

        public ItemDecorator(IItem item)
        {
            original = item;
        }

        public int Drink(Player player, Room room) { return 0; }

        public virtual void Collect(Player player)
        {
            player.Inventory.Add(this);

        }
        //public virtual void AddtoHand(Player player, char hand)
        //{
        //    if(hand == 'L')
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

        //public virtual void RemovefromHand(Player player, char hand, Room room)
        //{
        //    if(hand == 'L' && player.LeftHand!=null)
        //    {
        //        player.Inventory.Add(player.LeftHand);
        //        player.LeftHand = null;
                
        //    }
        //    else
        //    {
        //        if(player.RightHand!=null)
        //        {
        //            player.Inventory.Add(player.RightHand);
        //            player.RightHand = null;
        //        }
               
        //    }
        //}

        public override string ToString()
        {
            return original.ToString();
        }


    }
     
        
        //item decorators:
        //    lucky

  

    internal class LuckyItem : ItemDecorator
    {

        public override void Collect(Player player)
        {
            player.Attributes[3].value += 5;
            player.Inventory.Add(this);

        }
        public LuckyItem(IItem item): base(item) {


            original.Name += " (Lucky)";
        }

        //public override void AddtoHand(Player player, char hand)
        //{
        //    player.Attributes[3].value += 5;
       
        //    original.AddtoHand(player, hand);
        //}

        //public override void RemovefromHand(Player player, char hand, Room room)
        //{

        //    player.Attributes[3].value -= 5;
        //    original.RemovefromHand(player, hand,room);
        //}



        public override string ToString()
        {
            return $"{original.ToString()}";
        }

    }


    }
