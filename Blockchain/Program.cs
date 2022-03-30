using Newtonsoft.Json;
using System.Diagnostics;
using vardibileBlockchain;

Blockchain ourblockchain = new Blockchain();

//İşlemler
var watch = Stopwatch.StartNew();
ourblockchain.CreateTransaction(new Transaction("Ahmet", "Esad", 15));
ourblockchain.ProcessTransactions("AEB");
ourblockchain.CreateTransaction(new Transaction("Esad", "Barut", 10));
ourblockchain.ProcessTransactions("AEB");
watch.Stop();

//Yazdırma
Console.WriteLine($"Runtime: {watch.ElapsedMilliseconds} ms\n");
Console.WriteLine("Ahmet balance: " + ourblockchain.GetBalance("Ahmet").ToString() + " VRB");
Console.WriteLine("Esad balance: " + ourblockchain.GetBalance("Esad").ToString() + " VRB");
Console.WriteLine("Barut balance: " + ourblockchain.GetBalance("Barut").ToString() + " VRB");
Console.WriteLine("AEB balance: " + ourblockchain.GetBalance("AEB").ToString() + " VRB");
Console.WriteLine("\n" + JsonConvert.SerializeObject(ourblockchain,Formatting.Indented));
Console.ReadKey();
