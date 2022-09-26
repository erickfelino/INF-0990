public class JewelBlue : Jewel, Rechargeable {

    public JewelBlue() : base("JB ", 10){}

    public void Recharge(Robot r) 
    {
        r.energy = r.energy + 3;
    }
    
}
