public class Tree : Obstacle, Rechargeable {
    /// <summary>
    /// Classe para criar o objeto Tree
    /// </summary>
    /// <returns></returns>

    public Tree() : base("$$ ") {}

    public void Recharge(Robot r) 
    {
        r.energy = r.energy + 3;
    }

}