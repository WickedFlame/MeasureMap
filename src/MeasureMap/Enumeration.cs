using System;

namespace MeasureMap
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; }

        public string Name { get; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToLower()
        {
            return Name.ToLower();
        }

        public int CompareTo(object obj)
        {
            return Id.CompareTo(((Enumeration)obj).Id);
        }

        public static int Compare(Enumeration left, Enumeration right)
        {
            if (object.ReferenceEquals(left, right))
            {
                return 0;
            }

            if (left is null)
            {
                return -1;
            }

            return left.CompareTo(right);
        }

        public override bool Equals(object obj)
        {
            return obj is Enumeration en && en.Id == Id && en.ToLower() == ToLower();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static implicit operator string(Enumeration en)
        {
            return en.ToString();
        }

        public static bool operator ==(Enumeration left, Enumeration right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !(left == right);
        }

        public static bool operator <(Enumeration left, Enumeration right)
        {
            return Compare(left, right) < 0;
        }

        public static bool operator <=(Enumeration left, Enumeration right)
        {
            return Compare(left, right) <= 0;
        }

        public static bool operator >(Enumeration left, Enumeration right)
        {
            return Compare(left, right) > 0;
        }

        public static bool operator >=(Enumeration left, Enumeration right)
        {
            return Compare(left, right) >= 0;
        }
    }
}
