using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ConsoleApp4
{
    /// <summary>
    /// Абстрактный класс, от которого наследуются классы продукта, партии, набора
    /// </summary>
    public abstract class Goods
    {
        /// <summary>
        /// Позволяет выводить информацию массива, заполненную через классы
        /// </summary>
        public abstract void Print();
    }
    /// <summary>
    /// Класс функций продукта
    /// </summary>
    /// <remarks>
    /// Функция выводит одной строкой аргументы которые были выведены в классе
    /// </remarks>
    public class Product : Goods
    {
        /// <summary>
        /// Поля
        /// </summary>
        public string name;
        public int price;
        public DateTime dataizg;
        public DateTime srok;
        /// <summary>
        /// Конструктор, позволяющий задать значения полей при создании объекта класса
        /// </summary>
        /// <param name="name">Значение коэффицента name</param>
        /// <param name="price">Значение коэффицент price</param>
        /// <param name="dataizg">Значение коэффицент dataizg</param>
        /// <param name="srok">Значение коэффицент srok</param>
        public Product(string name, int price, DateTime dataizg, DateTime srok)
        {
            this.name = name;
            this.price = price;
            this.dataizg = dataizg;
            this.srok = srok;
        }
        public Product() { }
        /// <summary>
        /// Делает все коэффиценты класса в одну строку
        /// </summary>
        /// <returns>Строка</returns>
        public override string ToString()
        {
            return String.Format("Название: {0}; Цена: {1}; Дата изготовления: {2}; Срок годности: {3}", name, price, dataizg.ToString("dd/MM/yyyy"), srok.ToString("dd/MM/yyyy"));
        }
        /// <summary>
        /// Выводит все коэффиценты класса
        /// </summary>
        public override void Print()
        {
            Console.WriteLine("Название: {0}", name);
            Console.WriteLine("Цена: {0}", price);
            Console.WriteLine("Дата изготовления: {0}", dataizg.ToString("dd/MM/yyyy"));
            Console.WriteLine("Срок годности: {0}", srok);
        }


    }
    /// <summary>
    /// Класс функций партии
    /// </summary>
    /// <remarks>
    /// Функция выводит одной строкой аргументы которые были выведены в классе
    /// </remarks>
    public class Consignment : Goods
    {
        /// <summary>
        /// Коэффиценты
        /// </summary>
        public string name;
        public int price;
        public int kolvo;
        public DateTime dataizg;
        public DateTime srok;
        /// <summary>
        /// Конструктор, позволяющий задать значения полей при создании объекта класса
        /// </summary>
        /// <param name="name">Значение коэффицента name</param>
        /// <param name="price">Значение коэффицента price</param>
        /// <param name="kolvo">Значение коэффицента kolvo</param>
        /// <param name="dataizg">Значение коэффицента dataizg</param>
        /// <param name="srok">Значение коэффицента srok</param>
        public Consignment(string name, int price, int kolvo, DateTime dataizg, DateTime srok)
        {
            this.name = name;
            this.price = price;
            this.kolvo = kolvo;
            this.dataizg = dataizg;
            this.srok = srok;
        }
        public Consignment() { }
        /// <summary>
        /// Делает все коэффиценты класса в одну строку
        /// </summary>
        /// <returns>Строка</returns>
        public override string ToString()
        {
            return String.Format("Название: {0}; Цена: {1}; Количество: {2}; Дата изготовления: {3}; Срок годности: {4}", name, price, kolvo,
                dataizg.ToString("dd/MM/yyyy"), srok.ToString("dd/MM/yyyy"));
        }
        /// <summary>
        /// Выводит все коэффиценты класса
        /// </summary>
        public override void Print()
        {
            Console.WriteLine("Название: {0}", name);
            Console.WriteLine("Цена: {0}", price);
            Console.WriteLine("Количество: {0}", kolvo);
            Console.WriteLine("Дата изготовления: {0}", dataizg);
            Console.WriteLine("Срок годности: {0}", srok);
        }


    }
    /// <summary>
    /// Класс функций набора
    /// </summary>
    /// <remarks>
    /// Функция выводит одной строкой аргументы которые были выведены в классе
    /// </remarks>
    public class Set : Goods
    { /// <summary>
      /// Коэффиценты
      /// </summary>
        protected string name;
        protected int price;
        protected string perechen;
        /// <summary>
        /// Конструктор, позволяющий задать значения полей при создании объекта класса
        /// </summary>
        /// <param name="name">Значение коэффицента name</param>
        /// <param name="price">Значение коэффицента price</param>
        /// <param name="perechen">Значение коэффицента perechen</param>
        public Set(string name, int price, string perechen)
        {
            this.name = name;
            this.price = price;
            this.perechen = perechen;
        }

        public Set() { }
        /// <summary>
        /// Выводит все коэффиценты класса
        /// </summary>
        public override void Print()
        {
            Console.WriteLine("Название: " + name, "Цена: " + price, "Перечень: " + perechen);

        }
        /// <summary>
        /// Делает все коэффиценты класса в одну строку
        /// </summary>
        /// <returns>Строку</returns>
        public override string ToString()
        {
            return String.Format("Название: {0}; Цена: {1}; Перечень: {2}", name, price, perechen);
        }
    }
    /// <summary>
    /// Основной класс исполняемой программы, содержащий Main
    /// </summary>
    /// <remarks>
    /// Работа с классами продукта, партии и набора
    /// Входной файл имеет вид:
    /// Первый элемент каждой строки - название класса 
    /// Последующие элементы это значения для вставки 
    /// Разделитель - точка с запятой
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args">Список аргументов командной строки</param>
        static void Main(string[] args)
        {
            File.Create("objects.xml").Close();
            Goods[] g = new Goods[6];

            CultureInfo cultures = new CultureInfo("ru-RU");


            string fileName = @"in.txt";

            int arrayIdx = 0;
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    char[] spearator = { ';' };
                    string[] chunks = line.Split(spearator);
                    if (chunks.Length >= 3)
                    {

                        string itemType = chunks[0];
                        string itemName = chunks[1];
                        int itemPrice = Int32.Parse(chunks[2]);
                        switch (itemType)
                        {
                            case "Product":
                                Trace.WriteLine("Вызов конструктора метода Product");
                                g[arrayIdx++] = new Product(itemName, itemPrice, Convert.ToDateTime(chunks[3], cultures), Convert.ToDateTime(chunks[4], cultures));
                                XmlSerializer serializerProduct = new XmlSerializer(typeof(Product));
                                using (StreamWriter sw = new StreamWriter("objects.xml", true, System.Text.Encoding.Default))
                                {
                                    serializerProduct.Serialize(sw, g[arrayIdx - 1]);
                                    sw.WriteLine("\n");
                                }
                                break;
                            case "Consignment":
                                Trace.WriteLine("Вызов конструктора метода Consignment");
                                g[arrayIdx++] = new Consignment(itemName, itemPrice, Int32.Parse(chunks[3]), Convert.ToDateTime(chunks[4], cultures), Convert.ToDateTime(chunks[5], cultures));
                                XmlSerializer serializerConsignment = new XmlSerializer(typeof(Consignment));
                                using (StreamWriter sw = new StreamWriter("objects.xml", true, System.Text.Encoding.Default))
                                {
                                    serializerConsignment.Serialize(sw, g[arrayIdx - 1]);
                                    sw.WriteLine("\n");
                                }
                                break;
                            case "Set":
                                Trace.WriteLine("Вызов конструктора метода Set");
                                g[arrayIdx++] = new Set(itemName, itemPrice, chunks[3]);
                                XmlSerializer serializerSet = new XmlSerializer(typeof(Set));
                                using (StreamWriter sw = new StreamWriter("objects.xml", true, System.Text.Encoding.Default))
                                {
                                    serializerSet.Serialize(sw, g[arrayIdx - 1]);
                                    sw.WriteLine("\n");
                                }
                                break;

                        }
                    }
                }
            }


            DateTime now = DateTime.Now;
            for (int i = 0; i < g.Length; i++)
            {
                DateTime srok = DateTime.Now;
                if (g[i] is Product)
                {
                    srok = (g[i] as Product).srok;
                }
                else if (g[i] is Consignment)
                {
                    srok = (g[i] as Consignment).srok;
                }
                else if (g[i] is Set)
                {
                }
                bool prosrochen = DateTime.Compare(srok, now) < 0;
                Console.WriteLine(g[i] + (prosrochen ? "  [ПРОСРОЧЕН!]" : ""));
            }
            Console.ReadKey();
            Trace.Flush();



        }
    }
}