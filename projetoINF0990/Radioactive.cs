public class Radioactive : Obstacle, Damage {
    /// <summary>
    /// Está classe está relacionada ao item "radioativo" dentro do jogo, em que o robô sofre dano
    /// ao entrar em contado das proximidades do elemento radioativo
    /// </summary>
    /// <returns></returns>

    public Radioactive() : base("!! "){}

    public void Damage(Robot r) 
    {
        /// <summary>
        /// Cálculo feito para redução de vida
        /// </summary>
        r.energy = r.energy - 10;
    }
    
}