using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayList
{
    public class Program
    {
        static void Main(string[] args)
        {
            _ArrayList arrayList = new _ArrayList();
            arrayList.Add(101);
            arrayList.Add("ttt");
            arrayList.Add("rrr");
            arrayList.Add(10987645);
            arrayList.Add(100);
            arrayList.Add(10);

            Console.WriteLine(arrayList.Contains(10));
            Console.WriteLine(arrayList.IndexOf(10));
            arrayList.Insert(0, 1);
        }
    }
}
