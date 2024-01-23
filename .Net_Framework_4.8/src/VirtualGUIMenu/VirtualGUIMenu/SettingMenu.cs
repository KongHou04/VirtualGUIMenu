using System;
using System.Linq;
using ConsoleCleaner;


namespace VirtualGUIMenu
{
    /// <summary>
    ///     You can override methods to change Setting for your Menu 
    /// </summary>
    public class SettingMenu
    {
        #region Property
        public int StartingSpace = 0;
        public string Icon;
        public string Title;
        public string[] PropertyNameList;
        public string[] ButtonList;
        public int[] ValueSettingList;
        public string[][] ValueList;
        public int MaxLofPropertyName;
        public int MaxLofValue;
        public int MaxLofButton;
        public int MaxLofOption;
        public int MaxWidth;
        public double TitleSpaceEachSide;
        public int KeyIndex;
        public int MaxOfProperty;
        public bool[] BlockList;
        public int OptionQuit;
        public int Space = 7;
        #endregion


        #region Constructor
        public SettingMenu() { }
        #endregion


        #region Method
        /// <summary>
        ///     Sets values for the Menu
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="prop_name_list"></param>
        /// <param name="button_list"></param>
        /// <param name="value_setting_list"></param>
        /// <param name="value_list"></param>
        public void SetValue(string icon, string title, string[] prop_name_list, string[] button_list, int[] value_setting_list, string[][] value_list)
        {
            // Checks if Parameters are null
            this.Icon = icon ?? throw new ArgumentNullException(nameof(icon));
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.PropertyNameList = prop_name_list ?? throw new ArgumentNullException(nameof(prop_name_list));
            this.ButtonList = button_list ?? throw new ArgumentNullException(nameof(button_list));
            this.ValueSettingList = value_setting_list ?? throw new ArgumentNullException(nameof(value_setting_list));
            this.ValueList = value_list ?? throw new ArgumentNullException(nameof(value_list));

            // Checks if Value list and Value Setting list match each other 
            if (ValueSettingList.Count() != ValueList.Count())
                throw new Exception("Value Setting List and Value List must match each other");

            // Sets Option quit and Block list
            this.SetOptionQuit();
            this.BlockList = new bool[this.PropertyNameList.Count()];
            for (int i = 0; i < BlockList.Count(); i++)
            {
                BlockList[i] = false;
            }

            // Caculates MaxLength values
            this.MaxLofPropertyName = this.PropertyNameList.Max(x => x.Length);
            this.MaxLofValue = this.ValueList.Max(x => x.Max(y => y.Length));
            this.MaxLofButton = this.ButtonList.Max(x => x.Length);
            this.MaxLofOption = Math.Max(this.MaxLofPropertyName, this.MaxLofValue);
            this.MaxLofOption = Math.Max(this.MaxLofButton, this.MaxLofOption);
            this.MaxLofOption = Math.Max(this.Title.Length, this.MaxLofOption);

            // Changes the Menu witdh here (default 7) - must be greater than 8 or greater than icon.Length + 7
            this.MaxWidth = MaxLofOption + MaxLofValue + icon.Length + this.Space;
            // MaxWidth = 1 + MaxLofOption  + Icon.Length + 1  + 2 + MaxLofValue + 2 + 1;
            
        }


