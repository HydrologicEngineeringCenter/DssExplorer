using Hec.Dss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCatalogExplorer
{
    public class CatalogProperties
    {
        public Rounding round = Rounding.None;
        public TimeWindow.ConsecutiveValueCompression compression = TimeWindow.ConsecutiveValueCompression.None;
        public enum Rounding
        {
            None,
            Tenth,
            Hundredth,
            Thousandth
        }

        public CatalogProperties()
        {

        }

        public void SetRounding(int value)
        {
            switch (value)
            {
                case 0:
                    round = Rounding.None;
                    break;
                case 1:
                    round = Rounding.Tenth;
                    break;
                case 2:
                    round = Rounding.Hundredth;
                    break;
                case 3:
                    round = Rounding.Thousandth;
                    break;
                default:
                    break;
            }
        }

        public void SetCompression(int value)
        {
            switch (value)
            {
                case 0:
                    compression = TimeWindow.ConsecutiveValueCompression.None;
                    break;
                case 1:
                    compression = TimeWindow.ConsecutiveValueCompression.NoData;
                    break;
                case 2:
                    compression = TimeWindow.ConsecutiveValueCompression.ZeroAndNoData;
                    break;
                case 3:
                    compression = TimeWindow.ConsecutiveValueCompression.AnyValue;
                    break;
                default:
                    break;
            }
        }

        public double Round(double num)
        {
            switch(round)
            {
                case Rounding.None:
                    break;
                case Rounding.Tenth:
                    num = Math.Round(num, 1);
                    break;
                case Rounding.Hundredth:
                    num = Math.Round(num, 2);
                    break;
                case Rounding.Thousandth:
                    num = Math.Round(num, 3);
                    break;
                default:
                    break;
            }
            return num;
        }

    }
}
