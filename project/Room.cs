//char fullblock = '\u2588';

namespace project
{
    internal class Room
    {
        public Cell[,] Grid;

        public Room()
        {
            //initialize grid 
            Grid = new Cell[40, 20];
        }

    }

    internal class Cell
    {
        public List<IItem> Items = new();
        char character = ' ';
        public ConsoleColor Color;
        public IEnemy? Enemy;

        public Cell()
        {
            character = ' ';
            Color = ConsoleColor.White;
            Enemy = null;
        }


        //remove enemy

        public void RemoveEnemy()
        {
            Enemy = null;
            List<ConsoleColor> colors = new List<ConsoleColor>();
            colors.Add(ConsoleColor.Yellow);
            colors.Add(ConsoleColor.Green);
            colors.Add(ConsoleColor.Blue);
            colors.Add(ConsoleColor.Cyan);
            colors.Add(ConsoleColor.Magenta);

            Random random = new Random();
            Color = colors[random.Next() % (colors.Count)];
            if (Items.Count > 0)
            {

                character = Items[0].Name[0];
            }
            else
            {
                character = ' ';
            }

        }
        //add enemy

        public int AddEnemy(IEnemy _enemy)
        {
            if (Enemy != null) { return 0; }
            Enemy = _enemy;
            Color = ConsoleColor.Red;
            character = Enemy.Name[0];
            return 1;
        }

        //set wall
        public void SetWall()
        {
            if (Items.Count == 0) { character = '█'; }

        }

        public void RemoveWall()
        {
            if(character != '█') { return; }
            character = ' ';
            
        }

        //add item to list

        public void AddItem(IItem newitem)
        {

            if(character== '█') { return; }
            Items.Add(newitem);
            character = Items[0].Name[0];

            List<ConsoleColor> colors = new List<ConsoleColor>();
            colors.Add(ConsoleColor.Yellow);
            colors.Add(ConsoleColor.Green);
            colors.Add(ConsoleColor.Blue);
            colors.Add(ConsoleColor.Cyan);
            colors.Add(ConsoleColor.Magenta);

            Random random = new Random();
            Color = colors[random.Next() % (colors.Count)];

        }

        //remove all items from list and return them as an array

        public List<IItem> RemoveItems()
        {
            List<IItem> removeditems = new();
            foreach(IItem i in Items)
            {
                removeditems.Add(i);
            }
            Items.Clear();
            character = ' ';
            return removeditems;
        }

        public List<IItem> RemoveItem()
        {
            List<IItem> removeditem = new();

            removeditem.Add(Items[0]);
            Items.RemoveAt(0);

            if(Items.Count==0)
            {
                character = ' ';
            }
            else
            {
                character = Items[0].Name[0];
            }

            return removeditem;

        }

        //displaycell - used for displaying the whole grid later
        public override string ToString ()
        {
            return $"{character}";
        }


   


    }
    
}
