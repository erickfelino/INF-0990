public class JewelBlue : Jewel, Rechargeable {
    /// <summary>
    /// Classe herdade de Jewel, usada para criar o objeto do tipo JewelBlue
    /// </summary>
    /// <returns></returns>

    public JewelBlue() : base("JB ", 10){}
    /// <summary>
    /// JewelBlue tem a capacidade de curar o jogador como mostrado ter o campo "Rechargeable"
    /// </summary>
    /// <param name="r"></param>

    public void Recharge(Robot r) 
    {
        r.energy = r.energy + 3;
    }
    
}
