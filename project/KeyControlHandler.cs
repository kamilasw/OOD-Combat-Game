using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace project
{



    internal interface IKeyControlHandler
    {
        void SetNext(IKeyControlHandler nexthandler);
        string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room); //returns a log
    }


    //W
    internal class MoveUpHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public MoveUpHandler()
        {
            next = null;
        }
        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {

            string log = "you cannot move up!";
            if (keyinfo.Key == ConsoleKey.W && !shift)
            {
                int x, y;
                x = player.Position[0];
                y = player.Position[1];

                y = y-1;

                if (x < 0 || y < 0 || x >= 40 || y >= 20 || room.Grid[x, y].ToString() == "█" || room.Grid[x, y].Enemy != null)
                {
                    return log;
                }
                else
                {

                    player.UpdatePotionEffect();
                    player.Move('W');
                    log = "player moved up";
                    return log;

                }


            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }

            }

            return log;
        }


    }


    //S
    internal class MoveDownHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public MoveDownHandler() { next = null; }


        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "you cannot move down!";
            if (ConsoleKey.S == keyinfo.Key && !shift)
            {
                int x, y;
                x = player.Position[0];
                y = player.Position[1];

                y += 1;

                if (x < 0 || y < 0 || x >= 40 || y >= 20 || room.Grid[x, y].ToString() == "█" || room.Grid[x, y].Enemy != null)
                {
                    return log;
                }
                else
                {
                    player.UpdatePotionEffect();
                    player.Move('S');
                    log = "player moved down";
                    return log;
                }

            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }

            return log;
        }

    }

    //A
    internal class MoveLeftHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public MoveLeftHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "you cannot move left!";
            if (ConsoleKey.A == keyinfo.Key && !shift)
            {
                int x, y;
                x = player.Position[0];
                y = player.Position[1];

                x--;

                if (x < 0 || y < 0 || x >= 40 || y >= 20 || room.Grid[x, y].ToString() == "█" || room.Grid[x, y].Enemy != null)
                {
                    return log;
                }
                else
                {
                    player.UpdatePotionEffect();
                    player.Move('A');
                    log = "player moved left";
                    return log;
                }

            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }

            return log;
        }

    }


    //D
    internal class MoveRightHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;
        
        public MoveRightHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "you cannot move right!";

            if (ConsoleKey.D == keyinfo.Key && !shift)
            {
                int x, y;
                x = player.Position[0];
                y = player.Position[1];

                x++;

                if (x < 0 || y < 0 || x >= 40 || y >= 20 || room.Grid[x, y].ToString() == "█" || room.Grid[x, y].Enemy != null)
                {
                    return log;
                }
                else
                {
                    player.UpdatePotionEffect();
                    player.Move('D');
                    log = "player move right";
                    return log;
                }
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }
            return log;
        }
    }


    //UpArrow
    internal class MoveInvUpHandler : IKeyControlHandler
    {
        IKeyControlHandler? next;

        public MoveInvUpHandler() { next = null; }


        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "you've reached top of inventory!";

            if (ConsoleKey.UpArrow == keyinfo.Key && !shift)
            {
                if (player.SelectedItem == 0)
                {
                    return log;
                }

                player.TraverseInventory('U');
                log = "player selected an item";
                return log;
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }



            return log;
        }

    }

    //DownArrow
    internal class MoveInvDownHandler : IKeyControlHandler
    {
        IKeyControlHandler? next;

        public MoveInvDownHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "you've reached bottom of inventory!";

            if (ConsoleKey.DownArrow == keyinfo.Key && !shift)
            {
                if (player.SelectedItem >= player.Inventory.Count - 1)
                {
                    return log;
                }
                else
                {
                    player.TraverseInventory('D');
                    log = "player selected an item";
                    return log;
                }
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }
            return log;
        }
    }


    //Shift+E
    internal class PickUpItemsHandler : IKeyControlHandler
    {
        IKeyControlHandler? next;

        public PickUpItemsHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "there are no items to pick up";

            if (ConsoleKey.E == keyinfo.Key && shift)
            {
                if (room.Grid[player.Position[0], player.Position[1]].Items.Count == 0)
                {
                    return log;
                }
                else
                {
                    List<IItem> newitems = room.Grid[player.Position[0], player.Position[1]].RemoveItems();

                    player.AddItems(newitems);
                    log = "picked up all items";
                    return log;
                }
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }

            }
            return log;
        }

    }


    //E
    internal class PickUpItemHandler : IKeyControlHandler
    {
        public IKeyControlHandler? next;

        public PickUpItemHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "there are no items to pick up";

            if (ConsoleKey.E == keyinfo.Key && !shift)
            {
                if (room.Grid[player.Position[0], player.Position[1]].Items.Count == 0)
                {
                    return log;
                }
                else
                {
                    List<IItem> item = room.Grid[player.Position[0], player.Position[1]].RemoveItem();

                    player.AddItems(item);

                    log = "picked up one item";
                    return log;
                }
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }
            return log;
        }

    }


    //L
    internal class DropInvItemHandler : IKeyControlHandler
    {
        public IKeyControlHandler? next;

        public DropInvItemHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "inventory is empty";

            if (ConsoleKey.L == keyinfo.Key && !shift)
            {
                if (player.Inventory.Count <= 0)
                {
                    return log;
                }
                else
                {
                    player.RemoveItem(room);
                    log = "dropped item on the ground";
                    return log;
                }
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }
            return log;
        }
    }

    //Shift + L
    internal class DropInvItemsHandler : IKeyControlHandler
    {
        public IKeyControlHandler? next;

        public DropInvItemsHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "you have nothing!";

            if (ConsoleKey.L == keyinfo.Key && shift)
            {
                if (player.Inventory.Count <= 0 && player.LeftHand == null && player.RightHand == null)
                {
                    return log;
                }
                else
                {
                    while (player.Inventory.Count > 0)
                    {
                        player.RemoveItem(room);
                    }

                    if (player.LeftHand != null)
                    {
                        room.Grid[player.Position[0], player.Position[1]].AddItem(player.LeftHand);
                        player.LeftHand = null;
                    }
                    if (player.RightHand != null)
                    {
                        room.Grid[player.Position[0], player.Position[1]].AddItem(player.RightHand);
                        player.RightHand = null;
                    }

                    log = "dropped all items";
                }
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }
            return log;
        }
    }


    //Shift + N
    internal class DropLeftHandHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public DropLeftHandHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "left hand is empty!";

            if (ConsoleKey.N == keyinfo.Key && shift)
            {
                if (player.LeftHand == null)
                {

                    return log;
                }
                else
                {
                    player.LeftHand.HandtoGround(room, player, 'L');
                    log = "dropped left hand";
                    return log;
                }


            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }
            return log;

        }

    }


    //Shift + M
    internal class DropRightHandHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public DropRightHandHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "right hand is empty!";

            if (ConsoleKey.M == keyinfo.Key && shift)
            {
                if (player.RightHand == null)
                {
                    return log;
                }
                else
                {
                    player.RightHand.HandtoGround(room, player, 'R');
                    log = "dropped right hand";
                    return log;
                }


            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }
            return log;

        }
    }


    //Q
    internal class DrinkPotionHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public DrinkPotionHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler; 
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "item is not a potion!";

            if (ConsoleKey.Q == keyinfo.Key && !shift)
            {
                if (player.Inventory.Count == 0)
                {
                    log = "inventory is empty!";
                    return log;
                }


                if (player.Inventory[player.SelectedItem].Drink(player, room) == 0)
                {
                    return log;
                }

                log = "player drank a potion";
                return log;


            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }


                return log;

            }
        }
    }


    //M (no item in hand)
    internal class AddRightHandHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public AddRightHandHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "inventory is empty!";

            if (ConsoleKey.M == keyinfo.Key && player.RightHand == null && !shift)
            {
                if (player.Inventory.Count == 0)
                {
                    return log;
                }
                else
                {
                    if(player.InventorytoHand('R')==0)
                    {
                        log = "you can only hold weapons";
                        return log;
                    }
                    
                    log = "added item to right hand";
                    return log;
                }

            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }

            return log;
        }

    }


    //M (exists item in hand)
    internal class RemoveRightHandHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public RemoveRightHandHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "inventory is full!";
            if (ConsoleKey.M == keyinfo.Key && !shift && player.RightHand != null)
            {
                player.HandtoInventory('R', room);
                log = "removed item from right hand";
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }


            return log;
        }

    }

    //N (no item in hand)
    internal class AddLeftHandHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public AddLeftHandHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "inventory is empty!";

            if (ConsoleKey.N == keyinfo.Key && player.LeftHand == null && !shift)
            {
                if (player.Inventory.Count == 0)
                {
                    return log;
                }
                else
                {
                   if( player.InventorytoHand('L')==0)
                    {
                        log = "you can only hold weapons";
                        return log;
                    }
                    log = "added item to left hand";
                    return log;
                }

            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }

            return log;
        }

    }

    //N (exists item in hand)
    internal class RemoveLeftHandHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;

        public RemoveLeftHandHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "inventory is full!";
            if (ConsoleKey.N == keyinfo.Key && !shift && player.LeftHand != null)
            {
                player.HandtoInventory('L', room);
                log = "removed item from left hand";
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }


            return log;
        }

    }


    //W + Shift - attacks the enemy on top
    internal class TopAttackHandler: IKeyControlHandler
    {
        private IKeyControlHandler? next;
        public TopAttackHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }

        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "attack failed";

            if (keyinfo.Key == ConsoleKey.W && shift)
            {
                if (player.Position[1]-1 <0)
                {
                    log = "there is no enemy to attack";
                    return log;
                }
                if (room.Grid[player.Position[0], player.Position[1]-1].Enemy == null)
                {
                    log = "there is no enemy to attack";
                    return log;
                }

                Combat combat = new();

                ConsoleKeyInfo info;
                info = Console.ReadKey();

                IAttackVisitor visitor = new NormalAttackVisitor();

                switch(info.KeyChar)
                {
                    case '2':
                        visitor = new StealthAttackVisitor();
                        break;
                    case '3':
                        visitor = new MagicAttackVisitor();
                        break;

                }

                string log1 = "";

                log = combat.AttackEnemy(player, room.Grid[player.Position[0], player.Position[1]-1].Enemy, room, visitor, player.Position[0] , player.Position[1] - 1);

                log1 = combat.ReceiveDamage(player, room.Grid[player.Position[0], player.Position[1]-1].Enemy, room, visitor);

                if(log1 == "")
                {
                    return log1;
                }

                log = log + "\n" + log1;

            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }

            return log;
        }
    }

    //A + Shift - attack enemy on left 
    internal class LeftAttackHandler: IKeyControlHandler
    {
        private IKeyControlHandler? next;
        public LeftAttackHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }

        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "attack failed";

            if (keyinfo.Key == ConsoleKey.A && shift)
            {
                if (player.Position[0] - 1 < 0)
                {
                    log = "there is no enemy to attack";
                    return log;
                }
                if (room.Grid[player.Position[0]-1 , player.Position[1]].Enemy == null)
                {
                    log = "there is no enemy to attack";
                    return log;
                }

                Combat combat = new();

                ConsoleKeyInfo info;
                info = Console.ReadKey();

                IAttackVisitor visitor = new NormalAttackVisitor();

                switch (info.KeyChar)
                {
                    case '2':
                        visitor = new StealthAttackVisitor();
                        break;
                    case '3':
                        visitor = new MagicAttackVisitor();
                        break;

                }

                string log1 = "";

                log = combat.AttackEnemy(player, room.Grid[player.Position[0]-1, player.Position[1]].Enemy, room, visitor, player.Position[0]-1, player.Position[1]);

                log1 = combat.ReceiveDamage(player, room.Grid[player.Position[0]-1, player.Position[1]].Enemy, room, visitor);

                if (log1 == "")
                {
                    return log1;
                }

                log = log + "\n" + log1;


            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }

            return log;
        }

    }


    //S + shift - attack enemy down 
    internal class DownAttackHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;
        public DownAttackHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }

        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "attack failed";

            if (keyinfo.Key == ConsoleKey.S && shift)
            {
                if (player.Position[1] +1 < 0)
                {
                    log = "there is no enemy to attack";
                    return log;
                }
                if (room.Grid[player.Position[0], player.Position[1] +1].Enemy == null)
                {
                    log = "there is no enemy to attack";
                    return log;
                }

                Combat combat = new();

                ConsoleKeyInfo info;
                info = Console.ReadKey();

                IAttackVisitor visitor = new NormalAttackVisitor();

                switch (info.KeyChar)
                {
                    case '2':
                        visitor = new StealthAttackVisitor();
                        break;
                    case '3':
                        visitor = new MagicAttackVisitor();
                        break;

                }

                string log1 = "";

                log = combat.AttackEnemy(player, room.Grid[player.Position[0], player.Position[1]+1 ].Enemy, room, visitor, player.Position[0] , player.Position[1]+1);

                log1 = combat.ReceiveDamage(player, room.Grid[player.Position[0], player.Position[1] +1].Enemy, room, visitor);

                if (log1 == "")
                {
                    return log1;
                }

                log = log + "\n" + log1;


            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }

            return log;
        }

    }

    //D + shift - attack enemy right 
    internal class RightAttackHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;
        public RightAttackHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }

        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "attack failed";

            if (keyinfo.Key == ConsoleKey.D && shift)
            {
                if (player.Position[0] +1 < 0)
                {
                    log = "there is no enemy to attack";
                    return log;
                }
                if (room.Grid[player.Position[0]+1, player.Position[1] ].Enemy == null)
                {
                    log = "there is no enemy to attack";
                    return log;
                }

                Combat combat = new();

                ConsoleKeyInfo info;
                info = Console.ReadKey();

                IAttackVisitor visitor = new NormalAttackVisitor();

                switch (info.KeyChar)
                {
                    case '2':
                        visitor = new StealthAttackVisitor();
                        break;
                    case '3':
                        visitor = new MagicAttackVisitor();
                        break;

                }

                string log1 = "";

                log = combat.AttackEnemy(player, room.Grid[player.Position[0]+1, player.Position[1]].Enemy, room, visitor, player.Position[0]+1, player.Position[1]);

                log1 = combat.ReceiveDamage(player, room.Grid[player.Position[0]+1, player.Position[1]].Enemy, room, visitor);

                if (log1 == "")
                {
                    return log1;
                }

                log = log + "\n" + log1;


            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }
            }

            return log;
        }

    }


    //esc
    internal class ExitHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;
        public ExitHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {
            next = nexthandler;
        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            string log = "cannot close the game";
            if (keyinfo.Key == ConsoleKey.Escape && !shift)
            {
                return "";
              
            }
            else
            {
                if (next != null)
                {
                    log = next.Action(keyinfo, shift, player, room);
                }

            }

            return log;
        }
    }

    internal class GuardHandler : IKeyControlHandler
    {
        private IKeyControlHandler? next;
        public GuardHandler() { next = null; }

        public void SetNext(IKeyControlHandler nexthandler)
        {

        }
        public string Action(ConsoleKeyInfo keyinfo, bool shift, Player player, Room room)
        {
            return "invalid key - refer to instructions";
        }



    }


}
