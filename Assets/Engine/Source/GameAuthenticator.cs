using Mirror;
using KAG;

public class GameAuthenticator : NetworkAuthenticator
{
    #region Server
    public override void OnStartServer()
    {
        // Register a handler for the authentication request we expect from client
        NetworkServer.ReplaceHandler<PlayerInfo>(OnAuthenticationRequest, false);
    }

    public override void OnServerAuthenticate(NetworkConnection conn)
    {

    }

    public void OnAuthenticationRequest(NetworkConnection conn, PlayerInfo msg)
    {
        conn.authenticationData = msg;

        // Invoke the event to complete a successful authentication
        OnServerAuthenticated.Invoke(conn);
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        // Register a handler for the authentication response we expect from server
        NetworkClient.ReplaceHandler<PlayerInfo>(OnAuthenticationResponse, false);
    }

    public override void OnClientAuthenticate(NetworkConnection conn)
    {
        NetworkClient.Send(GameSession.Instance.player);
    }

    public void OnAuthenticationResponse(NetworkConnection conn, PlayerInfo msg)
    {
        // Invoke the event to complete a successful authentication
        OnClientAuthenticated.Invoke(conn);
    }
    #endregion
}
