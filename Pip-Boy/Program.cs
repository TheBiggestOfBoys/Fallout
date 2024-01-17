﻿using Pip_Boy.Sounds;

namespace Pip_Boy
{
    internal class Program
    {
        public static Random random = new();
        public static Player player = new("Player", [5, 5, 5, 5, 5, 5, 5]);
        public static Radio radio = new("C:\\Users\\jrsco\\source\\repos\\Pip-Boy\\Pip-Boy\\Sounds\\");
        public static PipBoy pipBoy = new();

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

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "PIP-Boy";
            SlowType("Hello, World!");
            SlowType("PIP-Boy 3000 MKIV");
            SlowType("Copyright 2075 RobCo Industries");
            SlowType("64kb Memory");

            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();

            for (int i = 0; i < Console.BufferHeight; i++)
            {
                Thread.Sleep(random.Next(25));
                Console.WriteLine();
            }
            Console.Clear();
            Console.Write(vaultTechLogo);
            Console.WriteLine();
            Console.WriteLine("VAULT-TEC");
            Thread.Sleep(1250);
            Console.Clear();
            Console.WriteLine(player);
            Thread radioThread = new(radio.Play);
            radio.Play();
            Thread.Sleep(1000000);
        }

        #region Slow Typing
        public static void SlowType(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(25);
            }
            Console.WriteLine();
        }

        public static void SlowType(string text, int delay)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public static void SlowType(string text, bool isRandom, int lower, int upper)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(random.Next(lower, upper));
            }
            Console.WriteLine();
        }
        #endregion
    }
}
