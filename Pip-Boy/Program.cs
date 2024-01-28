namespace Pip_Boy
{
    internal class Program
    {
        #region ASCII Images
        public static readonly string vaultTechLogo = @"
                                                          ########                     
                                                         ###########                   
                                                       ###############                 
                                                      #################                
                                                     #######     ######                
                                           ###############         ##############      
                                        #################           ################   
                                        ################    #####    ###############   
                                           ############   ########   ############      
                                                   ####   #########  ####              
                                     ##################   #########   ################ 
                                    ###################  ##########   #################
                                     ##################   #########   ################ 
                                                   ####   #########   ####             
                                           #############   #######   ############      
                                        ################    #####   ################   
                                        #################           ################   
                                           ###############         ##############      
                                                     #######    #######                
                                                      ################                 
                                                       ##############                  
                                                         ###########                    ";

        public static readonly string vaultBoyLogo = @"
                                                                                      ▒▒▒▒                                        
                                                                                    ▒▒░░░░▒▒▒▒                                    
                                                                            ████████░░    ░░░░████                                
                                                                ██        ██                      ██                              
                                                              ██░░▓▓    ▓▓░░                      ░░▓▓                            
                                                              ██    ▓▓▓▓                              ▓▓                          
                                                              ██            ██████████████              ██                        
                                                              ██          ██              ██    ████      ██                      
                                                            ██  ██      ██                  ████    ██    ██                      
                                                            ██    ▒▒▒▒▒▒                    ░░      ██    ██                      
                                                              ▒▒  ░░██░░▒▒                        ▒▒░░    ██                      
                                                              ██████▒▒██░░            ████      ██▒▒██    ██                      
                                                                  ██                      ██  ██  ██  ██  ██                      
              ▓▓▓▓▓▓                                            ██░░                            ██░░▓▓░░▓▓                        
            ██    ██                                            ██    ██      ██    ████      ██  ██  ██                          
          ██        ██                                          ██    ██    ██          ██      ██  ██████                        
          ██        ██                                        ██            ██                            ██                      
          ██        ██                                        ██          ▒▒░░                            ██                      
          ██        ██                                        ██          ▒▒▒▒                            ██                      
          ██        ▒▒▒▒                                      ██            ██                            ██                      
          ░░▓▓        ██▓▓                                    ██            ░░      ██                ██▓▓░░                      
        ██▓▓██▓▓▓▓        ██                                  ██    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓  ▓▓            ▓▓                            
    ▓▓▓▓          ████      ▓▓                                ██    ██                  ▓▓          ██                            
░░██                  ██      ██                                ██    ██              ████        ██                              
░░██                  ██      ██                                ██      ██████████████            ██                              
░░██    ████████████    ██      ████                              ██                            ██                                
    ████▒▒░░░░░░░░░░██████      ██▓▓████████                  ████████                        ████████                            
░░██                      ██    ████▒▒▒▒▒▒▒▒██████████████████        ████████              ██  ██    ████                        
░░██                      ██    ██░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██        ░░██░░░░              ░░▓▓░░    ██▓▓▓▓                      
    ▓▓▓▓    ▓▓▓▓▓▓▓▓    ██░░  ▓▓░░  ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██          ████                ▓▓        ██▒▒▒▒▓▓▓▓                  
        ██▓▓        ████      ██  ▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██            ██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓          ██▒▒▒▒▒▒██▓▓                
      ██                ██    ██  ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██                                    ██▒▒▒▒▒▒▒▒▒▒▒▒██              
      ██                ██  ██    ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██                                ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██            
        ████    ████████    ██  ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██                            ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██          
        ░░░░▒▒▒▒░░░░░░    ▒▒▒▒  ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▒▒▒▒▒▒▒▒▓▓▒▒                      ▒▒▒▒▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██        
            ░░░░▒▒▒▒▒▒▒▒▒▒▒▒  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓██▒▒▒▒▒▒▒▒▒▒▒▒▒▒            ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██      
                ░░░░░░░░░░▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▓▓▓▓▓▓░░  ██▒▒▒▒▒▒▒▒▒▒▒▒██          ▓▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓    
                              ██████████████            ██▒▒▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▓▓  
                                                        ██▒▒▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██  
                                                        ██▒▒▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
                                                        ██▒▒▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
                                                        ██▒▒▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
                                                          ██▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
                                                          ██▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒████▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
                                                          ██▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██░░░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
                                                          ██▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██        ████▒▒▒▒▒▒▒▒▒▒▒▒██
                                                          ██▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██        ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██
                                                          ██▒▒▒▒▒▒▒▒▒▒██          ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██      ░░▓▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒██";
        #endregion

        public static PipBoy pipBoy = new();

        static void Main()
        {
            bool boot = false;
            bool createPlayer = false;

            Console.ForegroundColor = pipBoy.color;
            Console.Title = "PIP-Boy 3000 MKIV";
            if (boot)
            {
                SlowType("PIP-Boy 3000 MKIV");
                SlowType("Copyright 2075 RobCo Industries");
                SlowType("64kb Memory");
                SlowType(new string('-', Console.WindowWidth));

                Console.Clear();
                Console.Write(vaultTechLogo);
                Console.WriteLine();
                SlowType("VAULT-TEC");
                Thread.Sleep(1250);

                Console.Clear();
                Console.WriteLine(vaultBoyLogo);
                SlowType("LOADING...");
                Thread.Sleep(2500);
                Console.Clear();
            }

            if (createPlayer)
                pipBoy.player = new Player();
            else
                pipBoy.player = new Player("Jake Scott", [5, 6, 7, 8, 9, 3, 4]);

            pipBoy.map.MovePlayer(null, null);

            #region Main Loop
            ConsoleKey key;
            do
            {
                Console.Clear();

                pipBoy.Highlight(pipBoy.currentPage.ToString(), true);
                Console.WriteLine();

                Console.WriteLine(pipBoy.ShowMenu());
                Console.WriteLine();

                pipBoy.ShowSubMenu(pipBoy.GetSubMenu());

                key = Console.ReadKey().Key;

                switch (key)
                {
                    #region Menu
                    case ConsoleKey.A:
                        pipBoy.ChangeMenu(false);
                        break;
                    case ConsoleKey.D:
                        pipBoy.ChangeMenu(true);
                        break;
                    #endregion

                    #region Sub-Menu
                    case ConsoleKey.LeftArrow:
                        pipBoy.ChangeSubMenu(false);
                        break;
                    case ConsoleKey.RightArrow:
                        pipBoy.ChangeSubMenu(true);
                        break;
                    #endregion

                    #region Radio
                    case ConsoleKey.Enter when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Radio:
                        pipBoy.radio.Play();
                        break;
                    case ConsoleKey.Add when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Radio:
                        pipBoy.radio.AddSong(pipBoy);
                        break;
                    #endregion

                    #region Map
                    case ConsoleKey.NumPad8 when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Map:
                        pipBoy.map.MovePlayer(true, null);
                        break;
                    case ConsoleKey.NumPad2 when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Map:
                        pipBoy.map.MovePlayer(false, null);
                        break;
                    case ConsoleKey.NumPad4 when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Map:
                        pipBoy.map.MovePlayer(null, false);
                        break;
                    case ConsoleKey.NumPad6 when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Map:
                        pipBoy.map.MovePlayer(null, true);
                        break;
                        #endregion
                }
            }
            while (key != ConsoleKey.Q);
            #endregion
        }

        /// <summary>
        /// Slowly Type out a message to the console
        /// </summary>
        /// <param name="message">The message</param>
        public static void SlowType(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }
    }
}
