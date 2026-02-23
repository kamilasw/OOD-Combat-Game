using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal interface ICurrency : IItem
    {

    }




    

    //currency:
    //    gold
    //    coin

    internal class Gold : ICurrency
    {
        public string Name {  get; set; }
      

       

        public int Drink(Player player, Room room) { return 0; }
        public Gold(string _name = "Gold")
        {
            Name = _name;
         
        }

        public void Collect(Player player)
        {

            //we assume its  gold
            player.Purse[1] += 1;

         

        }

        public override string ToString()
        {
            return $"{Name}";
        }


    }
    internal class Coin : ICurrency
    {
        public string Name { get; set; }
       

        public int Drink(Player player, Room room) { return 0; }
      
        public Coin(string _name = "Coin")
        {
            Name = _name;
          
        }

        public void Collect(Player player)
        {

            //we assume its  coin
            player.Purse[0] += 1;


        }

        public override string ToString()
        {
            return $"{Name}";
        }

    }

}
