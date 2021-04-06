﻿using Essentials.Helpers;
using Hazel;
using Reactor;
#if !S20201209 && !S20210305
using Reactor.Networking;
#endif
using System;
using System.Linq;

namespace Essentials.Options
{
    public partial class CustomOption
    {
        /*private static RpcHandler<EssentialsPlugin, (string, CustomOptionType, object)[]> Rpc = RpcHandler.Register<EssentialsPlugin, (string, CustomOptionType, object)[]>(PluginSingleton<EssentialsPlugin>.Instance, HandleRpc);

        internal static void HandleRpc(PlayerControl sender, (string, CustomOptionType, object)[] options)
        {
            if (sender?.Data == null) return;

            if (Debug) EssentialsPlugin.Logger.LogInfo($"{sender.Data.PlayerName} sent option(s):");
            foreach ((string ID, CustomOptionType Type, object Value) option in options)
            {
                CustomOption customOption = Options.FirstOrDefault(o => o.Type == option.Type && o.ID.Equals(option.ID, StringComparison.Ordinal));

                if (customOption == null)
                {
                    EssentialsPlugin.Logger.LogWarning($"Received option that could not be found: \"{option.ID}\" of type {option.Type}.");

                    continue;
                }

                if (Debug) EssentialsPlugin.Logger.LogInfo($"\"{option.ID}\" type: {option.Type}, value: {option.Value}, current value: {customOption.Value}");

                customOption.SetValue(option.Value, true);

                if (Debug) EssentialsPlugin.Logger.LogInfo($"\"{option.ID}\", set value: {customOption.Value}");
            }
        }*/

#if S20201209 || S20210305
        [RegisterCustomRpc]
#else
        [RegisterCustomRpc(0)]
#endif
        private protected class Rpc : PlayerCustomRpc<EssentialsPlugin, (byte[], CustomOptionType, object)>
        {
            public static Rpc Instance { get { return Rpc<Rpc>.Instance; } }

#if S20201209 || S20210305
            public Rpc(EssentialsPlugin plugin) : base(plugin)
#else
            public Rpc(EssentialsPlugin plugin, uint id) : base(plugin, id)
#endif
            {
            }

            public override RpcLocalHandling LocalHandling { get { return RpcLocalHandling.None; } }

            public override void Write(MessageWriter writer, (byte[], CustomOptionType, object) option)
            {
                writer.Write(option.Item1); // SHA1
                writer.Write((byte)option.Item2); // Type
                if (option.Item2 == CustomOptionType.Toggle) writer.Write((bool)option.Item3);
                else if (option.Item2 == CustomOptionType.Number) writer.Write((float)option.Item3);
                else if (option.Item2 == CustomOptionType.String) writer.Write((int)option.Item3);
            }

            public override (byte[], CustomOptionType, object) Read(MessageReader reader)
            {
                byte[] sha1 = reader.ReadBytes(SHA1Helper.Length);
                CustomOptionType type = (CustomOptionType)reader.ReadByte();
                object value = null;
                if (type == CustomOptionType.Toggle) value = reader.ReadBoolean();
                else if (type == CustomOptionType.Number) value = reader.ReadSingle();
                else if (type == CustomOptionType.String) value = reader.ReadInt32();

                return (sha1, type, value);
            }

            public override void Handle(PlayerControl sender, (byte[], CustomOptionType, object) option)
            {
                if (sender?.Data == null) return;

                byte[] sha1 = option.Item1;
                CustomOptionType type = option.Item2;
                CustomOption customOption = Options.FirstOrDefault(o => o.Type == type && o.SHA1.SequenceEqual(sha1));

                if (customOption == null)
                {
                    EssentialsPlugin.Logger.LogWarning($"Received option that could not be found, sha1: \"{string.Join("", sha1.Select(b => $"{b:X2}"))}\", type: {type}.");

                    return;
                }

                object value = option.Item3;

                if (Debug) EssentialsPlugin.Logger.LogInfo($"\"{customOption.ID}\" type: {type}, value: {value}, current value: {customOption.Value}");

                customOption.SetValue(value, true);

                if (Debug) EssentialsPlugin.Logger.LogInfo($"\"{customOption.ID}\", set value: {customOption.Value}");
            }
        }

        public static implicit operator (byte[] SHA1, CustomOptionType Type, object Value)(CustomOption option)
        {
            return (option.SHA1, option.Type, option.GetValue<object>());
        }

        /*public static implicit operator (int ID, CustomOptionType Type, object Value)(CustomOption option)
        {
            return (option.GetHashCode(), option.Type, option.GetValue<object>());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return PluginID.GetHashCode() ^ ConfigID.GetHashCode();// ^ Type.GetHashCode();
            }
        }*/
    }
}