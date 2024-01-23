using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ConsoleCleaner;

namespace VirtualGUIMenu
{
    /// <summary>
    ///     You can override methods to change Setting for your Menu 
    /// </summary>
    public class DefaultMenu
    {
        #region Property
        public int StartingSpace = 0;
        public string Icon;
        public string Title;
        public string[] OptionList;
        public int MaxLofOption;
        public int MaxLofContent;
        public int MaxWidth;
        public int Space = 13;
        public double TitleSpaceEachSide;
        public int OptionQuit;
        public int[] CutLineIndexList;

        public int MinWidth = 0;
        public int MinSpace = 2;
        public int OtherCharsLength = 3;
        #endregion

        #region Constructor
        public DefaultMenu() { }
        #endregion

        #region Method
        /// <summary>
        ///     Sets values for the Menu
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="title"></param>
        /// <param name="propertyNameList"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetValue(string icon, string title, string[] optionList)
        {
            // Checks if Parameters is null
            this.Icon = icon ?? throw new ArgumentNullException(nameof(icon));
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.OptionList = optionList ?? throw new ArgumentNullException(nameof(optionList));
            this.SetOptionQuit();

            // Caculates Max Length values
            this.MaxLofOption = this.OptionList.Max(x => x.Length);
            this.MaxLofContent = Math.Max(MaxLofOption, Title.Length);
            this.MaxWidth = MaxLofContent + (this.Icon.Length + this.Space);
        }


        /// <summary>
        ///     Sets flexible Values for the Menu
        /// </summary>
        /// <param name="minWidth"></param>
        /// <param name="minSpace"></param>
        public void SetFlexibleValues(int minWidth, int minSpace)
        {
            if (minWidth < 30)
                throw new Exception("This value is too small");
            if (minSpace < 2)
                throw new Exception("This value is too small");
            this.MinWidth = minWidth;
            this.MinSpace = minSpace;
        }



        /// <summary>
        ///     Sets space for the menu
        /// </summary>
        /// <param name="space"></param>
        public virtual void SetSpace(int space)
        {
            this.Space = space;
            this.MaxWidth = MaxLofContent + (this.Icon.Length + this.Space);
        }


        /// <summary>
        ///     Set Option quit
        /// </summary>
        public virtual void SetOptionQuit()
        {
            this.OptionQuit = OptionList.Count();
        }


        /// <summary>
        ///     Sets CutLineIndex list
        /// </summary>
        /// <param name="cutLineIndexList"></param>
        public virtual void SetCutLineIndexList(int[] cutLineIndexList)
        {
            this.CutLineIndexList = cutLineIndexList;
        }

        /// <summary>
        ///     Sets Start Space for the Menu
        /// </summary>
        /// <param name="s"></param>
        public void SetStartingSpace(int s)
        {
            this.StartingSpace = s;
        }


        /// <summary>
        ///     Sets the ordinal of Choice to min value
        /// </summary>
        /// <param name="choice"></param>
        public virtual void SetChoiceToMinValue(ref int choice) => choice = 1;


        /// <summary>
        ///     Sets the ordinal of Choice to max value
        /// </summary>
        /// <param name="choice"></param>
        public virtual void SetChoiceToMaxValue(ref int choice) => choice = this.OptionList.Count();


        /// <summary>
        ///     Increase the ordinal of Choice
        /// </summary>
        /// <param name="choice"></param>
        public virtual void IncreaseOrdinalOfChoice(ref int choice)
        {
            if (choice == this.OptionList.Count())
                SetChoiceToMinValue(ref choice);
            else
                choice++;
        }


        /// <summary>
        ///     Decreases the ordinal of Choice
        /// </summary>
        /// <param name="choice"></param>
        public virtual void DecreaseOrdinalOfChoice(ref int choice)
        {
            if (choice == 1)
                SetChoiceToMaxValue(ref choice);
            else
                choice--;
        }


        /// <summary>
        ///     Checks if the number key is valid
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public virtual bool IsValidNumber(ConsoleKeyInfo k) => (int.Parse(k.KeyChar.ToString()) <= this.OptionList.Count());


        /// <summary>
        ///     Prints the starting Space
        /// </summary>
        public void PrintStartingSpace()
        {
            for (int i = 0; i < StartingSpace; i++)
            {
                Console.Write(' ');
            }
        }


