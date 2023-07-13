using FlexiLeaf.Core.Extensions;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace FlexiLeaf.Core.Network.Packets
{
    public static class PacketHandler
    {
        private static readonly Dictionary<int, Type> packetTypes = new();
        private static readonly Dictionary<int, Delegate> HandleDict = new();
        private static Type[] HandlerMethodParameterTypes = Array.Empty<Type>();


        public static void Init(Assembly assembly, Type[] handlerMethodParameterTypes)
        {
            HandlerMethodParameterTypes = handlerMethodParameterTypes;
            RegisterPacketTypes();
            RegisterHandle(assembly);
        }

        private static void RegisterPacketTypes()
        {
            var packetClasses = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && typeof(Packet).IsAssignableFrom(t));

            foreach (var packetClass in packetClasses)
            {
                var idProperty = packetClass.GetProperty("Id");
                if (idProperty != null && idProperty.PropertyType == typeof(int))
                {
                    int id = (int)idProperty.GetValue(null)!;
                    packetTypes.Add(id, packetClass);
                }
            }
        }

        private static void RegisterHandle(Assembly assembly)
        {
            var packetMethods = assembly.GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Where(m => m.GetCustomAttribute<PacketHandlerAttribute>() != null);

            foreach (var method in packetMethods)
            {
                var parameters = method.GetParameters();
                if (typeof(Packet).IsAssignableFrom(parameters[0].ParameterType))
                {
                    var packetType = parameters[0].ParameterType;
                    var idProperty = packetType.GetProperty("Id");
                    int id = (int)idProperty!.GetValue(null)!;
                    var handler = method.CreateDelegate(HandlerMethodParameterTypes);
                    HandleDict.Add(id, handler);
                }
            }
        }

        public static bool ExecuteHandler(Packet packet, object client, bool PrintError = true)
        {
            if(packet == null) return false;

            if (HandleDict.ContainsKey(packet.MessageId))
            {
                HandleDict[packet.MessageId].DynamicInvoke(null, packet, client);
                return true;
            }
            else
            {
                if(PrintError)
                    Console.WriteLine($"No handler found for packet id: {packet.MessageId}");
            }
            return false;
        }

        public static Packet CreatePacketInstance(int id)
        {
            if (packetTypes.ContainsKey(id))
            {
                Type packetType = packetTypes[id];
                Packet packet = (Packet)Activator.CreateInstance(packetType)!;
                return packet;
            }
            return null!;
        }

    }
}
