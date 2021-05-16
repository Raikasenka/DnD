using System;
using System.Collections.Generic;
namespace DnDProject
{
	public class FightManager
	{
		/// <summary>
		/// Fight comprising a single player and an enemy.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="enemy"></param>
		public static void Fight(Player player, Monster enemy)
		{
			Console.WriteLine("\n" + player.Name + " has encountered: " + enemy.Type + "\n");
			int roundCount = 1;
			while (player.IsAlive() && enemy.IsAlive())
			{
				PrintRound(roundCount++);
				PlayerTurn(player, enemy);
				if (!enemy.IsAlive())
				{
					break;
				}
				EnemyAttack(player, enemy);
				Console.WriteLine();
			}
			if (player.IsAlive())
			{
				VictoryMessage(player, enemy);
			}
			else
			{
				DefeatMessage(player, enemy);
			}
		}
		public static void Fight(params Entity[] entities)
		{
			//Need a setup to find who is on what team. Player/Allies vs Enemies
			//Need to also discover the initiative order
			foreach(Entity thing in entities)
			{
				string entityType = thing.GetType().Name;
				switch (entityType)
				{
					case "Player":
						break;
					case "Monster":
						break;
				}
			}
		}
		private static void PrintRound(int round)
		{
			Console.WriteLine("Round " + round);
		}
		private static void DisplayHP(Player player, Monster enemy)
		{
			Console.WriteLine(player.Name + " has " + player.CurrentHP + "HP\t" + enemy.Type + " has " + enemy.CurrentHP + "HP");
		}

		private static void PlayerAttackMessage(Player player, int damage)
		{
			Console.WriteLine(player.Name + "'s attack has dealt " + damage + " damage!");
		}
		private static void EnemyAttackMessage(Monster enemy, int damage)
		{
			Console.WriteLine(enemy.Type + "'s attack has dealt " + damage + " damage!");
		}

		private static void VictoryMessage(Player player, Monster enemy)
		{
			Console.WriteLine(player.Name + " has defeated the " + enemy.Type);
		}
		private static void DefeatMessage(Player player, Monster enemy)
		{
			Console.WriteLine(player.Name + " has been defeated by the " + enemy.Type);
		}

		private static void PrintOptionScreen()
		{
			Console.Write(Constants.OPTIONQUERY
				+ "\n1. " + Constants.ATTACKPROMPT
				+ "\n2. " + Constants.SKILLPROMPT
				+ "\n3. " + Constants.ITEMPROMPT
				+ "\n4. " + Constants.FLEEPROMPT
				+ "\n" + Constants.SELECTIONPROMPT);
		}
		private static void NotAvailableMessage() { Console.WriteLine(Constants.NOTAVAILABLE); }

		private static int OptionChoice()
		{
			while (true)
			{
				try
				{
					int x = int.Parse(Console.ReadLine());
					/* FIXME: 2020-06-06 Options only include attacking for the moment thus x should be allowed to be any number in the list later on
							The resulting if will be of the format: if(x<1||x>n) where n is the size of the list. Throw exception*/
					if (!(x == 1 || x == 2))
					{
						throw new IndexOutOfRangeException();
					}
					return x;
				}
				catch (FormatException)
				{
					Console.Write(Constants.NOTANUMBERMSG);
				}
				catch (IndexOutOfRangeException)
				{
					Console.WriteLine("Please input a valid number (1-4)");
				}
			}
		}
		//These should be morphed into generalized Player->Entity and Entity->Entity where the result become entity.TakeDamage
		//This also allows the precedent of aoe attacks to go through in a foreach relative 'enemies' to roll the attack or morph one to return an int.
		//This format should also include a format of Entity->Target
		private static void PlayerAttack(Player player, Monster enemy)
		{
			int damage = player.Attack();
			PlayerAttackMessage(player, damage);
			enemy.TakeDamage(damage);
		}
		private static void EnemyAttack(Player player, Monster enemy)
		{
			Skill enemyAttack = enemy.SkillList[Static.RollAny(enemy.SkillList.Count) - 1];
			int damage = enemyAttack.Attack();
			EnemyAttackMessage(enemy, damage);
			player.TakeDamage(damage);
		}

		private static void PlayerSkill(Player player, Monster enemy)
		{
			List<Skill> skills=player.SkillList;
			Console.WriteLine(Constants.LEAVESKILLPROMPT);
			Console.WriteLine("0. Leave");
			for(int i=0;i<skills.Count;i++){
				Console.WriteLine(i+1+". "+skills[i].Name);
			}
			Console.Write(Constants.SELECTIONPROMPT);
			int choice;
			while(true){
				try {
					string str = Console.ReadLine();
					if (str.Trim().Equals("0")){
						Console.WriteLine();
						throw new Exception();
					}
					int x = int.Parse(str);
					if (x < 1 || x > skills.Count)
					{
						throw new IndexOutOfRangeException();
					}
					choice = --x; break;
				}
				catch (FormatException)
				{
					Console.Write(Constants.NOTANUMBERMSG);
				}
				catch (IndexOutOfRangeException)
				{
					Console.Write("Please input a valid number (1-" + skills.Count + ")>");
				}
			}
			int damage = skills[choice].Attack();
			PlayerAttackMessage(player, damage);
			enemy.TakeDamage(damage);
		}
		private static void PlayerTurn(Player player, Monster enemy)
		{
			while (true)
			{
				DisplayHP(player, enemy);
				PrintOptionScreen();
				try
				{
					switch (OptionChoice())
					{
						case 1: PlayerAttack(player, enemy); return;
						case 2: PlayerSkill(player, enemy); return;
						case 3:
						case 4:
							NotAvailableMessage(); return;
					}
				}
				catch (Exception){}
			}
		}
	}
}
