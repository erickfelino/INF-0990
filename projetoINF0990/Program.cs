public class JewelCollector {
    /// <summary>
    /// Classe que contem a função Main para movimentação e captação dos eventos
    /// </summary>
    /// 
/// <summary>
/// Funções
/// </summary>
  delegate void MoveNorth();
  delegate void MoveSouth();
  delegate void MoveEast();
  delegate void MoveWest();

/// <summary>
/// Eventos
/// </summary>
  static event MoveNorth OnMoveNorth; 
  static event MoveSouth OnMoveSouth; 
  static event MoveEast OnMoveEast;
  static event MoveWest OnMoveWest;     

  public static void Main() {
    /// <summary>
    /// Função Main do Jogo
    /// </summary>

      int w = 10;
      int h = 10;
      int level = 1;

      while(true)
      {
        Map map = new Map(w, h, level);
        Robot robot = new Robot(map);

        Console.WriteLine($"Level: {level}");
    
        try
        {

            bool Result = Run(robot);

            if(Result)
            {
                w++;
                h++;
                level++;
            } 
            else 
            {
                break;
            }
        }
        catch(RanOutOfEnergyException e)
        {
            Console.WriteLine("Robot ran out of energy!");
        }

      }
        
    }

    private static bool Run(Robot robot)
    {
        /// <summary>
        /// Método de movimentação do jogador
        /// </summary>

        OnMoveNorth += robot.MoveNorth; 
        OnMoveSouth += robot.MoveSouth; 
        OnMoveEast += robot.MoveEast; 
        OnMoveWest += robot.MoveWest;

        bool running = true;
    
        do {

            if(!robot.HasEnergy()) throw new RanOutOfEnergyException();

            robot.Print();
            
            Console.WriteLine("Enter the command: ");
            ConsoleKeyInfo command = Console.ReadKey(true);
    
            switch (command.Key.ToString())
            {
                case "W": OnMoveNorth(); break;
                case "S" : OnMoveSouth(); break;
                case "D" : OnMoveEast(); break;
                case "A" : OnMoveWest(); break;
                case "G" : robot.Get();break;
                case "Escape" : return false;
                default: Console.WriteLine(command.Key.ToString()); break;
            }

        } while (!robot.map.IsDone());

        return true;

    }

}