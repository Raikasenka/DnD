using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DnDProject
{
	/// <summary>
	/// Class used to host player functionalities
	/// </summary>
	public class Player: Entity
	{

		#region Variable declarations
			#region Social stats
				public string Name { get; set; }
				public string Race { get; set; }
				public Profession Profession { get; set; }
				public int Age { get; set; }
				public int Level { get; set; }
				public int MaxHP { get; set; }
				public int CurrentHP { get; set; }
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
			public Weapon EquippedWeapon { get; set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Regular constructor for Players
		/// </summary>
		/// <param name="name"></param>
		/// <param name="age"></param>
		/// <param name="race"></param>
		/// <param name="level"></param>
		/// <param name="profession"></param>
		/// <param name="strength"></param>
		/// <param name="dexterity"></param>
		/// <param name="constitution"></param>
		/// <param name="intelligence"></param>
		/// <param name="wisdom"></param>
		/// <param name="charisma"></param>
		/// <param name="skillString"></param>
		public Player(
			string name,
			int age,
			string race,
			int level,
			Profession profession,

			int strength,
			int dexterity,
			int constitution,
			int intelligence,
			int wisdom,
			int charisma,
			string skillString)
		{
			#region Social Stats
				Name = name;
				Age = age;
				Race = race;
				Level = level;
				Profession = profession;
			#endregion

			#region Core Stats
				Strength = strength;
				Dexterity = dexterity;
				Intelligence = intelligence;
				Constitution = constitution;
				Wisdom = wisdom;
				Charisma = charisma;
			#endregion

			#region CoreModifiers
			StrMod = (int)Math.Floor((strength - 10) / 2.0);
			DexMod = (int)Math.Floor((dexterity - 10) / 2.0);
			IntMod = (int)Math.Floor((intelligence - 10) / 2.0);
			ConMod = (int)Math.Floor((constitution - 10) / 2.0);
			WisMod = (int)Math.Floor((wisdom - 10) / 2.0);
			ChrMod = (int)Math.Floor((charisma - 10) / 2.0);
			#endregion
			MaxHP = profession.HitDie + ((level - 1) * (profession.HitDie / 2 + 1)) + (level * ConMod);
			CurrentHP = MaxHP;

			SkillList = new List<Skill>();
			if (!string.IsNullOrEmpty(skillString))
			{
				foreach (string str in skillString.Replace("\r\n","").Split("-"))
				{
					SkillList.Add(Skill.SkillList[str]);
				}
			}
		}
		#endregion

		#region Public methods
		public void TakeDamage(int damage) { CurrentHP -= damage; }
		public void Heal(int life) { CurrentHP = Math.Min(MaxHP, CurrentHP + life); }
		#region Stat Listing Methods
		/// <summary>
		/// Creates a formatted list of stats
		/// </summary>
		/// <returns>The list of stats</returns>
		public string ViewSimpleInfo()
		{
			string info = "Name: " + Name + "| Age: " + Age + "| Race: " + Race + "|\n";
			info += "Level: " + Level + "| Profession: " + Profession.Name + "|\n";
			return info;
		}

		/// <summary>
		/// Creates a more large formatted list of stats
		/// </summary>
		/// <returns>The list of stats</returns>
		public string ViewOfficialInfo()
		{
			string officialInfo = ViewSimpleInfo() + "\n";
			officialInfo += "STR: " + Strength + "\n";
			officialInfo += "DEX: " + Dexterity + "\n";
			officialInfo += "INT: " + Intelligence + "\n";
			officialInfo += "CON: " + Constitution + "\n";
			officialInfo += "WIS: " + Wisdom + "\n";
			officialInfo += "CHA: " + Charisma + "\n";
			return officialInfo;
		}

		/// <summary>
		/// Creates an in-depth formatted list of stats
		/// </summary>
		/// <returns>The list of stats</returns>
		public string ViewDetailedInfo()
		{
			string detailedInfo = ViewOfficialInfo() + "\n";
			detailedInfo += "STRMod: " + StrMod + "\n";
			detailedInfo += "DEXMod: " + DexMod + "\n";
			detailedInfo += "INTMod: " + IntMod + "\n";
			detailedInfo += "CONMod: " + ConMod + "\n";
			detailedInfo += "WISMod: " + WisMod + "\n";
			detailedInfo += "CHRMod: " + ChrMod + "\n\n";

			detailedInfo += "Max Health: " + MaxHP;
			detailedInfo += "| Health/level: " + ((Profession.HitDie / 2) + 1 + ConMod) + "\n";
			return detailedInfo;
		}

		/// <summary>
		/// Generates a graphical chart displaying stats.
		/// </summary>
		/// <returns>The list of stats</returns>
		public string StatChart()
		{
			string chart = "";
			chart += "\t" + Name + "'s stat chart\n";
			chart += "STR|" + StarString(Strength) + "\n";
			chart += "DEX|" + StarString(Dexterity) + "\n";
			chart += "CON|" + StarString(Constitution) + "\n";
			chart += "INT|" + StarString(Intelligence) + "\n";
			chart += "WIS|" + StarString(Wisdom) + "\n";
			chart += "CHR|" + StarString(Charisma) + "\n";
			chart += "   0----5----10---15---20---25---30\n";
			return chart;
		}
		/// <summary>
		/// Generates a string of * for stat displaying purposes
		/// </summary>
		/// <param name="value"></param>
		/// <returns>The string</returns>
		private static string StarString(int value)
		{
			return new string('*',Math.Max(0, value));
		}
		#endregion
		/// <summary>
		/// Performs the logic to create a rudementary save for the game
		/// </summary>
		public void Save()
		{
			try
			{
				Console.WriteLine("Which file do you wish to save in?(1-10)>");
				bool isValid = int.TryParse(Console.ReadLine(), out int saveFile);
				if (!isValid)
				{
					Console.WriteLine("Please input only numbers (1-10).\nTerminating save process...");
					return;
				}
				if (saveFile < 0 || saveFile > 10)
				{
					Console.WriteLine("Illegal file selection.\nTerminating save process...");
					return;
				}
				string location = new Regex("^.*DnDProject").Match(AppDomain.CurrentDomain.BaseDirectory).Value;
				if (!Directory.Exists(location + "\\UserSaves"))
				{
					Directory.CreateDirectory(location + "\\UserSaves");
				}
				using StreamWriter file = new StreamWriter(location + "\\UserSaves\\player" + saveFile + ".dat");
					file.WriteLine(PlayerToString());
			}
			catch (Exception e) 
			{ 
				Console.WriteLine(e.Message);
			}
		}
		/// <summary>
		/// Performs the logic to do a rudementary load for the game
		/// </summary>
		public static Player Load()
		{
			try
			{
				//Bad way to return to the top of the question?
				Start:
				Console.WriteLine("Which file do you wish to load?(1-10)>");
				bool isValid = int.TryParse(Console.ReadLine(), out int loadFile);
				if (!isValid)
				{
					Console.WriteLine("Please input only numbers (1-10).");
					goto Start;
				}
				if (loadFile < 0 || loadFile > 10)
				{
					Console.WriteLine("Illegal file selection.");
					goto Start;
				}
				string location = new Regex("^.*DnDProject").Match(AppDomain.CurrentDomain.BaseDirectory).Value;
				string[] data = Array.Empty<string>();
				using (StreamReader file = new StreamReader(location + "\\UserSaves\\player" + loadFile + ".dat"))
				{ 
					data = file.ReadToEnd().Split(",");
				}
					
				int i = 0;
				string name = data[i++];
				int age = int.Parse(data[i++]);
				string race = data[i++];
				int level = int.Parse(data[i++]);
				string prof = data[i++];
				Profession profession = null;
				switch (prof)
				{
					case "Barbarian":
						profession = Profession.BARBARIAN; break;
					case "Bard":
						profession = Profession.BARD; break;
					case "Cleric":
						profession = Profession.CLERIC; break;
					case "Druid":
						profession = Profession.DRUID; break;
					case "Fighter":
						profession = Profession.FIGHTER; break;
					case "Monk":
						profession = Profession.MONK; break;
					case "Paladin":
						profession = Profession.PALADIN; break;
					case "Ranger":
						profession = Profession.RANGER; break;
					case "Sorcerer":
						profession = Profession.SORCERER; break;
					case "Warlock":
						profession = Profession.WARLOCK; break;
					case "Wizard":
						profession = Profession.WIZARD; break;
					default:
						Console.WriteLine("There was an error in file formatting to acquire the Profession\nTerminating...");
						return null;
				}

				int strength = int.Parse(data[i++]);
				int dexterity = int.Parse(data[i++]);
				int intelligence = int.Parse(data[i++]);
				int constitution = int.Parse(data[i++]);
				int wisdom = int.Parse(data[i++]);
				int charisma = int.Parse(data[i++]);
				string skillString = data[i++];
				Player entity = new Player(name, age, race, level, profession,
					strength, dexterity, constitution, intelligence, wisdom, charisma, skillString);
				return entity;
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
			//If you somehow reach here, it means you've fucked with the file creation.
			//The files should save and load with the only user input being the file#
			return null;
		}
		/// <summary>
		/// Generates a comma delimited string holding 'all' relevant details for a player.
		/// </summary>
		/// <returns>The save string</returns>
		private string PlayerToString()
		{
			StringBuilder data = new StringBuilder();
			data.Append(Name + ",");
			data.Append(Age + ",");
			data.Append(Race + ",");
			data.Append(Level + ",");
			data.Append(Profession.Name + ",");
			data.Append(Strength + ",");
			data.Append(Dexterity + ",");
			data.Append(Constitution + ",");
			data.Append(Intelligence + ",");
			data.Append(Wisdom + ",");
			data.Append(Charisma + ",");
			if (SkillList.Count>0)
			{
				foreach (Skill skill in SkillList)
				{
					data.Append(skill.Name+"-");
				}
			}
			else
			{
				data.Append(',');
			}
			return data.ToString().Substring(0, data.Length - 1);
		}
		/// <summary>
		/// Performs an attack with the equipped weapon.
		/// </summary>
		/// <returns>The damage</returns>
		public int Attack() { return EquippedWeapon.Attack(); }
		/// <summary>
		/// Simple check to view if a player has died
		/// </summary>
		/// <returns></returns>
		public bool IsAlive() { return CurrentHP > 0; }
		#endregion
	}
}
