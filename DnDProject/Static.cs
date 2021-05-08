using System;

namespace DnDProject
{
	public class Static
	{
		/// <summary>
		/// General 'dice roller' now accepting any dice instead of 2,4,6,8,10,12,20,100
		/// </summary>
		/// <param name="x"></param>
		/// <returns>The 'roll'</returns>
		public static int RollAny(int x) { return (int)Math.Ceiling(new Random().NextDouble() * x); }
	}
}
