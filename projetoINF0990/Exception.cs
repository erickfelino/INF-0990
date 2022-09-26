public class OutOfMapException : Exception {}
/// <summary>
/// Exception para indicar que está fora dos limites do mapa
/// </summary>

public class OccupiedPositionException : Exception {}
/// <summary>
/// Exception para indicar que o robô está tentando entrar um local ocupado por outro elemento
/// </summary>

public class RanOutOfEnergyException : Exception {}
/// <summary>
/// Exception para indicar que o robô ficou sem energia
/// </summary>