using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace vardibileBlockchain
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public int Nonce { get; set; } = 0;
        public string Hash { get; set; }

        public Block(DateTime timeStamp, string previousHash, IList<Transaction> transaction)
        {
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Transactions = transaction;
        }

        public string HashCalculate()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}"));
            return Convert.ToBase64String(hash);
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.HashCalculate();
            }
        }
    }
}
