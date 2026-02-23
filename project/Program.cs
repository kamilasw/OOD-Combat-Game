using System;
using System.ComponentModel.Design.Serialization;
using System.Linq.Expressions;
using System.Reflection.Metadata;


namespace project
{
    internal class Program
    {
        

        static IKeyControlHandler SetUpChain(bool items, bool potions)
        {
            IKeyControlHandler moveUp = new MoveUpHandler();
            IKeyControlHandler moveLeft = new MoveLeftHandler();
            IKeyControlHandler moveDown = new MoveDownHandler();
            IKeyControlHandler moveRight = new MoveRightHandler();
            IKeyControlHandler moveInvUp = new MoveInvUpHandler();
            IKeyControlHandler moveInvDown = new MoveInvDownHandler();
            IKeyControlHandler pickupOne = new PickUpItemHandler();
            IKeyControlHandler pickupAll = new PickUpItemsHandler();
            IKeyControlHandler dropOne = new DropInvItemHandler();
            IKeyControlHandler dropAll = new DropInvItemsHandler();
            IKeyControlHandler dropLeft = new DropLeftHandHandler();
            IKeyControlHandler dropRight = new DropRightHandHandler();
            IKeyControlHandler drinkPotion = new DrinkPotionHandler();
            IKeyControlHandler addRight = new AddRightHandHandler();
            IKeyControlHandler removeRight = new RemoveRightHandHandler();
            IKeyControlHandler addLeft = new AddLeftHandHandler();
            IKeyControlHandler removeLeft = new RemoveLeftHandHandler();
            IKeyControlHandler exit = new ExitHandler();
            IKeyControlHandler guard = new GuardHandler();

            IKeyControlHandler attackup = new TopAttackHandler();
            IKeyControlHandler attackleft = new LeftAttackHandler();
            IKeyControlHandler attackdown = new DownAttackHandler();
            IKeyControlHandler attackright = new RightAttackHandler();

            moveUp.SetNext(moveLeft);
            moveLeft.SetNext(moveDown);
            moveDown.SetNext(moveRight);
            moveRight.SetNext(moveInvUp);
            moveInvUp.SetNext(moveInvDown);
            moveInvDown.SetNext(pickupAll);
            pickupAll.SetNext(pickupOne);
            pickupOne.SetNext(dropAll);
            dropAll.SetNext(dropOne);
            dropOne.SetNext(dropLeft);
            dropLeft.SetNext(dropRight);
            dropRight.SetNext(drinkPotion);
            drinkPotion.SetNext(addRight);
            addRight.SetNext(removeRight);
            removeRight.SetNext(addLeft);
            addLeft.SetNext(removeLeft);
            removeLeft.SetNext(attackleft);
            attackleft.SetNext(attackright);
            attackright.SetNext(attackup);
            attackup.SetNext(attackdown);
            attackdown.SetNext(exit);
            exit.SetNext(guard);

            return moveUp;
        }
    
      
        static void Main(string[] args)
        {
        

            //set up the display
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            //builder

            IDungeonBuilder builder = DungeonBuilder.FilledDungeon();
               

            Room room = builder.Build();



            GameDisplay display = GameDisplay.Instance;

          
            
            //initialize player
            Player player = new();
     

            string log = "new game started"; //used for any info to the player about whats happening

            IKeyControlHandler handler = SetUpChain(true, true);



            //game loop 
            while (true)
            {
                if(log == "")
                {
                    display.EndGame();
                }
                display.CleanEnemy();
                display.CleanPlayerInfo(player);
                display.CleanItems(room,player);
                display.CleanAction();
                display.DisplayDungeon(room, player);
                int s = display.DisplayPlayerInfo(player);
                display.DisplayEnemy(room, player);
                display.DisplayItems(room, player);
                display.DisplayAction(log);
                display.DisplayInstructions(builder);
                display.CleanEffects();
                display.DisplayEffects(player);

                

                log = "";

                ConsoleKeyInfo info;

                Console.CursorVisible = false ;
                Console.SetCursorPosition(41, s);

                bool shift = false;
                info= Console.ReadKey();
                ConsoleKey key = info.Key;

                if ((info.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    shift = true;
                }
                else
                {
                    shift = false;
                }

                log = handler.Action(info, shift, player, room);

                display.Clearthatannoyingonechar(41, s);

                


               


                /*
                 * key controls:
                 * W A S D - moving
                 * up/down arrows - traversing through the inventory
                 * M - equiping chosen weapon in RIGHT hand (clicking M when sth is there removes it from hand)
                 * N - equiping chosen weapon in LEFT hand  (either M/N works for 2 handed weapons)
                 * E - equiping dropped the items from the grid to inventory
                 * L - leave  on the grid the chosen item from the inventory
                 * 
                 */

             

                Console.CursorVisible = false;

                /*
                 - inventory,
                 - currently equipped items,
                 - if the player is standing on an item, information about it,
                 - the player’s current attribute values,
                 - the number of collected coins and gold.
                */

                
            }




        }
    }
}
