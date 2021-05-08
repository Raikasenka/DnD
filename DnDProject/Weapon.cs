using System;
using System.Collections.Generic;
using System.Text;

namespace DnDProject
{
	/// <summary>
	/// Class to host weapon functionalities
	/// </summary>
	public class Weapon
	{
		public string Name { get; }
		public string DamageType { get; }
		public int DiceRolls { get; }
		public int DiceRolled { get; }
		public int BaseDamage { get; }
		public int Durability { get; }

		#region Damage types
			public static readonly string BLUDGEONING ="Bludgeoning";
			public static readonly string PIERCING ="Piercing";
			public static readonly string SLASHING ="Slashing";
		#endregion
		/// <summary>
		/// Constructor for a breakable weapon
		/// </summary>
		/// <param name="nameIn"></param>
		/// <param name="diceRolls"></param>
		/// <param name="diceRolled"></param>
		/// <param name="baseDamage"></param>
		/// <param name="damageType"></param>
		/// <param name="durability"></param>
		public Weapon(string nameIn, int diceRolls, int diceRolled, int baseDamage, string damageType, int durability)
		{
			Name = nameIn;
			DiceRolls = diceRolls;
			DiceRolled = diceRolled;
			BaseDamage = baseDamage;
			DamageType = damageType;
			Durability = durability;
		}
		/// <summary>
		/// Constructor for an unbreakable weapon. IE Weapons of legends or something
		/// Technically could be avoided by assigning int.MaxValue on creation however too lazy
		/// </summary>
		/// <param name="nameIn"></param>
		/// <param name="diceRolls"></param>
		/// <param name="diceRolled"></param>
		/// <param name="baseDamage"></param>
		/// <param name="damageType"></param>
		public Weapon(string nameIn, int diceRolls, int diceRolled, int baseDamage, string damageType)
		{
			Name = nameIn;
			DiceRolls = diceRolls;
			DiceRolled = diceRolled;
			BaseDamage = baseDamage;
			DamageType = damageType;
			Durability = int.MaxValue;
		}
		/// <summary>
		/// Rolls the weapon for an attack
		/// </summary>
		/// <returns>The damage</returns>
		public int Attack()
		{
			int damage = BaseDamage;
			for (int x = 0; x < DiceRolls; x++)
			{
				damage += Static.RollAny(DiceRolled);
			}
			return damage;
		}
	}
}
