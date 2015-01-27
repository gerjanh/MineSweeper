using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public enum commands
    {
        get_connected,
        connected,
        get_disconnected,
        disconected,
        new_board,
        board_made,
        get_position,
        position,
        keep_alive,
        unknown_command

    }
    public enum parameter
    {
        id,
        x,
        y,
        bombs,
        point
    }
}
