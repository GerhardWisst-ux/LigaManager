using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ChartJS.Blazor
{
    public static class ChartJSColors
    {
        public static List<string> GetAllColors()
        {
            return new() { Pink, Orange, Bulbous, Turqouise, Aqua, Violet, Grey };
        }

        public static IEnumerable<string> GetRandomEnumerable(int count = 3,[Range(0,1)] float? transparency = null )
        {
            for (int i = 0; i < count; i++)
            {
                yield return $"rgba({new Random().Next(0,255)}, {new Random().Next(0, 255)} , {new Random().Next(0, 255)},{transparency?? 1})";
            }
        }


        public static IEnumerable<string> GetEnumerableOfSame(int count =3,string color = Aqua)
        {
            for (int i = 0; i < count; i++)
            {
                yield return color;
            }
        }
        public const string Pink = "rgb(255, 99, 132)";
        public const string Orange = "rgb(255, 159, 64)";
        public const string Bulbous = "rgb(255, 205, 86)";
        public const string Turqouise = "rgb(75, 192, 192)";
        public const string Aqua = "rgb(54, 162, 235)";
        public const string Violet = "rgb(153, 102, 255)";
        public const string Grey = "rgb(201, 203, 207)";
        public static class Transparent20
        {
            public const string Pink = "rgba(255, 99, 132, 0.2)";
            public const string Orange = "rgba(255, 159, 64, 0.2)";
            public const string Bulbous = "rgba(255, 205, 86, 0.2)";
            public const string Turqouise = "rgba(75, 192, 192, 0.2)";
            public const string Aqua = "rgba(54, 162, 235, 0.2)";
            public const string Violet = "rgba(153, 102, 255, 0.2)";
            public const string Grey = "rgba(201, 203, 207, 0.2)";

            public static List<string> GetAllColors()
            {
                return new() { Pink, Orange, Bulbous, Turqouise, Aqua, Violet, Grey };
            }
        }



        public static class Transparent40
        {
            public const string Pink = "rgba(255, 99, 132, 0.4)";
            public const string Orange = "rgba(255, 159, 64, 0.4)";
            public const string Bulbous = "rgba(255, 205, 86, 0.4)";
            public const string Turqouise = "rgba(75, 192, 192, 0.4)";
            public const string Aqua = "rgba(54, 162, 235, 0.4)";
            public const string Violet = "rgba(153, 102, 255, 0.4)";
            public const string Grey = "rgba(201, 203, 207, 0.4)";
            public static List<string> GetAllColors()
            {
                return new() { Pink, Orange, Bulbous, Turqouise, Aqua, Violet, Grey };
            }
        }

        public static class Transparent60
        {
            public const string Pink = "rgba(255, 99, 132, 0.6)";
            public const string Orange = "rgba(255, 159, 64, 0.6)";
            public const string Bulbous = "rgba(255, 205, 86, 0.6)";
            public const string Turqouise = "rgba(75, 192, 192, 0.6)";
            public const string Aqua = "rgba(54, 162, 235, 0.6)";
            public const string Violet = "rgba(153, 102, 255, 0.6)";
            public const string Grey = "rgba(201, 203, 207, 0.6)";
            public static List<string> GetAllColors()
            {
                return new() { Pink, Orange, Bulbous, Turqouise, Aqua, Violet, Grey };
            }
        }

        public static IEnumerable<string> Randomize20(int count)
        {

            var colorList = new List<string>()
            {
                Transparent20.Pink ,
                Transparent20.Orange ,
                Transparent20.Bulbous ,
                Transparent20.Turqouise,
                Transparent20.Aqua,
                Transparent20.Violet,
                Transparent20.Grey
            };
            for (int i = 0; i < count; i++)
            {
                yield return colorList.OrderBy(x => Guid.NewGuid()).First();
            }
        }
        public static IEnumerable<string> Randomize40(int count)
        {

            var colorList = new List<string>()
            {
                Transparent40.Pink ,
                Transparent40.Orange ,
                Transparent40.Bulbous ,
                Transparent40.Turqouise,
                Transparent40.Aqua,
                Transparent40.Violet,
                Transparent40.Grey
            };
            for (int i = 0; i < count; i++)
            {
                yield return colorList.OrderBy(x => Guid.NewGuid()).First();
            }
        }

        public static IEnumerable<string> Randomize60(int count)
        {

            var colorList = new List<string>()
            {
                Transparent60.Pink ,
                Transparent60.Orange ,
                Transparent60.Bulbous ,
                Transparent60.Turqouise,
                Transparent60.Aqua,
                Transparent60.Violet,
                Transparent60.Grey
            };
            for (int i = 0; i < count; i++)
            {
                yield return colorList.OrderBy(x => Guid.NewGuid()).First();
            }
        }
    }

}
