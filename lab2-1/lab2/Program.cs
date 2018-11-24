using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of the field");
            int size = int.Parse(Console.ReadLine());
            Field field = Field.getInstance(size);
            field.GetField();

            field.ShowField();

            while (true)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey();

                switch (keyinfo.Key)
                {
                    case ConsoleKey.S:
                        field.SaveGame();
                        break;

                    case ConsoleKey.Z:
                        field.LoadGame();
                        break;

                    case ConsoleKey.Spacebar:
                        if (field.CheckWin())
                        {
                            Console.Clear();
                            Console.WriteLine("Congratulations");
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        field.MoveUp();
                        field.ShowField();
                        break;

                    case ConsoleKey.DownArrow:
                        field.MoveDown();
                        field.ShowField();
                        break;

                    case ConsoleKey.LeftArrow:
                        field.MoveLeft();
                        field.ShowField();
                        break;

                    case ConsoleKey.RightArrow:
                        field.MoveRight();
                        field.ShowField();
                        break;
                }
                
            }
        }
    }
}

