
public abstract class ItemMap {
    /// <summary>
    /// Classe para tratar ícones e simbolos do jogo
    /// </summary>

    private string Symbol;

    public ItemMap(string Symbol)
    {
        this.Symbol = Symbol;
    }

    public sealed override string ToString()
    {
        return Symbol;
    }

}