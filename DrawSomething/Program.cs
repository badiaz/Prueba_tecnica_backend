var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/generate-Words", async context =>
{
	string letters = context.Request.Query["letters"];
	int desiredLength = int.Parse(context.Request.Query["length"]);

	List<string> validWords = new List<string>
	{
		"ganster", "gradius", "negrita", "tijeras", "trident", "turista"
	};

	List<string> generatedWords = checkWords(letters.ToLower(), desiredLength, validWords);

	await context.Response.WriteAsJsonAsync(generatedWords);
});

app.Run();

List<string> checkWords(string letters, int desiredLength, List<string> validWords)
{
	HashSet<string> words = new HashSet<string>();

	foreach (string word in validWords)
	{
		if (canFormWord(word, letters))
		{
			words.Add(word);
		}
	}
	return words.ToList();
}

bool canFormWord(string word, string letters)
{
	Dictionary<char, int> letterFrequency = new Dictionary<char, int>();

	foreach (char letter in letters)
	{
		if (!letterFrequency.ContainsKey(letter))
		{
			letterFrequency[letter] = 0;
		}
		letterFrequency[letter]++;
	}

	foreach (char letter in word)
	{
		if (!letterFrequency.ContainsKey(letter) || letterFrequency[letter] == 0)
		{
			return false;
		}
		letterFrequency[letter]--;
	}

	return true;
}
