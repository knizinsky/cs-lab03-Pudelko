using System;
using System.Collections;
using System.Globalization;

namespace Pudelko1
{
    public sealed class Pudelko : IEquatable<Pudelko>, IEnumerable
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;

        public double A => _a;
        public double B => _b;
        public double C => _c;

        public double Objetosc => Math.Round(_a * _b * _c, 9);
        public double Pole => Math.Round(2 * (_a * _b + _a * _c + _b * _c), 6);

        public enum UnitOfMeasure
        {
            milimeter,
            centimeter,
            meter
        }

        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            double maxDimensionInMeters = 10;

            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentOutOfRangeException("Nieprawidłowe rozmiary!");
            }

            if (unit == UnitOfMeasure.milimeter && (a <= 0.1 || b <= 0.1 || c <= 0.1))
            {
                throw new ArgumentOutOfRangeException("Nieprawidłowe rozmiary!");
            }
            else if (unit == UnitOfMeasure.centimeter && (a <= 0.01 || b <= 0.01 || c <= 0.01))
            {
                throw new ArgumentOutOfRangeException("Nieprawidłowe rozmiary!");
            }
            else if (unit == UnitOfMeasure.meter && (a <= 0.001 || b <= 0.001 || c <= 0.001))
            {
                throw new ArgumentOutOfRangeException("Nieprawidłowe rozmiary!");
            }

            switch (unit)
            {
                case UnitOfMeasure.milimeter:
                    a /= 1000;
                    b /= 1000;
                    c /= 1000;
                    maxDimensionInMeters /= 100;
                    break;
                case UnitOfMeasure.centimeter:
                    a /= 100;
                    b /= 100;
                    c /= 100;
                    maxDimensionInMeters /= 10;
                    break;
                case UnitOfMeasure.meter:
                    break;
                default:
                    throw new ArgumentException("Nieprawidłowa miara");
            }

            if (a > maxDimensionInMeters || b > maxDimensionInMeters || c > maxDimensionInMeters)
            {
                throw new ArgumentOutOfRangeException("Nieprawidłowe rozmiary!");
            }

            _a = a;
            _b = b;
            _c = c;
        }

        public override string ToString()
        {
            return $"{_a:F3} m × {_b:F3} m × {_c:F3} m";
        }

        public string ToString(string format)
        {
            switch (format)
            {
                case "m":
                    return $"{_a:F3} m × {_b:F3} m × {_c:F3} m";
                case "cm":
                    return $"{_a * 100:F1} cm × {_b * 100:F1} cm × {_c * 100:F1} cm";
                case "mm":
                    return $"{_a * 1000:F0} mm × {_b * 1000:F0} mm × {_c * 1000:F0} mm";
                case null:
                    return $"{_a:F3} m × {_b:F3} m × {_c:F3} m";
                default:
                    throw new FormatException("Nieprawidłowy format");
            }
        }

        public bool Equals(Pudelko other)
        {
            if (other == null)
                return false;

            double[] thisDimensions = { _a, _b, _c };
            double[] otherDimensions = { other.A, other.B, other.C };

            Array.Sort(thisDimensions);
            Array.Sort(otherDimensions);

            return Enumerable.SequenceEqual(thisDimensions, otherDimensions);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Pudelko))
                return false;
            else
                return Equals((Pudelko)obj);
        }

        public override int GetHashCode()
        {
            double[] dimensions = { _a, _b, _c };
            Array.Sort(dimensions);

            int hashCode = 17;
            foreach (double dimension in dimensions)
            {
                hashCode = hashCode * 31 + dimension.GetHashCode();
            }
            return hashCode;
        }

        public static bool operator ==(Pudelko a, Pudelko b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Pudelko a, Pudelko b)
        {
            return !(a == b);
        }
        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            double a = p1.A + p2.A;
            double b = p1.B + p2.B;
            double c = p1.C + p2.C;

            double maxDimensionInMeters = 10;

            if (a > maxDimensionInMeters || b > maxDimensionInMeters || c > maxDimensionInMeters)
            {
                throw new ArgumentOutOfRangeException("Nieprawidłowe rozmiary!");
            }

            return new Pudelko(a, b, c, Pudelko.UnitOfMeasure.meter);
        }

        public static explicit operator double[](Pudelko p)
        {
            return new double[] { p.A, p.B, p.C };
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> dimensions)
        {
            double a = dimensions.Item1 / 1000.0;
            double b = dimensions.Item2 / 1000.0;
            double c = dimensions.Item3 / 1000.0;

            return new Pudelko(a, b, c, Pudelko.UnitOfMeasure.meter);
        }

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return _a;
                    case 1:
                        return _b;
                    case 2:
                        return _c;
                    default:
                        throw new IndexOutOfRangeException("Nieprawidłowy indeks");
                }
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return _a;
            yield return _b;
            yield return _c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Pudelko Parse(string dimensions)
        {
            string[] dimensionParts = dimensions.Split(new char[] { '×', 'x' }, StringSplitOptions.RemoveEmptyEntries);
            if (dimensionParts.Length != 3)
            {
                throw new FormatException("Niepoprawny format");
            }

            double[] parsedDimensions = new double[3];
            for (int i = 0; i < 3; i++)
            {
                string dimensionPart = dimensionParts[i].Trim();
                string[] valueAndUnit = dimensionPart.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (valueAndUnit.Length != 2)
                {
                    throw new FormatException("Niepoprawny format");
                }

                double value;
                if (!double.TryParse(valueAndUnit[0], NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                {
                    throw new FormatException("Niepoprawny format");
                }

                switch (valueAndUnit[1])
                {
                    case "m":
                        parsedDimensions[i] = value;
                        break;
                    case "cm":
                        parsedDimensions[i] = value / 100;
                        break;
                    case "mm":
                        parsedDimensions[i] = value / 1000;
                        break;
                    default:
                        throw new FormatException("Niepoprawny format");
                }
            }

            return new Pudelko(parsedDimensions[0], parsedDimensions[1], parsedDimensions[2]);
        }
    }
}