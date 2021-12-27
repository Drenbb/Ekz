using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dll
{
    public struct FIO//Структура для хранения данных из CSV(ФИО и возраст пользователей)
    {
        public string Imya;
        public string Familia;
        public string Otch;
        public int Age;

        public FIO(string Familia, string Imya, string Otch, int Age)
        {
            this.Imya = Imya;
            this.Familia = Familia;
            this.Otch = Otch;
            this.Age = Age;
        }

        public string Show()
        {
            string s = " Фамилия -" + Familia + "Имя- " + Imya + " Отчество- " + Otch + ", Возраст - " + Age + "\n";
            return s;
        }
    }
    public static class Class1
    {
        public static FIO[] p;
        public static void read(string s)//Метод перевода из переменной стринг в массив стринг
        {
            int z = 0;
            p = new FIO[1];
            string[] ss = s.Split(';', '\n', '\r');//Удаление не нужных сиволов
            try
            {
                for (int i = 0; i < ss.Length; i++)//Запись из массива стринг в массив структуры
                {
                    p[z].Familia = ss[i];
                    p[z].Imya = ss[i + 1];
                    p[z].Otch = ss[i + 2];
                    p[z].Age = Convert.ToInt32(ss[i + 3]);
                    z++;
                    i += 4;
                    Array.Resize(ref p, p.Length + 1);
                }
            }
            catch
            {
                Array.Resize(ref p, p.Length - 1);
            }
        }
        public static void Second()//Метод поиска пользователей с фамилией Иванов
        {
            using (StreamWriter sw = new StreamWriter(File.Open("Ivanovy.csv", FileMode.Create)))
            {
                foreach (FIO f in p)
                {
                    if (f.Familia.Contains("Иванов"))
                    {

                        sw.Write(f.Familia + ";" + f.Imya + ";" + f.Otch + ";" + f.Age + "\r\n");

                    }
                }
            }
        }

        public static void Third()//Метод поиска среднего возраста всех пользователей и самого Длинного ФИО
        {
            float summ = 0;
            for (int i = 0; i < p.Length; i++)
            {
                summ += p[i].Age;
            }
            float average = summ / p.Length;
            using (StreamWriter sw = new StreamWriter(File.Open("AverageAge.csv", FileMode.Create)))
            {
                sw.Write("Средний возраст всех пользователей " + average);
            }


            int d = p[0].Familia.Length + p[0].Imya.Length + p[0].Otch.Length;
            int ind = 0;
            for (int i = 0; i < p.Length; i++)
            {
                if (d < p[i].Familia.Length + p[i].Imya.Length + p[i].Otch.Length)
                {
                    ind = i;
                    d = p[i].Familia.Length + p[i].Imya.Length + p[i].Otch.Length;
                }
            }
            string f = p[ind].Familia + " " + p[ind].Imya + " " + p[ind].Otch;
            int z = f.Length;
            RegistryKey registryKey = Registry.CurrentUser;
            if (registryKey.GetValueNames().Contains("MaxLen"))//Запись самого длинного ФИО в реестр
            {
                if (f != registryKey.GetValue("MaxLen"))
                {
                    registryKey.DeleteValue("MaxLen");
                    registryKey.SetValue("MaxLen", f);
                }
            }
            else
            {
                registryKey.SetValue("MaxLen", f);
            }

            if (registryKey.GetValueNames().Contains("AverageAge"))//Запись среднего возраста в реестр
            {
                if (f != registryKey.GetValue("AverageAge"))
                {
                    registryKey.DeleteValue("AverageAge");
                    registryKey.SetValue("AverageAge", average);
                }
            }
            else
            {
                registryKey.SetValue("AverageAge", average);
            }
        }
        public static void fourth()//Сохранение всего списка в Буффер обмена
        {
            string k = "";
            foreach (FIO f in p)
            {
                k += f.Familia + ";" + f.Imya + ";" + f.Otch + ";" + f.Age + "\r\n";
               
            }
            Clipboard.SetText(k);
        }
    }
  
}
