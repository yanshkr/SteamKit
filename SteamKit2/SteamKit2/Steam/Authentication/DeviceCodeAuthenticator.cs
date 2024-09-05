using System;
using System.Threading.Tasks;
using SteamKit2.Authentication;
using SteamKit2.Util;

namespace SteamKit2.Steam.Authentication;
/// <summary>
/// This is a default implementation of <see cref="IAuthenticator"/> to ease of use.
///
/// This implementation will use shared secret to generate device code.
/// </summary>
public class DeviceCodeAuthenticator(string sharedSecret) : IAuthenticator
{
    /// <inheritdoc />
    public Task<string> GetDeviceCodeAsync( bool previousCodeWasIncorrect )
    {
        return Task.FromResult( SteamGuardGenerator.Generate( sharedSecret ) );
    }

    /// <inheritdoc />
    public Task<string> GetEmailCodeAsync( string email, bool previousCodeWasIncorrect )
    {
        Console.Error.WriteLine( "STEAM EMAIL GUARD! Use the another guard input method..." );

        return Task.FromResult( string.Empty );
    }

    /// <inheritdoc />
    public Task<bool> AcceptDeviceConfirmationAsync()
    {
        Console.Error.WriteLine( "STEAM GUARD! Use the Steam Mobile App to confirm your sign in..." );

        return Task.FromResult( true );
    }
}
