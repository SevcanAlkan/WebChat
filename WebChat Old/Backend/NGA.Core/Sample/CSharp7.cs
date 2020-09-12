using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core.Sample
{
    public static class NGFeatures
    {
        public static int DigitSeperator = 1_000_000;

        /// <summary>
        /// This function returning to back sample X,Y,Z values
        /// Using; var (x, y, z) = Tuple();
        /// Or var (x, _, _) = Tuple();
        /// </summary>
        public static (int x, int y, int z) Tuple()
        {
            var conrdinates = (x: 1, y: 3, z: 0);
            return conrdinates;
        }

        public static int TryParse(string intValue)
        {
            if (int.TryParse(intValue, out var result))
            {
                return result;
            }

            return 0;
        }

        public static (int x, int y) LocalFunctions()
        {
            //var result = (x: getXValue(), y: getYValue());
            //return result;
            //OR;
            return getXYValue();

            //------------------------------------LOCAL FUNCTONS
            int getXValue()
            {
                int x = 5;

                return x;
            }

            int getYValue()
            {
                int y = 3;

                return y;
            }

            (int x, int y) getXYValue() => (x: getXValue(), y: getYValue());
        }

        public static string PatternMatching(IProduct obj)
        {
            //Before C# 7
            //var x = obj as KitchenProduct;
            //if (x != null)
            //{
            //    return "This is Null";
            //}

            //C# 7.0----------------------------------------
            //if (obj is KitchenProduct k)
            //{
            //    return "This is KitchenProduct";
            //}

            //Type switch 
            switch (obj)
            {
                case KitchenProduct kp:
                    return "This is KitchenProduct";
                case ElectronicProduct ep:
                    return "This is ElectronicProduct";
                case BaseProduct bp:
                    return "This is BaseProduct";
                case null:
                    return "This is Null";
            }

            return "I dont know what is this :(";
        }

        public static void RefReturn()
        {
            int x = 10;
            int y = 9;
            Max(ref x, ref y) = 12;
            //x should be 12
            Console.WriteLine($"X={x}, Y={y}");
        }
        public static ref int Max(ref int first, ref int second)
        {
            if (first > second)
            {
                return ref first;
            }
            else
            {
                return ref second;
            }
        }
    }

    //-------DEMO OBJECTS-------------------------
    public class BaseProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
    public class ElectronicProduct : BaseProduct, IProduct
    {

    }
    public class KitchenProduct : BaseProduct, IProduct
    {

    }
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
    }
}
