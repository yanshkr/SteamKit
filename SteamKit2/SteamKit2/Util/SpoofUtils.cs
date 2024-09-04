using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamKit2.Util;
internal class SpoofUtils
{
    private static string RandomString( int length )
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string( Enumerable.Repeat( chars, length )
            .Select( s => s[ Random.Shared.Next( s.Length ) ] ).ToArray() );
    }

    public static string RandomWindowsNetName()
    {
       return "DESKTOP-" + RandomString(Random.Shared.Next(6, 8));
    }

    public static string GetRandomMacAddress()
    {
        var buffer = new byte[ 6 ];
        Random.Shared.NextBytes( buffer );
        return string.Join( ':', buffer.Select( x => x.ToString( "X2" ) ) );
    }

    public static string RandomDiskSerialNumber()
    {
        var builder = new StringBuilder();

        for ( int i = 0; i < 16; i++ )
        {
            if ( Random.Shared.Next( 0, 2 ) == 0 )
            {
                char letter = ( char )Random.Shared.Next( 'A', 'Z' + 1 );
                builder.Append( letter );
            }
            else
            {
                char digit = ( char )Random.Shared.Next( '0', '9' + 1 );
                builder.Append( digit );
            }
        }

        return builder.ToString();
    }
}
