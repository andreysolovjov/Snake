using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Media;

namespace Snake
{
	class Program
	{
		static void Main( string[] args )
		{
			Music();
			Console.SetWindowSize( 100, 25 );
			
			Walls walls = new Walls( 80, 25 );
			walls.Draw();
	
			Point p = new Point( 4, 5, '*' );
			Snake snake = new Snake( p, 4, Direction.RIGHT );
			snake.Draw();

			Score score = new Score(85, 2);
			score.UpdateScore();

			FoodCreator foodCreator = new FoodCreator( 80, 25, '$' );
			Point food = foodCreator.CreateFood();
			food.Draw(ConsoleColor.Blue);

			FoodCreator badfoodCreator = new FoodCreator(30, 7, '@');
			Point badfood = badfoodCreator.CreateFood();
			badfood.Draw(ConsoleColor.Red);

			while (true)
			{
				if ( walls.IsHit(snake) || snake.IsHitTail() )
				{
					break;
				}
				if(snake.Eat(food) )
				{
					Score.score += 10;
					score.UpdateScore();
					food = foodCreator.CreateFood();
					food.Draw(ConsoleColor.Blue);

				}

				if (snake.BadEat(badfood))
				{
					Score.score -= 10;
					score.UpdateScore();
					badfood = badfoodCreator.CreateFood();
					badfood.Draw(ConsoleColor.Red);
				}

				if (Score.score == -10)
				{
					WriteGameOver();
					Console.ReadLine();
				}

				else
				{
					snake.Move();
				}

				Thread.Sleep( 100 );
				if ( Console.KeyAvailable )
				{
					ConsoleKeyInfo key = Console.ReadKey();
					snake.HandleKey( key.Key );
				}
			}
			WriteGameOver();
			Console.ReadLine();
      }


		static void WriteGameOver()
		{
			Console.Clear();
			int xOffset = 20;
			int yOffset = 8;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.SetCursorPosition( xOffset, yOffset++ );
			WriteText( "============================", xOffset, yOffset++ );
			WriteText( "	  GAME OVER", xOffset + 1, yOffset++ );
			yOffset++;
			WriteText( "Autor: Andrey Soloviev", xOffset + 2, yOffset++ );
			WriteText( "Желаете записать результат в таблицу очков?(Esc-выйти из программы/Enter-да)", xOffset + 1, yOffset++ );
			WriteText("Spacebar-Посмотреть таблицу результатов", xOffset, yOffset++ );
			if (Console.ReadKey().Key == ConsoleKey.Enter)
			{
				WriteTOfile();
				WriteGameOver();
			}
			if (Console.ReadKey().Key == ConsoleKey.Escape)
			{
				Exit();

			}
			if (Console.ReadKey().Key == ConsoleKey.Spacebar)
			{
				ShowTabel();

			}
		}

		static void WriteText( String text, int xOffset, int yOffset )
		{
			Console.SetCursorPosition( xOffset, yOffset );
			Console.WriteLine( text );
		}

		static void Exit()
		{
			Environment.Exit(0);
		}

		static void WriteTOfile()
		{
			string name;
			string Score_final = Score.score.ToString();
			Console.WriteLine("Напишите своё имя");
			name = Console.ReadLine();
			File.AppendAllText(@"C:\Users\Admin\Desktop\ScoreTabel.txt", name + ":" + Score_final + "\n");
		}

		static void ShowTabel()
		{
			Console.Clear();
			StreamReader fs = new StreamReader(@"C:\Users\Admin\Desktop\ScoreTabel.txt");
			while (!fs.EndOfStream)
			{
				string s = fs.ReadLine();
				Console.WriteLine(s);
			}
		}
		static void Music()
		{
			System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\Admin\source\repos\snake\Snake\Snake\Sounds\music1.wav");
			player.Play();

		}



	}
}
