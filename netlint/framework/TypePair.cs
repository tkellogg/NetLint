using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netlint.framework
{
	internal class TypePair
	{
		public TypePair(Type a, Type b)
		{
			Base = a;
			ComparedTo = b;
		}

		public Type Base { get; set; }
		public Type ComparedTo { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is TypePair)
			{
				var other = (TypePair)obj;
				return other.Base == Base && other.ComparedTo == ComparedTo;
			}
			else return false;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int ret = Base.GetHashCode() * 29;
				return ComparedTo.GetHashCode() * 31 + ret;
			}
		}

		public override string ToString()
		{
			return string.Format("[{0} => {1}]", Base, ComparedTo);
		}
	}
}
