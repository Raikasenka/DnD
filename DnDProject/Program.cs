using System.IO;

namespace DnDProject
{
	public class Program
	{
		public static readonly string workingFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
		public static void Main(string[] args)
		{
			Setup();
			Player user = new Player("Luke", 19, "Human", 5, Profession.BARBARIAN, 7, 8, 12, 11, 10, 5, "Bite-Javelin")
			{
				EquippedWeapon = new Weapon("Falchion", 3, 4, 0, "Blunt")
			};
			FightManager.Fight(user, Monster.CreateMonster(1));
			//FightManager.Fight(new Entity[] { user, Monster.CreateMonster(1), Monster.CreateMonster(1) });
		}
		static void Setup()
		{
			Skill.LoadAll();
		}
	}
}
