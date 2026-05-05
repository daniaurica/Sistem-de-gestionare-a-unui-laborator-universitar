using System;

namespace Model.Enumerari
{
    [Flags]
    public enum OptiuniEchipament
    {
        Nimic = 0,
        Wireless = 1,
        USB = 2,
        Bluetooth = 4,
        HDMI = 8
    }
}