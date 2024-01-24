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
            Console.ForegroundColor = pipBoy.color;
            Console.Title = "PIP-Boy";
            SlowType("PIP-Boy 3000 MKIV");
            SlowType("Copyright 2075 RobCo Industries");
            SlowType("64kb Memory");
            Console.WriteLine(new string('-', Console.WindowWidth));

            Console.Clear();
            Console.Write(vaultTechLogo);
            Console.WriteLine();
            Console.WriteLine("VAULT-TEC");
            Thread.Sleep(1250);

            Console.Clear();
            Console.WriteLine(vaultBoyLogo);
            Console.WriteLine("LOADING...");
            Thread.Sleep(2500);
            Console.Clear();

            pipBoy.player = pipBoy.CreatePlayer();

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
                    case ConsoleKey.A:
                        pipBoy.ChangeMenu(false);
                        break;
                    case ConsoleKey.D:
                        pipBoy.ChangeMenu(true);
                        break;

                    case ConsoleKey.LeftArrow:
                        pipBoy.ChangeSubMenu(false);
                        break;
                    case ConsoleKey.RightArrow:
                        pipBoy.ChangeSubMenu(true);
                        break;

                    case ConsoleKey.UpArrow:
                        pipBoy.Scroll(true);
                        break;
                    case ConsoleKey.DownArrow:
                        pipBoy.Scroll(false);
                        break;

                    case ConsoleKey.Enter when pipBoy.currentPage == PipBoy.Pages.DATA && pipBoy.dataPage == PipBoy.DataPages.Radio:
                        pipBoy.radio.Play();
                        break;
                }
            }
            while (key != ConsoleKey.Q);
        }

        public static void SlowType(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }
    }
}
