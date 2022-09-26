
public class Map {
    /// <summary>
    /// Classe responsável por todas as ações no maapa 2D, tanto para a a realocação dos elementos
    /// como para as impressões e randomizações do mapa
    /// </summary>

    private ItemMap[,] Matriz;
    public int h {get; private set;}
    public int w {get; private set;}
    public bool exploded;

    public Map(int w=10, int h=10, int level=1)
    ///
    /// Aspectos Básicos do mapa
    /// 
    {

        this.w = w <= 30 ? w : 30;
        this.h = h <= 30 ? h : 30;

        Matriz = new ItemMap[w, h];

        for (int i = 0; i < Matriz.GetLength(0); i++) {
            for (int j = 0; j < Matriz.GetLength(1); j++) {
                Matriz[i, j] = new Empty();
            }
        }

        if (level == 1) GenerateFixed();
        else GenerateRandom();
        
    }

    public void Insert(ItemMap Item, int x, int y)
    {
        /// <summary>
        /// Inserção do Mapa
        /// </summary>
        Matriz[x, y] = Item;
    }

    public void Update(int x_old, int y_old, int x, int y)
    {
        /// <summary>
        /// Updates do mapa a cada movimentação do jogador, e verificação do elemento "radioativo"
        /// </summary>
        if (x < 0 || y < 0 || x > this.w-1 || y > this.h-1)
        {
            throw new OutOfMapException();
        }

        if (IsAllowed(x, y))
        {
            Matriz[x, y] = Matriz[x_old, y_old];
            Matriz[x_old, y_old] = new Empty(); 
        }
        else
        {
            if (Matriz[x, y] is Damage r){

                GetExplosion(x_old, y_old,x,y);
                exploded = true;

            }
            else{

            throw new OccupiedPositionException();
            
            }
        }

    }

    public List<Jewel> GetJewels(int x, int y){
        /// <summary>
        /// Função de recolhimento das jewels do jogo
        /// </summary>
        /// <typeparam name="Jewel"></typeparam>
        /// <returns></returns>

        List<Jewel> NearJewels = new List<Jewel>();

        int[,] Coords = GenerateCoord(x, y);

        for (int i = 0; i < Coords.GetLength(0); i++) {

            Jewel? jewel = GetJewel(Coords[i, 0], Coords[i, 1]);
            
            if (jewel is not null) NearJewels.Add(jewel);
        
        }

        return NearJewels;

    }
    private Jewel? GetJewel(int x, int y)
    {
        /// <summary>
        /// Método para troca dos simbolos de jewel para empty
        /// </summary>

        if (Matriz[x, y] is Jewel jewel)
        {
            Matriz[x, y] = new Empty();
            return jewel;
        }

        return null;
    }

    private void GetExplosion(int x_old, int y_old, int x, int y)
    {
        /// <summary>
        /// Função para realizar o evento de exploxão do jogador ao entrar em contato direto
        /// com o elemento radioativo
        /// </summary>

        if (Matriz[x, y] is Damage damage)
        {
                Matriz[x, y] = Matriz[x_old, y_old];
                Matriz[x_old, y_old] = new Empty();
            
        }
    }
    public Rechargeable? GetRechargeable(int x, int y){
        /// <summary>
        /// Método para utilizar dos elementos que curam o jogador
        /// </summary>
        /// <returns></returns>

        int[,] Coords = GenerateCoord(x, y);

        for (int i = 0; i < Coords.GetLength(0); i++) 
            if (Matriz[Coords[i, 0], Coords[i, 1]] is Rechargeable r) return r;

        return null;

    }

    public Damage? GetDamage(int x, int y){
        /// <summary>
        /// Método para utilizar dos elementos que causam dano ao jogador
        /// </summary>
        /// <returns></returns>

        int[,] Coords = GenerateCoord(x, y);

        for (int i = 0; i < Coords.GetLength(0); i++) 
            if (Matriz[Coords[i, 0], Coords[i, 1]] is Damage r) return r;

        return null;

    }

    private int[,] GenerateCoord(int x, int y)
    {
        /// <summary>
        /// Método para recolher os valores das variáveis do mapa 2D
        /// </summary>
        int[,] Coords = new int[4, 2] {
            {x, y+1 < w-1 ? y+1 : w-1},
            {x, y-1 > 0 ? y-1 : 0},
            {x+1 < h-1 ? x+1 : h-1, y},
            {x-1 > 0 ? x-1 : 0, y }
        };

        return Coords;
    }

    private bool IsAllowed(int x, int y){
        /// <summary>
        /// Método para verificar se a casa que o jogador está tentando acessar pode ser ocupada
        /// </summary>
        return Matriz[x, y] is Empty;
    }

    public void Print() {
        /// <summary>
        /// Função para printar o mapa no terminal
        /// </summary>
        /// <returns></returns>

        for (int i = 0; i < Matriz.GetLength(0); i++) {
            for (int j = 0; j < Matriz.GetLength(1); j++) {
                Console.Write(Matriz[i, j]);
            }
            Console.Write("\n");
        }
    }

    public bool IsDone()
    {
        /// <summary>
        /// Método para verificar fim do jogo ao recolher todas as jewels
        /// </summary>
        /// <returns></returns>
        for (int i = 0; i < Matriz.GetLength(0); i++) {
            for (int j = 0; j < Matriz.GetLength(1); j++) {
                if (Matriz[i, j] is Jewel) return false;
            }
        }
        return true;

    }

    private void GenerateFixed()
    {
        /// <summary>
        /// Função para gerar o mapa 1 fixo
        /// </summary>
        /// <param name="JewelRed()"></param>
        this.Insert(new JewelRed(), 1, 9);
        this.Insert(new JewelRed(), 8, 8);
        this.Insert(new JewelGreen(), 9, 1);
        this.Insert(new JewelGreen(), 7, 6);
        this.Insert(new JewelBlue(), 3, 4);
        this.Insert(new JewelBlue(), 2, 1);

        this.Insert(new Water(), 5, 0);
        this.Insert(new Water(), 5, 1);
        this.Insert(new Water(), 5, 2);
        this.Insert(new Water(), 5, 3);
        this.Insert(new Water(), 5, 4);
        this.Insert(new Water(), 5, 5);
        this.Insert(new Water(), 5, 6);
        this.Insert(new Tree(), 5, 9);
        this.Insert(new Tree(), 3, 9);
        this.Insert(new Tree(), 8, 3);
        this.Insert(new Tree(), 2, 5);
        this.Insert(new Tree(), 1, 4);
    }

    private void GenerateRandom()
    {
       /// <summary>
       /// Função para gerar o mapa randomico do jogo
       /// </summary>
       /// <returns></returns>
        Random r = new Random(1);  

        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new JewelBlue(), xRandom, yRandom);

        }

        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new JewelGreen(), xRandom, yRandom);

        }

        for(int x = 0; x < 10; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new Water(), xRandom, yRandom);

        }

        for(int x = 0; x < 10; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new Tree(), xRandom, yRandom);

        }

        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);

            this.Insert(new Radioactive(), xRandom, yRandom);

        }
    }
}
