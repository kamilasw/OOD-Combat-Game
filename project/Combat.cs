using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal class Combat
    {
        public string AttackEnemy(Player player, IEnemy enemy,Room room, IAttackVisitor visitor, int x, int y)
        {
            string log = "";


            int damage = 0;

            IAcceptVisitor left, right;

            if (player.LeftHand != null)
            {
                left = (IAcceptVisitor)player.LeftHand;
            }
            else
            {
                left = new BareHands();
            }

            if (player.RightHand != null)
            {
                right = (IAcceptVisitor)player.RightHand;
            }
            else
            {
                right = new BareHands();
            }

            damage = right.Accept(visitor, player);

            if (player.LeftHand != null)
            {
                if (player.LeftHand is IWeapon weaponLeft && weaponLeft.TwoHanded == false)
                {
                    damage += left.Accept(visitor, player);
                }
            }

            damage = Math.Max(0, damage - enemy.ArmorPoints);
            enemy.LifePoints -= damage;



            if(enemy.LifePoints<=0)
            {  
                log = $"enemy {enemy.ToString()} has beed defeated!";
                room.Grid[x, y].RemoveEnemy();
                return log;
            }
            else
            {
                log = $"player attacks enemy with {damage} damage";
                return log;
            }

        }

        public string ReceiveDamage(Player player, IEnemy enemy, Room room, IAttackVisitor visitor)
        {
            int defence = 0;

            if (enemy == null)
            {
                return "enemy is dead";
            }

            IAcceptVisitor left,right;

            if (player.LeftHand!=null)
            {
                left = (IAcceptVisitor) player.LeftHand;
            }
            else
            {
                left = new BareHands();
            }

            if (player.RightHand != null)
            {
                right = (IAcceptVisitor)player.RightHand;
            }
            else
            {
                right = new BareHands();
            }

            defence = right.Accept(visitor, player);

            if (player.LeftHand != null)
            {
                if (player.LeftHand is IWeapon weaponLeft && weaponLeft.TwoHanded == false)
                {
                    defence += left.Accept(visitor, player);
                }
            }

            int damage  = Math.Max(0, enemy.AttackValue - defence);
            player.Attributes[2].value -= damage;

            if (player.Attributes[2].value <= 0)
            {
                return "";
            }
            else
            {
                return $"player hit by enemy with {damage} damage";
            }


        }
    }
}
