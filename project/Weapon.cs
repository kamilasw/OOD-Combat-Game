using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal interface IWeapon : IItem, IAcceptVisitor
    {
        public int Damage { get; set; }
        public bool TwoHanded { get; set; }

    
        public string Handiness();
     

    }


    //weapons:
    //    sword - damage 10
    //    dagger - damage 15
    //    sickle - damage 20
    //    polearm - damage 15 twohanded
    //    axe - damage 15
    //    crossbow - damage 10 twohanded

    internal interface IHeavyWeapon
    {
        string Type()
        {
            return "Heavy";
        }
    }
    
    internal interface ILightWeapon
    {
        string Type()
        {
            return "Light";
        }
    }
    
    internal interface IMagicWeapon
    {
        string Type()
        {
            return "Magic";
        }
    }

    internal interface IOtherWeapon
    {
        string Type()
        {
            return "Other";
        }
    }

    internal interface IAcceptVisitor
    {
        int Accept(IAttackVisitor visitor, Player player);
        int Accept(IDefenceVisitor visitor, Player player);
    }


    internal class BareHands : IOtherWeapon, IAcceptVisitor
    {
        public int Accept(IAttackVisitor visitor, Player player)
        {
            return visitor.VisitOtherWeapon(this, player);
        }

        public int Accept(IDefenceVisitor visitor, Player player)
        {
            return visitor.VisitOtherWeapon(this, player);
        }

        public override string ToString()
        {
            return "Bare Hands";
        }
    }

    internal class Dagger : IWeapon, ILightWeapon, IAcceptVisitor
    {
        public string Name { get; set; }


        public int Damage { get; set; }
        public bool TwoHanded { get; set; }

        public Dagger() : this("Dagger") { }
        public Dagger(string _name = "Dagger")
        {
            Name = _name;
            Damage = 15;
            TwoHanded = false;
        }

        public int Accept(IAttackVisitor visitor, Player player)
        {
            return visitor.VisitLightWeapon(this, player);
        }
      

        public int Accept(IDefenceVisitor visitor, Player player)
        {
            return visitor.VisitLightWeapon(this, player);
        }

        public string Handiness()
        {
            return TwoHanded ? "T" : "O";
        }

        public int Drink(Player player, Room room) { return 0; }
        public void Collect(Player player)
        {
            player.Inventory.Add(this);

           
        }

        public int AddtoHand(Player player, char hand)
        {
            if (hand == 'L')
            {
                player.LeftHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;

            }
            else
            {
                //hand == 'R'
                player.RightHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;
            }
            return 1;
        }

        public void RemovefromHand(Player player, char hand, Room room)
        {
            if (hand == 'L' && player.LeftHand != null)
            {
                player.Inventory.Add(player.LeftHand);
                player.LeftHand = null;

            }
            else
            {
                if (player.RightHand != null)
                {
                    player.Inventory.Add(player.RightHand);
                    player.RightHand = null;
                }

            }
        }
        public override string ToString()
        {
            return $"{Name} (D:{Damage}) ({Handiness()})";
        }

    }

    internal class Sword : IWeapon, IHeavyWeapon, IAcceptVisitor
    {
        public string Name { get; set; }
        

        public int Damage { get; set; }
        public bool TwoHanded { get; set; }

        public Sword() : this("Sword") { }
        public Sword(string _name = "Sword")
        {
            Name = _name;
            Damage = 10;
            TwoHanded = false;
        }

        public int Accept(IAttackVisitor visitor, Player player)
        {
            return visitor.VisitHeavyWeapon(this, player);
        }


        public int Accept(IDefenceVisitor visitor, Player player)
        {
            return visitor.VisitHeavyWeapon(this, player);
        }

        public string Handiness()
        {
            return TwoHanded ? "T" : "O";
        }
        public int Drink(Player player, Room room) { return 0; }
        public int AddtoHand(Player player, char hand)
        {
            if (hand == 'L')
            {
                player.LeftHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;

            }
            else
            {
                //hand == 'R'
                player.RightHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;
            }
            return 1;
        }

        public void RemovefromHand(Player player, char hand,Room room)
        {
            if (hand == 'L' && player.LeftHand != null)
            {
                player.Inventory.Add(player.LeftHand);
                player.LeftHand = null;

            }
            else
            {
                if (player.RightHand != null)
                {
                    player.Inventory.Add(player.RightHand);
                    player.RightHand = null;
                }

            }
        }
        public void Collect(Player player)
        {
            player.Inventory.Add(this);

           
        }

        public override string ToString()
        {
            return $"{Name} (D:{Damage}) ({Handiness()})";
        }

    }

    internal class Polearm : IWeapon,IHeavyWeapon, IAcceptVisitor
    {
        public string Name { get; set; }
      

        public int Damage { get; set; }
        public bool TwoHanded { get; set; }

        public Polearm() : this("Polearm") { }
        public Polearm(string _name = "Polearm")
        {
            Name = _name;
            Damage = 15;
            TwoHanded = true;
        }

         public int Accept(IAttackVisitor visitor, Player player)
        {
            return visitor.VisitHeavyWeapon(this, player);
        }


        public int Accept(IDefenceVisitor visitor, Player player)
        {
            return visitor.VisitHeavyWeapon(this, player);
        }


        public void HandtoGround(Room room, Player player, char hand)
        {
            if (player.LeftHand != null)
            {
                room.Grid[player.Position[0], player.Position[1]].AddItem(player.LeftHand);
                player.LeftHand = null;
                player.RightHand = null;
            }
           
        }

        public int Drink(Player player, Room room) { return 0; }
        public int AddtoHand(Player player, char hand)
        {
            if (player.LeftHand != null || player.RightHand != null)
            {
                return -1;
            }

                player.LeftHand = player.Inventory[player.SelectedItem];
                player.RightHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;
            return 1;
        }

        public void RemovefromHand(Player player, char hand, Room room)
        {
            if (player.RightHand!=null && player.LeftHand != null)
            {
                player.Inventory.Add(player.LeftHand);
                player.LeftHand = null;
                player.RightHand = null;
                

            }
        }

        public string Handiness()
        {
            return TwoHanded ? "T" : "O";
        }

        public void Collect(Player player)
        {
            player.Inventory.Add(this);

        }

        public override string ToString()
        {
            return $"{Name} (D:{Damage}) ({Handiness()})";
        }

    }

    internal class Crossbow : IWeapon, IMagicWeapon, IAcceptVisitor
    {
        public string Name { get; set; }
       

        public int Damage { get; set; }
        public bool TwoHanded { get; set; }

        public Crossbow() : this("Crossbow") { }
        public Crossbow(string _name = "Crossbow")
        {
            Name = _name;
            Damage = 10;
            TwoHanded = true;
        }


        public int Accept(IAttackVisitor visitor, Player player)
        {
            return visitor.VisitMagicWeapon(this, player);
        }


        public int Accept(IDefenceVisitor visitor, Player player)
        {
            return visitor.VisitMagicWeapon(this, player);
        }
        public void HandtoGround(Room room, Player player, char hand)
        {
            if (player.LeftHand != null)
            {
                room.Grid[player.Position[0], player.Position[1]].AddItem(player.LeftHand);
                player.LeftHand = null;
                player.RightHand = null;
            }

        }

        public int Drink(Player player, Room room) { return 0; }
        public int AddtoHand(Player player, char hand)
        {
            if(player.LeftHand != null || player.RightHand!=null)
            {
                return -1;
            }
                player.LeftHand = player.Inventory[player.SelectedItem];
                player.RightHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;

            return 1;
            
        }

        public void RemovefromHand(Player player, char hand, Room room)
        {
            if (player.RightHand != null && player.LeftHand != null)
            {
                player.Inventory.Add(player.LeftHand);
                player.LeftHand = null;
                player.RightHand = null;


            }
        }
        public string Handiness()
        {
            return TwoHanded ? "T" : "O";
        }

        public void Collect(Player player)
        {
            player.Inventory.Add(this);

         
        }

        public override string ToString()
        {
            return $"{Name} (D:{Damage}) ({Handiness()})";
        }


    }

    internal class Sickle : IWeapon, IHeavyWeapon, IAcceptVisitor
    {
        public string Name { get; set; }
    

        public int Damage { get; set; }
        public bool TwoHanded { get; set; }

        public Sickle() : this("Sickle") { }
        public Sickle(string _name = "Sickle")
        {
            Name = _name;
            Damage = 20;
            TwoHanded = false;
        }


        public int Accept(IAttackVisitor visitor, Player player)
        {
            return visitor.VisitHeavyWeapon(this, player);
        }


        public int Accept(IDefenceVisitor visitor, Player player)
        {
            return visitor.VisitHeavyWeapon(this, player);
        }
        public int Drink(Player player, Room room) { return 0; }
        public int AddtoHand(Player player, char hand)
        {
            if (hand == 'L')
            {
                player.LeftHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;

            }
            else
            {
                //hand == 'R'
                player.RightHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;
            }
            return 1;
        }

        public void RemovefromHand(Player player, char hand, Room room)
        {
            if (hand == 'L' && player.LeftHand != null)
            {
                player.Inventory.Add(player.LeftHand);
                player.LeftHand = null;

            }
            else
            {
                if (player.RightHand != null)
                {
                    player.Inventory.Add(player.RightHand);
                    player.RightHand = null;
                }

            }
        }
        public string Handiness()
        {
            return TwoHanded ? "T" : "O";
        }

        public void Collect(Player player)
        {
            player.Inventory.Add(this);

          
        }

        public override string ToString()
        {
            return $"{Name} (D:{Damage}) ({Handiness()})";
        }

    }

    internal class Axe : IWeapon, ILightWeapon, IAcceptVisitor
    {
        public string Name { get; set; }
      

        public int Damage { get; set; }
        public bool TwoHanded { get; set; }
        public int Drink(Player player, Room room) { return 0; }
        public int AddtoHand(Player player, char hand)
        {
            if (hand == 'L')
            {
                player.LeftHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;

            }
            else
            {
                //hand == 'R'
                player.RightHand = player.Inventory[player.SelectedItem];
                player.Inventory.RemoveAt(player.SelectedItem);
                player.SelectedItem = 0;
            }
            return 1;
        }

        public void RemovefromHand(Player player, char hand, Room room)
        {
            if (hand == 'L' && player.LeftHand != null)
            {
                player.Inventory.Add(player.LeftHand);
                player.LeftHand = null;

            }
            else
            {
                if (player.RightHand != null)
                {
                    player.Inventory.Add(player.RightHand);
                    player.RightHand = null;
                }

            }
        }

        public Axe() : this("Axe") { }
        public Axe(string _name = "Axe")
        {
            Name = _name;
            Damage = 15;
            TwoHanded = false;
        }


        public int Accept(IAttackVisitor visitor, Player player)
        {
            return visitor.VisitLightWeapon(this, player);
        }


        public int Accept(IDefenceVisitor visitor, Player player)
        {
            return visitor.VisitLightWeapon(this, player);
        }
        public string Handiness()
        {
            return TwoHanded ? "T" : "O";
        }

        public void Collect(Player player)
        {
            player.Inventory.Add(this);

         
        }

        public override string ToString()
        {
            return $"{Name} (D:{Damage}) ({Handiness()})";
        }
    }

    //decorators:
    //    lucky
    //    strong


    internal abstract class WeaponDecorator : IWeapon, IAcceptVisitor
    {
        protected IWeapon original;


        public virtual int Accept(IAttackVisitor visitor, Player player)
        {
            return original.Accept(visitor, player);
        }


        public virtual int Accept(IDefenceVisitor visitor, Player player)
        {
            return original.Accept(visitor, player);
        }

        public virtual string Name
        {
            get => original.Name;
            set => original.Name = value;
        }

        public virtual int Damage
        {
            get { return original.Damage; }
            set { original.Damage = value; }
        }

        public virtual bool TwoHanded
        {
            get { return original.TwoHanded; }
            set { original.TwoHanded = value; }
        }
        public int Drink(Player player, Room room) { return 0; }
        public WeaponDecorator(IWeapon weapon)
        {
           
            original = weapon;
        }

        public virtual void RemovefromHand(Player player, char hand, Room room)
        {
            original.RemovefromHand(player, hand,room);
        }
        public virtual int AddtoHand(Player player, char hand)
        {
            return original.AddtoHand(player, hand);
        }
        public virtual void Collect(Player player)
        {
            player.Inventory.Add(this);

        }

        public void HandtoGround(Room room, Player player, char hand)
        {
            original.HandtoGround(room, player, hand);

        }
        public string Handiness()
        {
            return original.Handiness();
        }

        public override string ToString()
        {
            return original.ToString();
        }


    }



    internal class LuckyWeapon : WeaponDecorator
    {

        public LuckyWeapon(IWeapon weapon) : base(weapon) { }

        public override string Name => $"{original.Name} (Lucky)";

        public override void Collect(Player player)
        {
            player.Inventory.Add(this);
            
        }

        public override int Accept(IAttackVisitor visitor, Player player)
        {
            return original.Accept(visitor, player);
        }


        public override int Accept(IDefenceVisitor visitor, Player player)
        {
            return original.Accept(visitor, player);
        }
        public override void RemovefromHand(Player player, char hand, Room room)
        {
            player.Attributes[3].value -= 5;
            original.RemovefromHand(player, hand, room);

        }

        
        public override int AddtoHand(Player player, char hand)
        {
            player.Attributes[3].value += 5;
           return  original.AddtoHand(player, hand);
        }
        public override string ToString()
        {
            return $"{Name} (D:{Damage}) ({Handiness()})";
        }
    }

   

    internal class StrongWeapon : WeaponDecorator
    {
       
        public StrongWeapon(IWeapon weapon): base(weapon) {
        }

        public override string Name => $"{original.Name} (Strong)";
        public override int Damage => original.Damage + 5;

        public override void RemovefromHand(Player player, char hand, Room room)
        {
            original.RemovefromHand(player, hand, room);

        }
        public override int AddtoHand(Player player, char hand)
        {
        
            return original.AddtoHand(player, hand);
        }

        public override int Accept(IAttackVisitor visitor, Player player)
        {
            return original.Accept(visitor, player);
        }


        public override int Accept(IDefenceVisitor visitor, Player player)
        {
            return original.Accept(visitor, player);
        }
        public override void Collect(Player player)
        {
            player.Inventory.Add(this);
        }
        public override string ToString()
        {
            return $"{Name} (D:{Damage}) ({Handiness()})";
        }
    }



}
