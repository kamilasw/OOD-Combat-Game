using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    interface IEffectObserver
    {
        string Name { get; }
        void Tick(Player player);
        bool IsActive {  get; }
        void RemoveEffect(Player player);
    }

    class HealthEffectObserver: IEffectObserver
    {
        public string Name => "Health Boost";
        private int duration = 5;

        public void Tick(Player player)
        {
            duration--;
            
        }

        public bool IsActive => duration > 0;

        public void RemoveEffect(Player player)
        {
            player.Attributes[2].value -= 2; 
        }

        public  HealthEffectObserver(Player player)
        {
            player.Attributes[2].value += 2;
        }


    }


    class WisdomEffectObserver: IEffectObserver
    {
        public string Name => "Wisdom Boost";

        private int totalduration;
        private int currenttick = 0;
        private int lastmultiplier = 1;

        public bool IsActive => currenttick <= totalduration;

        public WisdomEffectObserver(Player player, int _totalduration)
        {
            totalduration = _totalduration;
            currenttick = 0;
            player.Attributes[5].value *= lastmultiplier;
            this.Tick(player);
        }

        public void Tick(Player player)
        {
           

            player.Attributes[5].value /= lastmultiplier;

            currenttick++;

            if(IsActive)
            {

                lastmultiplier = totalduration - currenttick + 1;
                player.Attributes[5].value *= lastmultiplier;

            }
            


        }

        public void RemoveEffect(Player player)
        {
            if (lastmultiplier != 0)
            {
                player.Attributes[5].value /= lastmultiplier;
            }
        }

    }

    class StrengthEffectObserver: IEffectObserver
    {
        public string Name => "Strength Boost";
        public bool IsActive { get; private set; } = true;

        public StrengthEffectObserver(Player player)
        {
            player.Attributes[0].value += 5;
      
        }

        public void Tick(Player player)
        {

        }

        public void RemoveEffect(Player player)
        {
            player.Attributes[0].value -= 5;
        }


    }


   

    class AntidoteEffectObserver: IEffectObserver
    {
        public string Name => "Antidote Boost";
        public bool IsActive { get; private set; } = true;

        public AntidoteEffectObserver(Player player)
        {
            this.Tick(player);

        }

        public void Tick(Player player)
        {
           for (int i = player.ActiveEffects.Count - 1; i >= 0; i--)
           {
                player.ActiveEffects[i].RemoveEffect(player);
                player.ActiveEffects.RemoveAt(i);
           }
        }

        public void RemoveEffect(Player player)
        {
            
        }

    }
}
