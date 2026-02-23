using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace project
{

   
    internal interface IDungeonBuilder
    {

        //IDungeonBuilder FilledDungeon();
        //IDungeonBuilder EmptyDungeon();

        IDungeonBuilder AddWalls();
        IDungeonBuilder EmptyWalls();

        IDungeonBuilder AddPaths();
        IDungeonBuilder AddChambers();
        IDungeonBuilder AddCentalRoom();
        IDungeonBuilder AddItems();
        IDungeonBuilder AddWeapons();
        IDungeonBuilder AddModifiedWeapons();
        IDungeonBuilder AddPotions();
        IDungeonBuilder AddEnemies();
        Room Build();
        public List<string> GetInstruction();



    }

    internal class DungeonBuilder:IDungeonBuilder
    {
        private Room room = new Room();

        private List<string> instructions = new List<string>();


        private DungeonBuilder() { }

       
        public static IDungeonBuilder EmptyDungeon()
        {
            IDungeonBuilder builder = new DungeonBuilder();

            builder.EmptyWalls();

            return builder;
        }

        public IDungeonBuilder EmptyWalls()
        {

            instructions.Add("W A S D - moving ESC - exit game");

            for (int i = 0; i < room.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < room.Grid.GetLength(1); j++)
                {
                    room.Grid[i, j] = new Cell();
                }
            }
            return this;
        }

        public static IDungeonBuilder FilledDungeon()
        {
            IDungeonBuilder builder = DungeonBuilder.EmptyDungeon();

            builder.AddWalls()
                .AddPaths()
                .AddChambers()
                .AddCentalRoom()
                .AddItems()
                .AddWeapons()
                .AddPotions()
                .AddModifiedWeapons()
                .AddEnemies();

            return builder;

        }

        public IDungeonBuilder AddWalls()
        {
            instructions.Add("you cannot walk into walls!");

            for (int i = 0; i < room.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < room.Grid.GetLength(1); j++)
                {
                    room.Grid[i, j].SetWall();
                }
            }

            return this;
        }

        public IDungeonBuilder AddPaths()
        {
            Random rnd = new(int.Parse(DateTime.Now.ToString("HHmmss")));

            room.Grid[0, 0].RemoveWall();
          
            //horizontal

           for(int k=0;k<100;k++)
           {
                int length = rnd.Next();
                int x_start = rnd.Next(0, 40);
                int y_start = rnd.Next(0, 20);
                int horizontal = k % 2;

                switch (horizontal)
                {
                    case 0:
                        for (int i = x_start; i < (x_start + length) % 40; i++)
                        {
                            room.Grid[i, y_start].RemoveWall();
                        }

                        break;
                    case 1:

                        for (int i = y_start; i < (y_start + length) % 20; i++)
                        {
                            room.Grid[x_start, i].RemoveWall();
                        }

                        break;
                }
           }


            return this;
        }
        public IDungeonBuilder AddChambers()
        {
            Random rnd = new(int.Parse(DateTime.Now.ToString("HHmmss")));

            //lets make the chambers at most 3x3
            //we add 5 chambers

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    room.Grid[i, j].RemoveWall();
                }

            }
            for (int k = 0; k < 20; k++)
            {

                int x_start = rnd.Next(0, 40);
                int y_start = rnd.Next(0, 20);
                int x_size = rnd.Next(1, 5);
                int y_size = rnd.Next(1, 5);

                for (int i = x_start; i < (x_start + x_size) % 40; i++)
                {
                    for (int j = y_start; j < (y_start + y_size) % 20; j++)
                    {
                        room.Grid[i, j].RemoveWall();
                    }
                }
            }

            return this;

        }
        public IDungeonBuilder AddCentalRoom()
        {
            Random rnd = new(int.Parse(DateTime.Now.ToString("HHmmss")));

            //lets make the central room at the center make it 6x4

            

            for (int i = 20-4-1; i < 20+4-1; i++)
            {
                for (int j = 10-1-3; j < 10+3-1; j++)
                {
                    if(i!=0 && j != 0)
                    {
                        room.Grid[i, j].RemoveWall();
                    }
                    
                }
            }


            return this;
        }
        public IDungeonBuilder AddItems() //i assume we dont add any weapons here
        {
            List<IItem> dropped = [];
            Rock rock = new(); dropped.Add(rock);
            Sand sand = new(); dropped.Add(sand);
            Leaf leaf = new(); dropped.Add(leaf);
            Gold gold = new(); dropped.Add(gold); 
            Coin coin = new(); dropped.Add(coin);

            instructions.Add("up / down arrows - go through inventory");
            instructions.Add("M/N -equiping chosen item in a hand");
            instructions.Add("Shift + M/N - dropping item from a hand");
            instructions.Add("(M/N again removes item from hand)");
            instructions.Add("E - collecting one item from ground");
            instructions.Add("Shift + E - collecting all the items from ground");
            instructions.Add("L - drop chosen item from the inventory");
            instructions.Add("Shift + L - drop all items");





            Random rnd = new(int.Parse(DateTime.Now.ToString("HHmmss")));
            for (int i = 0; i < 60; i++)
            {
                int x = rnd.Next(0, 39);
                int y = rnd.Next(0, 19);
                if(room.Grid[x, y].ToString() == "█")
                {
                    i++;
                    continue;
                }
                if (x != 0 && y != 0)
                {
                    room.Grid[x, y].AddItem(dropped[rnd.Next(0, dropped.Count )]);
                }
            }

            return this;


        }
        public IDungeonBuilder AddWeapons()
        {

            List<IItem> dropped = [];
            Sword sword = new(); dropped.Add(sword);
            Polearm polearm = new(); dropped.Add(polearm);
            Dagger dagger = new(); dropped.Add(dagger);
            Crossbow crossbow = new(); dropped.Add(crossbow);
            Sickle sickle = new(); dropped.Add(sickle);
            Axe axe = new(); dropped.Add(axe);

 

            Random rnd = new(int.Parse(DateTime.Now.ToString("HHmmss")));
            for (int i = 0; i < 60; i++)
            {
                int x = rnd.Next(0, 39);
                int y = rnd.Next(0, 19);
                if (room.Grid[x, y].ToString() == "█")
                {
                    i++;
                    continue;
                }
                if (x != 0 && y != 0)
                {
                    room.Grid[x, y].AddItem(dropped[rnd.Next(0, dropped.Count)]);
                }
            }

            return this;

        }
        public IDungeonBuilder AddModifiedWeapons()
        {
            Type[] baseWeaponTypes = new[]
            {
                typeof(Sword), typeof(Polearm), typeof(Dagger),
                typeof(Crossbow), typeof(Sickle), typeof(Axe)
            };

            Random rnd = new(int.Parse(DateTime.Now.ToString("HHmmss")));


            for (int i = 0; i < 20; i++)
            {

                Type chosenType = baseWeaponTypes[rnd.Next(baseWeaponTypes.Length)];
                IWeapon freshWeapon = (IWeapon)Activator.CreateInstance(chosenType)!;


                IWeapon finalWeapon;
                if (rnd.Next(2) == 0)
                {
                    finalWeapon = new LuckyWeapon(freshWeapon);
                }
                else
                {
                    finalWeapon = new StrongWeapon(freshWeapon);

                }


               
               
                int x, y;
                while (true)
                {
                    x = rnd.Next(0, 39);
                    y = rnd.Next(0, 19);
                    if (room.Grid[x, y].ToString() != "█")
                        break;
                }
                room.Grid[x, y].AddItem(finalWeapon);
            }

            return this;



        }
        public IDungeonBuilder AddPotions()
        {
            List<IItem> dropped = [];

            instructions.Add("Q - drink selected potion");

      

            dropped.Add(new HealthPotion(new Potion()));
            dropped.Add(new WisdomPotion(new Potion()));
            dropped.Add(new WisdomPotion(new HealthPotion(new Potion())));
            dropped.Add(new StrengthPotion(new Potion()));
            dropped.Add(new AntidotePotion(new Potion()));

            Random rnd = new(int.Parse(DateTime.Now.ToString("HHmmss")));
            for (int i = 0; i < 60; i++)
            {
                int x = rnd.Next(0, 39);
                int y = rnd.Next(0, 19);
                if (room.Grid[x, y].ToString() == "█")
                {
                    i++;
                    continue;
                }
                if (x != 0 && y != 0)
                {
                
                    room.Grid[x, y].AddItem(dropped[rnd.Next(0, dropped.Count)]);
                }
            }

            return this;

        }
        public IDungeonBuilder AddEnemies()
        {
            List<IEnemy> dropped = [];

           
            Dragon dragon = new(); dropped.Add(dragon);
            Snake snake = new Snake(); dropped.Add(snake);
            Spider spider = new Spider(); dropped.Add(spider);
            Wolf wolf = new Wolf(); dropped.Add(wolf);

            instructions.Add("you cannot walk into enemies!");
            instructions.Add("W/A/S/D + Shift + 1/2/3 to attack");
            


            Random rnd = new(int.Parse(DateTime.Now.ToString("HHmmss")));
            for (int i = 0; i < 20; i++)
            {
                int x = rnd.Next(0, 39);
                int y = rnd.Next(0, 19);
                if (room.Grid[x, y].ToString() == "█" )
                {
                    i++;
                    continue;
                }
                if (x != 0 && y != 0)
                {
                    room.Grid[x, y].AddEnemy(dropped[rnd.Next(0,dropped.Count)]);
                }
            }

            return this;

        }


        public Room Build()
        {
            return room;
        }


        public List<string> GetInstruction()
        {
            return instructions;
        }


     

    }

    
}
