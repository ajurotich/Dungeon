using Xunit;
using DungeonLibrary;

namespace DungeonTests;

public class DungeonTests {

	[Fact]
	public void TestPlayerDamage() {
		Player testPlayer = new Player();
		testPlayer.Damage(20);
		Assert.Equal(40, testPlayer.Health);
	}
	
	[Fact]
	public void TestPlayerKill() {
		Player testPlayer = new Player();
		testPlayer.Damage(60);
		Assert.False(testPlayer.IsAlive);
	}
	
	[Fact]
	public void TestPlayerHeal() {
		Player testPlayer = new Player();
		testPlayer.Heal(20);
		Assert.Equal(60, testPlayer.Health);
	}

	[Fact]
	public void TestPlayerChangeArmour() {
		Player testPlayer = new Player();
		Armour testArmour = new Armour(ArmourType.Light);
		testPlayer.ChangeArmour(testArmour);
		Assert.Equal(testArmour, testPlayer.Armour);
	}

	[Fact]
	public void TestPlayerChangeWeapon() {
		Player testPlayer = new Player();
		Weapon testWeapon = new Weapon(WeaponType.Dagger);
		testPlayer.ChangeWeapon(testWeapon);
		Assert.Equal(testWeapon, testPlayer.Weapon);
	}
	
	[Fact]
	public void TestPlayerIncrementKillCount() {
		Player testPlayer = new Player();
		testPlayer.IncrementKillCount();
		Assert.Equal(1, testPlayer.KillCount);
	}

	[Fact]
	public void TestEnemyDamage() {
		Entity testEnemy = new Entity("Enemy", new Race(RaceType.Orc), new Armour(ArmourType.Heavy), new Weapon(WeaponType.Axe));
		testEnemy.Damage(20);
		Assert.Equal(60, testEnemy.Health);
	}

	[Fact]
	public void TestEnemyKill() {
		Entity testEnemy = new Entity("Enemy", new Race(RaceType.Orc), new Armour(ArmourType.Heavy), new Weapon(WeaponType.Axe));
		testEnemy.Damage(80);
		Assert.False(testEnemy.IsAlive);
	}

	[Fact]
	public void TestEnemyHeal() {
		Entity testEnemy = new Entity("Enemy", new Race(RaceType.Orc), new Armour(ArmourType.Heavy), new Weapon(WeaponType.Axe));
		testEnemy.Heal(20);
		Assert.Equal(80, testEnemy.Health);
	}

	[Fact]
	public void TestWorldSearch() {
		World testWorld = new World(false);
		testWorld.IncrementSearch();
		Assert.True(testWorld.IsSearched);
	}
	
	[Fact]
	public void TestWorldNotSearched() {
		World testWorld = new World("World", "Desctription");
		testWorld.IncrementSearch();
		Assert.False(testWorld.IsSearched);
	}

}
