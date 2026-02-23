using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal interface  IAttackVisitor
    {
        int VisitHeavyWeapon(IHeavyWeapon weapon, Player player);
        int VisitLightWeapon(ILightWeapon weapon, Player player);
        int VisitMagicWeapon(IMagicWeapon weapon, Player player);
        int VisitOtherWeapon(IOtherWeapon weapon, Player player);
    }

    internal interface IDefenceVisitor
    {
        int VisitHeavyWeapon(IHeavyWeapon weapon, Player player);
        int VisitLightWeapon(ILightWeapon weapon, Player player);
        int VisitMagicWeapon(IMagicWeapon weapon, Player player);
        int VisitOtherWeapon(IOtherWeapon weapon, Player player);
    }

    //attack visitors

     
    internal class NormalAttackVisitor: IAttackVisitor
    {
        public int VisitHeavyWeapon(IHeavyWeapon weapon, Player player)
        {
            return player.Attributes[0].value + player.Attributes[4].value;
        }
        public int VisitLightWeapon(ILightWeapon weapon, Player player)
        {
            return player.Attributes[1].value + player.Attributes[3].value;
        }
        
        public int VisitMagicWeapon(IMagicWeapon weapon, Player player)
        {
            return 1;
        }

        public int VisitOtherWeapon(IOtherWeapon weapon, Player player)
        {
            return 0;
        }
    }

    internal class StealthAttackVisitor : IAttackVisitor
    {
        public int VisitHeavyWeapon(IHeavyWeapon weapon, Player player)
        {
            return (player.Attributes[0].value + player.Attributes[4].value) / 2;
        }
        public int VisitLightWeapon(ILightWeapon weapon, Player player)
        {
            return 2 * (player.Attributes[1].value + player.Attributes[3].value);
        }

        public int VisitMagicWeapon(IMagicWeapon weapon, Player player)
        {
            return 1;
        }

        public int VisitOtherWeapon(IOtherWeapon weapon, Player player)
        {
            return 0;
        }
    }


    internal class MagicAttackVisitor : IAttackVisitor
    {
        public int VisitHeavyWeapon(IHeavyWeapon weapon, Player player)
        {
            return 1;
        }
        public int VisitLightWeapon(ILightWeapon weapon, Player player)
        {
            return 1;
        }

        public int VisitMagicWeapon(IMagicWeapon weapon, Player player)
        {
            return player.Attributes[5].value;
        }

        public int VisitOtherWeapon(IOtherWeapon weapon, Player player)
        {
            return 0;
        }
    }


    //defence visitors


    internal class RegularDefenseVisitor : IDefenceVisitor
    {
        public int VisitHeavyWeapon(IHeavyWeapon weapon, Player player)
        {
            return player.Attributes[0].value + player.Attributes[3].value; 
        }

        public int VisitLightWeapon(ILightWeapon weapon, Player player)
        {
            return player.Attributes[1].value + player.Attributes[3].value;
        }
           

        public int VisitMagicWeapon(IMagicWeapon weapon, Player player)
        {
            return player.Attributes[1].value + player.Attributes[3].value;
        }

        public int VisitOtherWeapon(IOtherWeapon weapon, Player player)
        {
            return player.Attributes[1].value;
        }

    }

    internal class StealthDefenseVisitor : IDefenceVisitor
    {
        public int VisitHeavyWeapon(IHeavyWeapon weapon, Player player)
        {
            return player.Attributes[0].value;
        }

        public int VisitLightWeapon(ILightWeapon weapon, Player player)
        {
            return player.Attributes[1].value;
        }

        public int VisitMagicWeapon(IMagicWeapon weapon, Player player)
        {
            return 0;
        }

        public int VisitOtherWeapon(IOtherWeapon weapon, Player player)
        {
            return 0;
        }
    }

    internal class MagicDefenseVisitor : IDefenceVisitor
    {
        public int VisitHeavyWeapon(IHeavyWeapon weapon, Player player)
        {
            return player.Attributes[3].value;
        }

        public int VisitLightWeapon(ILightWeapon weapon, Player player)
        {
            return player.Attributes[3].value; 
        }

        public int VisitMagicWeapon(IMagicWeapon weapon, Player player)
        {
            return player.Attributes[5].value * 2;
        } 

  
        public int VisitOtherWeapon(IOtherWeapon weapon, Player player)
        {
            return player.Attributes[3].value; 
        }
    }
}