        /// <summary>
        ///     Sets Option quit
        /// </summary>
        public virtual void SetOptionQuit()
        {
            this.OptionQuit = this.PropertyNameList.Count() + this.ButtonList.Count();
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
        ///     Sets Key index - unique
        /// </summary>
        /// <param name="k"></param>
        /// <exception cref="Exception"></exception>
        public void SetKeyIndex(int k)
        {
            if (k < 0)
                throw new Exception("Key Index cannot be negative");
            this.KeyIndex = k;
        }


        /// <summary>
        ///     Sets Max of Property number
        /// </summary>
        /// <param name="n"></param>
        /// <exception cref="Exception"></exception>
        public void SetMaxOfProperty(int n)
        {
            if (n < 0)
                throw new Exception("Max Property number cannot be negative");
            this.MaxOfProperty = n;
        }


        /// <summary>
        ///     Sets primary key for menu
        ///     You should override this method
        /// </summary>
        public virtual void SetUnique()
        {
            this.SetKeyIndex(0);
            this.SetMaxOfProperty(1);
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
        public virtual void SetChoiceToMaxValue(ref int choice) => choice = this.PropertyNameList.Count() + this.ButtonList.Count();


        /// <summary>
        ///     Increases the ordinal of Choice
        /// </summary>
        /// <param name="choice"></param>
        public virtual void IncreaseOrdinalOfChoice(ref int choice)
        {
            if (choice == this.PropertyNameList.Count() + this.ButtonList.Count())
                this.SetChoiceToMinValue(ref choice);
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
                this.SetChoiceToMaxValue(ref choice);
            else
                choice--;
        }


        /// <summary>
        ///     Increases the setting number
        /// </summary>
        /// <param name="n"></param>
        public virtual void IncreaseSettingNumber(int n)
        {
            if (n > this.PropertyNameList.Count())
                return;
            if (this.BlockList[n - 1])
                return;
            if (n < this.PropertyNameList.Count() + 1)
                if (this.ValueSettingList[n - 1] == this.ValueList[n - 1].Count() - 1)
                    this.ValueSettingList[n - 1] = 0;
                else
                    this.ValueSettingList[n - 1]++;
        }


        /// <summary>
        ///     Decreases the setting number
        /// </summary>
        /// <param name="n"></param>
        public virtual void DecreaseSettingNumber(int n)
        {
            if (n > this.PropertyNameList.Count())
                return;
            if (this.BlockList[n - 1])
                return;
            if (n < this.PropertyNameList.Count() + 1)
                if (this.ValueSettingList[n - 1] == 0)
                    this.ValueSettingList[n - 1] = this.ValueList[n - 1].Count() - 1;
                else
                    this.ValueSettingList[n - 1]--;
        }


        /// <summary>
        ///     Resets the Block list
        /// </summary>
        public virtual void ResetBlockList()
        {
            for (int i = 0; i < this.BlockList.Count(); i++)
                this.BlockList[i] = false;
        }


        /// <summary>
        ///     Resets the Value Setting list
        /// </summary>
        public virtual void ResetValueSettingList()
        {
            for (int i = 0; i < this.ValueSettingList.Count(); i++)
                this.ValueSettingList[i] = 0;
        }


        /// <summary>
        ///     Resets Setting value
        /// </summary>
        public virtual void ResetValue()
        {
            this.ResetValueSettingList();
            this.ResetBlockList();
        }


        /// <summary>
        ///     Updates Block list
        /// </summary>
        public virtual void UpdateBlockList()
        {
            // 
            this.SetUnique();

            int changedValueIndex = new int();
            int[] tempValueSettingList = new int[this.ValueSettingList.Count()];
            int j = 0;
            for (int i = 0; i < this.ValueSettingList.Count(); i++)
            {
                if (i != this.KeyIndex)
                {
                    tempValueSettingList[j] = this.ValueSettingList[i];
                    j++;
                }
            }
            if (tempValueSettingList.Count(x => x > 0) == this.MaxOfProperty)
            {
                for (int i = 0; i < this.ValueSettingList.Count(); i++)
                    if (this.ValueSettingList[i] > 0 && i != this.KeyIndex)
                    {
                        changedValueIndex = i;
                        break;
                    }
                for (int i = 0; i < this.BlockList.Count(); i++)
                {
                    if (i != this.KeyIndex && i != changedValueIndex)
                        this.BlockList[i] = true;
                }
                return;
            }
            ResetBlockList();
        }


        /// <summary>
        ///     Checks if the number key is valid
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public virtual bool IsValidNumber(ConsoleKeyInfo k) => (int.Parse(k.KeyChar.ToString()) <= this.PropertyNameList.Count() + this.ButtonList.Count());


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
        public void Print(int choice)
        {
            int i = new int();

            // StartLine
            this.PrintStartingSpace();
            for (i = 1; i <= this.MaxWidth; i++)
            {
                if (i == 1)
                    Console.Write('╔');
                else if (i == MaxWidth)
                    Console.Write("╗\n");
                else
                    Console.Write('═');
            }

            // Title space from each side
            TitleSpaceEachSide = (double)(this.MaxWidth - this.Title.Length) / 2;
            // Print Title
            this.PrintStartingSpace();
            string titleRow = string.Empty;
            if (this.TitleSpaceEachSide == (int)this.TitleSpaceEachSide)
            {
                string titleSpace = string.Empty;
                for (i = 1; i < this.TitleSpaceEachSide; i++)
                    titleSpace += '.';
                titleRow = '║' + titleSpace + this.Title + titleSpace + '║';
            }
            else
            {
                string titleSpace = string.Empty;
                for (i = 1; i < (this.TitleSpaceEachSide - 0.5); i++)
                    titleSpace += '.';
                titleRow = '║' + titleSpace + this.Title + titleSpace + '.' /*the different here*/ + '║';
            }
            Console.WriteLine(titleRow);

            // Options StartLine
            this.PrintStartingSpace();
            for (i = 1; i <= this.MaxWidth; i++)
            {
                if (i == 1)
                    Console.Write('║');
                else if (i == this.MaxWidth)
                    Console.Write("║\n");
                else if (i == this.MaxWidth - (this.MaxLofValue + 6 - 1))
                    Console.Write('╦');
                else
                    Console.Write('═');
            }

            // Prints Option
            for (i = 0; i < this.PropertyNameList.Count(); i++)
            {
                this.PrintStartingSpace();
                for (int j = 1; j <= this.MaxWidth; j++)
                {
                    int settingNumber = this.ValueSettingList[i];
                    if (j == 1)
                        Console.Write('║');
                    else if (j == this.MaxWidth)
                        Console.Write("║\n");
                    else
                    {
                        // Property Name area
                        if (j == 3)
                        {
                            int colorNum = i + 1;
                            while (colorNum > 15) { colorNum -= 15; }
                            if (!BlockList[i])
                                this.PropertyNameList[i].PintWithColor(colorNum);
                            else
                                this.PropertyNameList[i].PintWithColor(-1);
                            j += this.PropertyNameList[i].Length - 1;
                        }
                        else if (j == this.MaxWidth - (this.MaxLofValue + 6 + this.Icon.Length - 1))
                        {
                            if (choice == i + 1) Console.Write(this.Icon);
                            else
                                for (int m = 0; m < this.Icon.Length; m++)
                                {
                                    Console.Write(' ');
                                }
                            j += this.Icon.Length - 1;
                        }

                        // Setting area
                        else if (j >= this.MaxWidth - (this.MaxLofValue + 6 - 1) && j != this.MaxWidth)
                        {
                            int m = j - (this.MaxWidth - (this.MaxLofValue + 6));
                            if (m == 1)
                                Console.Write('║');
                            else if (m == 4)
                            {
                                if (this.ValueList[i][settingNumber].Length == this.MaxLofValue)
                                    if (!BlockList[i])
                                        Console.Write(this.ValueList[i][settingNumber]);
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.Write(this.ValueList[i][settingNumber]);
                                        Console.ResetColor();
                                    }
                                else
                                {
                                    if (!BlockList[i])
                                        Console.Write(this.ValueList[i][settingNumber]);
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.Write(this.ValueList[i][settingNumber]);
                                        Console.ResetColor();
                                    }
                                    for (int n = 1; n <= this.MaxLofValue - this.ValueList[i][settingNumber].Length; n++)
                                        Console.Write(' ');
                                }
                                j += this.MaxLofValue - 1;
                            }
                            else Console.Write(' ');
                        }
                        else
                            Console.Write(' ');
                    }
                }
            }

            // Options EndLine
            this.PrintStartingSpace();
            for (i = 1; i <= this.MaxWidth; i++)
            {
                if (i == 1)
                    Console.Write('║');
                else if (i == this.MaxWidth)
                    Console.Write("║\n");
                else if (i == this.MaxWidth - (this.MaxLofValue + 6 - 1))
                    Console.Write('╩');
                else
                    Console.Write('═');
            }

            // Button List
            for (i = 0; i < this.ButtonList.Count(); i++)
            {
                this.PrintStartingSpace();
                for (int m = 1; m <= this.MaxWidth; m++)
                {
                    if (m == 1)
                        Console.Write('║');
                    else if (m == 3)
                    {
                        int colorNum = i + 1 + PropertyNameList.Count();
                        while (colorNum > 15) { colorNum -= 15; }
                        this.ButtonList[i].ToString().PintWithColor(colorNum);
                        m += this.ButtonList[i].Length - 1;
                    }
                    else if (m == this.MaxWidth - this.Icon.Length)
                    {
                        if (choice == (i + this.PropertyNameList.Count() + 1))
                            Console.Write(Icon);
                        else
                            for (int n = 0; n < this.Icon.Length; n++)
                                Console.Write(' ');
                        m += this.Icon.Length - 1;
                    }
                    else if (m == this.MaxWidth)
                        Console.Write("║\n");
                    else
                        Console.Write(' ');
                }
            }

            // EndLine
            this.PrintStartingSpace();
            for (i = 1; i <= this.MaxWidth; i++)
            {
                if (i == 1)
                    Console.Write('╚');
                else if (i == this.MaxWidth)
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
                    this.ReturnNum(k, ref choice);
                    this.UpdateBlockList();
                    break;

                // Up and Down keys
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    this.DecreaseOrdinalOfChoice(ref choice);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    this.IncreaseOrdinalOfChoice(ref choice);
                    break;

                // Right and Left keys
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    this.DecreaseSettingNumber(choice);
                    this.UpdateBlockList();
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    this.IncreaseSettingNumber(choice);
                    this.UpdateBlockList();
                    break;

                // Quit keys
                case ConsoleKey.Escape:
                case ConsoleKey.Backspace:
                    choice = OptionQuit;
                    quit = true;
                    break;

                // Enter keys
                case ConsoleKey.Enter:
                    if (choice <= this.PropertyNameList.Count())
                    {
                        this.IncreaseSettingNumber(choice);
                        this.UpdateBlockList();
                    }
                    else
                        quit = true;
                    break;

                // Default keys
                default:
                    Console.WriteLine("Vui long chi su dung Up/Down - Left/Right, W/S - A/D, Enter/Esc Keys");
                    Console.ReadKey();
                    break;
            }
        }


        /// <summary>
        ///     Checks and returns a number if the user enter a number key
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
        ///     Runs the Menu
        /// </summary>
        /// <param name="choice"></param>
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
