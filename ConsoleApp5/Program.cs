using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Номиналы российских рублей могут принимать значения 1, 2, 5, 10, 50, 100, 500, 1000, 5000.");
        Console.WriteLine("Копейки представить как 0,01 (1 копейка), 0,05 (5 копеек), 0,1 (10 копеек), 0,5 (50 копеек).");
        Console.WriteLine("Введите первую сумму: ");
        string[] input1 = Console.ReadLine().Split(',');
        int rub = int.Parse(input1[0]);
        int kop = input1.Length > 1 ? int.Parse(input1[1]) : 0; // Добавляем проверку на наличие копеек

        Console.WriteLine("Введите вторую сумму: ");
        string[] input2 = Console.ReadLine().Split(',');
        int rub2 = int.Parse(input2[0]);
        int kop2 = input2.Length > 1 ? int.Parse(input2[1]) : 0; // Добавляем проверку на наличие копеек

        Money length1 = new Money(rub, kop);
        Money length2 = new Money(rub2, kop2);

        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Сложение");
        Console.WriteLine("2. Вычитание");
        Console.WriteLine("3. Умножение");
        Console.WriteLine("4. Деление");
        Console.WriteLine("5. Сравнение");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("Результат сложения: " + (length1 + length2));
                break;
            case 2:
                Console.WriteLine("Результат вычитания: " + (length1 - length2));
                break;
            case 3:
                Console.WriteLine("Введите множитель:");
                int multiplier = int.Parse(Console.ReadLine());
                Console.WriteLine("Результат умножения: " + (length1 * multiplier));
                break;
            case 4:
                Console.WriteLine("Введите делитель:");
                int divisor = int.Parse(Console.ReadLine());
                Console.WriteLine("Результат деления: " + (length1 / divisor));
                break;
            case 5:
                if (length1 < length2)
                    Console.WriteLine("Первая сумма меньше второй");
                else if (length1 > length2)
                    Console.WriteLine("Первая сумма больше второй");
                else
                    Console.WriteLine("Суммы равны");
                break;
            default:
                Console.WriteLine("Неверный выбор операции.");
                break;
        }
    }

    class Money
    {
        public int Rubs { get; set; }
        public int Kops { get; set; }

        public Money(int rubs, int kops)
        {
            Rubs = rubs;
            Kops = kops;
        }

        public static Money operator +(Money rub1, Money rub2)
        {
            int totalRubs = rub1.Rubs + rub2.Rubs;
            int totalKops = rub1.Kops + rub2.Kops;
            if (totalKops >= 100)
            {
                totalRubs += totalKops / 100;
                totalKops %= 100;
            }
            if (totalKops.ToString().Length == 1)
            {
                int normKops = totalKops * 10;
                totalKops = 0;
                totalKops = totalKops + normKops;
            }
            return new Money(totalRubs, totalKops);
        }

        public static Money operator -(Money rub1, Money rub2)
        {
            int totalRubs = rub1.Rubs - rub2.Rubs;
            int totalKops = rub1.Kops - rub2.Kops;
            if (totalKops < 0)
            {
                totalRubs -= 1;
                totalKops += 100;
            }
            if (totalKops.ToString().Length == 1)
            {
                int normKops = totalKops * 10;
                totalKops = 0;
                totalKops = totalKops + normKops;
            }
            return new Money(totalRubs, totalKops);
        }

        public static Money operator *(Money rub1, int multiplier)
        {
            int totalRubs = rub1.Rubs * multiplier;
            int totalKops = rub1.Kops * multiplier;
            if (totalKops >= 100)
            {
                totalRubs += totalKops / 100;
                totalKops %= 100;
            }
            if (totalKops.ToString().Length == 1)
            {
                int normKops = totalKops * 10;
                totalKops = 0;
                totalKops = totalKops + normKops;
            }
            return new Money(totalRubs, totalKops);
        }

        public static Money operator /(Money rub, int divisor)
        {
            int totalRubs = rub.Rubs / divisor;
            int totalKops = rub.Kops / divisor;
            if (totalKops.ToString().Length == 1)
            {
                int normKops = totalKops * 10;
                totalKops = 0;
                totalKops = totalKops + normKops;
            }
            return new Money(totalRubs, totalKops);
        }

        public static bool operator <(Money rub1, Money rub2)
        {
            int totalKops1 = rub1.Rubs * 100 + rub1.Kops;
            int totalKops2 = rub2.Rubs * 100 + rub2.Kops;

            return totalKops1 < totalKops2;
        }

        public static bool operator >(Money rub1, Money rub2)
        {
            int totalKops1 = rub1.Rubs * 100 + rub1.Kops;
            int totalKops2 = rub2.Rubs * 100 + rub2.Kops;
            return totalKops1 > totalKops2;
        }

        public override string ToString()
        {
            return $"{Rubs} рублей {Kops} копеек";
        }
    }
}
