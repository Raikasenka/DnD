using System.Collections.Generic;

namespace DnDProject
{
	public class Skill
	{
		public string Name { get; }
		public string Description { get; }
		public int DiceRolls { get; }
		public int DiceRolled { get; }
		public int BaseDamage { get; }
		public int BonusDiceRolls { get; }
		public int BonusDiceRolled { get; }
		public int BonusBaseDamage { get; }
		/// <summary>
		/// Private constructor for a Skill.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="diceRolls"></param>
		/// <param name="diceRolled"></param>
		/// <param name="baseDamage"></param>
		/// <param name="description"></param>
		private Skill(string name, int diceRolls, int diceRolled, int baseDamage, string description)
		{
			Name = name;
			DiceRolls = diceRolls;
			DiceRolled = diceRolled;
			BaseDamage = baseDamage;
			Description = description;
			BonusDiceRolls = 0;
			BonusDiceRolled = 0;
			BonusBaseDamage = 0;
		}
		/// <summary>
		/// Private constructor for a Skill where it contains bonus damage rolls.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="diceRolls"></param>
		/// <param name="diceRolled"></param>
		/// <param name="baseDamage"></param>
		/// <param name="bonusDiceRolls"></param>
		/// <param name="bonusDiceRolled"></param>
		/// <param name="bonusBaseDamage"></param>
		/// <param name="description"></param>
		private Skill(string name, int diceRolls, int diceRolled, int baseDamage, int bonusDiceRolls, int bonusDiceRolled, int bonusBaseDamage, string description)
		{
			Name = name;
			DiceRolls = diceRolls;
			DiceRolled = diceRolled;
			BaseDamage = baseDamage;
			BonusDiceRolls = bonusDiceRolls;
			BonusDiceRolled = bonusDiceRolled;
			BonusBaseDamage = bonusBaseDamage;
			Description = description;
		}
		/// <summary>
		/// The master list of skills.
		/// </summary>
		public readonly static Dictionary<string, Skill> SkillList = new Dictionary<string, Skill>();
		/// <summary>
		/// Performs the skill usage
		/// </summary>
		/// <returns>The damage</returns>
		public int Attack()
		{
			int damage = BaseDamage + BonusBaseDamage;
			for(int x=0; x<DiceRolls;x++){
				damage+=Static.RollAny(DiceRolled);
			}
			for(int x=0; x<BonusDiceRolls;x++){
				damage+=Static.RollAny(BonusDiceRolled);
			}
			return damage;
		}
		/// <summary>
		/// Inserts the skills into the dictionary
		/// </summary>
		public static void LoadAll()
		{
			SkillList.Add("Bite",new Skill("Bite", 1, 6, 1, "Bites the chosen target for 1d6+1 damage"));
			SkillList.Add("Javelin", new Skill("Javelin", 1, 6, 2, "Thrusts or throw a spear at a chosen target"));
		}
	}
}
