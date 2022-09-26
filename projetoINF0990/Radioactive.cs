public class Radioactive : Obstacle, Damage {

    public Radioactive() : base("!! "){}

    public void Damage(Robot r) 
    {
        r.energy = r.energy - 10;
    }
    
}