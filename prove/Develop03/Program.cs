class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new("2 Samuel", 6, 6, 7, ["And when they came to Nachon's threshingfloor, Uzzah put forth his hand to the ark of God, and toook hold of it; for the oxen shook it.", "And the anger of the Lord was kindled against Uzzah; and God smote him there for his error; and there he died by the ark of God."]);

        scripture.Memorize();
    }
}