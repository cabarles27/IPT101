using System;
class Program
{
	static void Main(string[] args)
	{
		string[] names = { "kaye", "jane", "Kai" };
		int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

		foreach (string name in names)
		{
			Console.Write(name + " ");
		}
		Console.WriteLine();
		foreach (int number in num)
		{
			Console.Write(number + " ");
		}
	}
}