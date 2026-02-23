using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal class Player
    {
        //attributes - at the beggining everything set as 1 scale?? 1-10 maybe
      

        public Attribute[] Attributes = new Attribute[6];

        //position
        public int[] Position = new int[2]; // (x,y)

        //hands - at the beggining set to null
        public IWeapon? RightHand;
        public IWeapon? LeftHand;

        //purse to store coins and gold  [0] is coins [1] is gold 
        public int[] Purse = new int[2];

        //inventory - items picked up by the player - at the beggining set to empty
        public List<IItem> Inventory = new();
        public int SelectedItem = 0;

        public List<IEffectObserver> ActiveEffects = new();

        public Player()
        {
            Attributes[0] = new Attribute("Strength");
            Attributes[1] = new Attribute("Dexterity");
            Attributes[2] = new Attribute("Health");
            
            Attributes[3] = new Attribute("Luck");
            Attributes[4] = new Attribute("Aggression");
            Attributes[5] = new Attribute("Wisdom");

            Attributes[2].value = 10; //max health (without potion) is 10, when its 0 you die?
            Attributes[0].value = 5; 
            Attributes[1].value = 5; 
            Attributes[3].value = 5; 
            Attributes[4].value = 5; 
            Attributes[5].value = 5; 


            Position = [0, 0];
            RightHand = null;
            LeftHand = null;
            SelectedItem = 0;
            Purse = [0, 0];
            Inventory = new List<IItem>();
         
        }




        public void UpdatePotionEffect()
        {
            for(int i= ActiveEffects.Count-1; i>=0; i--)
            {
              
                if(!ActiveEffects[i].IsActive)
                {
                   
                    ActiveEffects[i].RemoveEffect(this);
                    ActiveEffects.RemoveAt(i);
                }
                else
                {
                    ActiveEffects[i].Tick(this);
                }

            }

        }

       
      
        //add item

        public void AddItems(List<IItem> newitems)
        {
            foreach (IItem i in newitems)
            {
             
                i.Collect(this);
            }
        }

        public void RemoveItem(Room room)
        {
            IItem newitem = Inventory[SelectedItem];
            Inventory.RemoveAt(SelectedItem);
            room.Grid[Position[0], Position[1]].AddItem(newitem);
            SelectedItem = 0;
        }

        public void TraverseInventory(char direction) //U - up D - down
        {
            switch(direction)
            {
                case 'U':
                    SelectedItem--;
                    break;
                case 'D':
                    SelectedItem++;
                    break;
                default: break;
            }

        }


        //move player

        public void Move(char direction)
        {
            switch(direction)
            {
                case 'W':
                    Position[1] -= 1;break;
                case 'A':
                    Position[0] -= 1; break;
                case 'S':
                    Position[1] += 1; break;
                case 'D':
                    Position[0] += 1; break;
                default:break;
            }
        }

      

        //add to hands

        public int InventorytoHand(char hand)
        {
            
            return Inventory[SelectedItem].AddtoHand(this, hand);
        }

        public void HandtoInventory(char hand,Room room)
        {
            if (hand == 'L' && LeftHand!=null)
            {
                LeftHand.RemovefromHand(this, hand,room);
            }else
            {
                if(RightHand!=null)
                {
                    RightHand.RemovefromHand(this, hand,room);
                }
            }
        }
        
    }

    internal class Attribute
    {
        public string Name { get; set; }
        public int value;

        public Attribute(string _name = "Attribute") { Name = _name; value = 0; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