        /// <summary>
        ///     Prints the Menu
        /// </summary>
        /// <param name="choice"></param>
        private void Print(int choice)
        {
            int width = new int();
            if (this.MinWidth != 0)
            {
                if (this.MaxLofContent > (this.MinWidth - this.Icon.Length - this.MinSpace - this.OtherCharsLength))
                    width = this.MaxLofContent + this.Icon.Length + this.MinSpace + this.OtherCharsLength;
                else
                    width = MinWidth;
            }
            else
            {
                width = this.MaxWidth;
            }

            // StartLine
            this.PrintStartingSpace();
            for (int i = 1; i <= width; i++)
            {
                if (i == 1)
                    Console.Write('╔');
                else if (i == width)
                    Console.Write("╗\n");
                else
                    Console.Write('═');
            }

            // Print Title
            this.TitleSpaceEachSide = (double)(width - this.Title.Length) / 2;
            this.PrintStartingSpace();
            string titleRow = string.Empty;
            if (this.TitleSpaceEachSide == (int)this.TitleSpaceEachSide)
            {
                string titleSpace = string.Empty;
                for (int i = 1; i < this.TitleSpaceEachSide; i++)
                    titleSpace += '.';
                titleRow = '║' + titleSpace + this.Title + titleSpace + '║';
            }
            else
            {
                string titleSpace = string.Empty;
                for (int i = 1; i < (this.TitleSpaceEachSide - 0.5); i++)
                    titleSpace += '.';
                titleRow = '║' + titleSpace + this.Title + titleSpace + '.' /*the different here*/ + '║';
            }
            Console.WriteLine(titleRow);

            // Options StartLine
            this.PrintStartingSpace();
            for (int i = 1; i <= width; i++)
            {
                if (i == 1)
                    Console.Write('║');
                else if (i == width)
                    Console.Write("║\n");
                else
                    Console.Write('═');
            }

            // Prints Options
            for (int i = 0; i < this.OptionList.Count(); i++)
            {
                this.PrintStartingSpace();
                string option = ' ' + this.OptionList[i];
                for (int j = 1; j <= width; j++)
                {
                    if (j == 1)
                        Console.Write('║');
                    else if (j == width)
                        Console.Write("║\n");
                    else if (j == 2)
                    {
                        int colorNum = i + 1;
                        while (colorNum > 15) { colorNum -= 15; }
                        option.PintWithColor(colorNum);
                        j += option.Length - 1;
                    }
                    else if (j == width - this.Icon.Length)
                    {
                        if (i == choice - 1)
                            Console.Write(this.Icon);
                        else
                            for (int m = 0; m < Icon.Length; m++)
                                Console.Write(' ');
                        j += this.Icon.Length - 1;
                    }
                    else Console.Write(" ");
                }

                // Print CutLine
                if (CutLineIndexList != null )
                {
                    foreach (int cutLineIndex in CutLineIndexList)
                    {
                        if (cutLineIndex == i)
                        {
                            this.PrintStartingSpace();
                            for (int m = 1; m <= width; m++)
                            {
                                if (m == 1)
                                    Console.Write('║');
                                else if (m == width)
                                    Console.Write("║\n");
                                else
                                    Console.Write('═');
                            }
                            break;
                        }
                    }
                }

            }

            // EndLine
            this.PrintStartingSpace();
            for (int i = 1; i <= width; i++)
            {
                if (i == 1)
                    Console.Write('╚');
                else if (i == width)
                    Console.Write("╝\n");
                else
                    Console.Write('═');
            }
        }


        /// <summary>
        ///     Returns the key intered by the user
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="quit"></param>
        protected virtual void ReturnChoice(ref int choice, ref bool quit)
        {
            ConsoleKeyInfo k = Console.ReadKey(true);
            switch (k.Key)
            {
                // Number keys
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                case ConsoleKey.D7:
                case ConsoleKey.NumPad7:
                case ConsoleKey.D8:
                case ConsoleKey.NumPad8:
                case ConsoleKey.D9:
                case ConsoleKey.NumPad9:
                    ReturnNum(k, ref choice);
                    break;

                // Up and Down keys
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    DecreaseOrdinalOfChoice(ref choice);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    IncreaseOrdinalOfChoice(ref choice);
                    break;

                // Quit keys
                case ConsoleKey.Escape:
                case ConsoleKey.Backspace:
                    choice = this.OptionList.Count();
                    quit = true;
                    break;

                // Enter keys
                case ConsoleKey.Enter:
                    quit = true;
                    break;

                // Default keys
                default:
                    Console.WriteLine("Vui long chi su dung Up/Down, W/S Keys");
                    Console.ReadKey();
                    break;
            }
        }


        /// <summary>
        ///     Checks and returns a number if the user input a number key
        /// </summary>
        /// <param name="k"></param>
        /// <param name="choice"></param>
        public virtual void ReturnNum(ConsoleKeyInfo k, ref int choice)
        {

            if (IsValidNumber(k))
                choice = int.Parse(k.KeyChar.ToString());
            else
            {
                Console.WriteLine("If you wanna use number keys! Please use a correct number");
                Console.ReadKey(true);
            }
        }


        /// <summary>
        ///     Runs the menu
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int Run(int choice)
        {
            bool quit = false;
            do
            {
                // Menu runs here
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Console.SetCursorPosition(0, 0);
                //BetterConsole.Clear();
                this.Print(choice);
                ReturnChoice(ref choice, ref quit);
            }
            while (quit == false);
            return choice;
        }
        #endregion
    }
}
