using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal interface IEnemy
    {
        string Name { get; set; }
        int LifePoints {  get; set; }
        int AttackValue {  get; set; }
        int ArmorPoints {  get; set; }

    }

    internal class Dragon : IEnemy
    {
        public string Name { get; set; }
        public int LifePoints { get; set; }
        public int AttackValue { get; set; }
        public int ArmorPoints { get; set; }

        public Dragon(string _name = "Dragon")
        {
            Name = _name;
            LifePoints = 20;
            AttackValue = 15;
            ArmorPoints = 10;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }  
    
    internal class Snake : IEnemy
    {
        public string Name { get; set; }

        public int LifePoints { get; set; }
        public int AttackValue { get; set; }
        public int ArmorPoints { get; set; }

        public Snake(string _name = "Snake")
        {
            Name = _name;
            LifePoints = 10;
            AttackValue = 10;
            ArmorPoints = 4;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    } 
    
    internal class Wolf : IEnemy
    {
        public string Name { get; set; }


        public int LifePoints { get; set; }
        public int AttackValue { get; set; }
        public int ArmorPoints { get; set; }

        public Wolf(string _name = "Wolf")
        {
            Name = _name;
            LifePoints = 15;
            AttackValue = 8;
            ArmorPoints = 8;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }  
    
    internal class Spider : IEnemy
    {
        public string Name { get; set; }

        public int LifePoints { get; set; }
        public int AttackValue { get; set; }
        public int ArmorPoints { get; set; }

        public Spider(string _name = "Spider")
        {
            Name = _name;
            LifePoints = 5;
            AttackValue = 2;
            ArmorPoints = 0;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }



}
