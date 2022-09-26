
public class Robot : ItemMap {
    /// <summary>
    /// Classe para definição do robô e todas suas interações no jogo
    /// </summary>
    /// <value></value>

    public Map map {get; private set;}
    public Radioactive radioactive {get; private set;}
    private int x, y;
    private List<Jewel> Bag = new List<Jewel>();
    public int energy {get; set;}

    public Robot(Map map, int x=0, int y=0, int energy=10) : base("ME "){
        /// <summary>
        /// Instanciação do objeto robô
        /// </summary>
        this.map = map;
        this.x = x;
        this.y = y;
        this.energy = energy;

        this.map.Insert(this, x, y);
    }

    public void MoveNorth(){
        /// <summary>
        /// Função de movimento para cima
        /// </summary>
        /// <value></value>

        try
        {
            map.Update(this.x, this.y, this.x-1, this.y);
            this.x--;
            if (map.exploded == true){
                map.exploded = false;
                this.energy -= 30;
            }
            else{
                this.energy--;
            }

            Damage? DamageEnergy = map.GetDamage(this.x, this.y);
            DamageEnergy?.Damage(this);
        } 
        catch (OccupiedPositionException e)
        {

            Console.WriteLine($"Position {this.x-1}, {this.y} is occupied");
            
        }
        catch (OutOfMapException e)
        {
            Console.WriteLine($"Position {this.x-1}, {this.y} is out of map");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Position is prohibit");
        }      
       
    }

    public void MoveSouth(){
        /// <summary>
        /// Função de movimento para baixo
        /// </summary>
        /// <value></value>

        try
        {
            map.Update(this.x, this.y, this.x+1, this.y);
            this.x++;
            if (map.exploded == true){
                map.exploded = false;
                this.energy -= 30;
            }
            else{
                this.energy--;
            }

            Damage? DamageEnergy = map.GetDamage(this.x, this.y);
            DamageEnergy?.Damage(this);
        } 
        catch (OccupiedPositionException e)
        {

            Console.WriteLine($"Position {this.x+1}, {this.y} is occupied");
            
        }
        catch (OutOfMapException e)
        {
            Console.WriteLine($"Position {this.x+1}, {this.y} is out of map");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Position is prohibit");
        }        
        
    }

    public void MoveEast(){
        /// <summary>
        /// Função de movimento para leste
        /// </summary>
        /// <value></value>

        try
        {
            map.Update(this.x, this.y, this.x, this.y+1);
            this.y++;
            if (map.exploded == true){
                map.exploded = false;
                this.energy -= 30;
            }
            else{
                this.energy--;
            }

            Damage? DamageEnergy = map.GetDamage(this.x, this.y);
            DamageEnergy?.Damage(this);

        }
        catch (OccupiedPositionException e)
        {

            Console.WriteLine($"Position {this.x}, {this.y+1} is occupied");
            
        }
        catch (OutOfMapException e)
        {
            Console.WriteLine($"Position {this.x}, {this.y+1} is out of map");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Position is prohibit");
        }      
        
    }

    public void MoveWest(){
        /// <summary>
        /// Função de movimento para oeste
        /// </summary>
        /// <value></value>

        try
        {
            map.Update(this.x, this.y, this.x, this.y-1);
            this.y--;
            if (map.exploded == true){
                map.exploded = false;
                this.energy -= 30;
            }
            else{
                this.energy--;
            }

            Damage? DamageEnergy = map.GetDamage(this.x, this.y);
            DamageEnergy?.Damage(this);
        }
        catch (OccupiedPositionException e)
        {

            Console.WriteLine($"Position {this.x}, {this.y-1} is occupied");
            
        }
        catch (OutOfMapException e)
        {
            Console.WriteLine($"Position {this.x}, {this.y-1} is out of map");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Position is prohibit");
        }  
        
    }

    public void Get(){
        /// <summary>
        /// Função de resposta ao tentar recolher um objeto do jogo
        /// </summary>
        /// <returns></returns>

        Rechargeable? RechargeEnergy = map.GetRechargeable(this.x, this.y);

        RechargeEnergy?.Recharge(this);

        List<Jewel> NearJewels = map.GetJewels(this.x, this.y);

        foreach (Jewel j in NearJewels)
            Bag.Add(j);
        
    }

    private (int, int) GetBagInfo()
    {
        /// <summary>
        /// Método para informação do estádo da bolsa do jogador
        /// </summary>
        int Points = 0;

        foreach (Jewel j in this.Bag)
            Points += j.Points;

        return (this.Bag.Count, Points);

    }

    public void Print()
    {
        /// <summary>
        /// Impressão do mapa e informações do jogador
        /// </summary>
        /// <value></value>
        map.Print();

        (int ItensBag, int TotalPoints) = this.GetBagInfo();
        Console.WriteLine($"Itens Bag: {ItensBag} - Total Points: {TotalPoints} - Energy: {this.energy}");

    }

    public bool HasEnergy()
    {
        /// <summary>
        /// Verificação de status da energia do robô
        /// </summary>
        /// <value></value>
        return this.energy > 0;
    }

}