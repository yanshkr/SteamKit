using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SteamKit2.Util;
internal class SteamGuardGenerator
{
    private static readonly byte[] steamGuardCodeTranslations = "23456789BCDFGHJKMNPQRTVWXY"u8.ToArray();

    internal static string Generate( string sharedsecret )
    {
        return Generate( TimeUtils.GetSystemUnixTime(), sharedsecret );
    }

    internal static string Generate( long time, string sharedsecret )
    {
        ArgumentException.ThrowIfNullOrEmpty( sharedsecret, nameof( sharedsecret ) );

        string sharedSecretUnescaped = Regex.Unescape( sharedsecret );
        byte[] sharedSecretArray = Convert.FromBase64String( sharedSecretUnescaped );
        byte[] timeArray = new byte[ 8 ];

        time /= 30;

        for ( int i = 8; i > 0; i-- )
        {
            timeArray[ i - 1 ] = ( byte )time;
            time >>= 8;
        }

        using HMACSHA1 hmacGenerator = new()
        {
            Key = sharedSecretArray
        };

        byte[] hashedData = hmacGenerator.ComputeHash( timeArray );
        byte[] codeArray = new byte[ 5 ];

        byte b = ( byte )( hashedData[ 19 ] & 0xFu );
        int codePoint = ( hashedData[ b ] & 0x7F ) << 24 | ( hashedData[ b + 1 ] & 0xFF ) << 16 | ( hashedData[ b + 2 ] & 0xFF ) << 8 | hashedData[ b + 3 ] & 0xFF;

        for ( int i = 0; i < 5; i++ )
        {
            codeArray[ i ] = steamGuardCodeTranslations[ codePoint % steamGuardCodeTranslations.Length ];
            codePoint /= steamGuardCodeTranslations.Length;
        }

        return Encoding.UTF8.GetString( codeArray );
    }
}
