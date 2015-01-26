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
        get_turn,
        turn,
        get_position,
        position,
        get_number_of_players,
        number_of_players,
        unknown_command

    }
    public enum parameter
    {
        id,
        player,
        x,
        y,
        bombs,
        point
    }
}
