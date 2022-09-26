public abstract class Jewel : ItemMap {
    /// <summary>
    /// Classe para agregar o objeto do tipo "Jewel"
    /// </summary>
    /// <value></value>

    public int Points {get; private set;}

    public Jewel(string Symbol, int Points) : base(Symbol)
    {
        this.Points = Points;
    }

}