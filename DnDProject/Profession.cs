using System;

namespace DnDProject
{
	public class Profession
	{
		public string Name { get; }
		public int HitDie { get; }
		/// <summary>
		/// Constructor to generate the strict list of classes
		/// </summary>
		/// <param name="name"></param>
		/// <param name="hitDie"></param>
		private Profession(string name, int hitDie)
		{
			Name = name;
			HitDie = hitDie;
		}

		public static readonly Profession BARBARIAN = new Profession("Barbarian", 12);
		public static readonly Profession BARD = new Profession("Bard", 8);
		public static readonly Profession CLERIC = new Profession("Cleric", 8);
		public static readonly Profession DRUID = new Profession("Druid", 8);
		public static readonly Profession FIGHTER = new Profession("Fighter", 10);
		public static readonly Profession MONK = new Profession("Monk", 8);
		public static readonly Profession PALADIN = new Profession("Paladin", 10);
		public static readonly Profession RANGER = new Profession("Ranger", 10);
		public static readonly Profession SORCERER = new Profession("Sorcerer", 6);
		public static readonly Profession WARLOCK = new Profession("Warlock", 8);
		public static readonly Profession WIZARD = new Profession("Wizard", 8);
	}
}