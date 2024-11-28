namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WAZNE\nWAZNE\nZamiast emoji koloru moze wystapic znak zapytania. Nie wiem jak to jest w VSC bo projekt robie w riderze\nWAZNE\nWAZNE\n");
            // Stworzenie talii kart
            List<(Figure figure, Color color)> deck = CreateDeck();

            // Tasowanie talii
            deck = deck.OrderBy(_ => Guid.NewGuid()).ToList();

            // Przygotowanie graczy
            List<(Figure figure, Color color)> playersHand = new List<(Figure, Color)>();
            List<(Figure figure, Color color)> dealersHand = new List<(Figure, Color)>();

            // Rozdanie początkowych kart
            playersHand.Add(deck.Pop());
            playersHand.Add(deck.Pop());
            dealersHand.Add(deck.Pop());
            dealersHand.Add(deck.Pop());

            // Rozgrywka gracza
            Console.WriteLine("Twoje karty:");
            DisplayHand(playersHand);
            while (CalculateHandsValue(playersHand) < 21)
            {
                Console.WriteLine("Czy chcesz dobrać kartę? (t/n)");
                string decision = Console.ReadLine();
                if (decision.ToLower() == "t")
                {
                    playersHand.Add(deck.Pop());
                    Console.WriteLine("Twoje karty:");
                    DisplayHand(playersHand);
                }
                else
                {
                    break;
                }
            }

            // Sprawdzenie wyniku gracza
            if (CalculateHandsValue(playersHand) > 21)
            {
                Console.WriteLine("Przegrałeś! Masz powyżej 21.");
                return;
            }

            // Rozgrywka krupiera
            Console.WriteLine("Karty krupiera:");
            DisplayHand(dealersHand);
            while (CalculateHandsValue(dealersHand) < 17)
            {
                dealersHand.Add(deck.Pop());
                Console.WriteLine("Karty krupiera:");
                DisplayHand(dealersHand);
            }

            // Sprawdzenie wyniku krupiera
            int dealersValue = CalculateHandsValue(dealersHand);
            int playersValue = CalculateHandsValue(playersHand);

            if (dealersValue > 21 || playersValue > dealersValue)
            {
                Console.WriteLine("Wygrałeś!");
            }
            else if (playersValue == dealersValue)
            {
                Console.WriteLine("Remis!");
            }
            else
            {
                Console.WriteLine("Przegrałeś!");
            }
        }

        static List<(Figure figure, Color color)> CreateDeck()
        {
            var deck = new List<(Figure figure, Color color)>();
            foreach (Figure figure in Enum.GetValues(typeof(Figure)))
            {
                foreach (Color color in Enum.GetValues(typeof(Color)))
                {
                    deck.Add((figure, color));
                }
            }
            return deck;
        }

        static void DisplayHand(List<(Figure figure, Color color)> hand)
        {
            
            var kolorEmoji = new Dictionary<Color, string>
            {
                { Color.Hearts, "❤️" },
                { Color.Spades, "♠️" },
                { Color.Diamonds, "♦️" },
                { Color.Clubs, "♣️" }
            };
            
            foreach (var card in hand)
            {
                Console.WriteLine($"{card.figure} {kolorEmoji[card.color]}");
            }
            Console.WriteLine($"Wartość ręki: {CalculateHandsValue(hand)}");
        }

        static int CalculateHandsValue(List<(Figure figure, Color color)> hand)
        {
            int sum = hand.Sum(card => (int)card.figure);
            int aceCount = hand.Count(card => card.figure == Figure.Ace);

            // tutaj jest to czy as ma byc 1 czy 11
            while (sum > 21 && aceCount > 0)
            {
                sum -= 10;
                aceCount--;
            }

            return sum;
        }
    }
}