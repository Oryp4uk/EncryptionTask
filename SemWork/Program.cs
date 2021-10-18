using System;

namespace SemWork
{
    class Program
    {
        static void Main(string[] args)
        {
            bool encrypt = false;
            Console.WriteLine("Выберите вид работы \n 1 - Зашифровать слово\n 2 - Расшифровать слово");
            var method = Console.ReadLine();
            switch (method)
            {
                case "1":
                    encrypt = true;
                    break;
                case "2":
                    encrypt = false;
                    break;
                default:
                    Console.WriteLine("\nВыбран неправильный вид работы");
                    return;
            }
            Console.WriteLine("\nВведите слово");
            string msg = Console.ReadLine();
            Console.WriteLine("\nВведите ключ");
            var key = Console.ReadLine();

            if (encrypt)
            {
                Console.WriteLine("Выберите тип шифрования \n 1 - Шифр Цезаря\n 2 - Шифр Виженера\n 3 - XOR-шифр");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("\nРезультат: ");
                        Console.WriteLine(CaesarCipher.Encrypt(msg, int.Parse(key)));
                        break;
                    case "2":
                        Console.WriteLine("\nРезультат: ");
                        Console.WriteLine(VigenereCipher.Encrypt(msg, key));
                        break;
                    case "3":
                        Console.WriteLine("\nРезультат: ");
                        Console.WriteLine(XORCipher.Encrypt(msg, key));
                        break;
                    default:
                        Console.WriteLine("Выбран неправильный тип шифрования");
                        return;
                }
            }
            else
            {
                Console.WriteLine("Выберите тип дешифрования \n 1 - Шифр Цезаря\n 2 - Шифр Виженера\n 3 - XOR-шифр");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("\nРезультат: ");
                        Console.WriteLine(CaesarCipher.Decrypt(msg, int.Parse(key)));
                        break;
                    case "2":
                        Console.WriteLine("\nРезультат: ");
                        Console.WriteLine(VigenereCipher.Decrypt(msg, key));
                        break;
                    case "3":
                        Console.WriteLine("\nРезультат: ");
                        Console.WriteLine(XORCipher.Decrypt(msg, key));
                        break;
                    default:
                        Console.WriteLine("Выбран неправильный тип дешифрования");
                        return;
                }
            }
        }
    }

    class CaesarCipher
    {
        private static char Cipher(char ch, int key)
        {
            if (!((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')))
            {
                return ch;
            }
            char dict = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - dict) % 26) + dict);
        }

        public static string Encrypt(string msg, int key)
        {
            string result = "";

            foreach (char ch in msg)
            {
                result += Cipher(ch, key);
            }
            return result;
        }

        public static string Decrypt(string msg, int key)
        {
            return Encrypt(msg, 26 - key % 26);
        }
    }
    class VigenereCipher
    {
        private static string KeyCheck(string msg, string key)
        {
            if (key.Length < msg.Length)
            {
                for (int i = 0; key.Length < msg.Length; i++)
                {
                    key += key[i];
                }
                return key.ToUpper();
            }
            else
                return key.ToUpper();
        }

        public static string Encrypt(string msg, string key)
        {
            key = KeyCheck(msg, key);
            string result = "";
            msg = msg.ToUpper();
            for (int i = 0; i < msg.Length; i++)
            {
                if (!(msg[i] >= 'A' && msg[i] <= 'Z'))
                {
                    result += msg[i];
                }
                else
                {
                    int newChar = ((msg[i] + key[i]) % 26) + 'A';
                    result += (char)newChar;
                }

            }
            return result;
        }

        public static string Decrypt(string msg, string key)
        {
            key = KeyCheck(msg, key);
            string result = "";
            msg = msg.ToUpper();

            for (int i = 0; i < msg.Length && i < key.Length; i++)
            {
                int newChar = (msg[i] - key[i] + 26) % 26;

                newChar += 'A';
                result += (char)(newChar);
            }
            return result;
        }
    }
    class XORCipher
    {
        public static string Encrypt(string msg, string key)
        {
            string result = "";
            for (int i = 0; i < msg.Length; i++)
            {
                result += (msg[i] ^ key[i % key.Length])+" ";
            }

            return result;
        }

        public static string Decrypt(string msg, string key)
        {
            string result = "";
            for (int i = 0; i < msg.Length; i+=3)
            {
                if (msg[i] == ' ')
                    continue;
                else
                {
                    byte code = Convert.ToByte(msg.Substring(i, 2));
                    result += (char)(code ^ key[(i / 3) % key.Length]);
                }          
            }
            return result;
        }
    }
}