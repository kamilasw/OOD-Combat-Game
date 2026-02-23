using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project
{
    internal class GameDisplay
    {

        private static readonly Lazy<GameDisplay> _instance = new Lazy<GameDisplay>(() => new GameDisplay());


        // [column, row] - start of the panel
        private int[] DungeonPanel = new int[2];
        private int[] PlayerPanel = new int[4];
        private int[] EnemyPanel = new int[2];
        private int[] ActionPanel = new int[2];
        private int[] ItemsPanel = new int[3];

        private int[] InstructionPanel = new int[2];

        private int[] EffectsPanel = new int[3];


        private GameDisplay() 
        {
            DungeonPanel[0] = 0;
            DungeonPanel[1] = 0;

            PlayerPanel[0] = 41;
            PlayerPanel[1] = 0;
            PlayerPanel[2] = 0;// sizeo of previous inv
            PlayerPanel[3] = 0;//size of hands previously


            EnemyPanel[0] = 92;
            EnemyPanel[1] = 0;

            ActionPanel[0] = 0;
            ActionPanel[1] = 21;

            ItemsPanel[0] = 92;
            ItemsPanel[1] = 7;
            ItemsPanel[2] = 0; //size of last previous items

            InstructionPanel[0] = 0;
            InstructionPanel[1] = 24;

            EffectsPanel[0] = 0;
            EffectsPanel[1] = 38;
            EffectsPanel[2] = 0;

        }

        public static GameDisplay Instance
        {
            get { return _instance.Value; }
        }


        public void DisplayDungeon(Room room, Player player)
        {
            int x = DungeonPanel[0];
            int y = DungeonPanel[1];


            for(int i=0;i<20;i++)
            {
                Console.SetCursorPosition(x, y + i);
                for(int j=0;j<40;j++)
                {
                    Console.ForegroundColor = room.Grid[j,i].Color;
                    Console.Write(room.Grid[j, i].ToString());
                    Console.ResetColor();
                }
            }

            

            Console.SetCursorPosition(x + player.Position[0], y + player.Position[1]);

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("\u00B6");

        }

        public void Clearthatannoyingonechar(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        public void DisplayEnemy(Room room, Player player)
        {
            int x = EnemyPanel[0];
            int y = EnemyPanel[1];

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("**************************************************");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Enemies:                                          ");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("**************************************************");

            if (player.Position[0] - 1 >= 0 && room.Grid[player.Position[0] - 1, player.Position[1]].Enemy != null)
            {
                IEnemy enemy = room.Grid[player.Position[0] - 1, player.Position[1]].Enemy;
                Console.SetCursorPosition(x, y++);
                Console.Write($"Left: {room.Grid[player.Position[0] - 1, player.Position[1]].Enemy.Name} ");
                Console.Write($"LP:{enemy.LifePoints} A:{enemy.AttackValue} D:{enemy.ArmorPoints}");
             

            }
            else
            {
                Console.SetCursorPosition(x, y++);
               
            }

            if (player.Position[0] + 1 < room.Grid.GetLength(0) && room.Grid[player.Position[0] + 1, player.Position[1]].Enemy != null)
            {
                IEnemy enemy = room.Grid[player.Position[0] + 1, player.Position[1]].Enemy;
                Console.SetCursorPosition(x, y++);
                Console.Write($"Right: {room.Grid[player.Position[0] + 1, player.Position[1]].Enemy.Name} ");
                Console.Write($"LP:{enemy.LifePoints} A:{enemy.AttackValue} D:{enemy.ArmorPoints}");

            }
            else
            {
                Console.SetCursorPosition(x, y++);
               
            }

            if (player.Position[1] - 1 >= 0 &&  room.Grid[player.Position[0], player.Position[1] - 1].Enemy != null)
            {
                IEnemy enemy = room.Grid[player.Position[0], player.Position[1] - 1].Enemy;
                Console.SetCursorPosition(x, y++);
                Console.Write($"Up: {room.Grid[player.Position[0], player.Position[1] - 1].Enemy.Name} ");
                Console.Write($"LP:{enemy.LifePoints} A:{enemy.AttackValue} D:{enemy.ArmorPoints}");

            }
            else
            {
                Console.SetCursorPosition(x, y++);
              
            }

            if (player.Position[1] + 1 < room.Grid.GetLength(1) && room.Grid[player.Position[0], player.Position[1] + 1].Enemy != null)
            {
                IEnemy enemy = room.Grid[player.Position[0], player.Position[1] + 1].Enemy;
                Console.SetCursorPosition(x, y++);
                Console.Write($"Down: {room.Grid[player.Position[0], player.Position[1] + 1].Enemy.Name} ");
                Console.Write($"LP:{enemy.LifePoints} A:{enemy.AttackValue} D:{enemy.ArmorPoints}");


            }
            else
            {
                Console.SetCursorPosition(x, y++);
              
            }


        }

        public void CleanEnemy()
        {
            int x = EnemyPanel[0];
            int y = EnemyPanel[1];

            y += 3; //skip ***enemy** 

            Console.SetCursorPosition(x, y++);    
            Console.WriteLine("Left:                                                "); 

            Console.SetCursorPosition(x, y++);    
            Console.WriteLine("Right:                                               ");

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Up:                                                  ");

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Down:                                                ");

        }

        //returns selected item position
        public int DisplayPlayerInfo(Player player)
        {
            int selectedrow = 0;

            int x = PlayerPanel[0];
            int y = PlayerPanel[1];


            //purse

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("**************************************************");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Player Stats:");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("**************************************************");

           

            Console.SetCursorPosition(x, y++);
            Console.WriteLine($"Coins: {player.Purse[0]}");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine($"Gold: {player.Purse[1]}");



            //hand
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("**************************************************");

            
            if (player.RightHand!=null)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine($"Right Hand: {player.RightHand.ToString()}");
                PlayerPanel[3]++;
            }


            if (player.LeftHand != null)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine($"Left Hand: {player.LeftHand.ToString()}");
                PlayerPanel[3]++;
            }
 
            //attributes

           
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("**************************************************");
     
       
    

            for (int i=0;i<player.Attributes.Length;i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine($"{player.Attributes[i].ToString()}: {player.Attributes[i].value}");
            }

            //inventory
          
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("**************************************************");
           
            selectedrow = y;
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Console.SetCursorPosition(x, y++);
                if (i == player.SelectedItem)
                {
                    selectedrow = y-1;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                Console.WriteLine($"{i + 1}. {player.Inventory[i]}");
                Console.ResetColor();
                PlayerPanel[2]++;
            }


            return selectedrow;

        }

        public void CleanPlayerInfo(Player player)
        {
            int x = PlayerPanel[0];
            int y = PlayerPanel[1];

            y += 3;

            Console.SetCursorPosition(x, y++);
            Console.Write("Coins:                                             ");
            Console.SetCursorPosition(x, y++);
            Console.Write("Gold:                                              ");

            y++;

          
          

            int temp = 0;
            if (player.RightHand != null) { temp++; }
            if (player.LeftHand != null) { temp++; }

            if (temp != PlayerPanel[3])
            {
                for (int i = 0; i < PlayerPanel[3]; i++)
                {
                    Console.SetCursorPosition(x, y++);
                    Console.Write("                                                   ");
                }

                Console.SetCursorPosition(x, y++);
                Console.Write("                                                   ");
            }
            else
            {
                for(int i = 0; i < PlayerPanel[3]; i++) { y++; }
                y++;
            }


            Console.SetCursorPosition(x, y++);
            Console.Write("Strength:                                          ");
            Console.SetCursorPosition(x, y++); 
            Console.Write("Dexterity:                                         ");
            Console.SetCursorPosition(x, y++);
            Console.Write("Health:                                            ");
            Console.SetCursorPosition(x, y++);
            Console.Write("Luck:                                              ");

            if (temp!= PlayerPanel[3])
            {
                Console.SetCursorPosition(x, y++);
                
                Console.Write("                                                   ");
                Console.SetCursorPosition(x, y++);
                Console.Write("                                                   ");
            }
            else
            {
                Console.SetCursorPosition(x, y++);
                Console.Write("Aggression:                                        ");
                Console.SetCursorPosition(x, y++);
                Console.Write("Wisdom:                                            ");
            }


          

            if (temp != PlayerPanel[3])
            {
                Console.SetCursorPosition(x, y++);
                Console.Write("                                                   ");
            }
            else
            {
                y++;
            }

         

            if (PlayerPanel[2]!=player.Inventory.Count || PlayerPanel[3]!=temp)
            {
                for (int i = 0; i < PlayerPanel[2]; i++)
                {
                    Console.SetCursorPosition(x, y++);
                    Console.Write("                                                   ");
                }
            }

            PlayerPanel[3] = 0;
            PlayerPanel[2] = 0;



        }

        public void DisplayItems(Room room, Player player)
        {

           

            int x = ItemsPanel[0];
            int y = ItemsPanel[1];

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("**************************************************");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Items on the ground:");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("**************************************************");

            

            if (room.Grid[player.Position[0], player.Position[1]].Items.Count == 0) { return; }

            for (int i=0;i< (room.Grid[player.Position[0], player.Position[1]].Items.Count);i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.WriteLine($"{i+1}. {room.Grid[player.Position[0], player.Position[1]].Items[i].ToString()}");
                ItemsPanel[2]++;
            }


           

        }

        public void CleanItems(Room room, Player player)
        {


            int x = ItemsPanel[0];
            int y = ItemsPanel[1];

            y += 3;

            if(room.Grid[player.Position[0], player.Position[1]].Items.Count != ItemsPanel[2])
            {
                for (int i = 0; i < ItemsPanel[2]; i++)
                {
                    Console.SetCursorPosition(x, y++);
                    Console.WriteLine("                                                  ");
                }
            }


            ItemsPanel[2] = 0;

        }
        public void DisplayAction(string log)
        {
            int x = ActionPanel[0];
            int y = ActionPanel[1];

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Player Action:");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine($"{log}");

        }

        public void CleanAction()
        {
            int x = ActionPanel[0];
            int y = ActionPanel[1];

            y++;

            Console.SetCursorPosition(x, y++);
            Console.Write("                                        ");
            Console.SetCursorPosition(x, y++);
            Console.Write("                                        ");

        }

        public void DisplayInstructions(IDungeonBuilder buider)
        {
            List<string> instructions = buider.GetInstruction();

            int x = InstructionPanel[0];
            int y = InstructionPanel[1];

            foreach(string i in instructions)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write(i);
            }

           

        }


        public void DisplayEffects(Player player)
        {
            int x = EffectsPanel[0];
            int y = EffectsPanel[1];

            Console.SetCursorPosition(x, y++);
            Console.Write("Active effects:");
            
            foreach(IEffectObserver effect in player.ActiveEffects)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write($"{effect.Name}");
                EffectsPanel[2]++;
            }

        }

        public void CleanEffects()
        {
            int x = EffectsPanel[0];
            int y = EffectsPanel[1];

            y++;

            for(int i = 0; i < EffectsPanel[2];i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write("                                        ");

            }

        }

        public void EndGame()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("game over");
            Environment.Exit(0);
        }

    }


}
