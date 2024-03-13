using System.Collections.Concurrent;

namespace MultiplayerMover.Logic;

public static class GameState
{
    public static PlayerState JoinPlayer(string playerName)
    {
        PlayerState playerState = new PlayerState(playerName);
        GameState.playerState.TryAdd(playerName, playerState);
        RaisePlayerStateChanged();
        return playerState;
    }

    private static ConcurrentDictionary<string, PlayerState> playerState = new();

    public static event EventHandler<IEnumerable<(string playerName, PlayerState state)>>? PlayerStateChanged;
    public static void RaisePlayerStateChanged()
    {
        Players = playerState.Values;
        PlayerStateChanged?.Invoke(null, playerState.Select(kvp => (kvp.Key, kvp.Value)));
    }
    public static IEnumerable<PlayerState> Players { get; private set; }
}

public class PlayerState(string playerName)
{
    public string Name => playerName;
    public int X { get; set; }
    public int Y { get; set; }
}
