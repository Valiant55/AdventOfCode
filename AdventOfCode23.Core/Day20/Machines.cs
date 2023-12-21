using AdventOfCode23.Core.Common;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode23.Core.Day20;

public enum Pulse
{
    HIGH,
    LOW
}

public class Message
{
    public Pulse Pulse { get; set; }
    public string Sender { get; set; }
    public string Recipient { get; set; }

    public Message(Pulse pulse, string sender, string recipient)
    {
        Pulse = pulse;
        Sender = sender;
        Recipient = recipient;
    }
}

public interface IModule
{
    List<string> ConnectedRecipients { get; set; }
    List<Message> Handle(Message message);
}

public class FlipFlopModule : IModule
{
    public string Id { get; set; }
    public List<string> ConnectedRecipients { get; set; } = new();
    public bool IsOn { get; set; } = false;

    public List<Message> Handle(Message message)
    {
        if(message.Pulse == Pulse.LOW)
        {
            List<Message> result = ConnectedRecipients
                .Select(r =>
                {
                    return new Message(IsOn ? Pulse.LOW : Pulse.HIGH, Id, r);
                })
                .ToList();

            IsOn = !IsOn;

            return result;
        }

        return new List<Message>();
    }
}

public class ConjunctionModule : IModule
{
    public string Id { get; set; }
    public List<string> ConnectedRecipients { get; set; } = new();
    public Dictionary<string, Pulse> IncomingModules { get; set; } = new();

    public List<Message> Handle(Message message)
    {
        IncomingModules[message.Sender] = message.Pulse;

        Pulse sendPulse = Pulse.HIGH;
        if (IncomingModules.Values.All(v => v == Pulse.HIGH)) sendPulse = Pulse.LOW;

        return ConnectedRecipients
            .Select(r =>
            {
                return new Message(sendPulse, Id, r);
            })
            .ToList();
    }
}

public class UntypedModule : IModule
{
    public string Id { get; set; }
    public List<string> ConnectedRecipients { get; set; } = new();

    public List<Message> Handle(Message message)
    {
        return new List<Message>();
    }
}

public class BroadcastModule : IModule
{
    public string Id { get; set; } = "broadcaster";
    public List<string> ConnectedRecipients { get; set; } = new List<string>();

    public List<Message> Handle(Message message)
    {
        return ConnectedRecipients
            .Select(r =>
            {
                return new Message(message.Pulse, Id, r);
            })
            .ToList();
    }
}

public class Machines
{
    public Dictionary<string, IModule> Modules { get; set; }

    public Machines(List<(string, List<string>)> rawMachines)
    {
        Modules = new();

        foreach ((var id, var connections) in rawMachines)
        {
            if(id.StartsWith("%"))
            {
                Modules.Add(id[1..], new FlipFlopModule() { Id = id[1..], ConnectedRecipients = connections });
            }
            else if (id.StartsWith("&"))
            {
                Modules.Add(id[1..], new ConjunctionModule() { Id = id[1..], ConnectedRecipients = connections });
            }
            else if (id == "broadcaster")
            {
                Modules.Add(id, new BroadcastModule() { ConnectedRecipients = connections });
            }
            else
            {
                Modules.Add(id, new UntypedModule() { Id = id, ConnectedRecipients = connections });
            }
        }

        foreach((var id, var module) in Modules)
        {
            foreach(var conn in module.ConnectedRecipients)
            {
                if (Modules[conn].GetType() == typeof(ConjunctionModule))
                {
                    var mod = (ConjunctionModule)Modules[conn];
                    mod.IncomingModules.Add(id, Pulse.LOW);
                }
            }
        }
    }

    public long PushButtonForFinalMachine()
    {
        Dictionary<string, int> result = new();
        string rxInput = Modules
            .Where(kv => kv.Value.ConnectedRecipients.Contains("rx"))
            .Single().Key;

        for (int i = 1; i <= 5_000; i++)
        {
            Queue<Message> messageQueue = new Queue<Message>();
            messageQueue.Enqueue(new Message(Pulse.LOW, "button", "broadcaster"));

            while (messageQueue.TryDequeue(out Message message))
            {
                if(message.Recipient == "jm" && message.Pulse == Pulse.HIGH)
                {
                    result.TryAdd(message.Sender, i);
                }

                List<Message> newMessages = Modules[message.Recipient].Handle(message);

                foreach (var m in newMessages)
                {
                    messageQueue.Enqueue(m);
                }
            }

        }

        return Primes.LeastCommonMultiple(result.Values.ToList());
    }

    public long PushButton(long times)
    {
        (long, long) result = (0, 0);
        for(int i = 0; i < times; i++)
        {
            var push = PushButton();
            result = (result.Item1 + push.Item1, result.Item2 + push.Item2);
        }

        return result.Item1 * result.Item2;
    }

    public (long, long) PushButton()
    {
        Queue<Message> messageQueue = new Queue<Message>();
        messageQueue.Enqueue(new Message(Pulse.LOW, "button", "broadcaster"));
        long lowMessages = 1;
        long highMessages = 0;

        while(messageQueue.TryDequeue(out Message message))
        {
            List<Message> newMessages = Modules[message.Recipient].Handle(message);

            foreach (var m in newMessages)
            {
                messageQueue.Enqueue(m);

                if (m.Pulse == Pulse.LOW) lowMessages++;
                else if (m.Pulse == Pulse.HIGH) highMessages++;
            }
        }

        return (lowMessages, highMessages);
    }
}
