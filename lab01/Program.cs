using System;
using System.Net;
abstract class MusicalInstrument
{
    public string Name { get; set; }
    public string Characteristics { get; set; }

    public MusicalInstrument(string name, string characteristics)
    {
        Name = name;
        Characteristics = characteristics;
    }

    public abstract void Show();
    public abstract void Sound();
    public abstract void Desc();
    public abstract void History();

    public void ShowInfo()
    {
        Show();
        Console.WriteLine($"Characteristics: {Characteristics}");
        Desc();
        History();
        Sound();
        Console.WriteLine("\n*****************************\n");
    }
}

class Violin : MusicalInstrument
{
    public Violin(string name, string characteristics) : base(name, characteristics) { }

    public override void Show() => Console.WriteLine($"Instrument: {Name}");
    public override void Sound() => Console.WriteLine("To be honest, I really don't know how to explain sound with words!");
    public override void Desc() => Console.WriteLine("A string instrument");
    public override void History() => Console.WriteLine("Originated in Italy");
}

class Trombone : MusicalInstrument
{
    public Trombone(string name, string characteristics) : base(name, characteristics) { }

    public override void Show() => Console.WriteLine($"Instrument: {Name}");
    public override void Sound() => Console.WriteLine("To be honest, I really don't know how to explain sound with words!");
    public override void Desc() => Console.WriteLine("A brass instrument");
    public override void History() => Console.WriteLine("Developed in medieval period.");
}

class Ukulele : MusicalInstrument
{
    public Ukulele(string name, string characteristics) : base(name, characteristics) { }

    public override void Show() => Console.WriteLine($"Instrument: {Name}");
    public override void Sound() => Console.WriteLine("To be honest, I really don't know how to explain sound with words!");
    public override void Desc() => Console.WriteLine("A small instrument from Hawaii.");
    public override void History() => Console.WriteLine("Introduced in Hawaii.");
}
class Cello : MusicalInstrument
{
    public Cello(string name, string characteristics) : base(name, characteristics) { }

    public override void Show() => Console.WriteLine($"Instrument: {Name}");
    public override void Sound() => Console.WriteLine("To be honest, I really don't know how to explain sound with words!");
    public override void Desc() => Console.WriteLine("A string instrument.");
    public override void History() => Console.WriteLine("Evolved from the bass violin.");
}
class Program
{
    static void Main()
    {
        Console.WriteLine("Lab01");
        Console.WriteLine("Author: Dmytro Dmytryshchak; ZIPZ-24-1");
        Console.WriteLine("\n*****************************\n");

        MusicalInstrument[] instruments = new MusicalInstrument[]
        {
            new Violin("Violin", "Wooden, 4 strings"),
            new Trombone("Trombone", "Brass, bold sound"),
            new Ukulele("Ukulele", "Small, 4 strings"),
            new Cello("Cello", "Deep tone, 4 strings")
        };

        foreach (var instrument in instruments)
            instrument.ShowInfo();
    }
}