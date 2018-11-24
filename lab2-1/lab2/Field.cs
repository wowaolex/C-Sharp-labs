using System;
using System.Drawing;
using System.Linq;
using System.Text;


namespace lab2
{
    class Field
    {
        private static Field instance;
        private int size;
        private string[,] field;
        Random ran = new Random();
        Point current; //координаты пустого поля
        private GameHistory history;

        public static Field getInstance(int size)
        {
            if (instance == null)
                instance = new Field(size);
            return instance;
        }
        public Field(int size)
        {
            this.size = size;
            field = new string[size, size];
            history = new GameHistory();
        }
        public void SaveGame()
        {
            history.History.Push(new FieldMemento(field, size, current));
        }
        public bool LoadGame()
        {
            FieldMemento lastMemento = null;
            if (history.History.Count <= 0)
                return false;

            lastMemento = history.History.Pop();
            field = lastMemento.Field;
            size = lastMemento.Size;
            current = lastMemento.Current;
            ShowField();
            return true;
        }

        private bool CheckingForRepeat(string elem)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] == elem)
                        return true;
                }
            }
            return false;
        }
        public string[,] GetField()
        {
            string forCheck;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    forCheck = Convert.ToString(ran.Next(0, size * size));
                    while (CheckingForRepeat(forCheck) == true)
                    { forCheck = Convert.ToString(ran.Next(0, size * size)); }
                    field[i, j] = forCheck;
                    if (field[i, j] == "0") current = new Point(i, j);
                }
            }
            Console.WriteLine();
            return field;
        }
        public void ShowField()
        {
            Console.Clear();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if ((i == current.X) && (j == current.Y))
                    {
                        field[i, j] = "-_-";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.ResetColor();
                    }
                    Console.Write("{0,4}", field[i, j]);
                }
                Console.WriteLine();
            }
        }

        public string[] ToVectorArr(string[,] arr)
        {
            string[] newArr = new string[size * size];
            int count = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    newArr[count] = arr[i, j];
                    count++;
                }
            return newArr;
        }
        public string[,] ToTwoDimArr(string[] arr)
        {
            string[,] newArr = new string[size, size]; ;
            int count = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    newArr[i, j] = arr[count];
                    count++;
                }
            return newArr;
        }

        public void ReplaceCoursor(string[,] array)
        {
            string face = "-_-";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (array[i, j] == face)
                    {
                        array[i, j] = "0";
                    }
                }
            }
        }
        public void ReturnCoursor(string[,] array)
        {
            string zero = "0";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (array[i, j] == zero)
                    {
                        array[i, j] = "-_-";
                    }
                }
            }
        }

        public string[,] SortArr(string[,] myField)
        {
            myField = field;
            ReplaceCoursor(myField);
            string[] fieldArr = ToVectorArr(myField);
            int[] arrInt = fieldArr.Select(x => int.Parse(x)).ToArray();
            Array.Sort(arrInt);
            string[] arrStr = arrInt.Select(x => Convert.ToString(x)).ToArray();
            string[,] arrForCheck = ToTwoDimArr(arrStr);
            return arrForCheck;
        }

        public bool CheckWin()
        {
            string[,] temp = field;
            string[,] fieldCheck = SortArr(temp);
            ReplaceCoursor(field);

            if (ToVectorArr(fieldCheck).SequenceEqual(ToVectorArr(field)))
                return true;
            else
            {
                ReturnCoursor(field);
                return false;

            }
                  
        }

        public void SwapArrayElements(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }

        public void MoveRight()
        {
            if (current.Y < field.GetLength(1) - 1)
            {
                SwapArrayElements(ref field[current.X, current.Y], ref field[current.X, current.Y + 1]);
                current.Y++;
            }
        }
        public void MoveLeft()
        {
            if (current.Y > 0)
            {
                SwapArrayElements(ref field[current.X, current.Y], ref field[current.X, current.Y - 1]);
                current.Y--;
            }
        }
        public void MoveUp()
        {
            if (current.X > 0)
            {
                SwapArrayElements(ref field[current.X - 1, current.Y], ref field[current.X, current.Y]);
                current.X--;
            }
        }
        public void MoveDown()
        {
            if (current.X < field.GetLength(0) - 1)
            {
                SwapArrayElements(ref field[current.X + 1, current.Y], ref field[current.X, current.Y]);
                current.X++;
            }
        }
    }
}
