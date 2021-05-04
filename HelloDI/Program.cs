using NUnit.Framework;
using System;
using Xunit;

namespace HelloDI
{
    class Program
    {
        static void Main(string[] args)
        {
            IMessageWriter writer = new ConsoleMessageWriter();
            var salutation = new Salutation(writer);
            salutation.Exclaim();

            var writer1 = new ConsoleMessageWriterPlus();
            var salutation1 = new Salutation(writer1);
            salutation1.Exclaim();
        }
    }

    public interface IMessageWriter
    {
        public void WriteToConsole(string message);
    }

    public class ConsoleMessageWriter : IMessageWriter
    {
        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class ConsoleMessageWriterPlus : IMessageWriter
    {
        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }


    public class Salutation
    {
        private readonly IMessageWriter _messageWriter;

        public Salutation(IMessageWriter messageWriter)
        {
            _messageWriter = messageWriter;
        }

        public void Exclaim()
        {
            _messageWriter.WriteToConsole("Hello DI!");
        }
    }

    public class TestClass
    {
        [Fact]
        public void ExclaimWillWriteCorrectMesageToMessageWriter()
        {
            var writer = new SpyMessage();
            var sut = new Salutation(writer);
            sut.Exclaim();
            Assert.AreEqual(
                expected: "Hello DI!",
                actual: writer.WrittenMessage);

        }
    }
    public class SpyMessage : IMessageWriter
    {
        public string WrittenMessage { get; private set; }

        public void WriteToConsole(string message)
        {
            this.WrittenMessage += message;
        }
    }


}
