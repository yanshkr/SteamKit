using System;

namespace SteamKit2.Util;
internal class TimeUtils
{
    internal static long GetSystemUnixTime() => ( long )DateTime.UtcNow.Subtract( new DateTime( 1970, 1, 1 ) ).TotalSeconds;
}
