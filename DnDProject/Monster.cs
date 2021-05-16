using System;
using System.Collections.Generic;

namespace DnDProject
{
	/// <summary>
	/// Class used to host the monster/enemy functionality
	/// </summary>
	public class Monster: Entity
	{
		#region Variable declarations
			#region Social stats
				public string Type { get; }
				public int HitDie { get; }
				public int DiceRolled { get; }
				public int MaxHP { get; }
				public int CurrentHP { get; set; }
				public int PageNumber { get; }
			#endregion
			#region Core Stats 
				public int Strength { get; set; }
				public int Dexterity { get; set; }
				public int Intelligence { get; set; }
				public int Constitution { get; set; }
				public int Wisdom { get; set; }
				public int Charisma { get; set; }
			#endregion
			#region Core Modifiers
				public int StrMod { get; set; }
				public int DexMod { get; set; }
				public int IntMod { get; set; }
				public int ConMod { get; set; }
				public int WisMod { get; set; }
				public int ChrMod { get; set; }
			#endregion
			public List<Skill> SkillList { get; }
		#endregion
		/// <summary>
		/// Constructor for a monster
		/// </summary>
		/// <param name="type"></param>
		/// <param name="diceRolled"></param>
		/// <param name="hitDie"></param>
		/// <param name="strength"></param>
		/// <param name="dexterity"></param>
		/// <param name="constitution"></param>
		/// <param name="intelligence"></param>
		/// <param name="wisdom"></param>
		/// <param name="charisma"></param>
		/// <param name="pageNumber"></param>
		/// <param name="skillString"></param>
		Monster(
			string type,
			int diceRolled,
			int hitDie,

			int strength,
			int dexterity,
			int constitution,
			int intelligence,
			int wisdom,
			int charisma,

			int pageNumber,
			string skillString)
		{
			#region Social Stats
				Type = type;
				DiceRolled = diceRolled;
				HitDie = hitDie;
			#endregion

			#region Core Stats
				Strength = strength;
				Dexterity = dexterity;
				Constitution = constitution;
				Intelligence = intelligence;
				Wisdom = wisdom;
				Charisma = charisma;
			#endregion

			#region CoreModifiers
				StrMod = (int)Math.Floor((strength - 10) / 2.0);
				DexMod = (int)Math.Floor((dexterity - 10) / 2.0);
				IntMod = (int)Math.Floor((intelligence - 10) / 2.0);
				ChrMod = (int)Math.Floor((constitution - 10) / 2.0);
				WisMod = (int)Math.Floor((wisdom - 10) / 2.0);
				ChrMod = (int)Math.Floor((charisma - 10) / 2.0);
				PageNumber = pageNumber;
			#endregion

			MaxHP = (int)(diceRolled * (((hitDie + 1) / 2.0) + ConMod));
			CurrentHP = MaxHP;

			SkillList = new List<Skill>();
			//Skill list assigning
			if (!string.IsNullOrEmpty(skillString))
			{
				foreach (string str in skillString.Split("-"))
				{
					SkillList.Add(Skill.SkillList[str]);
				}
			}
		}
		public void TakeDamage(int damage) { CurrentHP -= damage; }
		public void Heal(int life) { CurrentHP = Math.Min(MaxHP, CurrentHP + life); }

		public List<Skill> GetSkillList() { return SkillList; }

		/*Multiple overloads will be required, will ask for a MonsterList of a specific area and will take that as an input
			Monster createMonster(int id, String areaMonsterPoolFile)
			Edit, that's bs. We can simply define the monster pool somewhere else and have the id part of a dictionary or something of the sort*/
		/// <summary>
		/// Performs the logic to generate a monster object
		/// Note: Multiple overloads will be required, will ask for a MonsterList of a specific area and will take that as an input
		///				Monster createMonster(int id, String areaMonsterPoolFile)
		///	Might not be necessary as the Monsterpool could contain the monster ids natively thus saving the need for that
		/// </summary>
		/// <param name="id"></param>
		/// <returns>The monster</returns>
		public static Monster CreateMonster(int id)
		{
			try 
			{
				string monsterData = Resources.MonsterList.Replace("\r","").Split("\n")[id];
				string[] data = monsterData.Split(",");
				int i = 0;
				string type = data[i++];
				int diceRolled = int.Parse(data[i++]);
				int hitDie = int.Parse(data[i++]);

				int strength = int.Parse(data[i++]);
				int dexterity = int.Parse(data[i++]);
				int intelligence = int.Parse(data[i++]);
				int constitution = int.Parse(data[i++]);
				int wisdom = int.Parse(data[i++]);
				int charisma = int.Parse(data[i++]);

				int pageNumber = int.Parse(data[i++]);
				string skillString = data[i++];
				Monster monster = new Monster(type, diceRolled, hitDie,
					strength, dexterity, constitution, intelligence, wisdom, charisma, pageNumber,skillString);
				return monster;
			}catch (Exception e) { Console.WriteLine(e.Message); }
			return null;//If ya down here ya fucked up somewhere
		}
		/// <summary>
		/// Simple check to view if a player has died
		/// </summary>
		/// <returns></returns>
		public bool IsAlive() { return CurrentHP > 0; }

	}
}
