using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class MiniGame : MonoBehaviour
    {
        public virtual void FinishGame(bool result)
        {
            Game.S.isMinigameEnabled = false;
            Destroy(this.gameObject);
            if (!result)
            {
                print("Игра проиграна");
            }
            else
            {
                print("Игра пройдена");
            }

            //Удалить
            Game.S.StartGame();
        }
    }
}
