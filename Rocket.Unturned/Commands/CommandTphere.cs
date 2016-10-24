﻿using Rocket.API;
using Rocket.API.Commands;
using Rocket.API.Exceptions;
using Logger = Rocket.API.Logging.Logger;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace Rocket.Unturned.Commands
{
    internal class CommandTphere : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public string Name
        {
            get { return "tphere"; }
        }

        public string Help
        {
            get { return "Teleports another player to you";}
        }

        public string Syntax
        {
            get { return "<player>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions
        {
            get { return new List<string>() { "rocket.tphere", "rocket.teleporthere" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length != 1)
            {
                U.Instance.Chat.Say(caller, U.Translate("command_generic_invalid_parameter"));
                throw new WrongUsageOfCommandException(caller, this);
            }
            UnturnedPlayer otherPlayer = UnturnedPlayer.FromName(command[0]);
            if (otherPlayer!=null && otherPlayer != caller)
            {
                otherPlayer.Teleport(player);
                Logger.Info(U.Translate("command_tphere_teleport_console", otherPlayer.CharacterName, player.CharacterName));
                U.Instance.Chat.Say(caller, U.Translate("command_tphere_teleport_from_private", otherPlayer.CharacterName));
                U.Instance.Chat.Say(otherPlayer, U.Translate("command_tphere_teleport_to_private", player.CharacterName));
            }
            else
            {
                U.Instance.Chat.Say(caller, U.Translate("command_generic_failed_find_player"));
                throw new WrongUsageOfCommandException(caller, this);
            }
        }
    }
}