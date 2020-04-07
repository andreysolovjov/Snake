using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Score
    {
        public static int score = 0;
        static int x, y;

        public Score(int posX, int posY)
        {
            x = posX;
            y = posY;
        }

        public void UpdateScore()
        {
            Console.SetCursorPosition(x, y);
            Console.Write("Score");
            Console.SetCursorPosition(x - 3, y + 1);
            Console.Write(score);
        }
    }
}
